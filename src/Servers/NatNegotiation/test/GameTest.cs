using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniSpy.Server.NatNegotiation.Aggregate.Redis;
using UniSpy.Server.Core.Abstraction.Interface;
using Xunit;

namespace UniSpy.Server.NatNegotiation.Test
{
    public class GameTest
    {
        [Fact]
        public void Anno1701_20230209()
        {
            new RedisClient().Db.Execute("FLUSHALL");
            var clients = new Dictionary<string, IClient>()
            {
                {"client1gp",MockObject.CreateClient("192.168.0.109",1111)},
                {"client1nn",MockObject.CreateClient("192.168.0.109",64900)},
                {"client2nn",MockObject.CreateClient("192.168.0.109",4901)},
            };
            // Given
            var gameClientInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameclient init
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x03,0x00,0x01,0xAC,0x1A,0x50,0x01,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x02,0x00,0x01,0xAC,0x1A,0x50,0x01,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x01,0x00,0x01,0xAC,0x1A,0x50,0x01,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00})
            };
            var gameServerInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameserver init
                new KeyValuePair<string, byte[]>("client1gp",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x03,0x01,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x02,0x01,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x00,0x01,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x50,0x7A,0x01,0x01,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
            };
            foreach (var request in gameServerInit)
            {
                Task.Run(() => ((ITestClient)clients[request.Key]).TestReceived(request.Value));
            }
            foreach (var request in gameClientInit)
            {
                Task.Run(() => ((ITestClient)clients[request.Key]).TestReceived(request.Value));
            }
            Thread.Sleep(5000);
            // Then
        }
        [Fact]
        public void Anno1701_20230127()
        {
            // Given
            var req1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x06, 0x00, 0x00, 0x35, 0x7A, 0xD4, 0x00, 0xA7, 0x08, 0xC0, 0xFC, 0xA7, 0x08, 0x00 };
            var req2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x06, 0x00, 0x00, 0x35, 0x7A, 0x99, 0x01, 0x78, 0x6D, 0xF0, 0x53, 0xE8, 0x13, 0x5D };
            // When
            var client = (ITestClient)MockObject.CreateClient("192.168.0.109", 64900);
            client.TestReceived(req1);
            client.TestReceived(req2);
            // Then
        }
        [Fact]
        /// <summary>
        /// natneg success
        /// </summary>
        public void GameSpySDK20221116()
        {
            new RedisClient().Db.Execute("FLUSHDB");
            var clients = new Dictionary<string, IClient>()
            {
                {"client1gp",MockObject.CreateClient("192.168.0.109",1111)},
                {"client1nn",MockObject.CreateClient("192.168.0.109",64900)},
                {"client2nn",MockObject.CreateClient("192.168.0.109",4901)},
            };
            var gameServerInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameserver init
                new KeyValuePair<string, byte[]>("client1gp",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x00,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x01,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x02,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x03,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
            };
            var gameClientInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameclient init
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x01,0x00,0x00,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x02,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x03,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00})
            };
            var gameServerReport = new KeyValuePair<string, byte[]>("client1gp", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA6, 0x38, 0xF1, 0x2B, 0xCC, 0x00, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var gameClientReport = new KeyValuePair<string, byte[]>("client2nn", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA6, 0x38, 0xF1, 0x2B, 0xCC, 0x00, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            foreach (var request in gameServerInit)
            {
                ((ITestClient)clients[request.Key]).TestReceived(request.Value);
            }
            foreach (var request in gameClientInit)
            {
                ((ITestClient)clients[request.Key]).TestReceived(request.Value);
            }
            Thread.Sleep(5000);

            Task.Run(() => ((ITestClient)clients[gameServerReport.Key]).TestReceived(gameServerReport.Value));
            Task.Run(() => ((ITestClient)clients[gameClientReport.Key]).TestReceived(gameClientReport.Value));

            Thread.Sleep(5000);

        }
        [Fact]
        /// <summary>
        /// natneg fail with retry, test the strategy change
        /// </summary>
        public void NatPunchStrategy20221117()
        {
            new RedisClient().Db.Execute("FLUSHDB");
            var clients1 = new Dictionary<string, IClient>()
            {
                {"client1gp",MockObject.CreateClient("192.168.0.109",1111)},
                {"client1nn",MockObject.CreateClient("192.168.0.109",64900)},
                {"client2nn",MockObject.CreateClient("192.168.0.109",4901)},
            };
            var gameServerInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameserver init
                new KeyValuePair<string, byte[]>("client1gp",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x00,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x01,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x02,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x03,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
            };
            var gameClientInit = new List<KeyValuePair<string, byte[]>>()
            {
                //gameclient init
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x01,0x00,0x00,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x02,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA6,0x38,0xF1,0x2B,0x03,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00})
            };
            // natneg fail
            var gameServerReport = new KeyValuePair<string, byte[]>("client1gp", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA6, 0x38, 0xF1, 0x2B, 0xCC, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var gameClientReport = new KeyValuePair<string, byte[]>("client2nn", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA6, 0x38, 0xF1, 0x2B, 0xCC, 0x01, 0x00, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            foreach (var request in gameServerInit)
            {
                ((ITestClient)clients1[request.Key]).TestReceived(request.Value);
            }
            foreach (var request in gameClientInit)
            {
                ((ITestClient)clients1[request.Key]).TestReceived(request.Value);
            }
            Thread.Sleep(5000);

            Task.Run(() => ((ITestClient)clients1[gameServerReport.Key]).TestReceived(gameServerReport.Value));
            Task.Run(() => ((ITestClient)clients1[gameClientReport.Key]).TestReceived(gameClientReport.Value));

            Thread.Sleep(5000);

            var clients2 = new Dictionary<string, IClient>()
            {
                {"client1gp",MockObject.CreateClient("192.168.0.109",1111)},
                {"client1nn",MockObject.CreateClient("192.168.0.109",64902)},
                {"client2nn",MockObject.CreateClient("192.168.0.109",4902)},
            };
            // new negotiation
            var gameServerInit2 = new List<KeyValuePair<string, byte[]>>()
            {
                //gameserver init
                new KeyValuePair<string, byte[]>("client1gp",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x00,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x01,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x02,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client1nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x03,0x01,0x01,0xC0,0xA8,0x00,0x6D,0x2B,0x67,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
            };
            var gameClientInit2 = new List<KeyValuePair<string, byte[]>>()
            {
                //gameclient init
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x01,0x00,0x00,0xC0,0xA8,0x00,0x6D,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x02,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00}),
                new KeyValuePair<string, byte[]>("client2nn",new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0xA7,0x38,0xF1,0x2B,0x03,0x00,0x00,0xC0,0xA8,0x00,0x6D,0xFD,0x84,0x67,0x6D,0x74,0x65,0x73,0x74,0x00})
            };
            // natneg success
            var gameServerReport2 = new KeyValuePair<string, byte[]>("client1gp", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA7, 0x38, 0xF1, 0x2B, 0xCC, 0x00, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            var gameClientReport2 = new KeyValuePair<string, byte[]>("client2nn", new byte[] {
                0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x0D, 0xA7, 0x38, 0xF1, 0x2B, 0xCC, 0x01, 0x01, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            foreach (var request in gameServerInit2)
            {
                ((ITestClient)clients2[request.Key]).TestReceived(request.Value);
            }
            foreach (var request in gameClientInit2)
            {
                ((ITestClient)clients2[request.Key]).TestReceived(request.Value);
            }
            Thread.Sleep(5000);

            Task.Run(() => ((ITestClient)clients2[gameServerReport2.Key]).TestReceived(gameServerReport2.Value));
            Task.Run(() => ((ITestClient)clients2[gameClientReport2.Key]).TestReceived(gameClientReport2.Value));

            Thread.Sleep(5000);

        }
        [Fact]
        public void NatFullConeTest()
        {
            // clean all stuff in database
            new RedisClient().Db.Execute("FLUSHDB");

            var client = MockObject.CreateClient("192.168.1.2", 9999);
            var server = MockObject.CreateClient("192.168.1.3", 9999);
            var clientInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };

            var serverInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientRequests = new List<byte[]> { clientInitGP, clientInitNN1, clientInitNN2, clientInitNN3 };
            var serverRequests = new List<byte[]> { serverInitGP, serverInitNN1, serverInitNN2, serverInitNN3 };


            foreach (var request in clientRequests)
            {
                ((ITestClient)client).TestReceived(request);
            }
            foreach (var request in serverRequests)
            {
                ((ITestClient)server).TestReceived(request);
            }
            // because the process is running in background we need to wait it finish, so we can debug
            // Thread.Sleep(10000);
            // Console.Read();
        }

        [Fact]
        public void NatConePortIncrement()
        {
            // clean all stuff in database
            new RedisClient().Db.Execute("FLUSHDB");

            var client = MockObject.CreateClient("192.168.1.2", 53935);
            var server = MockObject.CreateClient("192.168.1.3", 53935);

            var clientInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x00, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xBB, 0x37, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };

            var serverInitGP = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x00, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN1 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x01, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0x00, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN2 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x02, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var serverInitNN3 = new byte[] { 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x03, 0x00, 0x00, 0x00, 0x02, 0x9A, 0x03, 0x01, 0x01, 0x7F, 0x00, 0x01, 0x01, 0xD2, 0xAE, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00 };
            var clientRequests = new List<byte[]> { clientInitGP, clientInitNN1, clientInitNN2, clientInitNN3 };
            var serverRequests = new List<byte[]> { serverInitGP, serverInitNN1, serverInitNN2, serverInitNN3 };

            foreach (var request in clientRequests)
            {
                ((ITestClient)client).TestReceived(request);
            }
            foreach (var request in serverRequests)
            {
                ((ITestClient)server).TestReceived(request);
            }
            // because the process is running in background we need to wait it finish, so we can debug
            // Thread.Sleep(10000);
        }

        [Fact]
        public void Anno1701()
        {
            var client1GP = MockObject.CreateClient("31.18.120.193", 21701);
            var client1NN1 = MockObject.CreateClient("31.18.120.193", 51463);
            var client1NN2 = MockObject.CreateClient("31.18.120.193", 51463);
            var client1NN3 = MockObject.CreateClient("31.18.120.193", 51463);

            var client2GP = MockObject.CreateClient("79.209.224.29", 21701);
            var client2NN1 = MockObject.CreateClient("79.209.224.29", 51463);
            var client2NN2 = MockObject.CreateClient("79.209.224.29", 51463);
            var client2NN3 = MockObject.CreateClient("79.209.224.29", 51463);


            var client3GP = MockObject.CreateClient("79.209.224.29", 1024);
            var client3NN1 = MockObject.CreateClient("79.209.224.29", 55111);
            var client3NN2 = MockObject.CreateClient("79.209.224.29", 55111);
            var client3NN3 = MockObject.CreateClient("79.209.224.29", 55111);

            var requests = new List<KeyValuePair<IClient, byte[]>>()
            {
                new KeyValuePair<IClient, byte[]>(client1GP,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x00,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client1NN1,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x01,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client1NN2,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x02,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client1NN3,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x03,0x00,0x01,0xC0,0xA8,0x00,0xD5,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),

                new KeyValuePair<IClient, byte[]>(client2GP,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x00,0x01,0x01,0xC0,0xA8,0x00,0x32,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client2NN1,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x01,0x01,0x01,0xC0,0xA8,0x00,0x32,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client2NN2,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x02,0x01,0x01,0xC0,0xA8,0x00,0x32,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
                new KeyValuePair<IClient, byte[]>(client2NN3,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x03,0x00,0x00,0x00,0x1D,0x76,0x03,0x01,0x01,0xC0,0xA8,0x00,0x32,0x54,0xC5,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00}),
            };

            foreach (var req in requests)
            {
                ((ITestClient)req.Key).TestReceived(req.Value);
            }
            // because the process is running in background we need to wait it finish, so we can debug
            Thread.Sleep(10000);
        }

        [Fact]
        public void Flatout2pc20230519()
        {
            new RedisClient().FlushDb();
            var client1GP = MockObject.CreateClient("91.43.63.201", 23756);
            var client1NN1 = MockObject.CreateClient("91.43.63.201", 64871);
            var client1NN2 = MockObject.CreateClient("91.43.63.201", 64871);

            var client2GP = MockObject.CreateClient("91.43.63.201", 1025);
            var client2NN1 = MockObject.CreateClient("91.43.63.201", 41159);
            var client2NN2 = MockObject.CreateClient("91.43.63.201", 41159);
            var requests = new List<KeyValuePair<IClient, byte[]>>()
            {
                new KeyValuePair<IClient, byte[]>(client1GP,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x00,0x00,0x01,0xC0,0xA8,0x00,0x71,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00}),
                new KeyValuePair<IClient, byte[]>(client1NN1,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x01,0x00,0x01,0xC0,0xA8,0x00,0x71,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00}),
                new KeyValuePair<IClient, byte[]>(client1NN2,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x02,0x00,0x01,0xC0,0xA8,0x00,0x71,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00}),
                new KeyValuePair<IClient, byte[]>(client2GP,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x00,0x01,0x01,0xC0,0xA8,0x00,0x3C,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00}),
                new KeyValuePair<IClient, byte[]>(client2NN1,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x01,0x01,0x01,0xC0,0xA8,0x00,0x3C,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00}),
                new KeyValuePair<IClient, byte[]>(client2NN1,new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0xC8,0xDF,0x69,0x75,0x02,0x01,0x01,0xC0,0xA8,0x00,0x3C,0x00,0x00,0x66,0x6C,0x61,0x74,0x6F,0x75,0x74,0x32,0x70,0x63,0x00})
            };

            foreach (var req in requests)
            {
                (req.Key as ITestClient).TestReceived(req.Value);
            }
            Thread.Sleep(10000);
        }
        [Fact]
        /// <summary>
        /// NN1 packet's private port can not equal to NN2's port
        /// </summary>
        public void Gautletps2Test20230620()
        {
            var client1 = MockObject.CreateClient("192.168.9.13", 57430);
            var client2 = MockObject.CreateClient("192.168.9.12", 59624);

            var requests = new List<KeyValuePair<ITestClient, byte[]>>()
            {
                new KeyValuePair<ITestClient, byte[]>(client1,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x02,0x00,0x01,0xC0,0xA8,0x09,0x0D,0x08,0x0C,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00}),
                new KeyValuePair<ITestClient, byte[]>(client1,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x00,0x00,0x01,0xC0,0xA8,0x09,0x0D,0x00,0x00,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00}),
                new KeyValuePair<ITestClient, byte[]>(client1,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x01,0x00,0x01,0xC0,0xA8,0x09,0x0D,0x00,0x00,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00}),
                new KeyValuePair<ITestClient, byte[]>(client2,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x00,0x01,0x01,0xC0,0xA8,0x09,0x0C,0x00,0x00,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00}),
                new KeyValuePair<ITestClient, byte[]>(client2,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x01,0x01,0x01,0xC0,0xA8,0x09,0x0C,0x00,0x00,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00}),
                new KeyValuePair<ITestClient, byte[]>(client2,new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x02,0x00,0x0B,0x90,0x1C,0x24,0x02,0x01,0x01,0xC0,0xA8,0x09,0x0C,0x08,0x0C,0x67,0x61,0x75,0x6E,0x74,0x6C,0x65,0x74,0x70,0x73,0x32,0x00})
            };

            foreach (var item in requests)
            {
                item.Key.TestReceived(item.Value);
            }
        }
    }
}