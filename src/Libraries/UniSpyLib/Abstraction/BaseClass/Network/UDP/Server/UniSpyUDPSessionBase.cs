﻿using System;
using System.Net;
using System.Text;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Encryption;

namespace UniSpyLib.Network
{
    public class UniSpyUDPSessionBase : IUniSpySession
    {
        public UniSpyUDPServerBase Server { get; private set; }
        public EndPoint RemoteEndPoint { get; private set; }
        public IPEndPoint RemoteIPEndPoint => (IPEndPoint)RemoteEndPoint;
        public DateTime LastPacketReceivedTime { get; set; }
        public TimeSpan SessionExistedTime => DateTime.Now.Subtract(LastPacketReceivedTime);
        public UniSpyUDPSessionBase(UniSpyUDPServerBase server, EndPoint endPoint)
        {
            Server = server;
            RemoteEndPoint = endPoint;
            LastPacketReceivedTime = DateTime.Now;
        }

        public long Send(byte[] buffer, long offset, long size)
        {
            return Server.Send(RemoteEndPoint, buffer, offset, size);
        }

        public long Send(byte[] buffer)
        {
            return Server.Send(RemoteEndPoint, buffer);
        }

        public long Send(string buffer)
        {
            return Server.Send(RemoteEndPoint, buffer);
        }

        public bool SendAsync(byte[] buffer, long offset, long size)
        {
            return Server.SendAsync(RemoteEndPoint, buffer, offset, size);
        }

        public bool SendAsync(string text)
        {
            return Server.SendAsync(RemoteEndPoint, UniSpyEncoding.GetBytes(text));
        }

        public bool SendAsync(byte[] buffer)
        {
            return Server.SendAsync(RemoteEndPoint, buffer);
        }

        public bool BaseSendAsync(string buffer)
        {
            return Server.BaseSendAsync(RemoteEndPoint, buffer);
        }

        public bool BaseSendAsync(byte[] buffer)
        {
            return Server.BaseSendAsync(RemoteEndPoint, buffer);
        }

        public bool BaseSendAsync(byte[] buffer, long offset, long size)
        {
            return Server.BaseSendAsync(RemoteEndPoint, buffer, offset, size);
        }

    }
}
