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
    public abstract class Person {

        /// <summary>
        /// The field for firstName and its Get And Set
        /// </summary>
        [DataMember]
        public string firstName {
            get; set;
        }

        /// <summary>
        /// The field for lastName and its Get And Set
        /// </summary>
        [DataMember]
        public string lastName {
            get ; set ;
        }

        /// <summary>
        /// The field for mail and its Get And Set
        /// </summary>
        [DataMember]
        public string mail {
            get; set ;
        }

        /// <summary>
        /// The field for phone and its Get And Set
        /// </summary>
        [DataMember]
        public string phone {
            get ; set ;
        }

        /// <summary>
        /// Constructor for Person with all parameters.
        /// </summary>
        /// <param name="FirstName">the first name of the person</param>
        /// <param name="LastName">the last name of the person</param>
        /// <param name="Mail">the mail of the person</param>
        /// <param name="Phone">the phone number of the person</param>
        public Person(string FirstName, string LastName, string Mail, string Phone) {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.mail = Mail;
            this.phone = Phone;
        }

        /// <summary>
        /// An empty constructor for Person.
        /// </summary>
        public Person()
        {

        }
    }
}
