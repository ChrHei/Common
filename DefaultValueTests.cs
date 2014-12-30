using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    /// <summary>
    /// Summary description for DefaultValueTests
    /// </summary>
    [TestClass]
    public class DefaultValueTests
    {
        public DefaultValueTests()
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
        public void PrintDefaultValues()
        {
            //
            // TODO: Add test logic here
            //
            var data = new data();

            TestContext.WriteLine("int: {0}", data.i);
            TestContext.WriteLine("char: {0}", data.c);
            TestContext.WriteLine("string: {0}", data.s);
            TestContext.WriteLine("DateTime: {0}, ticks: {1}", data.dt, data.dt.Ticks);
            TestContext.WriteLine("TimeSpan: {0}, ticks: {1}", data.ts, data.ts.Ticks);



        }
        class data
        {
            public int i { get; set; }
            public char c { get; set; }
            public string s { get; set; }
            public DateTime dt { get; set; }
            public TimeSpan ts { get; set; }
        }

    }
}
