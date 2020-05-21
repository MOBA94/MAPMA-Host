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
    interface IESCAPEROOM <T> {

        void Create(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId,byte[] img);

        void Delete(int id);

        T GetForOwner(int ER_ID);

        IEnumerable<T> GetAllForOwner();

        void Update(T entity);
    }
}
