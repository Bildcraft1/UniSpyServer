using System.Collections.Generic;
using System.Linq;
using System.Net;
using Moq;
using UniSpy.Server.Chat.Application;
using UniSpy.Server.Chat.Handler;
using UniSpy.Server.Chat.Handler.CmdHandler.Channel;
using UniSpy.Server.Core.Abstraction.Interface;
using UniSpy.Server.Core.Encryption;
using Xunit;

namespace UniSpy.Server.Chat.Test
{
    public class GameTest
    {
        private Client _client;
        public GameTest()
        {
            var serverMock = new Mock<IServer>();
            serverMock.Setup(s => s.ServerID).Returns(new System.Guid());
            serverMock.Setup(s => s.ServerName).Returns("Chat");
            serverMock.Setup(s => s.ListeningIPEndPoint).Returns(new IPEndPoint(IPAddress.Any, 6666));
            var connectionMock = new Mock<ITcpConnection>();
            connectionMock.Setup(s => s.RemoteIPEndPoint).Returns(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            connectionMock.Setup(s => s.Server).Returns(serverMock.Object);
            connectionMock.Setup(s => s.ConnectionType).Returns(NetworkConnectionType.Tcp);
            _client = new Client(connectionMock.Object);
        }

        [Fact]
        public void Civilization4()
        {
            var rawRequests = new List<string>(){
                "CRYPT des 1 anno1701",
                "USRIP",
                "USER X419pGl4sX|18 127.0.0.1 peerchat.gamespy.com :aa3041ada9385b28fc4d4e47db288769",
                "NICK a1701-5",
                "CDKEY 81123-67814-77652-27631-11723-47707-22638-10701",
                "JOIN #GSP!anno1701 ","MODE #GSP!anno1701",
                @"GETCKEY #GSP!anno1701 * 008 0 :\b_flags","WHO a1701-5",
                "JOIN #GSP!anno1701!M9zK0KJaKM ",
                "MODE #GSP!anno1701!M9zK0KJaKM",
                @"SETCKEY #GSP!anno1701 a1701-5 :\b_flags\s",
                @"SETCKEY #GSP!anno1701!M9zK0KJaKM a1701-5 :\b_flags\sh",
                @"GETCKEY #GSP!anno1701!M9zK0KJaKM * 009 0 :\b_flags",
                "TOPIC #GSP!anno1701!M9zK0KJaKM :test",
                "MODE #GSP!anno1701!M9zK0KJaKM +l 4",
                "MODE #GSP!anno1701!M9zK0KJaKM -i-p-s+m+n+t+l+e 4",
                "PART #GSP!anno1701 :"
            };

            foreach (var raw in rawRequests)
            {
                new CmdSwitcher(_client, UniSpyEncoding.GetBytes(raw)).Switch();
            }
        }
        [Fact]
        public void TcpMessageSplitingTest()
        {
            var raws = new List<byte[]>(){
                UniSpyEncoding.GetBytes("GETCKEY"),
                UniSpyEncoding.GetBytes(" world"),
                UniSpyEncoding.GetBytes(" hi").Concat(new byte[]{0x0D,0x0A}).ToArray(),
            };
            foreach (var raw in raws)
            {
                ((ITestClient)_client).TestReceived(raw);
            }
            // Given

            // When

            // Then
        }
        [Fact]
        public void Worms3dTest()
        {
            var client1 = TestClasses.CreateClient("107.244.81.1", 8888);
            var client2 = TestClasses.CreateClient("91.34.72.1", 8889);
            var request1 = new List<string>()
            {
                // "CRYPT des 1 worms3\r\n",
                "USRIP\r\n",
                "USER X419pGl4sX|6 127.0.0.1 peerchat.gamespy.com :aa3041ada9385b28fc4d4e47db288769\r\n",
                "NICK worms10\r\n",
                "JOIN #GPG!622\r\n",
                "MODE #GPG!622\r\n",
                @"GETCKEY #GPG!622 * 024 0 :\username\b_flags"+"\r\n",
                "JOIN #GSP!worms3!Ml4lz344lM\r\n",
                "MODE #GSP!worms3!Ml4lz344lM\r\n",
                @"SETCKEY #GPG!622 worms10 :\b_flags\s"+"\r\n",
                @"SETCKEY #GSP!worms3!Ml4lz344lM worms10 :\b_flags\sh"+"\r\n",
                @"GETCKEY #GSP!worms3!Ml4lz344lM * 025 0 :\username\b_flags"+"\r\n",
                "TOPIC #GSP!worms3!Ml4lz344lM :tesr\r\n",
                "MODE #GSP!worms3!Ml4lz344lM +l 2\r\n",
                "PART #GPG!622 :Joined staging room\r\n",
                @"SETCKEY #GSP!worms3!Ml4lz344lM worms10 :\b_firewall\1\b_profileid\6\b_ipaddress\\b_publicip\255.255.255.255\b_privateip\192.168.0.60\b_authresponse\\b_gamever\1073\b_val\0"+"\r\n",
                "WHO worms10\r\n",
                @"SETCHANKEY #GSP!worms3!Ml4lz344lM :\b_hostname\test\b_hostport\\b_MaxPlayers\2\b_NumPlayers\1\b_SchemeChanging\0\b_gamever\1073\b_gametype\\b_mapname\Random\b_firewall\1\b_publicip\255.255.255.255\b_privateip\192.168.0.60\b_gamemode\openstaging\b_val\0\b_password\1"+"\r\n",
                @"GETKEY worms20 026 0 :\b_firewall\b_profileid\b_ipaddress\b_publicip\b_privateip\b_authresponse\b_gamever\b_val"+"\r\n",
                @"GETCKEY #GSP!worms3!Ml4lz344lM worms20 027 0 :\b_firewall\b_profileid\b_ipaddress\b_publicip\b_privateip\b_authresponse\b_gamever\b_val"+"\r\n",
                @"SETCHANKEY #GSP!worms3!Ml4lz344lM :\b_hostname\test\b_hostport\\b_MaxPlayers\2\b_NumPlayers\1\b_SchemeChanging\0\b_gamever\1073\b_gametype\\b_mapname\Random\b_firewall\1\b_publicip\255.255.255.255\b_privateip\192.168.0.60\b_gamemode\openstaging\b_val\0\b_password\1"+"\r\n",
                @"SETCHANKEY #GSP!worms3!Ml4lz344lM :\b_hostname\test\b_hostport\\b_MaxPlayers\2\b_NumPlayers\1\b_SchemeChanging\0\b_gamever\1073\b_gametype\\b_mapname\Random\b_firewall\1\b_publicip\255.255.255.255\b_privateip\192.168.0.60\b_gamemode\openstaging\b_val\0\b_password\1"+"\r\n",
                "UTM #GSP!worms3!Ml4lz344lM :MDM |Obj|3|Land.Time|0|LogicalSeed|3891226431|GraphicalSeed|3269271590|Land.RealSeed|3281489942|Land.Theme|Pirate.Lumps|LevelToUse|FE.Level.RandomLand|Land.Ind|0|Wormpot.Reel1|17|Wormpot.Reel2|17|Wormpot.Reel3|17|TimeStamp|6206364",
                "UTM #GSP!worms3!Ml4lz344lM :TDM aA",
                "UTM #GSP!worms3!Ml4lz344lM :SDM ASFE.Scheme.StandardCUnAACADCBBCACBBFFBKBB8C/C3C!A!A*C*C<D*B*B*B*B*B*B3C*B<A*C3CEC!A-C5C-C3C<C*A*B*B*C*B<CEC*B*C<B<D!A*B*B3B3C<D!A<D/C<C<D*C*A",
                "UTM worms20 :APE [01]privateip[02]192.168.0.60[01]publicip[02]255.255.255.255"
            };
            var request2 = new List<string>()
            {
                // "CRYPT des 1 worms3",
                "USRIP\r\n",
                "USER X419pGl4sX|7 127.0.0.1 peerchat.gamespy.com :5bb4f409fae8bc5aa1595cb6d5168a1c\r\n",
                "NICK worms20\r\n",
                "JOIN #GPG!622\r\n" ,
                "MODE #GPG!622\r\n",
                @"GETCKEY #GPG!622 * 008 0 :\username\b_flags"+"\r\n",
                "JOIN #GSP!worms3!Ml4lz344lM\r\n" ,
                "MODE #GSP!worms3!Ml4lz344lM\r\n",
                @"SETCKEY #GPG!622 worms20 :\b_flags\s"+"\r\n",
                @"SETCKEY #GSP!worms3!Ml4lz344lM worms20 :\b_flags\s"+"\r\n",
                @"GETCKEY #GSP!worms3!Ml4lz344lM * 009 0 :\username\b_flags"+"\r\n",
                "PART #GPG!622 :Joined staging room"+"\r\n",
                "UTM #GSP!worms3!Ml4lz344lM :APE [01]privateip[02]192.168.0.141[01]publicip[02]255.255.255.255\r\n",
                "WHO worms10"+"\r\n",
                @"GETCKEY #GSP!worms3!Ml4lz344lM worms10 010 0 :\b_firewall\b_profileid\b_ipaddress\b_publicip\b_privateip\b_authresponse\b_gamever\b_val",
                "WHO worms20"+"\r\n"
            };
            // first process client2, then client1 will get client2 in channel
            foreach (var raw in request1)
            {
                ((ITestClient)TestClasses.Client1).TestReceived(UniSpyEncoding.GetBytes(raw));
            }
            foreach (var raw in request2)
            {
                ((ITestClient)TestClasses.Client2).TestReceived(UniSpyEncoding.GetBytes(raw));
            }
        }
        [Fact]
        /// <summary>
        /// !! the channel name is wrong because the ip get from somewhere cause the problem
        /// client1 #GSP!worms3!MJ0NJ4c3aM
        /// client2 #GSP!worms3!MJ0NJ4c3aM
        /// </summary>
        public void Worm3dTest20220613()
        {

            var request1 = new List<string>()
            {
                // "CRYPT des 1 worms3\r\n",
                "USRIP\r\n",
                "USER X419pGl4sX|7 127.0.0.1 peerchat.gamespy.com :9964fa3fe73f6a1fbb986d2a04b1eb65\r\n",
                "NICK worms10\r\n",
                "JOIN #GPG!622\r\n",
                "MODE #GPG!622\r\n",
                @"GETCKEY #GPG!622 * 000 0 :\username\b_flags"+"\r\n",
                "PRIVMSG #GPG!622 :hello\r\n",
                "JOIN #GSP!worms3!MJ0NJ4c3aM\r\n",
                "MODE #GSP!worms3!MJ0NJ4c3aM\r\n",
                @"SETCKEY #GPG!622 worms10 :\b_flags\s\r\n",
                @"SETCKEY #GSP!worms3!MJ0NJ4c3aM worms10 :\b_flags\sh"+"\r\n",
                @"GETCKEY #GSP!worms3!MJ0NJ4c3aM * 001 0 :\username\b_flags"+"\r\n",
                "TOPIC #GSP!worms3!MJ0NJ4c3aM :main lobby message test\r\n",
                "MODE #GSP!worms3!MJ0NJ4c3aM +l 2"+"\r\n",
                // "PART #GPG!622 :Joined staging room\r\n",
                @"SETCKEY #GSP!worms3!MJ0NJ4c3aM worms10 :\b_firewall\1\b_profileid\7\b_ipaddress\\b_publicip\255.255.255.255\b_privateip\192.168.0.145\b_authresponse\\b_gamever\1073\b_val\0\r\n",
                "WHO worms10"+"\r\n",
                @"SETCHANKEY #GSP!worms3!MJ0NJ4c3aM :\b_hostname\\b_hostport\\b_MaxPlayers\2\b_NumPlayers\0\b_SchemeChanging\0\b_gamever\1073\b_gametype\\b_mapname\Random\b_firewall\1\b_publicip\255.255.255.255\b_privateip\192.168.0.145\b_gamemode\openstaging\b_val\0\b_password\1"+"\r\n",
                "PRIVMSG #GSP!worms3!MJ0NJ4c3aM :staging lobby message test"+"\r\n",
                @"UTM #GSP!worms3!MJ0NJ4c3aM :ATS \nick\worms10\msg\2046\|name|Human Team 1|w0|Dave|w1|Patrick|w2|The Morriss|w3|Mike|w4|Tony|w5|Gluckman|skill|0|grave|0|SWeapon|0|flag|71|speech|Classic|InGame|0|player|worms10|alliance|1|wormCount|77111416"+"\r\n",
                @"UTM #GSP!worms3!MJ0NJ4c3aM :RTS \team\Human Team 1\nick\worms10\msg\2174"+"\r\n"
            };

            var request2 = new List<string>()
            {
                // "CRYPT des 1 worms3\r\n",
                "USRIP\r\n",
                "USER X419pGl4sX|6 127.0.0.1 peerchat.gamespy.com :dd6283e1e349806c20991020e0d6897a\r\n",
                "NICK xiaojiuwo\r\n",
                "JOIN #GPG!622\r\n" ,
                "MODE #GPG!622\r\n",
                @"GETCKEY #GPG!622 * 000 0 :\username\b_flags"+"\r\n",
                "PING\r\n",
                "PRIVMSG #GPG!622 :hello from the other side\r\n",
                "JOIN #GSP!worms3!MJ0NJ4c3aM\r\n",
                "MODE #GSP!worms3!MJ0NJ4c3aM\r\n",
                @"SETCKEY #GPG!622 xiaojiuwo :\b_flags\s"+"\r\n",
                @"SETCKEY #GSP!worms3!MJ0NJ4c3aM xiaojiuwo :\b_flags\s"+"\r\n",
                @"GETCKEY #GSP!worms3!MJ0NJ4c3aM * 001 0 :\username\b_flags"+"\r\n",
                // "PART #GPG!622 :Joined staging room\r\n",
                "UTM #GSP!worms3!MJ0NJ4c3aM :APE [01]privateip[02]192.168.0.109[01]publicip[02]255.255.255.255\r\n",
                "WHO xiaojiuwo\r\n",
                // "PART #GSP!worms3!MJ0NJ4c3aM :Left Game\r\n"
            };
            var client1 = TestClasses.CreateClient();
            var client2 = TestClasses.CreateClient();

            foreach (var raw in request1)
            {
                ((ITestClient)client1).TestReceived(UniSpyEncoding.GetBytes(raw));
            }
            foreach (var raw in request2)
            {
                ((ITestClient)client2).TestReceived(UniSpyEncoding.GetBytes(raw));
            }
            var userCount = JoinHandler.Channels.First().Value.Users.Count;
            Assert.Equal(2, userCount);
            int count1 = ((Client)client1).Info.JoinedChannels.Count();
            Assert.Equal(2, count1);
            int count2 = ((Client)client2).Info.JoinedChannels.Count();
            Assert.Equal(2, count2);

        }

        [Fact]
        public void ConflictGlobalStorm()
        {
            var request = new List<string>()
            {
                // "CRYPT des 1 conflictsopc\r\n",
                ":s 705 * WKNYSMENXOZQHYVS MIYLETFVYDMBDFFV\r\n",
                "LOGIN 1 cgs1 149815eb972b3c370dee3b89d645ae14\r\n"
            };
            foreach (var raw in request)
            {
                ((ITestClient)TestClasses.Client2).TestReceived(UniSpyEncoding.GetBytes(raw));
            }
        }
    }
}