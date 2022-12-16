using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    //如果按照 要測試的函數名_控制變因_預期結果 的命名方式，會有點太長了
    //如果一個函式友可能很多的結果，可以用這種命名方式
    public class BookingHelper_OverlappingBookingsExistTests
    {
        [Test]
        public void BookingStartsAndFinishedBeforeAnExistingBooking_ReturnEmptyString()
        {
            var repository = new Mock<IBookingRepository>();
            repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                new Booking()
                {
                    Id=2,
                    ArrivalDate=new DateTime(2022,12,6,14,0,0),
                    DepartureDate=new DateTime(2022,12,10,10,0,0),
                    Reference="a"
                }
            }.AsQueryable());


            var result=BookingHelper.OverlappingBookingsExist(new Booking 
            { 
                Id = 1,
                ArrivalDate = new DateTime(2022, 12, 1, 14, 0, 0), // 傳入一個預訂日期比較早的
                DepartureDate = new DateTime(2022, 12, 5, 10, 0, 0),
            }, 
            repository.Object);

            Assert.That(result, Is.Empty);
        }
    }
}
