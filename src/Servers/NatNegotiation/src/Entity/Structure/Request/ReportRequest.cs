﻿using System;
using System.Linq;
using UniSpyServer.Servers.NatNegotiation.Abstraction.BaseClass;
using UniSpyServer.Servers.NatNegotiation.Entity.Contract;
using UniSpyServer.Servers.NatNegotiation.Entity.Enumerate;
using UniSpyServer.UniSpyLib.Encryption;

namespace UniSpyServer.Servers.NatNegotiation.Entity.Structure.Request
{
    [RequestContract(RequestType.Report)]
    public sealed class ReportRequest : RequestBase
    {
        public NatNegResult? NatResult { get; private set; }
        public NatNegType? NatType { get; private set; }
        public NatMappingScheme? MappingScheme { get; private set; }
        public string GameName { get; private set; }
        public ReportRequest(byte[] rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            base.Parse();
            PortType = (NatPortType)RawRequest[13];
            ClientIndex = RawRequest[14];
            NatResult = (NatNegResult)RawRequest[15];
            NatType = (NatNegType)BitConverter.ToUInt16(RawRequest.Skip(17).Take(2).ToArray());
            MappingScheme = (NatMappingScheme)BitConverter.ToInt32(RawRequest.Skip(19).Take(4).ToArray());
            GameName = UniSpyEncoding.GetString(RawRequest.Skip(23).ToArray());
        }
    }
}
