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
    interface ICUSTOMER<T> {

        void Register(T entity);
        T Get(string username);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(string username);
        
    }

}
