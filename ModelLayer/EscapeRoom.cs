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

        public EscapeRoom(int escapeRoomID, string name, decimal price, decimal cleanTime, decimal maxClearTime, string description) {
            this.escapeRoomID = escapeRoomID;
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            checkList = new List<string>();
        }
        public EscapeRoom(string name, decimal price, decimal cleanTime, decimal maxClearTime, string description) {
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            checkList = new List<string>();
        }

        public EscapeRoom() {
        }

        public void AddToList(string item) {
            checkList.Add(item);
        }
    }
}
