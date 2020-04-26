using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

 namespace ProductService.ControlLayer {
   class EscapeRoomController {
        private IESCAPEROOM<EscapeRoom> DBER;

        public EscapeRoomController() {
            DBER = new DBEscapeRoom();
        }

        public EscapeRoom GetForOwner(int ER_ID) {
            return DBER.GetForOwner(ER_ID);
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {
            return DBER.GetAllForOwner();
        }

        public List<TimeSpan> GetTimesForEscapeRoom(int ER_ID) {
            List<TimeSpan> Time = new List<TimeSpan>();
            TimeSpan onetime = new TimeSpan();

            var hours = 0;
            var minutes = 0;            
             EscapeRoom esc =  GetForOwner(ER_ID);
            for (var i = 960; i <= 1330; i += decimal.ToInt32(esc.maxClearTime) + decimal.ToInt32( esc.cleanTime) ) {
                hours = (i / 60);
                minutes = i % 60;
                if (minutes < 10) {
                    minutes = '0' + minutes; // adding leading zero to minutes portion
                }

                Console.WriteLine(hours +","+ minutes);
                onetime(hours, minutes, 00);
                //add the value to dropdownlist
                select.append($( '<option></option>' )
                    .attr('value', hours)
                    (hours , minutes));
            }

            return Time;
        }
    }
}
