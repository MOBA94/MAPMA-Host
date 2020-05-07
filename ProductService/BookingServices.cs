using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;
namespace ProductService {
   [ServiceBehavior(Namespace = "http://localhost:8734/Design_Time_Addresses/Booking")]
    public class BookingServices : IBookingServices {

        public int Create(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {

            BookingController BCon = new BookingController();

            return BCon.Create(EmpID, username, ER_ID, bookTime, AOP, Bdate);
        }

        public void Delete(int EscID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            BookingController BCon = new BookingController();
            BCon.Delete(EscID, username, ER_ID, bookTime, AOP, Bdate);
        }

        public Booking Get(int EmpID, string username, DateTime Bdate) {
            BookingController BCon = new BookingController();

            return BCon.Get(EmpID, username, Bdate);
        }

        public IEnumerable<Booking> GetAll ( )
        {
            BookingController bookCon = new BookingController();

            return bookCon.GetAll();
        }

        public void Update(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate, int bookId) {
            BookingController bookCon = new BookingController();
            bookCon.Update(EmpID, username, ER_ID, bookTime, AOP, Bdate, bookId);
        }
    }
}
