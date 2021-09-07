﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceConnectionManager.Entity.Contract;
using PresenceSearchPlayer.Entity.Exception.General;

namespace PresenceConnectionManager.Entity.Structure.Request
{
    [RequestContract("addbuddy")]
    internal sealed class AddBuddyRequest : PCMRequestBase
    {
        public uint FriendProfileID { get; private set; }
        public string AddReason { get; private set; }
        public AddBuddyRequest(string rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            base.Parse();


            if (!KeyValues.ContainsKey("sesskey") || !KeyValues.ContainsKey("newprofileid") || !KeyValues.ContainsKey("reason"))
            {
                throw new GPParseException("addbuddy request is invalid.");
            }

            uint friendPID;
            if (!uint.TryParse(KeyValues["newprofileid"], out friendPID))
            {
                throw new GPParseException("newprofileid format is incorrect.");
            }

            FriendProfileID = friendPID;
            AddReason = KeyValues["reason"];
        }
    }
}
