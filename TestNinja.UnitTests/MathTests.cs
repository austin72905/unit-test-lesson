using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Fundamentals.Math _math;
        /*
            每次測試都應該創建一個新的實體
            但是每次都要寫就很麻煩
            這時可以用到SetUp、TearDown
            
            SetUp每個測試開始前都會執行

            TearDown每個測試結束後都會執行 
         
        */
        //SetUp
        [SetUp]
        //方法名隨意
        public void SetUp()
        {
            _math = new Fundamentals.Math();
        }
        //TearDown


        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            //var math=new Fundamentals.Math();

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        //修改為傳參的方式，比較美麗
        [Test]
        [TestCase(2,1,2)]   //使用這個標籤就能輕易地控制傳入的參數
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnGreaterArgument(int a,int b,int expectedResult)
        {

            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));

        }

        #region 改寫前
        [Test]
        public void Max_FirstArgumentIsGreater_ReturnFirstArgument()
        {

            var result= _math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));

        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnSecondArgument()
        {

            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentIsEqual_ReturnSameArgument()
        {

            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }
        #endregion
    }
}
