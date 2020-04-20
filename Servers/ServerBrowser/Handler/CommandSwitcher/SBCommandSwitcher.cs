﻿using GameSpyLib.Common.BaseClass;
using GameSpyLib.Logging;
using ServerBrowser.Entity.Enumerator;
using ServerBrowser.Handler.CommandHandler.AdHoc.ServerInfo;
using ServerBrowser.Handler.CommandHandler.ServerList.UpdateOptionSwitcher;

namespace ServerBrowser.Handler.CommandSwitcher
{
    public class SBCommandSwitcher : CommandSwitcherBase
    {
        public void Switch(SBSession session, byte[] recv)
        {
            //we do not need to handle GOA query because it is handled by game server
            switch ((SBClientRequestType)recv[2])
            {
                case SBClientRequestType.ServerListRequest:
                    UpdateOptionSwitcher.Switch(session, recv);
                    break;
                case SBClientRequestType.ServerInfoRequest:
                    new ServerInfoHandler(session, recv).Handle();
                    break;
                case SBClientRequestType.PlayerSearchRequest:
                    throw new System.NotImplementedException();
                    break;
                case SBClientRequestType.MapLoopRequest:
                    throw new System.NotImplementedException();
                    break;
                case SBClientRequestType.SendMessageRequest:
                    throw new System.NotImplementedException();
                    //TODO
                    //Cryptorx's game use this command
                    break;
                default:
                    LogWriter.UnknownDataRecieved(recv);
                    break;
            }
        }
    }
}
