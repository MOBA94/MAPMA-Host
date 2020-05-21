using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

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
    interface IEMPLOYEE<T> {
        void Create(Employee entity);
        Employee Get(int id);
        IEnumerable<Employee> GetAll();
        void Update(Employee entity);
        void Delete(int id);
    }
}
