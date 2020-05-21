using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.DataAccessLayer;
using ModelLayer;

namespace ProductService.ControlLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    class EmployeeController {
        private IEMPLOYEE<Employee> DBE;

        /// <summary>
        /// the constuctor for EmployeeController
        /// </summary>
        public EmployeeController() {
            DBE = new DBEmployee();
        }

        /// <summary>
        /// the method to get a employees id
        /// </summary>
        /// <param name="id"> employees id</param>
        /// <returns>a employee</returns>
        public Employee Get(int id) {
            return DBE.Get(id);
        }

        /// <summary>
        /// the metthod to get all the employees
        /// </summary>
        /// <returns>a list whit employees</returns>
        public IEnumerable<Employee> getAll() {
            return DBE.GetAll();
        }
    }
}
