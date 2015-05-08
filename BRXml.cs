using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using CommonTests.IO;
using System.Xml.XPath;

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
            int noOfElements = 100;
            int noOfFiles = 5;
            string ISBNSuffixFilter = "978951"; // finland

            string xmlPath = Path.GetFullPath(@"..\..\Data\Xml\Bokrondellen\art_Onix_BBF_all_products_2014123_161012_150507_1327.xml");
            string targetFolder = Path.GetFullPath(@"..\..\Data\Xml\Bokrondellen\parts");

            for (int i=0; i < noOfFiles; i++)
            {
                int index = startIndex + i * noOfElements;
                ExtractPartFromXml(xmlPath, ISBNSuffixFilter, index , noOfElements);

            }
                

        }

        [TestCategory("Bokrondellen")]
        [TestMethod]
        public void CountElementsInXml()
        {
            string xmlPath = Path.GetFullPath(@"..\..\Data\Xml\Bokrondellen\art_Onix_BBF_all_products_2014123_161012_150507_1327.xml");
            FileInfo dataFile = new FileInfo(xmlPath);

            Assert.IsTrue(dataFile.Exists);
            int items;

            using (XmlReader reader = XmlReader.Create(dataFile.FullName, new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                XPathDocument doc = new XPathDocument(reader);

                XPathNavigator nav = doc.CreateNavigator();

                items = Convert.ToInt32(nav.Evaluate("count(artikelregister/artikel)"));
            }

            TestContext.WriteLine("Document contained {0} items.", items);
        }

        private void ModifyElement(XmlDocument doc)
        {
            XmlNode taxNode = doc.SelectSingleNode("/artikel/moms");

            if (taxNode == null)
            {
                // add code here to create a new node at the proper location
            }
            else if (string.IsNullOrWhiteSpace(taxNode.InnerText) || taxNode.InnerText == "0")
            {
                taxNode.InnerText = "10"; // default tax in finland
            }

            XmlNode statusNode = doc.SelectSingleNode("/artikel/lagerstatus");

            if (statusNode == null)
            {
                // add code here to create a new node at the proper location
            }
            else if (string.IsNullOrWhiteSpace(statusNode.InnerText))
            {
                statusNode.InnerText = "Spärrad";
            }

        }

        public void ExtractPartFromXml(string inputFileName, string isbnSuffix, int startIndex, int count)
        {
            //int startIndex = 400;
            //int noOfElements = 100;
            //string ISBNSuffixFilter = "978951"; // finland

            int takeCount = 0;
            int skipCount = 0;

            //string xmlPath = Path.GetFullPath(@"..\..\Data\Xml\Bokrondellen\art_Onix_BBF_all_products_2014123_161012_150507_1327.xml");
            string targetFolder = Path.GetFullPath(@"..\..\Data\Xml\Bokrondellen\parts");

            string fileName = string.Format("{0}-{1:D4}-{2:D4}{3}",
                Path.GetFileNameWithoutExtension(inputFileName),
                startIndex + 1,
                startIndex + count,
                Path.GetExtension(inputFileName));

            using (XmlReader reader = XmlReader.Create(inputFileName, new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                using (XmlWriter writer = XmlWriter.Create(Path.Combine(targetFolder, fileName), new XmlWriterSettings() { Indent = true }))
                {
                    writer.WriteStartElement("artikelregister");
                    reader.ReadToFollowing("filinformation");

                    XmlDocument headerElement = new XmlDocument();

                    XmlNode fileInformation = headerElement.ReadNode(reader);
                    headerElement.AppendChild(fileInformation);

                    XmlNode marketNode = headerElement.SelectSingleNode("/standardmarknad");

                    if (marketNode == null)
                    {
                        XmlElement marketElement = headerElement.CreateElement("standardmarknad");
                        marketElement.InnerText = "FINLAND";
                        headerElement.DocumentElement.AppendChild(marketElement);
                    }


                    writer.WriteNode(headerElement.CreateNavigator(), true);

                    if (reader.Name != "artikel")
                        reader.ReadToFollowing("artikel");


                    while (!reader.EOF && takeCount < count)
                    {
                        if (reader.Name == "artikel")
                        {
                            StringBuilder sb = new StringBuilder();

                            using (XmlWriter innerWriter = XmlWriter.Create(new StringWriterUTF8(sb), new XmlWriterSettings() { Indent = true }))
                            {
                                innerWriter.WriteNode(reader, true);
                            }

                            XmlDocument doc = new XmlDocument();

                            doc.LoadXml(sb.ToString());

                            XmlNode node = doc.SelectSingleNode("/artikel/artikelnummer");

                            if (node != null && !string.IsNullOrWhiteSpace(node.InnerText) && (isbnSuffix == null || (node.InnerText.Length >= isbnSuffix.Length && node.InnerText.Substring(0, isbnSuffix.Length) == isbnSuffix)))
                            {
                                if (skipCount >= startIndex)
                                {
                                    ModifyElement(doc);
                                    writer.WriteNode(doc.DocumentElement.CreateNavigator(), true);
                                    takeCount++;
                                }
                                else
                                {
                                    skipCount++;
                                }
                            }
                        }
                        else
                            reader.ReadToFollowing("artikel");

                    }

                    writer.WriteEndElement();


                }
            }
        }
    }

    public class StringWriterUTF8 : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public StringWriterUTF8(StringBuilder sb)
            : base(sb)
        {

        }

        public XmlWriter CreateXmlWriter()
        {
            return XmlWriter.Create(this);
        }

        public XmlWriter CreateXmlWriter(XmlWriterSettings settings)
        {
            return XmlWriter.Create(this, settings);
        }
    }
}
