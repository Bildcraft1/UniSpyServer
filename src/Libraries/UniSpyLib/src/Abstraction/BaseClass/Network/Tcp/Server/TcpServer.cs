using System;
using System.Net;
using UniSpyServer.UniSpyLib.Abstraction.Interface;

namespace UniSpyServer.UniSpyLib.Abstraction.BaseClass.Network.Tcp.Server
{
    /// <summary>
    /// This is a template class that helps creating a TCP Server with logging functionality and ServerName, as required in the old network stack.
    /// </summary>
    public abstract class TcpServer : NetCoreServer.TcpServer, IServer
    {
        public Guid ServerID { get; private set; }
        /// <summary>
        /// Initialize TCP server with a given IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public string ServerName { get; private set; }

        IPEndPoint IServer.Endpoint => (IPEndPoint)Endpoint;

        public TcpServer(Guid serverID, string serverName, IPEndPoint endpoint) : base(endpoint)
        {
            ServerID = serverID;
            ServerName = serverName;
        }

        public override bool Start()
        {
            if (OptionSendBufferSize > int.MaxValue || OptionReceiveBufferSize > int.MaxValue)
            {
                throw new ArgumentException("Buffer size can not big than length of integer!");
            }
            return base.Start();
        }
        protected abstract IClient CreateClient(IConnection connection);

        protected override NetCoreServer.TcpSession CreateSession() => new TcpConnection(this);

        protected override void OnConnecting(NetCoreServer.TcpSession connection)
        {
            base.OnConnecting(connection);
            CreateClient((IConnection)connection);
        }
    }
}
