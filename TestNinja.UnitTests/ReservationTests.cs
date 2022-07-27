using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        /*
            TDD 開發模式
            
            先寫測試，再開始寫code

            步驟
            1. 先寫一個結果失敗的測試
            2. 寫一個簡單的程式碼，讓結果通過
            3. 逐步改寫成你正式環境的程式碼
         
        */
        [SetUp]
        public void Setup()
        {
        }

        /*
            命名規則
            要測試的函數名_控制變因_預期結果

            使用3A 撰寫 單元測試內容
    
            1. Arrange  :  測試物件的初始化、定義需要使用的參數
            2. Act  :  呼叫被測試的方法
            3. Assert  :  驗證結果


            專案
            如果有一個專案叫TestNinja  那要有個測試專案叫 TestNinja.UnitTests

            類名          Reservation          ReservationTests

            如果一個方法變因很多也可以把該方法獨立一個類 (類名_方法名)
        */
        [Test]
        public void CanBeCancelledBy_UserAdmin_ReturnsTrue()
        {
            //Arrange
            var reservation = new Reservation();
            //Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //Assert
            /*
                有多種寫法，挑一個喜歡的寫法就好
            */
            Assert.That(result, Is.True);
            //Assert.IsTrue(result);
            //Assert.That(result == true);
        }
        [Test]
        public void CanBeCancelledBy_SameUserIsCancellingTheReservation_ReturnsTrue()
        {
            var user=new User();
            var reservation = new Reservation() {MadeBy=user };

            var result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserIsCancellingTheReservation_ReturnsFalse()
        {
            var user = new User();
            var reservation = new Reservation() { MadeBy = user };

            var result = reservation.CanBeCancelledBy(new User());

            Assert.IsFalse(result);
        }

        /*
            良好的單元測試
            1. 不要再測試裡面寫邏輯判斷(if else)

            那些不要測試
            1. 三方庫的代碼
            2. c#語言本身的功能
        */
    }
}