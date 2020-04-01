using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccessLayer {
    interface IESCAPEROOM <T> {

        void Create(T entity);

        void Delete(int id);

        T GetForOwner(int ER_ID);

        IEnumerable<T> GetAllForOwner();

        void Update(T entity);
    }
}
