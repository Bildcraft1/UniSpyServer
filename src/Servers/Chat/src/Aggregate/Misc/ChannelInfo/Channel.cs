using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UniSpy.Server.Chat.Abstraction.Interface;
using UniSpy.Server.Chat.Aggregate.Redis.Contract;
using UniSpy.Server.Chat.Application;
using UniSpy.Server.Chat.Contract.Request.Channel;
using UniSpy.Server.Chat.Exception;
using UniSpy.Server.Chat.Exception.IRC.Channel;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Core.Logging;

namespace UniSpy.Server.Chat.Aggregate.Misc.ChannelInfo
{
    public enum PeerRoomType
    {
        /// <summary>
        /// the first channel that a connected user joined at first time
        /// </summary>
        Title,
        /// <summary>
        /// User created room for gaming
        /// </summary>
        Staging,
        /// <summary>
        /// User created room which can be seperated by categories
        /// </summary>
        Group,
        /// <summary>
        /// Testing room
        /// </summary>
        Normal
    }
    public sealed class Channel
    {
        /// <summary>
        /// When game connects to the server, the player will enter the default channel for communicating with other players.
        /// </summary>
        public const string TitleRoomPrefix = "#GSP";
        /// <summary>
        /// When a player creates their own game and is waiting for others to join they are placed in a separate chat room called the "staging room"
        /// Staging rooms have two title seperator like #GSP!xxxx!xxxx
        /// </summary>
        public const string StagingRoomPrefix = "#GSP";
        /// <summary>
        /// group rooms is used split the list of games into categories (by gametype, skill, region, etc.). In this case, when entering the title room, the user would get a list of group rooms instead of a list of games
        /// Group room have one title seperator like #GPG!xxxxxx
        /// </summary>
        public const string GroupRoomPrefix = "#GPG";
        public const char TitleSeperator = '!';

        public static Func<string, PeerRoomType> GetRoomType = (channelName) =>
        {
            if (IsStagingRoom(channelName))
            {
                return PeerRoomType.Staging;
            }
            else if (IsTitleRoom(channelName))
            {
                return PeerRoomType.Title;
            }
            else if (IsGroupRoom(channelName))
            {
                return PeerRoomType.Group;
            }
            else
            {
                return PeerRoomType.Normal;
            }
        };
        private static Func<string, bool> IsStagingRoom = (channelName) =>
        {
            var a = channelName.Count(c => c == TitleSeperator) == 2 ? true : false;
            var b = channelName.StartsWith(StagingRoomPrefix) ? true : false;
            return a && b;
        };
        private static Func<string, bool> IsTitleRoom = (channelName) =>
        {
            var a = channelName.Count(c => c == TitleSeperator) == 1 ? true : false;
            var b = channelName.StartsWith(TitleRoomPrefix) ? true : false;
            return a && b;
        };
        private static Func<string, bool> IsGroupRoom = (channelName) =>
        {
            var a = channelName.Count(c => c == TitleSeperator) == 1 ? true : false;
            var b = channelName.StartsWith(GroupRoomPrefix) ? true : false;
            return a && b;
        };
        /// <summary>
        /// Channel name
        /// </summary>
        /// <value></value>
        public string Name { get; private set; }
        /// <summary>
        /// The maximum number of users that can be in the channel
        /// </summary>
        /// <value></value>
        public int MaxNumberUser { get; private set; }
        public ChannelMode Mode { get; private set; }
        public DateTime CreateTime { get; private set; }
        /// <summary>
        /// | key -> Nickname | value -> ChannelUser|
        /// </summary>
        /// <value></value>
        public IDictionary<string, ChannelUser> BanList { get; private set; }
        /// <summary>
        /// | key -> Nickname | value -> ChannelUser|
        /// </summary>
        /// <value></value>
        public IDictionary<string, ChannelUser> Users { get; private set; }
        public IDictionary<string, string> ChannelKeyValue { get; private set; }
        public ChannelUser Creator { get; private set; }
        public bool IsPeerServer { get; private set; }
        public PeerRoomType RoomType => GetRoomType(Name);
        public string Password { get; private set; }
        public string Topic { get; set; }
        public Redis.ChatMessageChannel MessageBroker { get; private set; }
        public Channel(string name, IChatClient client, string password = null)
        {
            Name = name;
            CreateTime = DateTime.Now;
            Mode = new ChannelMode();
            BanList = new ConcurrentDictionary<string, ChannelUser>();
            Users = new ConcurrentDictionary<string, ChannelUser>();
            ChannelKeyValue = new ConcurrentDictionary<string, string>();
            MaxNumberUser = 200;
            Mode.SetDefaultModes();
            MessageBroker = new Redis.ChatMessageChannel(Name);
            IsPeerServer = StorageOperation.Persistance.IsPeerLobby(name);
            if (IsPeerServer)
            {
                Creator = AddUser(client, password);
            }
            else
            {
                Creator = AddUser(client, password, true, true);
            }

        }

        /// <summary>
        /// Send message to all users in this channel
        /// except the sender
        /// </summary>
        /// <returns></returns>
        public void MultiCast(IClient sender, IResponse message, bool isSkipSender = false)
        {
            foreach (var user in Users.Values)
            {
                if (user.IsRemoteUser)
                {
                    continue;
                }
                if (isSkipSender)
                {
                    if (user.RemoteIPEndPoint.Equals(sender.Connection.RemoteIPEndPoint))
                    {
                        continue;
                    }
                }
                user.ClientRef.Send(message);
                sender.LogNetworkMultiCast((string)message.SendingBuffer);
            }
        }
        public string GetAllUsersNickString()
        {
            string nicks = "";
            foreach (var user in Users.Values)
            {
                if (user.IsChannelCreator)
                {
                    nicks += "@" + user.ClientRef.Info.NickName + " ";
                }
                else
                {
                    nicks += user.ClientRef.Info.NickName + " ";
                }
            }
            //if user equals last user in channel we do not add space after it
            nicks = nicks.Substring(0, nicks.Length - 1);
            return nicks;
        }
        private void AddBindOnUserAndChannel(ChannelUser joiner)
        {
            // !! we can not directly use the Contains() method that ConcurrentDictionary or 
            // !! ConcurrentBag provide because it will not work properly.
            if (!Users.ContainsKey(joiner.Info.NickName))
            {
                Users.TryAdd(joiner.Info.NickName, joiner);
            }

            if (!joiner.Info.JoinedChannels.ContainsKey(this.Name))
            {
                joiner.Info.JoinedChannels.TryAdd(this.Name, this);
            }

        }
        private void RemoveBindOnUserAndChannel(ChannelUser leaver)
        {
            //!! we should use ConcurrentDictionary here
            //!! FIXME: when removing user from channel, 
            //!! we should do more checks on user not only just TryTake()
            if (Users.ContainsKey(leaver.Info.NickName))
            // !! we takeout wrong user from channel
            {
                var kv = new KeyValuePair<string, ChannelUser>(
                    leaver.Info.NickName,
                    Users[leaver.Info.NickName]);
                Users.Remove(kv);
            }

            if (leaver.Info.JoinedChannels.ContainsKey(this.Name))
            {
                var kv = new KeyValuePair<string, Channel>(this.Name, this);
                leaver.Info.JoinedChannels.Remove(kv);
            }

        }

        public ChannelUser GetChannelUser(IClient client)
        {
            return Users.Values.FirstOrDefault(u => u.Connection.RemoteIPEndPoint == client.Connection.RemoteIPEndPoint);
        }
        public bool IsUserBanned(ChannelUser user)
        {
            return IsUserBanned(user.ClientRef);
        }
        private bool IsUserBanned(IChatClient client)
        {
            if (!BanList.ContainsKey(client.Info.NickName))
            {
                return false;
            }
            if (BanList[client.Info.NickName].Connection.RemoteIPEndPoint != client.Connection.RemoteIPEndPoint)
            {
                return false;
            }
            return true;
        }
        public bool IsUserExisted(ChannelUser user) => IsUserExisted(user.ClientRef);
        public bool IsUserExisted(IChatClient client) => Users.ContainsKey(client.Info.NickName);
        private void Validation(IChatClient client, string password)
        {
            if (Mode.IsInviteOnly)
            {
                //invited only
                throw new IRCChannelException("This is an invited only channel.", IRCErrorCode.InviteOnlyChan, Name);
            }
            if (IsUserBanned(client))
            {
                throw new IRCBannedFromChanException($"You are banned from this channel:{Name}.", Name);
            }
            if (IsUserExisted(client))
            {
                throw new ChatException($"{client.Info.NickName} is already in channel {Name}");
            }
            if (client.Info.IsJoinedChannel(Name))
            {
                // we do not send anything to this user and users in this channel
                throw new ChatException($"User: {client.Info.NickName} is already joined the channel: {Name}");
            }
            if (Password is not null)
            {
                if (password is null)
                {
                    throw new ChatException("You must input password to join this channel.");
                }
                if (Password != password)
                {
                    throw new ChatException("Password is not correct");
                }
            }
        }
        public ChannelUser AddUser(IChatClient client, string password = null, bool IsChannelCreator = false, bool IsChannelOperator = false)
        {
            Validation(client, password);
            var user = new ChannelUser(client, this);
            user.SetDefaultProperties(IsChannelCreator, IsChannelOperator);
            AddBindOnUserAndChannel(user);
            return user;
        }
        public void VerifyPassword(string pass)
        {
            if (Password != pass)
            {
                throw new ChatException("Password is not correct");
            }
        }
        public void RemoveUser(IChatClient client)
        {
            var user = GetChannelUser(client);

            if (user is not null)
            {
                RemoveUser(user);
            }
        }
        public void RemoveUser(ChannelUser user)
        {
            RemoveBindOnUserAndChannel(user);
        }
        public ChannelUser GetChannelUser(string nickName) => Users.ContainsKey(nickName) == true ? Users[nickName] : null;

        /// <summary>
        /// We only care about how to set mode in this channel
        /// we do not need to care about if the user is legal
        /// because MODEHandler will check for us
        /// </summary>
        /// <param name="changer"></param>
        /// <param name="cmd"></param>
        public void SetProperties(ChannelUser changer, ModeRequest request)
        {
            // todo check permission of each operation
            foreach (var op in request.ModeOperations)
            {
                switch (op)
                {
                    case ModeOperationType.AddChannelUserLimits:
                        MaxNumberUser = request.LimitNumber;
                        break;
                    case ModeOperationType.RemoveChannelUserLimits:
                        MaxNumberUser = 200;
                        break;
                    case ModeOperationType.AddBanOnUser:
                        AddBanOnUser(request);
                        break;
                    case ModeOperationType.RemoveBanOnUser:
                        RemoveBanOnUser(request);
                        break;
                    case ModeOperationType.AddChannelPassword:
                        Password = request.Password;
                        break;
                    case ModeOperationType.RemoveChannelPassword:
                        Password = null;
                        break;
                    case ModeOperationType.AddChannelOperator:
                        AddChannelOperator(request);
                        break;
                    case ModeOperationType.RemoveChannelOperator:
                        RemoveChannelOperator(request);
                        break;
                    case ModeOperationType.EnableUserVoicePermission:
                        EnableUserVoicePermission(request);
                        break;
                    case ModeOperationType.DisableUserVoicePermission:
                        DisableUserVoicePermission(request);
                        break;
                    default:
                        Mode.SetChannelModes(op);
                        break;
                }
            }
        }
        private void AddBanOnUser(ModeRequest request)
        {
            var result = Users.Values.Where(u => u.Info.NickName == request.NickName);
            if (result.Count() != 1)
            {
                return;
            }
            ChannelUser user = result.First();

            if (BanList.Values.Where(u => u.Info.NickName == request.NickName).Count() == 1)
            {
                return;
            }

            BanList.TryAdd(user.Info.NickName, user);
        }
        private void RemoveBanOnUser(ModeRequest request)
        {
            var result = BanList.Where(u => u.Value.Info.NickName == request.NickName);
            if (result.Count() == 1)
            {
                var keyValue = result.First();
                BanList.Remove(keyValue);
                return;
            }
            if (result.Count() > 1)
            {
                LogWriter.LogError($"Multiple user with same nick name in channel {Name}");
            }
        }

        private void AddChannelOperator(ModeRequest request)
        {
            //check whether this user is in this channel
            var result = Users.Where(u => u.Value.Info.UserName == request.UserName);
            if (result.Count() != 1)
            {
                return;
            }
            var kv = result.First();

            //if this user is already in operator we do not add it
            if (kv.Value.IsChannelOperator)
            {
                return;
            }
            kv.Value.IsChannelOperator = true;
        }

        private void RemoveChannelOperator(ModeRequest request)
        {
            var result = Users.Where(u => u.Value.Info.UserName == request.UserName);
            if (result.Count() != 1)
            {
                return;
            }
            var keyValue = result.First();

            if (keyValue.Value.IsChannelCreator)
            {
                keyValue.Value.IsChannelCreator = false;
            }
        }

        private void EnableUserVoicePermission(ModeRequest request)
        {
            var result = Users.Where(u => u.Value.Info.UserName == request.UserName);
            if (result.Count() != 1)
            {
                return;
            }

            var kv = result.First();

            if (kv.Value.IsVoiceable)
            {
                kv.Value.IsVoiceable = true;
            }

        }
        private void DisableUserVoicePermission(ModeRequest request)
        {
            var result = Users.Where(u => u.Value.Info.UserName == request.UserName);
            if (result.Count() != 1)
            {
                return;
            }

            var kv = result.First();
            if (kv.Value.IsVoiceable)
            {
                kv.Value.IsVoiceable = false;
            }
        }

        public void SetChannelKeyValue(Dictionary<string, string> keyValue)
        {
            foreach (var kv in keyValue)
            {
                if (ChannelKeyValue.ContainsKey(kv.Key))
                {
                    ChannelKeyValue[kv.Key] = kv.Value;
                }
                else
                {
                    ChannelKeyValue.Add(kv.Key, kv.Value);
                }
            }
        }

        public string GetChannelValueString(List<string> keys)
        {
            string values = "";
            foreach (var key in keys)
            {
                if (ChannelKeyValue.ContainsKey(key))
                {
                    values += @"\" + ChannelKeyValue[key];
                }
            }
            return values;
        }
    }
}
