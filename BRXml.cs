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
            int noOfElements = 15000;
            string ISBNSuffixFilter = "978951"; // finland

            string xmlPath = @"D:\Users\chrhei\Documents\Visual Studio 2012\Projects\CommonTests\Data\Xml\art_Onix_BBF_all_products_2014123_161012.xml";
            string targetFolder = @"D:\Users\chrhei\Documents\Visual Studio 2012\Projects\CommonTests\Data\Xml";

            StringBuilder sb = new StringBuilder();

            XPathCollection pathCollection = new XPathCollection();

            pathCollection.Add("/*/filinformation");
            pathCollection.Add("/*/artikel");

            string fileName = string.Format("{0}-{1:D4}-{2:D4}{3}",
                Path.GetFileNameWithoutExtension(xmlPath),
                startIndex + 1,
                startIndex + noOfElements,
                Path.GetExtension(xmlPath));

            List<string> fprisList = new List<string>();

            using (XPathReader reader = new XPathReader(xmlPath, pathCollection, new XmlReaderSettings() { IgnoreWhitespace = true }))
            {
                using (XmlWriter writer = XmlWriter.Create(Path.Combine(targetFolder, fileName), new XmlWriterSettings() { Indent = true }))
                //using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true }))
                {
                    int skipCount = 0;
                    int takeCount = 0;

                    writer.WriteStartElement("artikelregister");

                    reader.ReadUntilMatch();

                    writer.WriteNode(reader, true);     // filinformation

                    while (takeCount < noOfElements && !reader.EOF)
                    {
                        if (reader.Match(1))
                        {
                            if (startIndex <= skipCount)
                            {
                                StringBuilder innerSb = new StringBuilder();
                                using (XmlWriter innerWriter = new StringWriterUTF8(innerSb).CreateXmlWriter(new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                                {
                                    innerWriter.WriteNode(reader, true);
                                }

                                // Create temporary XmlDocument used to navigate content 
                                XmlDocument doc = new XmlDocument();

                                // load current <artikel> to doc
                                doc.LoadXml(innerSb.ToString());
                                // get node for <artikelnummer>
                                XmlNode node = doc.SelectSingleNode("/artikel/artikelnummer");

                                // if SKU matches ISBNSuffixFilter then write SKU to file
                                if (node != null && !string.IsNullOrWhiteSpace(node.InnerText) && node.InnerText.Length >= ISBNSuffixFilter.Length && node.InnerText.Substring(0, ISBNSuffixFilter.Length) == ISBNSuffixFilter)
                                {
                                    writer.WriteNode(doc.DocumentElement.CreateNavigator(), true);
                                    takeCount++;
                                }
                            }
                            else
                            {
                                skipCount++;
                                StringBuilder innerSb = new StringBuilder();
                                using (XmlWriter innerWriter = new StringWriterUTF8(innerSb).CreateXmlWriter(new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                                {
                                    innerWriter.WriteNode(reader, true);
                                }
                                reader.ReadUntilMatch();
                            }
                        }
                        else
                        {
                            StringBuilder innerSb = new StringBuilder();
                            using (XmlWriter innerWriter = new StringWriterUTF8(innerSb).CreateXmlWriter(new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                            {
                                innerWriter.WriteNode(reader, true);
                            }
                        }
                    }
                    writer.WriteEndElement();
                    TestContext.WriteLine("Created file with {0} items", takeCount);
                }
            }
            //TestContext.WriteLine(sb.ToString());
        }

        [TestCategory("Bokrondellen")]
        [TestMethod]
        public void ExtractPartFromXml2()
        {
            int startIndex = 400;
            int noOfElements = 100;
            string ISBNSuffixFilter = "978951"; // finland

            int takeCount = 0;
            int skipCount = 0;

            string xmlPath = Path.GetFullPath(@"..\..\Data\Xml\art_Onix_BBF_all_products_2014123_161012.xml");
            string targetFolder = Path.GetFullPath(@"..\..\Data\Xml");

            string fileName = string.Format("{0}-{1:D4}-{2:D4}{3}",
                Path.GetFileNameWithoutExtension(xmlPath),
                startIndex + 1,
                startIndex + noOfElements,
                Path.GetExtension(xmlPath));

            using (XmlReader reader = XmlReader.Create(xmlPath, new XmlReaderSettings() { IgnoreWhitespace = true }))
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


                    while (!reader.EOF && takeCount < noOfElements)
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

                            if (node != null && !string.IsNullOrWhiteSpace(node.InnerText) && node.InnerText.Length >= ISBNSuffixFilter.Length && node.InnerText.Substring(0, ISBNSuffixFilter.Length) == ISBNSuffixFilter)
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
