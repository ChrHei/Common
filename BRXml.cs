﻿using System;
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
            int noOfElements = 100;

            string xmlPath = @"D:\Users\chrhei\Documents\Visual Studio 2012\Projects\CommonTests\Data\Xml\art_Onix_BBF_all_products_2014123_161012.xml";
            string targetFolder = @"D:\Users\chrhei\Documents\Visual Studio 2012\Projects\CommonTests\Data\Xml";

            StringBuilder sb = new StringBuilder();

            XPathCollection pathCollection = new XPathCollection();

            pathCollection.Add("/*/filinformation");
            pathCollection.Add("/*/artikel");

            string fileName = string.Format("{0}-{1:D4}-{2:D4}{3}",
                Path.GetFileNameWithoutExtension(xmlPath),
                startIndex,
                noOfElements,
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
                            if (startIndex >= skipCount)
                            {
                                StringBuilder innerSb = new StringBuilder();
                                using (XmlWriter innerWriter = new StringWriterUTF8(innerSb).CreateXmlWriter(new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                                {
                                    innerWriter.WriteNode(reader, true);
                                }
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(innerSb.ToString());
                                XmlNode node = doc.SelectSingleNode("/artikel/artikelnummer");

                                if (node != null && !string.IsNullOrWhiteSpace(node.InnerText) && node.InnerText.Length > 2 && node.InnerText.Substring(0, 3) == "978")
                                {
                                    writer.WriteNode(doc.DocumentElement.CreateNavigator(), true);
                                    takeCount++;
                                }
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
            //TestContext.WriteLine(sb.ToString());
            TestContext.WriteLine("No price: {0}", fprisList.Count(f => string.IsNullOrWhiteSpace(f)));
        }
    }

    public class StringWriterUTF8 : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public StringWriterUTF8(StringBuilder sb) : base(sb)
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
