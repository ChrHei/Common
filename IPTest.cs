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
            //
            // TODO: Add test logic here
            //

            Assert.IsTrue(IsIpInRange("192.168.0.244", "192.168.0.0/24"));
            Assert.IsFalse(IsIpInRange("192.168.1.244", "192.168.0.0/24"));
            Assert.IsTrue(IsIpInRange("192.168.0.33", "192.168.0.32/27"));
            Assert.IsFalse(IsIpInRange("192.168.0.33", "192.168.0.64/27"));
            
            Assert.IsTrue(IsIpInRange("192.168.0.33", "192.168.0.33/32"));
            

            //uint ipAddress = ParseIPAddress("192.168.0.24");
            //uint mask = ~(0xFFFFFFFF >> 24);

            //uint network = ipAddress & mask;

            //TestContext.WriteLine("Is little endian: {0}", BitConverter.IsLittleEndian);

            //byte[] networkBytes = BitConverter.GetBytes(network);

            //if (BitConverter.IsLittleEndian)
            //    networkBytes = networkBytes.OfType<byte>().Reverse().ToArray();

            //TestContext.WriteLine("IP: {0}", Convert.ToString(ipAddress, 2));
            //TestContext.WriteLine("Mask: {0}", Convert.ToString(mask, 2));
            //TestContext.WriteLine("Network: {0}", Convert.ToString(mask & ipAddress, 2));

            //TestContext.WriteLine("Network address: {0}", string.Join(".", networkBytes.OfType<byte>().Select(b => b.ToString())));
        }

        [TestMethod]
        public void TestShift()
        {
            uint roof = 0xFFFFFFFF;

            for (int bits = 24; bits < 31; bits++ )
            {
                uint result = ~(roof >> bits);
                TestContext.WriteLine("{0} -> {1:x} -> {2}", bits, result, Convert.ToString(result, 2).PadLeft(32, '0'));
            }
        }


        public bool IsIpInRange(string ipAddress, string networkAddressAndCidr)
        {
            IPAddress address = IPAddress.Parse(ipAddress);
            IPAddress network = IPAddress.Parse(networkAddressAndCidr.Substring(0, networkAddressAndCidr.IndexOf("/")));
            
            int bits = int.Parse(networkAddressAndCidr.Substring(networkAddressAndCidr.IndexOf("/") + 1, networkAddressAndCidr.Length - networkAddressAndCidr.IndexOf("/") - 1));


            uint addressNumber = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            uint networkNumber = BitConverter.ToUInt32(network.GetAddressBytes(), 0);

            uint maskNumber = ~(0xffffffff >> bits);

            if (BitConverter.IsLittleEndian)
            {
                byte[] maskBytes = BitConverter.GetBytes(maskNumber);
                Array.Reverse(maskBytes);
                maskNumber = BitConverter.ToUInt32(maskBytes, 0);
            }

            

            return (addressNumber & (uint)maskNumber) == networkNumber;
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
