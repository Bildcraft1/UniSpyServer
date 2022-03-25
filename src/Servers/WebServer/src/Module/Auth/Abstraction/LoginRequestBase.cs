using System.Linq;
using UniSpyServer.Servers.WebServer.Abstraction;
using UniSpyServer.Servers.WebServer.Module.Auth.Exception;

namespace UniSpyServer.Servers.WebServer.Module.Auth.Abstraction
{
    public abstract class LoginRequestBase : RequestBase
    {
        protected LoginRequestBase(string rawRequest) : base(rawRequest)
        {
        }

        public int Version { get; private set; }
        public int PartnerCode { get; private set; }
        public int NamespaceId { get; private set; }
        public override void Parse()
        {
            base.Parse();
            if (!_contentElement.Descendants().Any(p => p.Name.LocalName == "version"))
            {
                throw new AuthException("version is missing from the request");
            }
            var version = _contentElement.Descendants().First(p => p.Name.LocalName == "version").Value;
            Version = int.Parse(version);

            if (!_contentElement.Descendants().Any(p => p.Name.LocalName == "partnercode"))
            {
                throw new AuthException("partnercode is missing from the request");
            }
            var partnercode = _contentElement.Descendants().First(p => p.Name.LocalName == "partnercode").Value;
            PartnerCode = int.Parse(partnercode);

            if (!_contentElement.Descendants().Any(p => p.Name.LocalName == "namespaceid"))
            {
                throw new AuthException("namespaceid is missing from the request");
            }
            var namespaceid = _contentElement.Descendants().First(p => p.Name.LocalName == "namespaceid").Value;
            NamespaceId = int.Parse(namespaceid);
        }
    }
}