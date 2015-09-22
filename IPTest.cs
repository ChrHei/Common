using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CommonTests
{
    /// <summary>
    /// Summary description for IPTest
    /// </summary>
    [TestClass]
    public class IPTest
    {
        public IPTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestIpRanges()
        {



            Assert.IsTrue(IsIpInRange("192.168.0.244", "192.168.0.0/24"));
            Assert.IsFalse(IsIpInRange("192.168.1.244", "192.168.0.0/24"));
            Assert.IsTrue(IsIpInRange("192.168.0.33", "192.168.0.32/27"));
            Assert.IsFalse(IsIpInRange("192.168.0.33", "192.168.0.64/27"));

        
            
        
        }

        [TestMethod]
        [TestCategory("Taxi Stockholm")]
        public void TestBNet()
        {
            string network1 = "10.100.0.0/16";
            string network2 = "10.200.0.0/16";

            for(int i = 1; i < 255; i++)
            {
                for(int j = 1; j < 255; j++)
                {
                    string address = string.Format("10.100.{0}.{1}", i, j);
                    Assert.IsTrue(IsIpInRange(address, network1));
                    Assert.IsFalse(IsIpInRange(address, network2));
                }
            }
        }

        [TestMethod]
        public void TestShift()
        {
            uint roof = 0xFFFFFFFF;

            for (int bits = 24; bits < 31; bits++)
            {
                uint result = ~(roof >> bits);
                TestContext.WriteLine("{0} -> {1:x} -> {2}", bits, result, Convert.ToString(result, 2).PadLeft(32, '0'));
            }
        }


        public bool IsIpInRange(string ipAddress, string networkAddressAndCidr)
        {
            IPAddress address = IPAddress.Parse(ipAddress);

            string[] networkChunks = networkAddressAndCidr.Split('/');

            IPAddress network = IPAddress.Parse(networkChunks[0]);

            int bits = int.Parse(networkChunks[1]);

            uint addressNumber = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            uint networkNumber = BitConverter.ToUInt32(network.GetAddressBytes(), 0);

            uint maskNumber = ~(0xffffffff >> bits);

            if (BitConverter.IsLittleEndian)
            {
                byte[] maskBytes = BitConverter.GetBytes(maskNumber);
                Array.Reverse(maskBytes);
                maskNumber = BitConverter.ToUInt32(maskBytes, 0);
            }

            return (addressNumber & maskNumber) == networkNumber;
        }

        public uint ParseIPAddress(string ipAddress)
        {
            string[] chunks = ipAddress.Split('.');

            if (chunks.Length != 4)
                throw new Exception("Invalid IP address.");

            uint part1 = uint.Parse(chunks[0]) << 24;
            uint part2 = uint.Parse(chunks[1]) << 16;
            uint part3 = uint.Parse(chunks[2]) << 8;
            uint part4 = uint.Parse(chunks[3]);

            return part1 + part2 + part3 + part4;

        }
    }
}
