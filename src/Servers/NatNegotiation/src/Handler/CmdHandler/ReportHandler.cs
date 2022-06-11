using System.Linq;
using UniSpyServer.Servers.NatNegotiation.Abstraction.BaseClass;

using UniSpyServer.Servers.NatNegotiation.Entity.Enumerate;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Request;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Response;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure.Result;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Logging;

namespace UniSpyServer.Servers.NatNegotiation.Handler.CmdHandler
{
    /// <summary>
    /// Get nat neg result report success or fail
    /// </summary>
    
    public sealed class ReportHandler : CmdHandlerBase
    {
        private new ReportRequest _request => (ReportRequest)base._request;
        private new ReportResult _result { get => (ReportResult)base._result; set => base._result = value; }
        public ReportHandler(IClient client, IRequest request) : base(client, request)
        {
            _result = new ReportResult();
        }

        protected override void DataOperation()
        {
            switch (_request.NatResult)
            {
                case NatNegResult.Success:
                    // if there is a success p2p connection, we delete the init info in redis
                    _redisClient.Context.Where(k => k.Cookie == _request.Cookie).ToList()
                            .ForEach(k => _redisClient.DeleteKeyValue(k));
                    LogWriter.Info("Nat negotiation success.");
                    break;
                case NatNegResult.DeadBeatPartner:
                    LogWriter.Info($"Parter of client {_client.Session.RemoteIPEndPoint} has no response.");
                    goto default;
                case NatNegResult.InitTimeOut:
                    LogWriter.Info($"Client {_client.Session.RemoteIPEndPoint} nat initialization failed.");
                    break;
                case NatNegResult.PingTimeOut:
                    LogWriter.Info($"Client {_client.Session.RemoteIPEndPoint} nat ping failed.");
                    goto default;
                case NatNegResult.UnknownError:
                    LogWriter.Info($"Client {_client.Session.RemoteIPEndPoint} nat negotiation unknown error occured.");
                    break;
                default:
                    var request = new ConnectRequest
                    {
                        PortType = NatPortType.NN1,
                        Version = _request.Version,
                        Cookie = _request.Cookie,
                        IsUsingRelay = true
                    };
                    new ConnectHandler(_client, request).Handle();
                    // var packets = _redisClient.Values.Where(k => k.Cookie == _request.Cookie).ToList();
                    // foreach (var packet in packets)
                    // {
                    //     packet.RetryNatNegotiationTime++;
                    //     _redisClient.SetValue(packet);
                    // }
                    break;
            }
        }

        protected override void ResponseConstruct()
        {
            _response = new ReportResponse(_request, _result);
        }
    }
}
