using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Schema;
using System.Xml;

namespace CommonTests
{
    /// <summary>
    /// Summary description for XmlValidator
    /// </summary>
    [TestClass]
    public class XmlValidator
    {
        public XmlValidator()
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
        public void TestCatalogImportValidation()
        {
            //
            // TODO: Add test logic here
            //

            string filePath = @" d:\temp\Bokrondellen\xml\art_Onix-messages-test-staging-20150602-all-fixed-measurements\output\art_Onix-messages-test-staging-20150602-all-fixed-measurements.xml";
            string schemaPath = @"d:\websites\Bokrondellen\Bokrondellen.Common\Classes\Schema\book-import.xsd";

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
            {
                using (StreamReader schemaReader = new StreamReader(schemaPath, Encoding.UTF8))
                {
                    XmlSchema schema = XmlSchema.Read(schemaReader, new ValidationEventHandler(this.Reader_ValidationEventHandler));
                    XmlReaderSettings settings = new XmlReaderSettings
                    {
                        ValidationType = ValidationType.Schema
                    };
                    settings.Schemas.Add(schema);
                    settings.ValidationEventHandler += new ValidationEventHandler(this.Reader_ValidationEventHandler);

                    using (XmlReader reader2 = XmlReader.Create(reader, settings))
                    {
                        while (reader2.Read())
                        {
                        }
                    }
                }
            }


        }

        private void Reader_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            throw new Exception(string.Format("{0}\r\n{1}", e.Message, e.Exception.StackTrace));
        }

        [TestMethod]
        [TestCategory("Bokrondellen")]
        public void PrependXmlDeclarationTest()
        {
            string srcPath = @"d:\websites\Bokrondellen (clean install)\import\ImportUtility\DataFiles\Catalog.xml";
            string tmpPath = srcPath + ".tmp";

            using (XmlReader reader = XmlReader.Create(srcPath))
            {
                while (reader.Read()) { }
            }
        }

        [TestMethod]
        [TestCategory("Bokrondellen")]
        public void AddMarketToXml()
        {
            string sourceFolder = @" d:\temp\Bokrondellen\xml\art_Onix-messages-test-staging-20150602-all-fixed-measurements";
            string targetFolder = Path.Combine(sourceFolder, "output");

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            DirectoryInfo di = new DirectoryInfo(sourceFolder);

            XmlReaderSettings readerSettings = new XmlReaderSettings()
            {
                IgnoreWhitespace = true
            };

            XmlWriterSettings writerSettings = new XmlWriterSettings()
            {
                Indent = true,
            };

            foreach (FileInfo file in di.GetFiles("*.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(Path.Combine(targetFolder, file.Name), writerSettings))
                {
                    using (XmlReader reader = XmlReader.Create(file.FullName, readerSettings))
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.XmlDeclaration: // will be added by the writer
                                    break;

                                case XmlNodeType.Element:
                                    writer.WriteStartElement(reader.Name);
                                    if (reader.HasAttributes)
                                    {
                                        writer.WriteAttributes(reader, true);
                                    }
                                    if (reader.IsEmptyElement)
                                        writer.WriteEndElement();
                                    break;
                                case XmlNodeType.EndElement:
                                    if (reader.Name == "filinformation")
                                        writer.WriteElementString("standardmarknad", "FINLAND");
                                    writer.WriteEndElement();
                                    break;
                                case XmlNodeType.CDATA:
                                    writer.WriteCData(reader.Value);
                                    break;
                                case XmlNodeType.Text:
                                    writer.WriteString(reader.Value);
                                    break;
                                default:
                                    throw new Exception(string.Format("Unhandled node type found: {0}", reader.NodeType));
                            }
                        }

                    }

                }


            }


        }
    }
}
