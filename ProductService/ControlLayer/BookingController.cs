using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    class BookingController {
        private CustomerController CusCon;
        private EscapeRoomController ERCon;
        private EmployeeController ECon;
        private IBOOKING<Booking> dbBook;

        public BookingController() {
            CusCon = new CustomerController();
            ERCon = new EscapeRoomController();
            ECon = new EmployeeController();
            dbBook = new DBBooking();
        }

        public void Create(int EmpID, string username, int ER_ID, DateTime bookTime, int AOP, DateTime Bdate) {

            Booking tempBook = new Booking {
                emp = ECon.Get(EmpID),
                cus = CusCon.Get(username),
                er = ERCon.GetForOwner(ER_ID)
            };
            tempBook.bookingTime = bookTime;
            tempBook.amountOfPeople = AOP;
            tempBook.date = Bdate;

            dbBook.Create(tempBook);
        }

        public void Delete(int EmpID, string username, int ER_ID, DateTime bookTime, int AOP, DateTime Bdate) {
            
            Booking tempBook = new Booking {
                emp = ECon.Get(EmpID),
                cus = CusCon.Get(username),
                er = ERCon.GetForOwner(ER_ID),
                amountOfPeople = AOP,
                bookingTime = bookTime,
                date = Bdate
            };

            dbBook.Delete(tempBook);
        }

        public Booking Get(int EscID, string username, DateTime Bdate) {
            return dbBook.Get(EscID, username, Bdate);
        }

        public IEnumerable<Booking> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Booking entity) {
            throw new NotImplementedException();
        }
    }
}
