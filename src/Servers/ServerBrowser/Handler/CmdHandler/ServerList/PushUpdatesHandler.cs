﻿using QueryReport.Entity.Structure;
using QueryReport.Entity.Structure.Misc;
using QueryReport.Handler.SystemHandler.Redis;
using ServerBrowser.Abstraction.BaseClass;
using ServerBrowser.Entity.Enumerate;
using ServerBrowser.Entity.Structure.Packet.Response;
using ServerBrowser.Entity.Structure.Result;
using System.Linq;
using UniSpyLib.Abstraction.Interface;

namespace ServerBrowser.Handler.CmdHandler
{
    /// <summary>
    /// Search peer to peer game servers to client
    /// </summary>
    internal sealed class PushUpdatesHandler : ServerListHandlerBase
    {
        private new GeneralRequestResult _result
        {
            get => (GeneralRequestResult)base._result;
            set => base._result = value;
        }
        public PushUpdatesHandler(IUniSpySession session, IUniSpyRequest request) : base(session, request)
        {
            _result = new GeneralRequestResult();
        }
        protected override void DataOperation()
        {
            var searchKey = new GameServerInfoRedisKey()
            {
                GameName = _request.GameName
            };

            _result.GameServerInfos = GameServerInfoRedisOperator.GetMatchedKeyValues(searchKey).Values.ToList();
            _result.Flag = GameServerFlags.HasFullRulesFlag;
            //TODO do filter
            //**************Currently we do not handle filter**********************
        }

        protected override void ResponseConstruct()
        {
            _response = new GeneralRequestResponse(_request, _result);
        }
    }
}
