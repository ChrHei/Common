using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CommonTests
{
	/// <summary>
	/// Summary description for Sodra
	/// </summary>
	[TestClass]
	public class Sodra
	{
		public Sodra()
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
		[TestCategory("Södra")]
		public void Transform()
		{
            string dataFolder = @"..\..\Data\Xml\Sodra";
            string xsltFolder  = @"..\..\Data\Xslt";

            string dataFileName = Path.Combine(dataFolder, "utdelningsavisering.xml");
            string outputFileName = Path.Combine(dataFolder, "utdelningsavisering_output.xml");
            string xsltFileName = Path.Combine(xsltFolder, "XsltTest.xslt");

            FileInfo dataFile = new FileInfo(dataFileName);
            FileInfo xsltFile = new FileInfo(xsltFileName);
            FileInfo outputFile = new FileInfo(outputFileName);


            TestContext.WriteLine("Input file: {0} {1}", dataFile.Exists, dataFile.FullName);
            TestContext.WriteLine("Xslt file: {0} {1}", xsltFile.Exists, xsltFile.FullName);
            TestContext.WriteLine("Output file: {0} {1}", outputFile.Exists, outputFile.FullName);

            XsltHelper.TransformXslt(xsltFile.FullName, dataFile.FullName, outputFile.FullName);
		}
	}
}
