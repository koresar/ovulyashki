using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace WomenCalendar.UnitTests
{
    /// <summary>
    /// Summary description for TestWoman
    /// </summary>
    [TestClass]
    public class TestWoman
    {
        public TestWoman()
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
        public void SaveLoadWomanWithData()
        {
            Program.InitializeEnvironmentStuff();
            MsgBox.AlwaysAnswer = MsgBox.Answer.Yes;
            Woman w = new Woman();

            w.Name = "vasya";
            w.Password = "pwd";
            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 11, 11)));
            Assert.IsTrue(w.AddConceptionDay(new DateTime(2011, 11, 21)));
            foreach (var item in typeof(Schedule).Assembly.GetTypes().
                Where(t => t.IsSubclassOf(typeof(Schedule)) && !t.IsAbstract))
            {
                w.Schedules.Add((Activator.CreateInstance(item) as Schedule).CreateDefault(new DateTime(2011, 11, 11)));
            }
            w.Notes.Add(new DateTime(2011, 11, 21), "bambini!");
            w.HadSexList.Add(new DateTime(2011, 11, 21), true);
            w.Health.Add(new DateTime(2011, 11, 11), 1);
            w.CFs.Add(new DateTime(2011, 11, 21), CervicalFluid.Stretchy);
            w.BBT.Add(new DateTime(2011, 11, 11), 36.9);

            var womanFile = Path.Combine(this.TestContext.TestDir, "tmp.woman");
            Assert.IsTrue(Program.SaveWomanTo(w, womanFile));

            var w2 = Woman.ReadFrom(womanFile);
            Assert.IsNotNull(w2);

            File.Delete(womanFile);
            Assert.IsTrue(w2.Equals(w));
        }
    }
}
