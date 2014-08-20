using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace CommonTests
{
	/// <summary>
	/// Summary description for StringTests
	/// </summary>
	[TestClass]
	public class StringTests
	{
		public StringTests()
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
        [TestCategory("String tests")]
        public void TestUrlExpression()
		{
			string[] data = new string[]
			{
                @"I ett långt stycke med flera länkar till bl a http://www.consid.se
bör man ändå kunna ersätta http://consid.se/se/karriaer/lediga-tjaenster/net-utvecklare-till-goeteborg/ 
eftersom allting avbryts https://intranet.industriall-union.org/Calendar/peoplethisweek/edit/ så fort
man skriver ett blanksteg. Sedan kan man läsa på www.dagensnyheter.se för att se vad som händer.
Eva Marie påstår att länkar som är speciella https://www.google.se/#q=regex+c%23+example inte funger
och då måste jag ju fixa det"
			};

            Regex re = new Regex(@"(http://|https://|www\.)([^\s])*", RegexOptions.IgnoreCase);
			foreach (string s in data)
			{
                
				MatchCollection matches = re.Matches(s);

				foreach (Match m in matches)
					TestContext.WriteLine("{0} => {1} => {2}\r\n", s, m.Groups[1].Value, m.Value);


                string result = re.Replace(s, CreateBitLy);
                TestContext.WriteLine(result);


			}
		}

        private string CreateBitLy(Match m)
        {
            string prefix = m.Groups[1].Value;
            string current = m.Value;
            
            return "http://bit.ly/" + RandomString(6); 
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Test a url pattern and removes trailing numbers. Used by client cache-prevent
        /// url rewriter written by Tibor
        /// </summary>
        [TestMethod]
        [TestCategory("String tests")]
        public void TestRegex()
        {
            string s = "http://syncron.local/assets/css/main.20140805115625.css";
            string pattern =@"^(.*)\.\d+\.(css|js)$";

            TestContext.WriteLine("Before: {0}", s);
            s = Regex.Replace(s, pattern, @"$1.$2");

            TestContext.WriteLine("After: {0}", s);

        }

        [TestMethod]
        [TestCategory("IndustriALL")]
        public void TestTimeZone()
        {
            // set value to convert
            string pageTimeZone = "Central Europe Standard Time";
            
            // create a time zone info object
            TimeZoneInfo ti = string.IsNullOrWhiteSpace(pageTimeZone) ? null : TimeZoneInfo.FindSystemTimeZoneById(pageTimeZone);
            
            // get current local time
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, ti);

            // get timezone offset for local time (including daylight saving time)
            string currentUtcOffset = string.IsNullOrEmpty(pageTimeZone) ? "" : localTime.Offset.ToString(@"\+hhmm");

            TestContext.WriteLine("Page time zone: {0}", pageTimeZone);
            TestContext.WriteLine("Current local time for time zone:{0}", localTime);
            TestContext.WriteLine("Time zone base UTC offset: {0}", ti.BaseUtcOffset.ToString(@"\+hhmm"));
            TestContext.WriteLine("Time zone current UTC offset: {0}", currentUtcOffset);
            TestContext.WriteLine("Is daylight saving time: {0}", ti.IsDaylightSavingTime(localTime));
        }

        /// <summary>
        /// Added comment #1
        /// </summary>
        public void GitTest_1()
        {

        }

        /// <summary>
        /// Added comment #2
        /// </summary>
        public void GitTest_2()
        {

        }

	}

    
}
