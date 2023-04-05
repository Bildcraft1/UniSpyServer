using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using UniSpy.LinqToRedis;
using UniSpy.Server.Core.Config;
using UniSpy.Server.Core.Extension.Redis;
using UniSpy.Server.Core.Misc;

namespace UniSpy.Server.QueryReport.V1.Aggregation.Redis
{
    public record GameServerInfo : RedisKeyValueObject
    {
        [RedisKey]
        public Guid? ServerID { get; set; }
        [RedisKey]
        [JsonConverter(typeof(IPAddresConverter))]
        public IPAddress HostIPAddress { get; set; }
        [RedisKey]
        public int? QueryReportPort { get; set; }
        [JsonIgnore]
        public IPEndPoint QueryReportIPEndPoint => new IPEndPoint(HostIPAddress, (int)QueryReportPort);
        [RedisKey]
        public string GameName { get; set; }
        public bool IsValidated { get; set; }
        /// <summary>
        /// The key values that contians all the information about this game server
        /// </summary>
        public Dictionary<string, string> KeyValues { get; set; }
        public GameServerInfo() : base(TimeSpan.FromSeconds(30))
        {
        }
    }
    public class RedisClient : RedisClient<GameServerInfo>
    {
        public RedisClient() : base(ConfigManager.Config.Redis.RedisConnection, (int)RedisDbNumber.GameServerV1)
        {
        }
    }
}