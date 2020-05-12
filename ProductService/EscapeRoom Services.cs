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

        public void CreateEscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, byte[] img) {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.CreateRoomOwner(name, description, maxClearTime, cleanTime, price, rating, empId, img);
        }

        public void DeleteEscapeRoom ( int ER_ID )
        {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.DeleteEscapeRoom(ER_ID);
        }

        public List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate) {
            EscapeRoomController EscCon = new EscapeRoomController();
            return EscCon.FreeTimes(ER_ID,Bdate);
        }

        public void Update(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, int EscId, byte[] img) {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.UpdateRoom(name, description, maxClearTime, cleanTime, price, rating, empId, EscId, img);
        }
    }
}
