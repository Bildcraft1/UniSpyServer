﻿using PresenceConnectionManager.Abstraction.BaseClass;
using PresenceSearchPlayer.Entity.Exception.General;


namespace PresenceConnectionManager.Entity.Structure.Request
{
    internal class InviteToRequest : PCMRequestBase
    {
        public uint ProductID { get; protected set; }
        public uint ProfileID { get; protected set; }
        public InviteToRequest(string rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            base.Parse();

            if (!KeyValues.ContainsKey("productid"))
            {
                throw new GPParseException("productid is missing.");
            }

            if (!KeyValues.ContainsKey("sesskey"))
            {
                throw new GPParseException("sesskey is missing.");

            }

            uint productID;
            if (!uint.TryParse(KeyValues["productid"], out productID))
            {
                throw new GPParseException("productid format is incorrect.");
            }

            ProductID = productID;

            uint profileID;
            if (!uint.TryParse(KeyValues["profileid"], out profileID))
            {
                throw new GPParseException("profileid format is incorrect.");
            }
            ProfileID = profileID;
        }
    }
}
