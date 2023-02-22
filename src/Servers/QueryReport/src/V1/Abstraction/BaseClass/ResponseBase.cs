using System;

namespace UniSpy.Server.QueryReport.V1.Abstraction.BaseClass
{
    public abstract class ResponseBase : UniSpy.Server.Core.Abstraction.BaseClass.ResponseBase
    {
        protected new RequestBase _request => (RequestBase)base._request;
        protected new ResultBase _result => (ResultBase)base._result;
        public new string SendingBuffer
        {
            get => (string)base.SendingBuffer;
            protected set => base.SendingBuffer = value;
        }
        public ResponseBase(RequestBase request, ResultBase result) : base(request, result)
        {
        }
        public override void Build()
        {
            SendingBuffer += @"/final/";
        }
    }
}
