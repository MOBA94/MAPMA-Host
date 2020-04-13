using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.ControlLayer;
namespace ProductService {
    class BookingServices : IBookingServices {

        public void Create(int EmpID, string username, int ER_ID, DateTime bookTime, int AOP, DateTime Bdate) {

            BookingController BCon = new BookingController();

            BCon.Create(EmpID, username, ER_ID, bookTime, AOP, Bdate);
        }
    }
}
