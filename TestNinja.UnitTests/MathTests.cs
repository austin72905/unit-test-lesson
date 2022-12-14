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

        /*
            Trust Worthy Test
            你改你實作方法的返回結果，如果還是pass，代表你測試錯東西了
        */

        [Test]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            //var math=new Fundamentals.Math();

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        /*
            Ignore 標籤
        
            比起註解掉，使用這個標籤會提醒你那些測試被跳過了
        */

        //修改為傳參的方式，比較美麗
        [Test]
        [TestCase(2,1,2)]   //使用這個標籤就能輕易地控制傳入的參數
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        [Ignore("為何要忽視這個測試")]
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

        /*
            Ordered    check the items are sorted
            Unique     check the items are not duplicate
        */

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnNumberUpToLimit()
        {
            var result=_math.GetOddNumbers(5);

            //Assert.That(result,Is.Not.Empty);
            //Assert.That(result.Count(), Is.EqualTo(3));

            //Assert.That(result.Count(), Does.Contain(1));
            //Assert.That(result.Count(), Does.Contain(3));
            //Assert.That(result.Count(), Does.Contain(5));

            //easy way to write
            Assert.That(result, Is.EquivalentTo(new [] { 1, 3, 5 }));

            /*
                want to check the items are sorted or duplicate
           
            */
            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }
    }
}
