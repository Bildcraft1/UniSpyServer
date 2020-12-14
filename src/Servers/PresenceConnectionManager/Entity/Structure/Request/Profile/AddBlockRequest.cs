﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceSearchPlayer.Entity.Enumerate;
using System.Collections.Generic;

namespace PresenceConnectionManager.Entity.Structure.Request
{
    public class AddBlockRequest : PCMRequestBase
    {
        public uint ProfileID;
        public AddBlockRequest(string rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            base.Parse();

            if( ErrorCode != GPErrorCode.NoError)
            {
                return;
            }

            if (!KeyValues.ContainsKey("profileid"))
            {
                ErrorCode = GPErrorCode.Parse; return;
            }

            uint profileID;
            if (!uint.TryParse(KeyValues["profileid"], out profileID))
            {
                ErrorCode = GPErrorCode.Parse; return;
            }

            ProfileID = profileID;

            ErrorCode = GPErrorCode.NoError;
        }
    }
}
