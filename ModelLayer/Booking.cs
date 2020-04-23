using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ModelLayer
{
    [DataContract]
    public class Booking
    {
        [DataMember]
        public int amountOfPeople
        {
            get; set;
        }
        [DataMember]
        public DateTime date { get; set; }
        [DataMember]
        public TimeSpan bookingTime { get; set; }
        [DataMember]
        public EscapeRoom er { get; set; }
        [DataMember]
        public Customer cus { get; set; }
        [DataMember]
        public Employee emp { get; set; }

        public Booking(int amountOfPeople, DateTime date, TimeSpan bookingTime)
        {
            this.amountOfPeople = amountOfPeople;
            this.date = date;
            this.bookingTime = bookingTime;
        }

        public Booking(int amountOfPeople, DateTime date, TimeSpan bookingTime, Customer cus, EscapeRoom er)
        {
            this.amountOfPeople = amountOfPeople;
            this.date = date;
            this.bookingTime = bookingTime;
            this.cus = cus;
            this.er = er;

        }

        public Booking(int amountOfPeople, DateTime date, TimeSpan bookingTime, Employee emp, EscapeRoom er)
        {
            this.amountOfPeople = amountOfPeople;
            this.date = date;
            this.bookingTime = bookingTime;
            this.emp = emp;
            this.er = er;
        }

        public Booking() {
        
        }
    }
}
