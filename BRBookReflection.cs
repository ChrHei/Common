using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    /// <summary>
    /// Summary description for BRBookReflection
    /// </summary>
    [TestClass]
    public class BRBookReflection
    {
        public BRBookReflection()
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
        public void GetLockedPropertiesFromBook()
        {
            //Assembly.ReflectionOnlyLoadFrom(@"D:\websites\Bokrondellen\wwwroot\bin\Bokrondellen.Configuration.dll");
            Assembly assembly = Assembly.LoadFrom(@"D:\websites\Bokrondellen\wwwroot\bin\Bokrondellen.Common.dll");

            Type type = assembly.GetType("Bokrondellen.Common.Classes.Book");
            Type lockTypeAttribute = assembly.GetType("Bokrondellen.Common.Classes.LockTypeAttribute");
            Type lockTypeEnum = assembly.GetType("Bokrondellen.Common.Classes.LockTypeEnum");
            Array enumValues = Enum.GetValues(lockTypeEnum);

            var properties = type.GetProperties();

            foreach (var p in properties)
            {
                if (p.CustomAttributes.Any(a => a.AttributeType.Equals(lockTypeAttribute)))
                {
                    CustomAttributeData lockTypeData = p.CustomAttributes.First(a => a.AttributeType.Equals(lockTypeAttribute));

                    int value = (int)lockTypeData.ConstructorArguments.First().Value;

                    TestContext.WriteLine("{0}, {1}", p.Name, enumValues.GetValue(value));
                }
                    
            }

        }
    }
}
