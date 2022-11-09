using System.Linq;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Redis.GameServer;
using UniSpyServer.Servers.QueryReport.Entity.Structure.Request;
using UniSpyServer.Servers.ServerBrowser.Abstraction.BaseClass;
using UniSpyServer.Servers.ServerBrowser.Entity.Exception;
using UniSpyServer.Servers.ServerBrowser.Entity.Structure.Request;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Extensions;
using UniSpyServer.UniSpyLib.Logging;

namespace UniSpyServer.Servers.ServerBrowser.Handler.CmdHandler
{
    /// <summary>
    /// we need forward this to game server
    /// </summary>

    public sealed class NatNegMsgHandler : CmdHandlerBase
    {
        private static QueryReport.Entity.Structure.Redis.RedisChannel _redisChannel;
        // private static UdpClient _udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, 27900));
        private new NatNegMsgRequest _request => (NatNegMsgRequest)base._request;
        private GameServerInfo _gameServer;
        public NatNegMsgHandler(IClient client, IRequest request) : base(client, request)
        {
            if (_redisChannel is null)
            {
                _redisChannel = new QueryReport.Entity.Structure.Redis.RedisChannel();
            }
        }

        protected override void RequestCheck()
        {
            base.RequestCheck();
            if (_client.Info.AdHocMessage is null)
            {
                throw new SBException("There are no server messages in client ServerMessageList.");
            }

            _gameServer = _gameServerRedisClient.Context.FirstOrDefault(x =>
                x.HostIPAddress == _client.Info.AdHocMessage.TargetIPEndPoint.Address &
                x.QueryReportPort == (ushort)_client.Info.AdHocMessage.TargetIPEndPoint.Port);
            if (_gameServer is null)
            {
                throw new SBException("There is no matching game server regesterd.");
            }

        }

        protected override void DataOperation()
        {
            // the message must be sent by query report server where client is reporting to.
            var message = new ClientMessageRequest()
            {
                ServerBrowserSenderId = _client.Connection.Server.ServerID,
                NatNegMessage = _request.RawRequest,
                InstantKey = _gameServer.InstantKey,
                TargetIPEndPoint = _gameServer.QueryReportIPEndPoint,
                CommandName = QueryReport.Entity.Enumerate.RequestType.ClientMessage
            };
            _redisChannel.PublishMessage(message);
            LogWriter.Info($"Send client message to QueryReport Server: {_gameServer.ServerID} [{StringExtensions.ConvertByteToHexString(message.NatNegMessage)}]");
            // set adhoc message to null
            _client.Info.AdHocMessage = null;
        }
    }
}
