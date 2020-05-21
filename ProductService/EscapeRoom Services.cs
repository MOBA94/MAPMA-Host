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

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class EscapeRoom_Services : IEscapeRoom_Services {

        /// <summary>
        /// the method to get a escpa-room whit the id
        /// </summary>
        /// <param name="ER_ID">escape-rooms id</param>
        /// <returns>a escape-room</returns>
        public EscapeRoom GetForOwner(int ER_ID) {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetForOwner(ER_ID);
        }

        /// <summary>
        /// the method to get all the escape-rooms
        /// </summary>
        /// <returns>a list whit all the escape-rooms</returns>
        public IEnumerable<EscapeRoom> GetAllForOwner() {

            EscapeRoomController EscCon = new EscapeRoomController();

            return EscCon.GetAllForOwner();
        }

        /// <summary>
        /// the method to create a escape-room whit the parameters
        /// </summary>
        /// <param name="name">the name off the escape-room</param>
        /// <param name="description">the description of the escape-room</param>
        /// <param name="maxClearTime">the time for how long the customer have the room</param>
        /// <param name="cleanTime">the time for preparing the room to a new customer</param>
        /// <param name="price"> the price</param>
        /// <param name="rating"> the rating there is set to 0 at start</param>
        /// <param name="empId"> Employees id</param>
        /// <param name="img"> a image</param>
        public void CreateEscapeRoom(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, byte[] img) {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.CreateRoomOwner(name, description, maxClearTime, cleanTime, price, rating, empId, img);
        }

        /// <summary>
        /// the method to delete a escape-room by id
        /// </summary>
        /// <param name="ER_ID">escape-rooms id</param>
        public void DeleteEscapeRoom ( int ER_ID )
        {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.DeleteEscapeRoom(ER_ID);
        }

        /// <summary>
        /// a method to give all the time not tacking in that day in that escape-room
        /// </summary>
        /// <param name="ER_ID">escape-rooms id</param>
        /// <param name="Bdate">the date of a day</param>
        /// <returns>a list of the times a customer can chose from an book the time</returns>
        public List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate) {
            EscapeRoomController EscCon = new EscapeRoomController();
            return EscCon.FreeTimes(ER_ID,Bdate);
        }

        /// <summary>
        /// the method to update the escape-rooms parameters
        /// </summary>
        /// <param name="name">the name off the escape-room</param>
        /// <param name="description">the description of the escape-room</param>
        /// <param name="maxClearTime">the time for how long the customer have the room</param>
        /// <param name="cleanTime">the time for preparing the room to a new customer</param>
        /// <param name="price"> the price</param>
        /// <param name="rating"> the rating there is set to 0 at start</param>
        /// <param name="empId">Employees id</param>
        /// <param name="escId">escape-rooms id</param>
        /// <param name="img"> a image</param>
        public void Update(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, int EscId, byte[] img) {
            EscapeRoomController EscCon = new EscapeRoomController();
            EscCon.UpdateRoom(name, description, maxClearTime, cleanTime, price, rating, empId, EscId, img);
        }
    }
}
