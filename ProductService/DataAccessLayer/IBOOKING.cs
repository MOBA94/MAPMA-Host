using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccessLayer {
    interface IBOOKING<T> {
        void Create(T entity);
        T Get(EscapeRoom er, Customer cus, DateTime Bdate);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(Booking book);
    }
}
