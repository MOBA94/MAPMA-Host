﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    class BookingController : ICRUD<Booking> {
        private CustomerController CusCon;
        private EscapeRoomController ERCon;
        private IBOOKING dbBook;

        public BookingController() {
            CusCon = new CustomerController();
            ERCon = new EscapeRoomController();
            dbBook = new IBOOKING();
        }

        public void Create(Booking book) {
            dbBook.Create(book);
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public Booking Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Booking entity) {
            throw new NotImplementedException();
        }
    }
}