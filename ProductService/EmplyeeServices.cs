using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;

namespace ProductService  {
    public class EmplyeeServices : IEmplyeeServices {

    public Employee Get(int id) {
        EmployeeController EmpCon = new EmployeeController();

        return EmpCon.Get(id);
    }



    }
}
