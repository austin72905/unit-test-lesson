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
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArriveOn(2022, 12, 6),
                DepartureDate = DepartOn(2022, 12, 10),
                Reference = "a"
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());


        }

        [Test]
        public void BookingStartsAndFinishedBeforeAnExistingBooking_ReturnEmptyString()
        {
            
            var result=BookingHelper.OverlappingBookingsExist(new Booking 
            { 
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate,days:2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
            },
            _repository.Object);

            Assert.That(result, Is.Empty);
        }


        [Test]
        public void BookingStartsBeforeAndFinishedInTheMiddleOfAnExistingBooking_ReturnExistedBookingReference()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate),
            },
            _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishedAfterAnExistingBooking_ReturnExistedBookingReference()
        {

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate),
            },
            _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }



        private DateTime ArriveOn(int year,int month,int day)
        {
            return new DateTime(year,month,day,14,0,0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime,int days=1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime)
        {
            return dateTime.AddDays(1);
        }
    }
}
