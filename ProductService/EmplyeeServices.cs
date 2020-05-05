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
    public class EmplyeeServices : IEmplyeeServices {

    public Employee Get(int id) {
        EmployeeController EmpCon = new EmployeeController();

        return EmpCon.Get(id);
    }

        public IEnumerable<Employee> GetAll() {
            EmployeeController EmpCon = new EmployeeController();

            return EmpCon.getAll();
        }

        

    }
}
