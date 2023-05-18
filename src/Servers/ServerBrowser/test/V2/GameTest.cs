using System.Collections.Generic;
using UniSpy.Server.Core.Abstraction.Interface;
using Xunit;

namespace UniSpy.Server.ServerBrowser.V2.Test
{
    public class GameTest
    {
        [Fact]
        public void Gmtest20200309()
        {
            var qrReq = new byte[] { 0x03, 0xEA, 0x2B, 0xAF, 0x50, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x69, 0x70, 0x30, 0x00, 0x31, 0x39, 0x32, 0x2E, 0x31, 0x36, 0x38, 0x2E, 0x31, 0x32, 0x32, 0x2E, 0x32, 0x32, 0x36, 0x00, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x70, 0x6F, 0x72, 0x74, 0x00, 0x31, 0x31, 0x31, 0x31, 0x31, 0x00, 0x6E, 0x61, 0x74, 0x6E, 0x65, 0x67, 0x00, 0x31, 0x00, 0x73, 0x74, 0x61, 0x74, 0x65, 0x63, 0x68, 0x61, 0x6E, 0x67, 0x65, 0x64, 0x00, 0x33, 0x00, 0x67, 0x61, 0x6D, 0x65, 0x6E, 0x61, 0x6D, 0x65, 0x00, 0x67, 0x6D, 0x74, 0x65, 0x73, 0x74, 0x00, 0x68, 0x6F, 0x73, 0x74, 0x6E, 0x61, 0x6D, 0x65, 0x00, 0x47, 0x61, 0x6D, 0x65, 0x53, 0x70, 0x79, 0x20, 0x51, 0x52, 0x32, 0x20, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x00, 0x67, 0x61, 0x6D, 0x65, 0x76, 0x65, 0x72, 0x00, 0x32, 0x2E, 0x30, 0x30, 0x00, 0x68, 0x6F, 0x73, 0x74, 0x70, 0x6F, 0x72, 0x74, 0x00, 0x32, 0x35, 0x30, 0x30, 0x30, 0x00, 0x6D, 0x61, 0x70, 0x6E, 0x61, 0x6D, 0x65, 0x00, 0x67, 0x6D, 0x74, 0x6D, 0x61, 0x70, 0x31, 0x00, 0x67, 0x61, 0x6D, 0x65, 0x74, 0x79, 0x70, 0x65, 0x00, 0x61, 0x72, 0x65, 0x6E, 0x61, 0x00, 0x6E, 0x75, 0x6D, 0x70, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x73, 0x00, 0x31, 0x30, 0x00, 0x6E, 0x75, 0x6D, 0x74, 0x65, 0x61, 0x6D, 0x73, 0x00, 0x32, 0x00, 0x6D, 0x61, 0x78, 0x70, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x73, 0x00, 0x33, 0x32, 0x00, 0x67, 0x61, 0x6D, 0x65, 0x6D, 0x6F, 0x64, 0x65, 0x00, 0x6F, 0x70, 0x65, 0x6E, 0x70, 0x6C, 0x61, 0x79, 0x69, 0x6E, 0x67, 0x00, 0x74, 0x65, 0x61, 0x6D, 0x70, 0x6C, 0x61, 0x79, 0x00, 0x31, 0x00, 0x66, 0x72, 0x61, 0x67, 0x6C, 0x69, 0x6D, 0x69, 0x74, 0x00, 0x30, 0x00, 0x74, 0x69, 0x6D, 0x65, 0x6C, 0x69, 0x6D, 0x69, 0x74, 0x00, 0x34, 0x30, 0x00, 0x67, 0x72, 0x61, 0x76, 0x69, 0x74, 0x79, 0x00, 0x38, 0x30, 0x30, 0x00, 0x72, 0x61, 0x6E, 0x6B, 0x69, 0x6E, 0x67, 0x6F, 0x6E, 0x00, 0x31, 0x00, 0x00, 0x00, 0x0A, 0x70, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x5F, 0x00, 0x73, 0x63, 0x6F, 0x72, 0x65, 0x5F, 0x00, 0x64, 0x65, 0x61, 0x74, 0x68, 0x73, 0x5F, 0x00, 0x70, 0x69, 0x6E, 0x67, 0x5F, 0x00, 0x74, 0x65, 0x61, 0x6D, 0x5F, 0x00, 0x74, 0x69, 0x6D, 0x65, 0x5F, 0x00, 0x00, 0x4A, 0x6F, 0x65, 0x20, 0x50, 0x6C, 0x61, 0x79, 0x65, 0x72, 0x00, 0x34, 0x00, 0x32, 0x00, 0x37, 0x37, 0x00, 0x30, 0x00, 0x31, 0x38, 0x35, 0x00, 0x4C, 0x33, 0x33, 0x74, 0x20, 0x30, 0x6E, 0x33, 0x00, 0x36, 0x00, 0x32, 0x34, 0x00, 0x36, 0x38, 0x00, 0x31, 0x00, 0x38, 0x32, 0x30, 0x00, 0x52, 0x61, 0x70, 0x74, 0x6F, 0x72, 0x00, 0x31, 0x30, 0x00, 0x32, 0x39, 0x00, 0x32, 0x31, 0x36, 0x00, 0x31, 0x00, 0x36, 0x36, 0x34, 0x00, 0x47, 0x72, 0x38, 0x31, 0x00, 0x38, 0x00, 0x36, 0x00, 0x33, 0x32, 0x37, 0x00, 0x31, 0x00, 0x36, 0x39, 0x37, 0x00, 0x46, 0x6C, 0x75, 0x62, 0x62, 0x65, 0x72, 0x00, 0x31, 0x35, 0x00, 0x32, 0x00, 0x31, 0x37, 0x39, 0x00, 0x30, 0x00, 0x34, 0x38, 0x00, 0x53, 0x61, 0x72, 0x67, 0x65, 0x00, 0x39, 0x00, 0x31, 0x32, 0x00, 0x33, 0x33, 0x37, 0x00, 0x30, 0x00, 0x32, 0x39, 0x36, 0x00, 0x56, 0x6F, 0x69, 0x64, 0x00, 0x32, 0x37, 0x00, 0x32, 0x39, 0x00, 0x34, 0x35, 0x00, 0x30, 0x00, 0x33, 0x35, 0x35, 0x00, 0x72, 0x75, 0x6E, 0x61, 0x77, 0x61, 0x79, 0x00, 0x32, 0x34, 0x00, 0x34, 0x00, 0x31, 0x39, 0x37, 0x00, 0x31, 0x00, 0x34, 0x32, 0x38, 0x00, 0x50, 0x68, 0x33, 0x61, 0x72, 0x00, 0x33, 0x30, 0x00, 0x33, 0x30, 0x00, 0x33, 0x33, 0x39, 0x00, 0x31, 0x00, 0x35, 0x32, 0x35, 0x00, 0x77, 0x68, 0x30, 0x30, 0x74, 0x00, 0x33, 0x31, 0x00, 0x32, 0x38, 0x00, 0x32, 0x36, 0x39, 0x00, 0x31, 0x00, 0x37, 0x37, 0x00, 0x00, 0x02, 0x74, 0x65, 0x61, 0x6D, 0x5F, 0x74, 0x00, 0x73, 0x63, 0x6F, 0x72, 0x65, 0x5F, 0x74, 0x00, 0x61, 0x76, 0x67, 0x70, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x00, 0x00, 0x52, 0x65, 0x64, 0x00, 0x34, 0x38, 0x37, 0x00, 0x33, 0x33, 0x36, 0x00, 0x42, 0x6C, 0x75, 0x65, 0x00, 0x38, 0x32, 0x00, 0x34, 0x35, 0x38, 0x00 };
            var sbReq = new byte[] { 0x00, 0x09, 0x01, 0xC0, 0xA8, 0x7A, 0xE2, 0x2B, 0x67 };
            var qrClient = QueryReport.V2.Test.MockObject.CreateClient("192.168.122.226", 11111);
            var sbClient = ServerBrowser.V2.Test.MockObject.CreateClient();

            (qrClient as ITestClient).TestReceived(qrReq);
            (sbClient as ITestClient).TestReceived(sbReq);

        }
        [Fact]
        public void SplitSendMessageTest()
        {
            // Given
            var splitedRequests = new List<byte[]>()
            {
                // send message request
                new byte[]{0x00,0x13,0x02,0x4F,0xD1,0xE0,0x1D,0x54,0xC5},
                // natneg message request
                new byte[]{0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x00,0x00,0x2C,0xFD}
            };
            var compeleteRequest = new byte[] { 0x00, 0x13, 0x02, 0x4F, 0xD1, 0xE0, 0x1D, 0x54, 0xC5, 0xFD, 0xFC, 0x1E, 0x66, 0x6A, 0xB2, 0x00, 0x00, 0x2C, 0xFD };
            // When
            foreach (var req in splitedRequests)
            {
                ((ITestClient)MockObject.SBClient).TestReceived(req);
            }
            ((ITestClient)MockObject.SBClient).TestReceived(compeleteRequest);
        }
        [Fact]
        public void Gmtest20220613()
        {
            // Given
            var qrRequests = new List<byte[]>()
            {
                new byte[]{0x09,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00},
                new byte[]{0x03,0xB4,0xA3,0xCC,0x80,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x31,0x30,0x39,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x36,0x35,0x30,0x30,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x30,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x33,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x4D,0x79,0x20,0x53,0x65,0x72,0x76,0x65,0x72,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x00,0x33,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x38,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x31,0x2E,0x30,0x31,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x50,0x65,0x65,0x72,0x50,0x6C,0x61,0x79,0x65,0x72,0x31,0x00,0x30,0x00,0x00,0x00,0x00},
                // new byte[]{0x08,0xB4,0xA3,0xCC,0x80},
                // new byte[] {0x03,0xB4,0xA3,0xCC,0x80,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x31,0x30,0x39,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x36,0x35,0x30,0x30,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x30,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x32,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x00}
            };
            var sb1Requests = new List<byte[]>()
            {
                new byte[]{0x00,0x5A,0x00,0x01,0x03,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x29,0x3E,0x7C,0x23,0x43,0x5D,0x68,0x49,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6D,0x61,0x78,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6E,0x75,0x6D,0x73,0x65,0x72,0x76,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x00,0x00,0x20}
            };
            var sb2Requests = new List<byte[]>()
            {
                new byte[]{0x00,0x5A,0x00,0x01,0x03,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x29,0x3E,0x7C,0x23,0x43,0x5D,0x68,0x49,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6D,0x61,0x78,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6E,0x75,0x6D,0x73,0x65,0x72,0x76,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x00,0x00,0x20},
                new byte[]{0x00,0x5A,0x00,0x01,0x03,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x24,0x68,0x41,0x78,0x23,0x39,0x59,0x70,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6D,0x61,0x78,0x77,0x61,0x69,0x74,0x69,0x6E,0x67,0x5C,0x6E,0x75,0x6D,0x73,0x65,0x72,0x76,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x00,0x00,0x20},
                new byte[]{0x00,0x5E,0x00,0x01,0x03,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x28,0x76,0x7D,0x30,0x33,0x74,0x35,0x52,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x3D,0x33,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x00,0x00,0x04},
                new byte[]{0x00,0x5E,0x00,0x01,0x03,0x00,0x00,0x00,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x67,0x6D,0x74,0x65,0x73,0x74,0x00,0x2D,0x42,0x4C,0x67,0x32,0x73,0x28,0x26,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x3D,0x33,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x00,0x00,0x04}
            };
            foreach (var qrReq in qrRequests)
            {
                ((ITestClient)MockObject.QRClient).TestReceived(qrReq);
            }

            foreach (var sbReq in sb2Requests)
            {
                ((ITestClient)MockObject.SBClient).TestReceived(sbReq);
            }
        }
        /// <summary>
        /// Fixed 20221108
        /// </summary>
        [Fact]
        public void Anno1701Date20220620()
        {
            // because when search on redis, redis require the server ip and port as key words,
            // the ip and port in qr should match when sb execute ServerInfoRequest
            // therefore, we create client based on IP 91.43.50.186:21701 to test qr and sb
            var qrClient = QueryReport.V2.Test.MockObject.CreateClient("91.43.50.186", 21701);
            var sbClient = MockObject.CreateClient();
            var qrRequests = new List<byte[]>()
            {   
                // avaliable check
                new byte[] {0x09,0x00,0x00,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00},
                // heart beat
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x33,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x33,0x37,0x37,0x35,0x36,0x33,0x30,0x37,0x36,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x33,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x33,0x37,0x37,0x35,0x36,0x33,0x30,0x37,0x36,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                new byte[] {0x03,0x1D,0x55,0xCC,0xCA,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x38,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x31,0x30,0x39,0x31,0x32,0x37,0x36,0x32,0x30,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x30,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x5F,0x32,0x32,0x30,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00},
                //client message
                new byte[] {0xFE,0xFD,0x03,0x1D,0x55,0xCC,0xCA,0x54,0x54,0x54,0x00,0x00,0x5B,0x2B,0x32,0xBA,0x00,0x00,0x00,0x00,0xC5,0x54,0x00,0x00},
                // keep alive
                new byte[] {0x08,0x1D,0x55,0xCC,0xCA}
            };

            var sbRequests = new List<byte[]>()
            {
                new byte[] {0x00,0x9A,0x00,0x01,0x03,0x8F,0x55,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x44,0x3A,0x40,0x6F,0x29,0x4F,0x6B,0x68,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x20,0x69,0x73,0x20,0x6E,0x75,0x6C,0x6C,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x5C,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x5C,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x5C,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x00,0x00,0x00,0x04},
                new byte[] {0x00,0x9A,0x00,0x01,0x03,0x8F,0x55,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x41,0x48,0x6C,0x3D,0x27,0x6C,0x68,0x49,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x20,0x69,0x73,0x20,0x6E,0x75,0x6C,0x6C,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x5C,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x5C,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x5C,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x00,0x00,0x00,0x04},
                new byte[] {0x00,0x9A,0x00,0x01,0x03,0x8F,0x55,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x54,0x73,0x46,0x68,0x48,0x6A,0x76,0x51,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x20,0x69,0x73,0x20,0x6E,0x75,0x6C,0x6C,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x5C,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x5C,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x5C,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x00,0x00,0x00,0x04},
                //! error occur in this request
                new byte[] {0x00,0x09,0x01,0x5B,0x2B,0x32,0xBA,0x54,0xC5},
                // natneg request
                new byte[] {0xFD,0xFC,0x1E,0x66,0x6A,0xB2,0x00,0x00,0x17,0x31}
            };

            foreach (var qrReq in qrRequests)
            {
                ((ITestClient)qrClient).TestReceived(qrReq);
            }

            foreach (var sbReq in sbRequests)
            {
                ((ITestClient)sbClient).TestReceived(sbReq);
            }
        }
        [Fact]
        public void Anno1701Date20221104()
        {
            var qrClient1 = QueryReport.V2.Test.MockObject.CreateClient("79.209.224.29", 21701);
            var qrClient2 = QueryReport.V2.Test.MockObject.CreateClient("31.18.120.193", 21701);
            var sbClient1 = ServerBrowser.V2.Test.MockObject.CreateClient("79.209.224.29", 45340);
            var sbclient2 = ServerBrowser.V2.Test.MockObject.CreateClient("31.18.120.193", 50587);
            var requests = new List<KeyValuePair<string, byte[]>>(){
                new KeyValuePair<string, byte[]>("qr1",new byte[] {0x09,0x00,0x00,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x0}),
                new KeyValuePair<string, byte[]>("qr1",new byte[] {0x03,0x98,0x92,0x25,0xA0,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x30,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x30,0x2E,0x35,0x30,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x69,0x70,0x31,0x00,0x31,0x39,0x32,0x2E,0x31,0x36,0x38,0x2E,0x31,0x32,0x32,0x2E,0x31,0x00,0x6C,0x6F,0x63,0x61,0x6C,0x70,0x6F,0x72,0x74,0x00,0x32,0x31,0x37,0x30,0x31,0x00,0x6E,0x61,0x74,0x6E,0x65,0x67,0x00,0x31,0x00,0x73,0x74,0x61,0x74,0x65,0x63,0x68,0x61,0x6E,0x67,0x65,0x64,0x00,0x33,0x00,0x67,0x61,0x6D,0x65,0x6E,0x61,0x6D,0x65,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x69,0x70,0x00,0x30,0x00,0x70,0x75,0x62,0x6C,0x69,0x63,0x70,0x6F,0x72,0x74,0x00,0x30,0x00,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x00,0x28,0x75,0x6E,0x6B,0x6E,0x6F,0x77,0x6E,0x20,0x67,0x61,0x6D,0x65,0x29,0x00,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x00,0x6F,0x70,0x65,0x6E,0x73,0x74,0x61,0x67,0x69,0x6E,0x67,0x00,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x31,0x00,0x6D,0x61,0x78,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x34,0x00,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x00,0x32,0x31,0x39,0x30,0x33,0x00,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x52,0x61,0x6E,0x64,0x6F,0x6D,0x20,0x6D,0x61,0x70,0x00,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x00,0x45,0x61,0x73,0x79,0x00,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x6F,0x70,0x74,0x69,0x6F,0x6E,0x73,0x00,0x33,0x36,0x39,0x31,0x37,0x34,0x34,0x36,0x38,0x00,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x00,0x00,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x00,0x00,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x50,0x76,0x50,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x77,0x69,0x6E,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x73,0x00,0x30,0x00,0x73,0x65,0x74,0x74,0x69,0x6E,0x67,0x73,0x5F,0x75,0x73,0x65,0x72,0x63,0x6F,0x6E,0x74,0x65,0x6E,0x74,0x5F,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x00,0x00,0x00,0x00,0x01,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x70,0x69,0x6E,0x67,0x5F,0x00,0x00,0x73,0x70,0x6F,0x72,0x65,0x73,0x69,0x72,0x69,0x75,0x73,0x00,0x30,0x00,0x30,0x00,0x00,0x01,0x00}),
                new KeyValuePair<string, byte[]>("sb1",new byte[] {0x00,0x9A,0x00,0x01,0x03,0x8F,0x55,0x00,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x61,0x6E,0x6E,0x6F,0x31,0x37,0x30,0x31,0x00,0x52,0x63,0x58,0x3B,0x4D,0x28,0x7B,0x47,0x67,0x72,0x6F,0x75,0x70,0x69,0x64,0x20,0x69,0x73,0x20,0x6E,0x75,0x6C,0x6C,0x00,0x5C,0x68,0x6F,0x73,0x74,0x6E,0x61,0x6D,0x65,0x5C,0x67,0x61,0x6D,0x65,0x6D,0x6F,0x64,0x65,0x5C,0x67,0x61,0x6D,0x65,0x76,0x65,0x72,0x5C,0x67,0x61,0x6D,0x65,0x74,0x79,0x70,0x65,0x5C,0x70,0x61,0x73,0x73,0x77,0x6F,0x72,0x64,0x5C,0x6D,0x61,0x70,0x6E,0x61,0x6D,0x65,0x5C,0x6E,0x75,0x6D,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6E,0x75,0x6D,0x61,0x69,0x70,0x6C,0x61,0x79,0x65,0x72,0x73,0x5C,0x6F,0x70,0x65,0x6E,0x73,0x6C,0x6F,0x74,0x73,0x5C,0x67,0x61,0x6D,0x65,0x76,0x61,0x72,0x69,0x61,0x6E,0x74,0x00,0x00,0x00,0x00,0x04}),
                new KeyValuePair<string, byte[]>("qr2",new byte[] {0x07,0x98,0x92,0x25,0xA0,0x00,0x00,0x00,0x00}),

            };
        }
    }
}
