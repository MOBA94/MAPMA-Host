using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.ControlLayer;

namespace ProductService {
    [ServiceBehavior(Namespace = "http://localhost:8735/Design_Time_Addresses/Escaperoom/")]
    public class EscapeRoom_Services : IEscapeRoom_Services {

        public EscapeRoom GetForOwner(int ER_ID) {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetForOwner(ER_ID);
        }

        public IEnumerable<EscapeRoom> GetAllForOwner() {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetAllForOwner();
        }

        public void CreateEscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId) {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.CreateRoomOwner(name, description, maxClearTime, cleanTime, price, rating, empId);
        }
    }
}
