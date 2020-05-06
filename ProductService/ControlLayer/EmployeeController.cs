using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.DataAccessLayer;
using ModelLayer;

namespace ProductService.ControlLayer {
    class EmployeeController {
        private IEMPLOYEE<Employee> DBE;

        public EmployeeController() {
            DBE = new DBEmployee();
        }

        public Employee Get(int id) {
            return DBE.Get(id);
        }

        public IEnumerable<Employee> getAll() {
            return DBE.GetAll();
        }
    }
}
