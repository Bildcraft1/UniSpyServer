﻿using System.Net;
using Chat.Entity.Structure;
using Chat.Entity.Structure.ChatCommand;
using Chat.Entity.Structure.Enumerator.Request;
using Chat.Server;
using GameSpyLib.Common.Entity.Interface;

namespace Chat.Handler.CommandHandler.USRIP
{
    public class USRIPHandler : ChatCommandHandlerBase
    {
        public USRIPHandler(IClient client, ChatCommandBase cmd, string response) : base(client, cmd, response)
        {
        }

        public override void ConstructResponse()
        {
            base.ConstructResponse();
            
            string IP = ((IPEndPoint)_session.Socket.RemoteEndPoint).Address.ToString();

           _sendingBuffer =  ChatCommandBase.BuildCommandString((int)ChatResponse.UserIP,
                _session.ClientInfo.NickName
                + " :" + _session.ClientInfo.NickName
                + "=+ " + _session.ClientInfo.UserName
                + "@" +
                IP);
        }

        public override void SetCommandName()
        {
            CommandName = ChatRequest.USRIP.ToString();
        }
    }
}
