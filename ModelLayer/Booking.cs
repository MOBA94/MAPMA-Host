using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer
{

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
    public class Booking
    {

        /// <summary>
        /// The field for amountOfPeople and its Get And Set
        /// </summary>
        [DataMember]
        public int amountOfPeople
        {
            get; set;
        }

        /// <summary>
        /// The field for date and its Get And Set
        /// </summary>
        [DataMember]
        public DateTime date { get; set; }

        /// <summary>
        /// The field for bookingTime and its Get And Set
        /// </summary>
        [DataMember]
        public TimeSpan bookingTime { get; set; }

        /// <summary>
        /// The field for the associated escaperoom and its Get And Set
        /// </summary>
        [DataMember]
        public EscapeRoom er { get; set; }

        /// <summary>
        /// The field for the associated customer and its Get And Set
        /// </summary>
        [DataMember]
        public Customer cus { get; set; }

        /// <summary>
        /// The field for the associated employee and its Get And Set
        /// </summary>
        [DataMember]
        public Employee emp { get; set; }

        /// <summary>
        /// The field for the booking id and its Get And Set
        /// </summary>
        [DataMember]
        public int Id {
            get; set;
         }

        /// <summary>
        /// Constructor for Booking with all the parameters
        /// </summary>
        /// <param name="amountOfPeople">the amount of peple on the booking</param>
        /// <param name="date">the date of the booking</param>
        /// <param name="bookingTime">the time for the booking</param>
        /// <param name="cus">the customer on the booking</param>
        /// <param name="er">the booked escaperoom</param>
        /// <param name="emp">the employee on the booking</param>
        public Booking(int amountOfPeople, DateTime date, TimeSpan bookingTime, Customer cus, EscapeRoom er, Employee emp)
        {
            this.amountOfPeople = amountOfPeople;
            this.date = date;
            this.bookingTime = bookingTime;
            this.cus = cus;
            this.er = er;
            this.emp = emp;

        }

        /// <summary>
        /// An Empty constructor for Booking
        /// </summary>
        public Booking() {
        
        }
    }
}
