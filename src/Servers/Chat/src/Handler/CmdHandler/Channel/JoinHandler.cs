﻿using System.Linq;
using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using UniSpyServer.Servers.Chat.Entity.Contract;
using UniSpyServer.Servers.Chat.Entity.Exception;
using UniSpyServer.Servers.Chat.Entity.Exception.IRC.Channel;
using UniSpyServer.Servers.Chat.Entity.Exception.IRC.General;
using UniSpyServer.Servers.Chat.Entity.Structure.Misc;
using UniSpyServer.Servers.Chat.Entity.Structure.Misc.ChannelInfo;
using UniSpyServer.Servers.Chat.Entity.Structure.Request.Channel;
using UniSpyServer.Servers.Chat.Entity.Structure.Request.General;
using UniSpyServer.Servers.Chat.Entity.Structure.Response.Channel;
using UniSpyServer.Servers.Chat.Entity.Structure.Result.Channel;
using UniSpyServer.Servers.Chat.Handler.SystemHandler.ChannelManage;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Database.DatabaseModel;

namespace UniSpyServer.Servers.Chat.Handler.CmdHandler.Channel
{
    /// <summary>
    /// Game will only join one channel at one time
    /// </summary>
    [HandlerContract("JOIN")]
    public sealed class JoinHandler : LogedInHandlerBase
    {
        private new JoinRequest _request => (JoinRequest)base._request;
        private new JoinResult _result { get => (JoinResult)base._result; set => base._result = value; }
        private new JoinResponse _response { get => (JoinResponse)base._response; set => base._response = value; }
        Entity.Structure.Misc.ChannelInfo.Channel _channel;
        ChannelUser _user;
        public JoinHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _result = new JoinResult();
        }

        //1.筛选出所要加入的频道，如果不存在则创建(select the channel that user want to join, if channel does not exist creating it)
        //2.检查用户名nickname是否已经在频道中存在(check if user's nickname existed in channel)
        //若存在则提醒用户名字冲突
        //不存在则加入频道
        //广播加入信息
        //发送频道模式给此用户
        //发送频道用户列表给此用户

        protected override void RequestCheck()
        {
            base.RequestCheck();
            //some GameSpy game only allow one player join one chat room
            //but GameSpy Arcade can join more than one channel
            if (_session.UserInfo.JoinedChannels.Count > 3)
            {
                throw new ChatIRCTooManyChannels($"{_session.UserInfo.NickName} is join too many channels");
            }
        }

        protected override void DataOperation()
        {
            _user = new ChannelUser(_session.UserInfo);
            // if (_session.UserInfo.IsJoinedChannel(_request.ChannelName))
            // {
            //     // this is for not making game crash
            //     // if user is in this channel and we send the channel info back
            // }
            if (ChannelManager.GetChannel(_request.ChannelName, out _channel))
            {
                //join
                if (_session.UserInfo.IsJoinedChannel(_request.ChannelName))
                {
                    // we do not send anything to this user and users in this channel
                    throw new Exception($"User: {_user.UserInfo.NickName} is already joined the channel: {_request.ChannelName}");
                }
                else
                {
                    if (_channel.Mode.IsInviteOnly)
                    {
                        //invited only
                        throw new IRCChannelException("This is an invited only channel.", IRCErrorCode.InviteOnlyChan, _request.ChannelName);
                    }
                    if (_channel.IsUserBanned(_user))
                    {
                        throw new IRCBannedFromChanException($"You are banned from this channel:{_request.ChannelName}.", _request.ChannelName);
                    }
                    if (_channel.Users.Count >= _channel.MaxNumberUser)
                    {
                        throw new IRCChannelIsFullException($"The channel:{_request.ChannelName} you are join is full.", _request.ChannelName);
                    }
                    //if all pass, it mean  we excute join channel
                    _user.SetDefaultProperties(false);
                    //simple check for avoiding program crash
                    if (_channel.IsUserExisted(_user))
                    {
                        throw new Exception($"{_session.UserInfo.NickName} is already in channel {_request.ChannelName}");
                    }
                    _channel.AddBindOnUserAndChannel(_user);

                }
            }
            else
            {
                //create
                if (IsPeerLobby(_request.ChannelName))
                {
                    _channel.IsPeerServer = true;
                    _channel = new Entity.Structure.Misc.ChannelInfo.Channel(_request.ChannelName, _user);
                    _user.SetDefaultProperties(false, false);
                }
                else
                {
                    _channel = new Entity.Structure.Misc.ChannelInfo.Channel(_request.ChannelName);
                    _user.SetDefaultProperties(true, true);
                }
                _channel.AddBindOnUserAndChannel(_user);
                ChannelManager.AddChannel(_request.ChannelName, _channel);
            }

            _result.AllChannelUserNicks = _channel.GetAllUsersNickString();
            _result.JoinerNickName = _session.UserInfo.NickName;
            _result.ChannelModes = _channel.Mode.ToString();
            _result.JoinerPrefix = _session.UserInfo.IRCPrefix;
        }

        private bool IsPeerLobby(string name)
        {
            // TODO! check the room name by search the name on the official room name in database
            string[] buffer = name.Split('!', System.StringSplitOptions.RemoveEmptyEntries);
            if (buffer.Length != 3)
            {
                return false;
            }
            using (var db = new UniSpyContext())
            {
                var officialRoom = db.Games.Join(db.Grouplists, g => g.Gameid, gl => gl.Gameid, (g, gl) => new { g, gl }).Where(x => x.gl.Roomname == buffer[1]).FirstOrDefault();
                if (officialRoom == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            // using (var client = new RedisClient())
            // {

            //     var peerRoom = client.Values.Where(x => x.GameName == buffer[1]).ToList();
            //     if (buffer[2].Length > 2 && peerRoom.Count > 0)
            //     {
            //         return true;
            //     }
            //     else
            //     {
            //         return false;
            //     }
            // }
        }

        protected override void ResponseConstruct()
        {
            _response = new JoinResponse(_request, _result);
        }

        protected override void Response()
        {
            // base.Response();
            if (_response == null)
            {
                return;
            }

            //first we send join information to all user in this channel
            _channel.MultiCast(_response);

            var namesRequest = new NamesRequest
            {
                ChannelName = _request.ChannelName
            };
            new NamesHandler(_session, namesRequest).Handle();

            var userModeRequest = new ModeRequest
            {
                RequestType = ModeRequestType.GetChannelUserModes,
                ChannelName = _request.ChannelName,
                NickName = _user.UserInfo.NickName,
                UserName = _user.UserInfo.UserName,
                Password = _request.Password
            };
            new ModeHandler(_session, userModeRequest).Handle();
        }
    }
}
