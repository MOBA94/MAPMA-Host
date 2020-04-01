using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    class EscapeRoomController {
        private DBEscapeRoom DBER;

        public EscapeRoomController() {
            DBER = new DBEscapeRoom();
        }

        public EscapeRoom GetForOwner(int ER_ID) {
            return DBER.GetForOwner(ER_ID);
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {
            return DBER.GetAllForOwner();
        }
    }
}
