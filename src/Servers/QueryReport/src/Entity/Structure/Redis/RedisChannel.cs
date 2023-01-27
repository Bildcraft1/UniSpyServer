using UniSpyServer.Servers.QueryReport.Application;
using UniSpyServer.Servers.QueryReport.V2.Entity.Exception;
using UniSpyServer.Servers.QueryReport.V2.Entity.Structure.Request;
using UniSpyServer.Servers.QueryReport.V2.Handler.CmdHandler;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using UniSpyServer.UniSpyLib.Entity.Structure;
using UniSpyServer.UniSpyLib.Extensions;
using UniSpyServer.UniSpyLib.Logging;

namespace UniSpyServer.Servers.QueryReport.V2.Entity.Structure.Redis
{
    public sealed class RedisChannel : UniSpyLib.Abstraction.BaseClass.RedisChannelBase<ClientMessageRequest>
    {
        public RedisChannel() : base(UniSpyRedisChannelName.NatNegCookieChannel)
        {
        }
        public override void ReceivedMessage(ClientMessageRequest message)
        {
            IClient client;

            // LogWriter.LogNetworkReceiving(message.TargetIPEndPoint,  message.NatNegMessage, true);
            if (Client.ClientPool.ContainsKey(message.TargetIPEndPoint))
            {
                client = Client.ClientPool[message.TargetIPEndPoint];
            }
            else
            {
                throw new QRException($"Client:{message.TargetIPEndPoint} not found.");
            }
            client.LogInfo($"Get client message from server browser: {message.ServerBrowserSenderId} [{StringExtensions.ConvertByteToHexString(message.NatNegMessage)}]");
            new ClientMessageHandler(client, message).Handle();
        }
    }
}