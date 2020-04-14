using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;

namespace ProductService {
    public class EscapeRoom_Services : IEscapeRoom_Services {

        public EscapeRoom GetForOwner(int ER_ID) {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetForOwner(ER_ID);
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetAllForOwner();
        }
    }
}
