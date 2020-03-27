using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer {

    [DataContract]
    public class Employee : Person {

        [DataMember]
        public string address {
            get ; set ;
        }
        [DataMember]
        public int zipcode {
            get ; set ;
        }
        [DataMember]
        public string cityName {
            get ; set ;
        }
        [DataMember]
        public string region {
            get ; set;
        }
        [DataMember]
        public int employeeID {
            get ; set ;
        }
        public Employee(string address, int zipCode, string cityName, int employeeID, string firstName, string lastName, string mail, string phone) : base(firstName, lastName, mail, phone) {
            this.address = address;
            this.zipcode = zipcode;
            this.cityName = cityName;
            this.region = region;
            this.employeeID = employeeID;
        }
    }
}
