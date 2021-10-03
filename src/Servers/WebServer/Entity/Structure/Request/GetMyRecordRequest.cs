using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebServer.Abstraction;
using WebServer.Entity.Contract;

namespace WebServer.Entity.Structure.Request
{
    [RequestContract("GetMyRecords")]
    public class GetMyRecordRequest : RequestBase
    {
        public uint GameId { get; set; }
        public string SecretKey { get; set; }
        public string LoginTicket { get; set; }
        public string TableId { get; set; }
        public List<FieldsObject> Fields { get; set; }
        public GetMyRecordRequest(string rawRequest) : base(rawRequest)
        {
        }

        public override void Parse()
        {
            var gameId = _contentElement.Descendants().Where(p => p.Name.LocalName == "gameid").First().Value;
            GameId = uint.Parse(gameId);
            var secretKey = _contentElement.Descendants().Where(p => p.Name.LocalName == "secretKey").First().Value;
            SecretKey = secretKey;
            var loginTicket = _contentElement.Descendants().Where(p => p.Name.LocalName == "loginTicket").First().Value;
            LoginTicket = loginTicket;
            var tableid = _contentElement.Descendants().Where(p => p.Name.LocalName == "tableid").First().Value;
            TableId = tableid;
            var fieldsNode = _contentElement.Descendants().Where(p => p.Name.LocalName == "fields").First();
            var fields = new List<FieldsObject>();
            foreach (XElement element in fieldsNode.Nodes())
            {
                fields.Add(new FieldsObject(element.Value, element.Name.LocalName));
            }
        }
    }
}