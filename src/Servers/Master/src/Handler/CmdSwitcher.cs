using System.Collections.Generic;
using UniSpy.Server.Core.Abstraction.BaseClass;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Core.Encryption;
using UniSpy.Server.Core.Logging;

namespace UniSpy.Server.Master.Handler
{
    public sealed class CmdSwitcher : CmdSwitcherBase
    {
        private new string _rawRequest => UniSpyEncoding.GetString((byte[])base._rawRequest);

        public CmdSwitcher(IClient client, object rawRequest) : base(client, rawRequest)
        {
        }

        protected override IHandler CreateCmdHandlers(object name, object rawRequest)
        {
            //todo add v1 support
            _client.LogError("todo add v1 support");
            switch ((string)name)
            {
                case "heartbeat":
                case "echo":
                case "validate":
                default:
                    return null;
            }
        }

        protected override void ProcessRawRequest()
        {
            // qr v1 protocol
            var cmdFrags = (_rawRequest).Split('\\');
            var name = cmdFrags[0];
            _requests.Add(new KeyValuePair<object, object>(name, _rawRequest));
        }
    }
}