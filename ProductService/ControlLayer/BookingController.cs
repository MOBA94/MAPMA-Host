﻿using System;
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

        public int Create(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            List<TimeSpan> checklist = ERCon.FreeTimes(ER_ID, Bdate);
            if (checklist.Count == 0) {
                return 0;
            }
            else {
                Booking tempBook = new Booking {
                    emp = ECon.Get(EmpID),
                    cus = CusCon.Get(username),
                    er = ERCon.GetForOwner(ER_ID)
                };
                tempBook.bookingTime = bookTime;
                tempBook.amountOfPeople = AOP;
                tempBook.date = Bdate;

                dbBook.Create(tempBook);
                return 1;
            }
            
        }

        public void Delete(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate) {
            
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
            return dbBook.GetAll();
        }

        public void Update(Booking entity) {
            throw new NotImplementedException();
        }

        public List<Booking> CheckBooking(int EscID, DateTime Bdate) {
            return dbBook.CheckBooking(EscID, Bdate);
        }
    }
}
