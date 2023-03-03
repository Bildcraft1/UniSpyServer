using UniSpy.Server.Chat.Abstraction.BaseClass;
using UniSpy.Server.Chat.Aggregate.Misc.ChannelInfo;
using UniSpy.Server.Chat.Contract.Request.General;
using UniSpy.Server.Chat.Contract.Response.General;
using UniSpy.Server.Chat.Contract.Result.General;
using UniSpy.Server.Chat.Handler.CmdHandler.Channel;
using UniSpy.Server.Core.Abstraction.Interface;

namespace UniSpy.Server.Chat.Handler.CmdHandler.General
{
    //todo unfinished

    public sealed class ListHandler : LogedInHandlerBase
    {
        private new ListRequest _request => (ListRequest)base._request;
        private new ListResult _result { get => (ListResult)base._result; set => base._result = value; }
        //:irc.foonet.com 321 Pants Channel :Users  Name\r\n:irc.foonet.com 323 Pants :End of /LIST\r\n
        public ListHandler(IClient client, IRequest request) : base(client, request)
        {
            _result = new ListResult();
        }
        protected override void DataOperation()
        {
            //add list response header
            foreach (var channel in ChannelManager.Channels.Values)
            {
                //TODO
                //add channel information here
                ListDataModel channelInfo = new ListDataModel
                {
                    ChannelName = channel.Name,
                    TotalChannelUsers = channel.Users.Count,
                    ChannelTopic = channel.Topic
                };
                _result.ChannelInfoList.Add(channelInfo);
            }
        }

        protected override void ResponseConstruct()
        {
            _response = new ListResponse(_request, _result);
        }
    }
}
