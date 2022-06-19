using UniSpyServer.Servers.Chat.Abstraction.BaseClass;
using System.Collections.Generic;

namespace UniSpyServer.Servers.Chat.Entity.Structure.Result.Channel
{
    public sealed class GetCKeyDataModel
    {
        public string NickName { get; set; }
        public string UserValues { get; set; }
    }
    public sealed class GetCKeyResult : ResultBase
    {
        public List<GetCKeyDataModel> DataResults { get; }
        public string ChannelName { get; set; }
        public GetCKeyResult()
        {
            DataResults = new List<GetCKeyDataModel>();
        }
    }
}
