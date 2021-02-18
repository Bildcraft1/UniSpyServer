﻿using System;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Logging;
using UniSpyLib.Network;

namespace UniSpyLib.Abstraction.BaseClass.Network.UDP
{
    public class UniSpyUDPSessionManagerBase : UniSpySessionManagerBase
    {
        protected TimeSpan _expireTimeInterval { get; set; }
        private Timer _timer;

        public UniSpyUDPSessionManagerBase()
        {
            //default settings
            _timer = new Timer
            {
                Enabled = true,
                Interval = 60000,
                AutoReset = true
            };//10000
            _expireTimeInterval = new TimeSpan(0, 0, 120);
            _timer.Start();
            _timer.Elapsed += (s, e) => CheckExpiredSession();
        }

        protected virtual void CheckExpiredSession()
        {
            //log which expire manager excuted
            LogWriter.LogCurrentClass(this);
            Parallel.ForEach(Sessions.Values, (session) =>
            {
                UniSpyUDPSessionBase sess = (UniSpyUDPSessionBase)session;
                // we calculate the interval between last packe and current time
                if (sess.SessionExistedTime > _expireTimeInterval)
                {
                    DeleteSession(sess.RemoteIPEndPoint);
                }
            });
        }

        public UniSpyUDPSessionBase GetSession(IPEndPoint key)
        {
            IUniSpySession session;
            if (Sessions.TryGetValue(key, out session))
            {
                return (UniSpyUDPSessionBase)session;
            }
            else
            {
                return null;
            }
        }

        public bool AddSession(IPEndPoint key, UniSpyUDPSessionBase session)
        {
            return Sessions.TryAdd(key, session);
        }

        public bool DeleteSession(IPEndPoint key)
        {
            return Sessions.TryRemove(key, out _);
        }
    }
}
