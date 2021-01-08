﻿using System;
using PresenceSearchPlayer.Abstraction.BaseClass;
using PresenceSearchPlayer.Entity.Structure.Result;
using UniSpyLib.Abstraction.BaseClass;

namespace PresenceConnectionManager.Entity.Structure.Response
{
    public class NewUserResponse : PSPResponseBase
    {
        protected new NewUserResult _result => (NewUserResult)base._result;
        public NewUserResponse(UniSpyResultBase result) : base(result)
        {
        }

        protected override void BuildNormalResponse()
        {
            SendingBuffer =
                $@"\nur\\userid\{_result.User.Userid}\profileid\{_result.SubProfile.Profileid}\id\{_result.Request.OperationID}\final\";
        }
    }
}
