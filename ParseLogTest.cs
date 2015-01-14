using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace CommonTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ParseLogTest
    {
        public ParseLogTest()
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


        [TestCategory("Bokrondellen")]
        [TestMethod]
        public void BRParseDeweyLog()
        {
            const string LOG_FOLDER = @"d:\temp\Bokrondellen\logs\log-dewey-import";
            //
            // TODO: Add test logic here
            //
            DirectoryInfo rootFolder = new DirectoryInfo(LOG_FOLDER);
            Regex re = new Regex(@"Setting Dewey code '(\d+?\.\d+?)' on ISBN '(\d+?)'");
            FileInfo[] files = rootFolder.GetFiles().OrderBy(f => f.Name).ToArray();

            string outputPath = Path.Combine(LOG_FOLDER, "output.csv");

            if (File.Exists(outputPath))
                File.Delete(outputPath);

            using (StreamWriter writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                writer.WriteLine("ISBN;Dewey");
                foreach (FileInfo file in files)
                {
                    if (file.FullName != outputPath)
                    {
                        TestContext.WriteLine("Parsing file {0}", file.Name);
                        using (StreamReader reader = new StreamReader(file.FullName, Encoding.UTF8))
                        {
                            string currentLine;
                            while ((currentLine = reader.ReadLine()) != null)
                            {
                                if (re.IsMatch(currentLine))
                                {
                                    Match match = re.Match(currentLine);
                                    writer.WriteLine("{0};{1}", match.Groups[2].Value, match.Groups[1].Value);
                                }
                            }
                        }
                    }
                }
                writer.Flush();
                writer.Close();
            }
        }
    }
}
