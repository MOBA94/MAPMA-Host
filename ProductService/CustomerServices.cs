using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.ControlLayer;
using ModelLayer;
using System.ServiceModel;

namespace ProductService {
    [ServiceBehavior(Namespace = "http://localhost:8737/Design_Time_Addresses/Customer/")]
    public class CustomerServices : ICustomerServices {

        public Customer Get(string username) {

            
            CustomerController CusCon = new CustomerController();

            return CusCon.Get(username);
        }

        public Customer Login ( string inputPassword, string username )
        {
            CustomerController CusCon = new CustomerController();
            return CusCon.Login(inputPassword, username);
        }

        public void Register ( Customer cus, string password )
        {
            CustomerController CusCon = new CustomerController();
            CusCon.Register(cus,password);
        }
    }
}
