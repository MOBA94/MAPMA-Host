using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ProductService.DataAccessLayer {
    interface IEMPLOYEE<T> {
        void Create(Employee entity);
        Employee Get(int id);
        IEnumerable<Employee> GetAll();
        void Update(Employee entity);
        void Delete(int id);
    }
}
