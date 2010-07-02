using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WomenCalendar;

namespace WomenCalendar.UnitTests
{
    /// <summary>
    /// Это класс теста для DayCellAppearanceTest, в котором должны
    /// находиться все модульные тесты DayCellAppearanceTest
    /// </summary>
    [TestClass()]
    public class DayCellAppearanceTest
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Получает или устанавливает контекст теста, в котором предоставляются
        /// сведения о текущем тестовом запуске и обеспечивается его функциональность.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }
            
            set
            {
                this.testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты теста
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext)
        // {
        // }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup()
        // {
        // }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста
        // [TestInitialize()]
        // public void MyTestInitialize()
        // {
        // }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup()
        // {
        // }
        #endregion

        /// <summary>
        /// Тест для GetAllCurrentColorsAsArgb
        /// </summary>
        [TestMethod()]
        public void GetAllCurrentColorsAsArgbTest()
        {
            DayCellAppearance target = new DayCellAppearance();
            target.BackConceptionDay = Color.DeepSkyBlue;
            target.BackPregnancyDay = Color.LightCyan;
            target.BackMenstruationDay = Color.LightPink;
            target.BackPredictedMenstruationDay = ControlPaint.LightLight(Color.LightPink);
            target.BackOvulationDay = Color.Gold;
            target.BackSafeSex = Color.LightGreen;
            target.BackEmpty = Color.White;
            target.BackHadSex = Color.White;
            target.BackHaveNote = Color.White;
            target.BackBoyDay = Color.White;
            target.BackGirlDay = Color.White;
            int[] expected = new int[11]; 
            expected[0] = 16760576;    
            expected[1] = 16777184;
            expected[2] = 12695295;    
            expected[3] = 14736383;    
            expected[4] = 55295;    
            expected[5] = 9498256;    
            expected[6] = 16777215;    
            expected[7] = 16777215;    
            expected[8] = 16777215;    
            expected[9] = 16777215;
            expected[10] = 16777215;
            int[] actual;
            actual = target.GetAllCurrentColorsAsArgb();
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
