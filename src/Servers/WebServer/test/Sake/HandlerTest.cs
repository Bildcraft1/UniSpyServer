using UniSpy.Server.WebServer.Module.Sake.Handler;
using UniSpy.Server.WebServer.Module.Sake.Contract.Request;
using Xunit;

namespace UniSpy.Server.WebServer.Test.Sake
{
    public class HandlerTest
    {
        [Fact]
        public void CreateRecordTest()
        {
            var client = TestClasses.CreateClient();
            var request = new CreateRecordRequest(RawRequests.CreateRecord);
            var handler = new CreateRecordHandler(client, request);
            handler.Handle();
        }
    }
}