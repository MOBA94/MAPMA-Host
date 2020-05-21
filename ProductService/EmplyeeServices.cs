using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;

namespace ProductService  {
    [ServiceBehavior(Namespace = "http://localhost:8736/Design_Time_Addresses/Employee/")]

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class EmplyeeServices : IEmplyeeServices {
        /// <summary>
        /// the method to get a employee
        /// </summary>
        /// <param name="id">employees id</param>
        /// <returns>a employee</returns>
    public Employee Get(int id) {
        EmployeeController EmpCon = new EmployeeController();

        return EmpCon.Get(id);
    }
        /// <summary>
        /// the method to get all employees
        /// </summary>
        /// <returns>a list whit all employees</returns>
        public IEnumerable<Employee> GetAll() {
            EmployeeController EmpCon = new EmployeeController();

            return EmpCon.getAll();
        }

        

    }
}
