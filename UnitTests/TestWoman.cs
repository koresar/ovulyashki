using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using WomenCalendar.Forms;

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

        private Woman GetDummyWoman()
        {
            Woman w = new Woman();
            w.Name = "vasya";
            w.Password = "pwd";
            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 11, 11)));
            w.Menstruations[0].Egestas.Take(2).ToList().ForEach(e => w.Menstruations[0].Egestas[e.Key] = 1);
            Assert.IsTrue(w.AddConceptionDay(new DateTime(2011, 11, 21)));
            foreach (var item in typeof(Schedule).Assembly.GetTypes().
                Where(t => t.IsSubclassOf(typeof(Schedule)) && !t.IsAbstract))
            {
                w.Schedules.Add((Activator.CreateInstance(item) as Schedule).CreateDefault(new DateTime(2011, 11, 11)));
            }
            w.Notes[new DateTime(2011, 11, 21)] = "bambini!";
            w.HadSexList[new DateTime(2011, 11, 21)] = true;
            w.Health[new DateTime(2011, 11, 11)] = 1;
            w.CFs[new DateTime(2011, 11, 21)] = CervicalFluid.Stretchy;
            w.BBT[new DateTime(2011, 11, 11)] = 36.9;

            return w;
        }

        [TestMethod]
        public void SaveLoadWomanWithData()
        {
            Program.InitializeEnvironmentStuff();
            MsgBox.AlwaysAnswer = MsgBox.Answer.Yes;
            ErrorForm.DoNotShow = true;

            Woman w = this.GetDummyWoman();

            var womanFile = Path.Combine(this.TestContext.TestDir, "tmp.woman");
            Assert.IsTrue(Program.SaveWomanTo(w, womanFile));

            var w2 = Woman.ReadFrom(womanFile);
            Assert.IsNotNull(w2);

            File.Delete(womanFile);

            // make sure data loaded is exactly the same as was before save.
            Assert.IsTrue(w2.Equals(w));

            try
            {
                Woman.ReadFrom(@"c:\Unexistent123321.woman");
                Assert.Fail();
            }
            catch { }
        }

        [TestMethod]
        public void AveragePeriod()
        {
            Program.InitializeEnvironmentStuff();
            MsgBox.AlwaysAnswer = MsgBox.Answer.Yes;
            ErrorForm.DoNotShow = true;

            Woman w = this.GetDummyWoman();

            bool invoked = false;
            w.AveragePeriodLengthChanged += () => invoked = true;
            w.AveragePeriodLength = w.AveragePeriodLength + 1;

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void AddMenstruationsDay()
        {
            Program.InitializeEnvironmentStuff();
            MsgBox.AlwaysAnswer = MsgBox.Answer.Yes;
            ErrorForm.DoNotShow = true;

            Woman w = this.GetDummyWoman();

            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 11)));
            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 12)));
            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 13)));
            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 14)));
            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 15)));
            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 11, 16)));

            w = new Woman();
            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 11, 11)));
            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 12, 11)));
            Assert.AreEqual(w.AveragePeriodLength, 30);

            MsgBox.AlwaysAnswer = MsgBox.Answer.No;
            Assert.IsFalse(w.AddMenstruationDay(new DateTime(2011, 11, 26)));            
        }

        [TestMethod]
        public void IsPredictedAsMenstruationDay()
        {
            Program.InitializeEnvironmentStuff();
            MsgBox.AlwaysAnswer = MsgBox.Answer.Yes;
            ErrorForm.DoNotShow = true;

            Woman w = new Woman();
            Assert.IsFalse(w.IsPredictedAsMenstruationDay(new DateTime(2011, 11, 11)));

            Assert.IsTrue(w.AddMenstruationDay(new DateTime(2011, 11, 11)));
            Assert.IsFalse(w.IsPredictedAsMenstruationDay(new DateTime(2010, 11, 11)));
            Assert.IsFalse(w.IsPredictedAsMenstruationDay(new DateTime(2011, 11, 15)));
            Assert.IsFalse(w.IsPredictedAsMenstruationDay(new DateTime(2011, 11, 16)));
            Assert.IsTrue(w.IsPredictedAsMenstruationDay(new DateTime(2011, 12, 11)));
        }
    }
}
