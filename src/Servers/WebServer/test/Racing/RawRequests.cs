namespace UniSpy.Server.WebServer.Test.Racing
{
    public class RawRequests
    {
        public const string GetContestData =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:GetContestData>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:courseid>0</ns1:courseid>
                    </ns1:GetContestData>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string GetFriendRankings =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:GetFriendRankings>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:courseid>0</ns1:courseid>
                        <ns1:profileid>0</ns1:profileid>
                    </ns1:GetFriendRankings>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string GetRegionalData =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:GetRegionalData>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                    </ns1:GetRegionalData>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string GetTenAboveRankings =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:GetTenAboveRankings>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:courseid>0</ns1:courseid>
                        <ns1:profileid>0</ns1:profileid>
                    </ns1:GetTenAboveRankings>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string GetTopTenRankings =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:GetTopTenRankings>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:courseid>0</ns1:courseid>
                    </ns1:GetTopTenRankings>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string SubmitGhost =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:SubmitGhost>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:courseid>0</ns1:courseid>
                        <ns1:profileid>0</ns1:profileid>
                        <ns1:score>XXXXXX</ns1:score>
                        <ns1:fileid>0</ns1:fileid>
                    </ns1:SubmitGhost>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";

        public const string SubmitScores =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""
                xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/""
                xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                xmlns:ns1=""http://gamespy.net/sake"">
                <SOAP-ENV:Body>
                    <ns1:SubmitScores>
                        <ns1:gamedata>0</ns1:gamedata>
                        <ns1:regionid>0</ns1:regionid>
                        <ns1:profileid>0</ns1:profileid>
                        <ns1:gameid>0</ns1:gameid>
                        <ns1:scoremode>0</ns1:scoremode>
                        <ns1:scoredatas>XXXXXX</ns1:scoredatas>
                    </ns1:SubmitScores>
                </SOAP-ENV:Body>
            </SOAP-ENV:Envelope>";
    }
}
