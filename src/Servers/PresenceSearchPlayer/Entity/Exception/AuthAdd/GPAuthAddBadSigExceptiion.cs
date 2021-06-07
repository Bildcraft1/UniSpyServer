using System;
using PresenceSearchPlayer.Abstraction.BaseClass;
using PresenceSearchPlayer.Entity.Enumerate;
using PresenceSearchPlayer.Entity.Exception.General;

namespace PresenceSearchPlayer.Entity.Exception.AuthAdd
{
    public class GPAuthAddBadSigException : GPException
    {
        public GPAuthAddBadSigException() : base("The signature in authentication is invalid!", GPErrorCode.AuthAddBadSig)
        {
        }

        public GPAuthAddBadSigException(string message) : base(message, GPErrorCode.AuthAddBadSig)
        {
        }

        public GPAuthAddBadSigException(string message, System.Exception innerException) : base(message, GPErrorCode.AuthAddBadSig, innerException)
        {
        }
    }
}