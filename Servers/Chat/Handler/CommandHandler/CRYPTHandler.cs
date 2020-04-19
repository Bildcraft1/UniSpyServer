﻿using Chat.Entity.Structure;
using Chat.Entity.Structure.ChatCommand;
using Chat.Handler.SystemHandler.Encryption;
using Chat.Server;
using GameSpyLib.Common.Entity.Interface;
using GameSpyLib.Extensions;
using GameSpyLib.Logging;

namespace Chat.Handler.CommandHandler
{
    public class CRYPTHandler : ChatCommandHandlerBase
    {
        CRYPT _cryptCmd;

        public CRYPTHandler(IClient client, ChatCommandBase cmd) : base(client, cmd)
        {
            _cryptCmd = (CRYPT)_cmd;
        }

        public override void CheckRequest()
        {
            base.CheckRequest();

            // CRYPT des 1 gamename
            //_clientInfo.GameName = _recv[3];
            _session.ClientInfo.GameName = _cryptCmd.GameName;
        }

        public override void DataOperation()
        {
            base.DataOperation();
            if (!DataOperationExtensions.GetSecretKey(_session.ClientInfo.GameName, out _session.ClientInfo.GameSecretKey)
                || _session.ClientInfo.GameSecretKey == null)
            {
                LogWriter.ToLog(Serilog.Events.LogEventLevel.Error, "secret key not found!");
                return;
            }
        }

        public override void ConstructResponse()
        {
            base.ConstructResponse();

            // 2. Prepare two keys
            ChatCrypt.Init(_session.ClientInfo.ClientCTX, ChatServer.ClientKey, _session.ClientInfo.GameSecretKey);
            ChatCrypt.Init(_session.ClientInfo.ServerCTX, ChatServer.ServerKey, _session.ClientInfo.GameSecretKey);

            // 3. Response the crypt command
            _sendingBuffer = _cryptCmd.BuildRPL(ChatServer.ClientKey, ChatServer.ServerKey);
        }

        public override void Response()
        {
            //set use encryption flag to true
            _client.SendAsync(_sendingBuffer);
            _session.ClientInfo.UseEncryption = true;
        }
    }
}
