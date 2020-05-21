using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    [DataContract]
   public class Customer : Person {

        /// <summary>
        /// The field for customerNo and its Get And Set
        /// </summary>
        [DataMember]
        public int customerNo {
            get; set;
        }

        /// <summary>
        /// The field for password and its Get And Set
        /// </summary>
        [DataMember]
        public string password {
            get; set;
        }

        /// <summary>
        /// The field for username and its Get And Set
        /// </summary>
        [DataMember]
        public string username {
            get; set;
        }

        /// <summary>
        /// Constuctor for Customer that has all parameters
        /// </summary>
        /// <param name="customerNo">The customer number </param>
        /// <param name="password">The hashed password</param>
        /// <param name="username">the username of the customer</param>
        /// <param name="FirstName">the first name of the customer</param>
        /// <param name="LastName">the last name of the customer</param>
        /// <param name="Mail">the mail of the customer</param>
        /// <param name="Phone">the phone number of the customer</param>
        public Customer(int customerNo, string password, string username, string FirstName, string LastName, string Mail, string Phone): base (FirstName, LastName, Mail, Phone) {
            this.customerNo = customerNo;
            this.password = password;
            this.username = username;
        }

        /// <summary>
        /// An empty constructor for Customer
        /// </summary>
        public Customer ( ) {
        }
    }
}
