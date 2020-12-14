﻿using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Extensions;
using QueryReport.Entity.Enumerate;
using System;
using UniSpyLib.Abstraction.BaseClass;

namespace QueryReport.Abstraction.BaseClass
{
    public class QRRequestBase : UniSpyRequestBase
    {
        public static readonly byte[] MagicData = { 0xFE, 0XFD };
        public int InstantKey { get; protected set; }
        public new QRPacketType CommandName
        {
            get { return (QRPacketType)base.CommandName; }
            protected set { base.CommandName = value; }
        }
        public new byte[] RawRequest
        {
            get { return (byte[])base.RawRequest; }
            protected set { base.RawRequest = value; }
        }
        public new bool ErrorCode
        {
            get { return (bool)base.ErrorCode; }
            protected set { base.ErrorCode = value; }
        }

        public QRRequestBase(byte[] rawRequest) : base(rawRequest)
        {
            RawRequest = rawRequest;
        }

        public override void Parse()
        {
            if (RawRequest.Length < 3)
            {
                ErrorCode = false;
            }
            CommandName = (QRPacketType)RawRequest[0];
            InstantKey = BitConverter.ToInt32(ByteTools.SubBytes(RawRequest, 1, 4));
            ErrorCode = true;
        }
    }
}
