using System;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Core.Encryption;
using UniSpy.Server.Core.Logging;
using UniSpy.Server.Core.Extension;

namespace UniSpy.Server.Core.Abstraction.BaseClass
{
    public abstract class ClientBase : IClient, ITestClient, IDisposable
    {
        public IServer Server { get; private set; }
        public IConnection Connection { get; private set; }
        public ICryptography Crypto { get; set; }
        public ClientInfoBase Info { get; protected set; }
        /// <summary>
        /// The timer to count and invoke some event
        /// </summary>
        protected EasyTimer _timer { get; set; }
        /// <summary>
        /// Is logging the raw byte[] requests
        /// </summary>
        public bool IsLogRaw { get; protected set; }
        public ClientBase(IConnection connection, IServer server)
        {
            Connection = connection;
            Server = server;
            EventBinding();
        }
        protected virtual void EventBinding()
        {
            switch (Connection.ConnectionType)
            {
                case NetworkConnectionType.Tcp:
                    ((ITcpConnection)Connection).OnReceive += OnReceived;
                    ((ITcpConnection)Connection).OnConnect += OnConnected;
                    ((ITcpConnection)Connection).OnDisconnect += OnDisconnected;
                    break;
                case NetworkConnectionType.Udp:
                    ((IUdpConnection)Connection).OnReceive += OnReceived;
                    _timer = new EasyTimer(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(1));
                    _timer.Elapsed += (s, e) => CheckExpiredClient();
                    _timer.Start();
                    break;
                case NetworkConnectionType.Http:
                    ((IHttpConnection)Connection).OnReceive += OnReceived;
                    break;
                case NetworkConnectionType.Test:
                    this.LogVerbose("Using unit-test mock connection.");
                    break;
                default:
                    throw new Exception("Unsupported connection type.");
            }
        }
        /// <summary>
        /// Only work for tcp
        /// </summary>
        // protected virtual void OnConnected() => ClientManagerBase.AddClient(this);
        protected virtual void OnConnected() { }

        /// <summary>
        /// Only work for tcp
        /// </summary>
        protected virtual void OnDisconnected() => Dispose();

        protected abstract ISwitcher CreateSwitcher(object buffer);
        /// <summary>
        /// Invoked when received message from game client
        /// </summary>
        /// <param name="buffer">Byte[], string, Http</param>
        protected virtual void OnReceived(object buffer)
        {
            switch (Connection.ConnectionType)
            {
                case NetworkConnectionType.Tcp:
                    goto default;
                case NetworkConnectionType.Udp:
                    _timer.RefreshLastActiveTime();
                    goto default;
                case NetworkConnectionType.Http:
                    var tempBuffer = ((IHttpRequest)buffer).Body;
                    this.LogNetworkReceiving(tempBuffer);
                    break;
                default:
                    buffer = DecryptMessage((byte[])buffer);
                    this.LogNetworkReceiving((byte[])buffer);
                    break;
            }
            // we let child class to create swicher for us
            var switcher = CreateSwitcher(buffer);
            switcher.Handle();
        }
        protected virtual byte[] DecryptMessage(byte[] buffer)
        {
            if (Crypto is not null)
            {
                return Crypto.Decrypt(buffer);
            }
            else
            {
                return buffer;
            }
        }
        /// <summary>
        /// Check if the udp client is expired,
        /// this method will only be invoked by UdpServer
        /// </summary>
        protected void CheckExpiredClient()
        {
            // we calculate the interval between last packe and current time
            if (_timer.IsExpired)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            this.LogDebug("client disposed.");
            switch (Connection.ConnectionType)
            {
                case NetworkConnectionType.Tcp:
                    ((ITcpConnection)Connection).OnReceive -= OnReceived;
                    ((ITcpConnection)Connection).OnConnect -= OnConnected;
                    ((ITcpConnection)Connection).OnDisconnect -= OnDisconnected;
                    ClientManagerBase.RemoveClient(this);
                    break;
                case NetworkConnectionType.Udp:
                    ((IUdpConnection)Connection).OnReceive -= OnReceived;
                    _timer.Dispose();
                    ClientManagerBase.RemoveClient(this);
                    break;
                case NetworkConnectionType.Http:
                    ((IHttpConnection)Connection).OnReceive -= OnReceived;
                    break;
            }
        }
        /// <summary>
        /// Sending IResponse to client(ciphertext or plaintext)
        /// </summary>
        public void Send(IResponse response)
        {
            byte[] buffer = null;
            response.Build();
            if (response.SendingBuffer.GetType() == typeof(string))
            {
                buffer = UniSpyEncoding.GetBytes((string)response.SendingBuffer);
            }
            else
            {
                buffer = (byte[])response.SendingBuffer;
            }
            this.LogNetworkSending(buffer);
            //Encrypt the response if Crypto is not null
            if (Crypto is not null)
            {
                buffer = Crypto.Encrypt(buffer);
            }
            Connection.Send(buffer);
        }
        /// <summary>
        /// Received function for unit-test
        /// </summary>
        /// <param name="buffer">Raw byte array</param>
        void ITestClient.TestReceived(byte[] buffer)
        {
            if (Crypto is not null)
            {
                Crypto = null;
            }
            OnReceived(buffer);
        }
    }
}
