using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WomenCalendar.UnitTests
{
    /// <summary>
    /// Summary description for TestHoroscop
    /// </summary>
    [TestClass]
    public class TestHoroscop
    {
        public TestHoroscop()
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
        public void ZodiacSignName()
        {
            Program.InitializeEnvironmentStuff();
            var langFilePath = TEXT.FindAllLangFiles()["en"];
            TEXT.ApplyLanguageFile("en", langFilePath);

            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 1, 5)), "Capricorn");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 2, 5)), "Aquarius");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 3, 5)), "Pisces");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 4, 5)), "Aries");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 5, 5)), "Taurus");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 6, 5)), "Gemini");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 7, 5)), "Cancer");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 8, 5)), "Leo");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 9, 5)), "Virgo");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 10, 5)), "Libra");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 11, 5)), "Scorpio");
            Assert.AreEqual(HoroscopDatePair.GetZodiacSignName(new DateTime(2011, 12, 5)), "Sagittarius");
        }
    }
}
