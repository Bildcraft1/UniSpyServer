using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebServer.Abstraction;
using WebServer.Entity.Contract;

namespace WebServer.Entity.Structure.Request
{
    [RequestContract("UpdateRecord")]
    public class UpdateRecordRequest : RequestBase
    {
        public uint GameId { get; set; }
        public string SecretKey { get; set; }
        public string LoginTicket { get; set; }
        public string TableId { get; set; }
        public string RecordId { get; set; }

        public List<FieldsObject> Values { get; set; }
        public UpdateRecordRequest(string rawRequest) : base(rawRequest)
        {
            Values = new List<FieldsObject>();
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
            var recordid = _contentElement.Descendants().Where(p => p.Name.LocalName == "recordid").First().Value;
            RecordId = recordid;
            var valuesNode = _contentElement.Descendants().Where(p => p.Name.LocalName == "values").First();
            foreach (XElement element in valuesNode.Nodes())
            {
                Values.Add(new FieldsObject(element.Value, element.Name.LocalName));
            }
        }
    }
}