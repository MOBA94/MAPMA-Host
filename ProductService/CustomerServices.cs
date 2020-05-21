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

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class CustomerServices : ICustomerServices {
        /// <summary>
        /// the method to get a customer from the controller
        /// </summary>
        /// <param name="username">customers user name</param>
        /// <returns> customer</returns>
        public Customer Get(string username) {

            
            CustomerController CusCon = new CustomerController();

            return CusCon.Get(username);
        }
        /// <summary>
        /// the method to login from the web to the controller
        /// </summary>
        /// <param name="inputPassword">the password for the customers input fro the web</param>
        /// <param name="username">customers user name input for the web</param>
        /// <returns>a customer</returns>
        public Customer Login ( string inputPassword, string username )
        {
            CustomerController CusCon = new CustomerController();
            return CusCon.Login(inputPassword, username);

        }
        /// <summary>
        /// a method to create a customer to the controller
        /// </summary>
        /// <param name="cus"> all the info about the customer</param>
        /// <param name="password"> the chosen password for the customer</param>
        /// <returns>a number to tell if the user name are tacken</returns>
        public int Register ( Customer cus, string password )
        {
            CustomerController CusCon = new CustomerController();
            return CusCon.Register(cus,password);
        }
    }
}
