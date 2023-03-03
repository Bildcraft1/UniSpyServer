using UniSpy.Server.Chat.Exception;

namespace UniSpy.Server.Chat.Abstraction.BaseClass
{
    public class ChannelRequestBase : RequestBase
    {
        public string ChannelName { get; set; }
        public ChannelRequestBase() { }
        public ChannelRequestBase(string rawRequest) : base(rawRequest) { }
        public override void Parse()
        {
            base.Parse();

            if (_cmdParams is null || _cmdParams?.Count < 1)
            {
                throw new ChatException("Channel name is missing.");
            }
            ChannelName = _cmdParams[0];
        }
    }
}
