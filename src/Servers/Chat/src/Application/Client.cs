using System.Threading.Tasks;
using UniSpy.Server.Chat.Abstraction.Interface;
using UniSpy.Server.Chat.Aggregate;
using UniSpy.Server.Chat.Aggregate.Misc;
using UniSpy.Server.Chat.Contract.Request.General;
using UniSpy.Server.Chat.Handler;
using UniSpy.Server.Chat.Handler.CmdHandler.General;
using UniSpy.Server.Core.Abstraction.BaseClass;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Core.Encryption;
using UniSpy.Server.Core.Logging;

namespace UniSpy.Server.Chat.Application
{
    public class Client : ClientBase, IShareClient
    {
        public new ClientInfo Info { get => (ClientInfo)base.Info; private set => base.Info = value; }
        public new ITcpConnection Connection => (ITcpConnection)base.Connection;
        public bool IsRemoteClient => !ClientManager.ClientPool.ContainsKey(Connection.RemoteIPEndPoint);
        private BufferCache _bufferCache = new BufferCache();
        private RemoteClient _remoteClient;
        public Client(IConnection connection, IServer server) : base(connection, server)
        {
            Info = new ClientInfo();
            _remoteClient = new RemoteClient(this);
        }
        public Client(IConnection connection, IServer server, ClientInfo info) : this(connection, server)
        {
            Info = info;
            _remoteClient = new RemoteClient(this);
        }

        protected override void OnReceived(object buffer)
        {
            var message = DecryptMessage((byte[])buffer);
            if (_bufferCache.ProcessBuffer(message, out var completeBuffer))
            {
                this.LogNetworkReceiving(completeBuffer);
                var switcher = CreateSwitcher(completeBuffer);
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    switcher.Handle();
                }
                else
                {
                    Task.Run(() => switcher.Handle());
                }
            }
        }
        protected override void OnDisconnected()
        {
            if (Info.IsLoggedIn)
            {
                var req = new QuitRequest()
                {
                    Reason = $"{Info.NickName} Disconnected."
                };
                new QuitHandler(this, req).Handle();
                Info.IsLoggedIn = false;
            }
            base.OnDisconnected();
        }
        protected override ISwitcher CreateSwitcher(object buffer) => new CmdSwitcher(this, UniSpyEncoding.GetString((byte[])buffer));
        public RemoteClient GetRemoteClient() => _remoteClient;
        protected override void OnConnected()
        {
            StorageOperation.Persistance.RemoveClient(this);
            base.OnConnected();
        }
    }
}