using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.ControlLayer;
using ModelLayer;

namespace ProductService {
    public class CustomerServices : ICustomerServices {

        public Customer Get(string username) {

            
            CustomerController CusCon = new CustomerController();

            return CusCon.Get(username);
        }
    }
}
