using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace WomenCalendar.UnitTests
{
    /// <summary>
    /// Summary description for TestTEXT
    /// </summary>
    [TestClass]
    public class TestTEXT
    {
        public TestTEXT()
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
        public void LoadingPossibility()
        {
            Program.InitializeEnvironmentStuff();

            var langFiles = TEXT.FindAllLangFiles();
            Assert.IsTrue(langFiles.Count >= 3);

            Assert.IsTrue(TEXT.ApplyLanguageFile(langFiles.First().Key, langFiles.First().Value));
        }

        [TestMethod]
        public void AllTextIsTranslated()
        {
            Program.InitializeEnvironmentStuff();

            var langFiles = TEXT.FindAllLangFiles();
            Assert.IsTrue(langFiles.Count >= 3);

            var defaults = TEXT.Get.AllDefaults();

            var emptyDefaultValues = defaults.Where(item => string.IsNullOrEmpty(item.Key)).Select(item => item.Value).ToList();
            if (emptyDefaultValues.Any())
            {
                Assert.Fail(string.Format(
                    "In default language '{0}' there is no key for translation(s): {1}.",
                    TEXT.DefaultLang,
                    emptyDefaultValues.Aggregate((s1, s2) => s1 + ", " + s2)));
            }

            var emptyDefaultKeys = defaults.Where(item => string.IsNullOrEmpty(item.Value)).Select(item => item.Key).ToList();
            if (emptyDefaultKeys.Any())
            {
                Assert.Fail(string.Format(
                    "In default language '{0}' there is no translation for key(s): {1}.",
                    TEXT.DefaultLang,
                    emptyDefaultKeys.Aggregate((s1, s2) => s1 + ", " + s2)));
            }

            foreach (var langItem in langFiles.Where(i => i.Key != TEXT.DefaultLang))
            {
                Assert.IsTrue(TEXT.ApplyLanguageFile(langItem.Key, langItem.Value));
                var langFileName = Path.GetFileName(langItem.Value);

                var translations = TEXT.Get.AllTranslations();

                var emptyValues = translations.Where(item => string.IsNullOrEmpty(item.Key)).Select(item => item.Value).ToList();
                if (emptyValues.Any())
                {
                    Assert.Fail(string.Format(
                        "In language '{0}' there is no key for translation(s): {1}.",
                        langFileName,
                        emptyValues.Aggregate((s1, s2) => s1 + ", " + s2)));
                }

                var emptyKeys = translations.Where(item => string.IsNullOrEmpty(item.Value)).Select(item => item.Key).ToList();
                if (emptyKeys.Any())
                {
                    Assert.Fail(string.Format(
                        "In language '{0}' there is no translation for key(s): {1}.",
                        langFileName,
                        emptyKeys.Aggregate((s1, s2) => s1 + ", " + s2)));
                }

                if (translations.Count != defaults.Count)
                {
                    var mismatchKeys = translations.Keys.Count > defaults.Keys.Count ?
                        translations.Keys.Except(defaults.Keys) :
                        defaults.Keys.Except(translations.Keys);
                    Assert.Fail(string.Format(
                        "Number of translation keys of '{0}' ({3}) mismatch with default language '{1}' ({4}). Mismatch key(s) are: {2}.",
                        langFileName,
                        TEXT.DefaultLang,
                        mismatchKeys.Aggregate((s1, s2) => s1 + ", " + s2),
                        translations.Count,
                        defaults.Count));
                }

                var absentKeys = defaults.Keys.Except(translations.Keys).ToList();
                if (absentKeys.Any())
                {
                    Assert.Fail(string.Format(
                        "In file '{0}' there is no translation for key(s): {1}.",
                        langFileName,
                        absentKeys.Aggregate((s1, s2) => s1 + ", " + s2)));
                }

                var redundantKeys = translations.Keys.Except(defaults.Keys).ToList();
                if (redundantKeys.Any())
                {
                    var redundantKeysString = absentKeys.Aggregate((s1, s2) => s1 + ", " + s2);
                    Assert.Fail(string.Format(
                        "In file '{0}' there is redundant translation for key(s): {1}.",
                        Path.GetFileName(langItem.Value),
                        redundantKeysString));
                }
            }            
        }
    }
}
