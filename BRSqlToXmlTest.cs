using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPiServer.ServiceLocation;
using EPiServer;
using EPiServer.Common.Content;
using EPiServer.Core;

namespace CommonTests
{
    /// <summary>
    /// Summary description for BRSqlToXmlTest
    /// </summary>
    [TestClass]
    public class BRSqlToXmlTest
    {
        public BRSqlToXmlTest()
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
        [TestCategory("Bokrondellen")]
        public void GetProductTest()
        {
            EPiServer.Initialization.InitializeContext();

            IContentLoader loader = ServiceLocator.Current.GetInstance<IContentLoader>();

            ContentReference link = new ContentReference(3);
            ContentData data = loader.Get<ContentData>(link);
        }
    }
}
