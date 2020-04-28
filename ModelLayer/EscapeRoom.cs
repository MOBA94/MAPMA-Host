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
        [DataMember]
        public List<TimeSpan> AvalibleTimes {
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
           this.AvalibleTimes = AddTimesToList();
        }
        public EscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price) {
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            checkList = new List<string>();
            this.AvalibleTimes = AddTimesToList();
        }

        public EscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId) {
            this.name = name;
            this.price = price;
            this.cleanTime = cleanTime;
            this.maxClearTime = maxClearTime;
            this.description = description;
            this.rating = rating;
            this.emp.employeeID = empId;
            this.AvalibleTimes = AddTimesToList();
        }

        public EscapeRoom() {
            checkList = new List<string>();
            this.AvalibleTimes = AddTimesToList();
        }

        public void AddToList(string item) {
            checkList.Add(item);
        }

        public List<TimeSpan> AddTimesToList() {
            TimeSpan time1, time2, time3, time4, time5;
            time1 = new TimeSpan(16, 00, 00);
            time2 = new TimeSpan(18, 00, 00);
            time3 = new TimeSpan(20, 00, 00);
            time4 = new TimeSpan(22, 00, 00);
            time5 = new TimeSpan(00, 00, 00);

            List < TimeSpan > TimeList = new List<TimeSpan>() { time1, time2, time3, time4, time5 };

            return TimeList;


        }
    }
}
