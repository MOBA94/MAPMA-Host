using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer {
    [DataContract]
    public class EscapeRoom {

        [DataMember]
        public int escapeRoomID {
            get; set;
        }

        [DataMember]
        public string name {
            get; set;
        }
        [DataMember]
        public decimal price {
            get; set;
        }
        [DataMember]
        public decimal rating {
            get; set;
        }
        [DataMember]
        public decimal cleanTime {
            get; set;
        }
        [DataMember]
        public decimal maxClearTime {
            get; set;
        }
        [DataMember]
        public List<string> checkList {
            get; set;
        }
        [DataMember]
        public string description {
            get; set;
        }
        [DataMember]
        public Employee emp {
            get; set;
        }

        public EscapeRoom(int escapeRoomID, string name, string description, decimal maxClearTime, decimal cleanTime, decimal price) {
            this.escapeRoomID = escapeRoomID;
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            checkList = new List<string>();
        }
        public EscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price) {
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            checkList = new List<string>();
        }

        public EscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId) {
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            this.rating = rating;
            this.emp.employeeID = empId;
        }

        public EscapeRoom() {
            checkList = new List<string>();
        }

        public void AddToList(string item) {
            checkList.Add(item);
        }
    }
}
