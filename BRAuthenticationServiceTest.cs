using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
	/// <summary>
	/// Summary description for BRAuthenticationServiceTest
	/// </summary>
	[TestClass]
	public class BRAuthenticationServiceTest
	{
		public BRAuthenticationServiceTest()
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
		public void TestServiceAuthentication()
		{
			//
			// TODO: Add test logic here
			//
			AuthenticateClient client = new AuthenticateClient();

			Dictionary<string, Guid> data = new Dictionary<string,Guid>();

			data.Add("pocketshop1", new Guid("7E94BD0F-BC68-43CD-BD1C-ECD3FAAF3B59"));
			data.Add("christerp", new Guid("9C80195D-EF3A-4B2A-868D-0F3E246D860E"));
			data.Add("mariab", new Guid("F8E3A02A-613C-427C-9CB5-D143FBF118BC"));
			data.Add("dkvmo", new Guid("4E8A4272-5DD5-4C1A-8D17-169D31461C02"));
			data.Add("icatest", new Guid("93A03D81-FCA2-4FC7-B720-7C7251DC18FD"));
			data.Add("samdisttest", new Guid("878E9442-10D1-4A0B-B22B-96EAEABC2507"));
			data.Add("univerb", new Guid("7368A05F-A611-4FB6-B057-64A80FC511FE"));

			foreach (var item in data)
			{
				string id = client.GetUserId(item.Value);
				TestContext.WriteLine("Received id {0} from ticket {1} for user {2}", id, item.Value, item.Key);
			}	
	
			client.Close();
		}

		[TestCategory("Bokrondellen")]
		[TestMethod]
		public void TestTicketValidation()
		{

		}

	}
}
