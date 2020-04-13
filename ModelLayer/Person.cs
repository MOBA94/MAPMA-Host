using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace ModelLayer {

    [DataContract]
    public abstract class Person {
        [DataMember]
        public string firstName {
            get; set;
        }

        [DataMember]
        public string lastName {
            get ; set ;
        }

        [DataMember]
        public string mail {
            get; set ;
        }

        [DataMember]
        public string phone {
            get ; set ;
        }

        public Person(string FirstName, string LastName, string Mail, string Phone) {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.mail = Mail;
            this.phone = Phone;
        }

        public Person()
        {

        }
    }
}
