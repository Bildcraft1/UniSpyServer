using UniSpyServer.Servers.ServerBrowser.Entity.Enumerate;
using UniSpyServer.Servers.ServerBrowser.Entity.Structure.Request;
using Xunit;

namespace UniSpyServer.Servers.UniSpyServer.Servers.ServerBrowser.Test
{
    public class RequestTest
    {
        [Fact]
        public void ServerListTest()
        {
            var raw = new byte[]{0x00, 0x55,
            0x00, //command name
            0x01, 0x03, 0x00, 0x00, 0x00,
            0x00, 0x67, 0x6d, 0x74, 0x65, 0x73, 0x74, 0x00,
            0x67, 0x6d, 0x74, 0x65, 0x73, 0x74, 0x00, 0x6e,
            0x29, 0x29, 0x34, 0x31, 0x58, 0x4d, 0x36, 0x00,
            0x5c, 0x68, 0x6f, 0x73, 0x74, 0x6e, 0x61, 0x6d,
            0x65, 0x5c, 0x67, 0x61, 0x6d, 0x65, 0x74, 0x79,
            0x70, 0x65, 0x5c, 0x6d, 0x61, 0x70, 0x6e, 0x61,
            0x6d, 0x65, 0x5c, 0x6e, 0x75, 0x6d, 0x70, 0x6c,
            0x61, 0x79, 0x65, 0x72, 0x73, 0x5c, 0x6d, 0x61,
            0x78, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x73,
            0x00, 0x00, 0x00, 0x00, 0x00};
            var request = new ServerListRequest(raw);
            request.Parse();
            Assert.Equal(RequestType.ServerListRequest, request.CommandName);

            raw = new byte[] { 0x00, 0x58,
             0x00,
             0x01, 0x03, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6d, 0x74, 0x65, 0x73, 0x74, 0x00, 0x67, 0x6d, 0x74, 0x65, 0x73, 0x74, 0x00, 0x42, 0x46, 0x5d, 0x6c, 0x6b, 0x22, 0x2c, 0x35, 0x31, 0x32, 0x33, 0x00, 0x5c, 0x68, 0x6f, 0x73, 0x74, 0x6e, 0x61, 0x6d, 0x65, 0x5c, 0x6e, 0x75, 0x6d, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x73, 0x5c, 0x6d, 0x61, 0x78, 0x70, 0x6c, 0x61, 0x79, 0x65, 0x72, 0x73, 0x5c, 0x6d, 0x61, 0x70, 0x6e, 0x61, 0x6d, 0x65, 0x5c, 0x67, 0x61, 0x6d, 0x65, 0x74, 0x79, 0x70, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00 };
            request = new ServerListRequest(raw);
            request.Parse();
        }
    }
}
