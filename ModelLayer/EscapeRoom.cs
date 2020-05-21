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
    public class EscapeRoom {

        /// <summary>
        /// The field for escapeRoomID and its Get And Set
        /// </summary>
        [DataMember]
        public int escapeRoomID {
            get; set;
        }

        /// <summary>
        /// The field for name and its Get And Set
        /// </summary>
        [DataMember]
        public string name {
            get; set;
        }

        /// <summary>
        /// The field for price and its Get And Set
        /// </summary>
        [DataMember]
        public decimal price {
            get; set;
        }

        /// <summary>
        /// The field for rating and its Get And Set
        /// </summary>
        [DataMember]
        public decimal rating {
            get; set;
        }

        /// <summary>
        /// The field for escapeRoomID and its Get And Set
        /// </summary>
        [DataMember]
        public decimal cleanTime {
            get; set;
        }

        /// <summary>
        /// The field for maxClearTime and its Get And Set
        /// </summary>
        [DataMember]
        public decimal maxClearTime {
            get; set;
        }

        /// <summary>
        /// The field for checkList and its Get And Set
        /// </summary>
        [DataMember]
        public List<string> checkList {
            get; set;
        }

        /// <summary>
        /// The field for description and its Get And Set
        /// </summary>
        [DataMember]
        public string description {
            get; set;
        }

        /// <summary>
        /// The field for the asociated Employee and its Get And Set
        /// </summary>
        [DataMember]
        public Employee emp {
            get; set;
        }

        /// <summary>
        /// The field for the list AvalibleTimes and its Get And Set
        /// </summary>
        [DataMember]
        public List<TimeSpan> AvalibleTimes {
            get; set;
        }

        /// <summary>
        /// The field for the bytes of an Image and its Get And Set
        /// </summary>
        [DataMember]
        public byte[] Image {
            get; set;
        }

        /// <summary>
        /// Constructor for EcapeRoom with all parameters
        /// </summary>
        /// <param name="escapeRoomID">The id of an escaperoom</param>
        /// <param name="name">the name of the escaperoom</param>
        /// <param name="description">the description of an escaperoom</param>
        /// <param name="maxClearTime">the max clear time for the escaperoom</param>
        /// <param name="cleanTime">the cleaning time for the esacperoom</param>
        /// <param name="price">the price of an escaperoom</param>
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

        /// <summary>
        /// an empty constructor for escaperoom
        /// </summary>
        public EscapeRoom() {
            checkList = new List<string>();
            this.AvalibleTimes = AddTimesToList();
        }

        /// <summary>
        /// Adds a string item to the list checkList
        /// </summary>
        /// <param name="item">The name of the item</param>
        public void AddToList(string item) {
            checkList.Add(item);
        }

        /// <summary>
        /// Adds some default times to the list TimeList
        /// </summary>
        /// <returns>returns the timeList with the added values</returns>
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
