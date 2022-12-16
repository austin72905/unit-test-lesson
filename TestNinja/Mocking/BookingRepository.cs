using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
    public class BookingRepository: IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId=null)
        {
            var unitOfWork = new UnitOfWork(); //這邊 有點向是查詢資料庫的行為，可以用 Repository 來獨立出來
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            // int? 可以用 HasValue 來判斷是否有值
            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id == excludedBookingId.Value);

            return bookings;


        }
    }
}
