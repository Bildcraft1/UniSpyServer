﻿using Chat.Abstraction.BaseClass;
using UniSpyLib.Logging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UniSpyLib.Abstraction.Interface;
using UniSpyLib.Abstraction.BaseClass;

namespace Chat.Handler.CommandSwitcher
{
    public class ChatRequestSerializer : UniSpyRequestSerializerBase
    {
        protected new string _rawRequest;
        public ChatRequestSerializer(object rawRequest) : base(rawRequest)
        {
            _rawRequest = (string)rawRequest;
        }


        public override IUniSpyRequest Serialize()
        {
            ChatRequestBase generalRequest = new ChatRequestBase(_rawRequest);
            generalRequest.Parse();
            if (!generalRequest.ErrorCode)
            {
                LogWriter.ToLog(LogEventLevel.Error, "Invalid request!");
                return null;
            }

            Type requestType = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .FirstOrDefault(t => t.Name == generalRequest.CommandName + "Request");
            if (requestType != null)
            {
                var request = Activator.CreateInstance(requestType, generalRequest.RawRequest);
                if (request == null)
                {
                    LogWriter.ToLog(LogEventLevel.Error, $"Unknown request {generalRequest.CommandName}!");
                    return null;
                }
                return (IUniSpyRequest)request;
            }
            else
            {
                LogWriter.ToLog(LogEventLevel.Error, $"Request: {generalRequest.CommandName} not implemented!");
                return null;
            }
        }


    }
}
