using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccessLayer {
    interface IBOOKING<T> {
        void Create(T entity);
        Booking Get(int EscID, string username, DateTime Bdate);
        List<Booking> CheckBooking(int EscID,  DateTime Bdate);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(Booking book);
        IEnumerable<T> GetAllFromUser(string username);
        IEnumerable<Booking> GetAllOneRoom ( int EscId );
    }
}
