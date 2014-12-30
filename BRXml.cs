using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using CommonTests.IO;
using GotDotNet.XPath;

namespace CommonTests
{
    /// <summary>
    /// Summary description for BRXml
    /// </summary>
    [TestClass]
    public class BRXml
    {
        public BRXml()
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
        public void ExtractPartFromXml()
        {
            int startIndex = 0;
            int noOfElements = 2;

            string xmlPath = @"D:\Users\chrhei\Documents\Visual Studio 2012\Projects\CommonTests\Data\Xml\art_Onix_BBF_all_products_2014123_161012.xml";
            // extract file information element
            //XmlDocument doc = new XmlDocument();
            //doc.Load(xmlPath);

            //XmlNode root = doc.DocumentElement;

            //XmlNode fileInfo = root.SelectSingleNode("filinformation");

            //XElement xFileInfo = XElement.Parse(fileInfo.OuterXml);

            StringBuilder fileInfo = new StringBuilder();

            XPathCollection pathCollection = new XPathCollection();

            pathCollection.Add("//artikelregister/filinformation");
            pathCollection.Add("//artikelregister/artikel");

            using (XPathReader reader = new XPathReader(xmlPath, pathCollection, new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                using (XmlWriter writer = XmlWriter.Create(new StringWriterUTF8(fileInfo), new XmlWriterSettings() { Indent = true }))
                {
                    int skipCount = 0;
                    int takeCount = 0;

                    writer.WriteStartElement("artikelregister");

                    reader.ReadUntilMatch();

                    writer.WriteNode(reader, true);

                    while (takeCount < noOfElements && !reader.EOF)
                    {
                        if (reader.Match(1))
                        {
                            if (startIndex >= skipCount)
                            {
                                writer.WriteNode(reader, true);
                                takeCount++;
                            }
                            else
                            {
                                skipCount++;
                            }

                        }
                        else
                        {
                            reader.ReadUntilMatch();
                        }
                    }
                    writer.WriteEndElement();
                }
            }

            //TestContext.WriteLine(fileInfo.OuterXml);

            TestContext.WriteLine(fileInfo.ToString());
        }

    }
}
