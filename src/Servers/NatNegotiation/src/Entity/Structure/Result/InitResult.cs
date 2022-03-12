using System.Net;
using UniSpyServer.Servers.NatNegotiation.Abstraction.BaseClass;
using UniSpyServer.Servers.NatNegotiation.Entity.Enumerate;

namespace UniSpyServer.Servers.NatNegotiation.Entity.Structure.Result
{
    public sealed class InitResult : CommonResultBase
    {
        public IPEndPoint PrivateIPEndPoint { get; set; }
        public uint? Cookie { get; set; }
        public NatPortType PortType { get; set; }
        public byte? ClientIndex { get; set; }
        public InitResult()
        {
            PacketType = ResponseType.InitAck;
        }
    }
}
