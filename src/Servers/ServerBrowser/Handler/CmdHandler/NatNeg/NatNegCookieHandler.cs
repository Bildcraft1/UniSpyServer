﻿using System.Linq;
using Newtonsoft.Json;
using QueryReport.Entity.Structure;
using QueryReport.Entity.Structure.NatNeg;
using ServerBrowser.Abstraction.BaseClass;
using ServerBrowser.Entity.Enumerate;
using ServerBrowser.Entity.Structure.Request;
using StackExchange.Redis;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Abstraction.Interface;

namespace ServerBrowser.Handler.CmdHandler
{
    /// <summary>
    /// we need forward this to game server
    /// </summary>
    public class NatNegCookieHandler : SBCmdHandlerBase
    {
        protected new AdHocRequest _request
        {
            get { return (AdHocRequest)base._request; }
            set { base._request = value; }
        }
        private NatNegCookie _natNegCookie;
        private GameServerInfo _gameServer;
        public NatNegCookieHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _natNegCookie = new NatNegCookie();
        }

        protected override void RequestCheck()
        {
            base.RequestCheck();
            if (_session.ServerMessageList.Count == 0)
            {
                _errorCode = SBErrorCode.Parse;
                return;
            }
            _request = _session.ServerMessageList[0];
            _session.ServerMessageList.Remove(_request);

            var result = GameServerInfo.RedisOperator.GetMatchedKeyValues(_request.TargetServerIP)
                .Values.Where(s => s.ServerData.KeyValue.ContainsKey("hostport"))
                .Where(s => s.ServerData.KeyValue["hostport"] == _request.TargetServerHostPort);

            if (result.Count() != 1)
            {
                _errorCode = SBErrorCode.NoServersFound;
                return;
            }
            _gameServer = result.FirstOrDefault();
        }

        protected override void DataOperation()
        {
            base.DataOperation();
            _natNegCookie.GameServerRemoteIP = _request.TargetServerIP;
            _natNegCookie.GameServerRemotePort = _gameServer.RemoteQueryReportPort;
            _natNegCookie.NatNegMessage = _request.RawRequest;
        }

        protected override void ResponseConstruct()
        {
            base.ResponseConstruct();
        }

        protected override void Response()
        {
            ISubscriber sub = UniSpyServerManagerBase.Redis.GetSubscriber();

            string jsonStr = JsonConvert.SerializeObject(_natNegCookie);

            sub.Publish("NatNegCookieChannel", jsonStr);
        }
    }
}
