﻿using GameStatus.Abstraction.BaseClass;
using GameStatus.Entity.Enumerate;
using GameStatus.Entity.Structure.Misc;
using GameStatus.Handler.SystemHandler;
using Serilog.Events;
using UniSpyLib.Abstraction.BaseClass;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Encryption;
using UniSpyLib.Logging;
using UniSpyLib.Network;

namespace GameStatus.Handler.CmdSwitcher
{
    internal sealed class GSCmdSwitcher : UniSpyCmdSwitcherBase
    {
        private new string _rawRequest
        {
            get => (string)base._rawRequest;
            set => base._rawRequest = value;
        }
        public GSCmdSwitcher(IUniSpySession session, object rawRequest) : base(session, rawRequest)
        {
        }

        protected override void SerializeCommandHandlers()
        {
            foreach (var request in _requests)
            {
                var handler = new GSCmdHandlerFactory(_session, request).Serialize();
                if (handler == null)
                {
                    return;
                }
                _handlers.Add(handler);
            }
        }

        protected override void SerializeRequests()
        {
            var request = new GSRequestFactory(_rawRequest).Serialize();
            request.Parse();
            if ((GSErrorCode)request.ErrorCode != GSErrorCode.NoError)
            {
                LogWriter.ToLog(LogEventLevel.Error, ErrorMessage.ToMsg(((GSRequestBase)request).ErrorCode));
                return;
            }
            _requests.Add(request);
        }
    }
}
