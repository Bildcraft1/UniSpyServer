using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using UniSpyServer.Servers.NatNegotiation.Entity.Structure;
using UniSpyServer.Servers.NatNegotiation.Handler;
using UniSpyServer.Servers.NatNegotiation.Handler.CmdHandler;
using UniSpyServer.UniSpyLib.Abstraction.Interface;
using Xunit;

namespace UniSpyServer.Servers.NatNegotiation.Test
{
    public class GameTest
    {
        public IClient _client;
        public IClient _server;
        public GameTest()
        {
            var serverMock = new Mock<IServer>();
            serverMock.Setup(s => s.ServerID).Returns(new System.Guid());
            serverMock.Setup(s => s.ServerName).Returns("NatNegotiation");
            serverMock.Setup(s => s.Endpoint).Returns(new IPEndPoint(IPAddress.Any, 99));
            var clientSessionMock = new Mock<IUdpSession>();
            clientSessionMock.Setup(s => s.RemoteIPEndPoint).Returns(new IPEndPoint(IPAddress.Parse("192.168.1.2"), 9999));
            clientSessionMock.Setup(s => s.Server).Returns(serverMock.Object);
            clientSessionMock.Setup(s => s.ConnectionType).Returns(NetworkConnectionType.Udp);
            clientSessionMock.Setup(s => s.SessionExistedTime).Returns(new System.TimeSpan(0, 0, 0, 0, 0));
            _client = new Client(clientSessionMock.Object);
            var serverSessionMock = new Mock<IUdpSession>();
            serverSessionMock.Setup(s => s.RemoteIPEndPoint).Returns(new IPEndPoint(IPAddress.Parse("192.168.1.3"), 9999));
            serverSessionMock.Setup(s => s.Server).Returns(serverMock.Object);
            serverSessionMock.Setup(s => s.ConnectionType).Returns(NetworkConnectionType.Udp);
            serverSessionMock.Setup(s => s.SessionExistedTime).Returns(new System.TimeSpan(0, 0, 0, 0, 0));
            _server = new Client(serverSessionMock.Object);

        }
        [Fact]
        public void NegotiationTest()
        {
            var clientInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };


            var serverInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientRequests = new List<byte[]> { clientInitGP, clientInitNN1, clientInitNN2, clientInitNN3 };
            var serverRequests = new List<byte[]> { serverInitGP, serverInitNN1, serverInitNN2, serverInitNN3 };

            Client.ClientPool.Add(_client.Session.RemoteIPEndPoint, _client);
            Client.ClientPool.Add(_server.Session.RemoteIPEndPoint, _server);
            foreach (var request in clientRequests)
            {
                var switcher = new CmdSwitcher(_client, request);
                switcher.Switch();
            }

            foreach (var request in serverRequests)
            {
                var switcher = new CmdSwitcher(_server, request);
                switcher.Switch();
            }


            // When

            // Then
        }
    }
}