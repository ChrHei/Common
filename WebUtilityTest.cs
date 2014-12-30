using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CommonTests
{
    /// <summary>
    /// Summary description for WebUtilityTest
    /// </summary>
    [TestClass]
    public class WebUtilityTest
    {
        public WebUtilityTest()
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

        [TestCategory("WebUtility")]
        [TestMethod]
        public void HtmlDecodeTest()
        {
            string[] data = new []{
                "http://www.bokrondellen.se/Orderresponse?orderno={0}&amp;status=accepted&amp;brid={1}",
                "http://www.bokrondellen.se/Orderresponse?orderno={0}&amp;status=rejected&amp;brid={1}",
                "",
                null
            };

            foreach (var s in data)
            {
                var decoded = WebUtility.HtmlDecode(s);
                Console.WriteLine(decoded);
            }
                

        }
    }
}
