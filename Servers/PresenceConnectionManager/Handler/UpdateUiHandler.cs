﻿using GameSpyLib.Common;
using PresenceConnectionManager.Enumerator;
using System.Collections.Generic;

namespace PresenceConnectionManager.Handler
{
    public class UpdateUiHandler
    {
        public static void UpdateUi(GPCMClient client, Dictionary<string, string> dict)
        {
            GameSpyUtils.PrintReceivedGPDictToLogger("profilelist", dict);
            GameSpyUtils.SendGPError(client.Stream, GPErrorCode.General, "This request is not supported yet.");
        }

    }
}
