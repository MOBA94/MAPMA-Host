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

        public void CreateRoomOwner (string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId) {
            DBER.Create(name, description, maxClearTime, cleanTime, price, rating, empId);
        }

        public void DeleteEscapeRoom (int ER_ID){
            DBER.Delete(ER_ID);
        }
    }
}
