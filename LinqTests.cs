using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    /// <summary>
    /// Summary description for LinqTests
    /// </summary>
    [TestClass]
    public class LinqTests
    {

        private List<string> data = new List<string>();

        public LinqTests()
        {
            InitializeData();
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
        public void IEnumerableTest()
        {
            IEnumerable<int> numbers = data.Select(s => int.Parse(s));

            foreach (int i in numbers)
                TestContext.WriteLine("{0}", i);
        }

        [TestMethod]
        public void ArrayTest()
        {
            IEnumerable<int> numbers = data.Select(s => int.Parse(s)).ToArray();

            foreach (int i in numbers)
                TestContext.WriteLine("{0}", i);
        }

        [TestMethod]
        public void IEnumerableAddTest()
        {
            var query = data.Select(s => s);

            AddSomeData();

            foreach (string s in query)
                TestContext.WriteLine(s);
        }

        [TestMethod]
        public void ArrayAddTest()
        {
            var result = data.ToList();

            AddSomeData();

            foreach (string s in result)
                TestContext.WriteLine(s);
        }

        private void InitializeData()
        {
            data.AddRange(new[] { "1", "2", "3", "fyra" });
        }

        private void AddSomeData()
        {
            data.AddRange(new[] { "fem", "sex" });
        }
    }
}
