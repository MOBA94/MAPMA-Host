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
    public class Employee : Person {

        /// <summary>
        /// The field for address and its Get And Set
        /// </summary>
        [DataMember]
        public string address {
            get ; set ;
        }

        /// <summary>
        /// The field for zipcode and its Get And Set
        /// </summary>
        [DataMember]
        public int zipcode {
            get ; set ;
        }

        /// <summary>
        /// The field for cityName and its Get And Set
        /// </summary>
        [DataMember]
        public string cityName {
            get ; set ;
        }

        /// <summary>
        /// The field for region and its Get And Set
        /// </summary>
        [DataMember]
        public string region {
            get ; set;
        }

        /// <summary>
        /// The field for employeeID and its Get And Set
        /// </summary>
        [DataMember]
        public int employeeID {
            get ; set ;
        }

        /// <summary>
        /// Contructor for Employee with all the parameters
        /// </summary>
        /// <param name="address">the address of the employee</param>
        /// <param name="zipCode">the zipCode of the employee</param>
        /// <param name="cityName">the citryName of the employee</param>
        /// <param name="employeeID">the employees ID</param>
        /// <param name="firstName">the employees first name</param>
        /// <param name="lastName">the employees last name</param>
        /// <param name="mail">the employees mail</param>
        /// <param name="phone">the employees phone number</param>
        public Employee(string address, int zipCode, string cityName, int employeeID, string firstName, string lastName, string mail, string phone) : base(firstName, lastName, mail, phone) {
            this.address = address;
            this.zipcode = zipcode;
            this.cityName = cityName;
            this.region = region;
            this.employeeID = employeeID;
        }

        /// <summary>
        /// An empty contructor for Employee
        /// </summary>
        public Employee ( )
        {
        }
    }
}
