using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer {

    [DataContract]
   public class Customer : Person { 

        [DataMember]
        public int customerNo {
            get; set;
        }

        [DataMember]
        public string password {
            get; set;
        }

        [DataMember]
        public string username {
            get; set;
        }
    }
}
