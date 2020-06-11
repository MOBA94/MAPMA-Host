using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccessLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    interface IBOOKING<T> {
        int Create(T entity);
        Booking Get(int EscID, string username, DateTime Bdate);
        List<Booking> CheckBooking(int EscID,  DateTime Bdate);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(Booking book);
        IEnumerable<T> GetAllFromUser(string username);
        IEnumerable<Booking> GetAllOneRoom ( int EscId );
    }
}
