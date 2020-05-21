using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;
using ModelLayer;
using System.Drawing;
using System.IO;

namespace ProductService.ControlLayer {


    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class EscapeRoomController {
        private IESCAPEROOM<EscapeRoom> DBER;

        /// <summary>
        /// the contructor for EscaperoomController
        /// </summary>
        public EscapeRoomController() {
            DBER = new DBEscapeRoom();
            
           
        }

        /// <summary>
        /// the method to get a escaperoom 
        /// </summary>
        /// <param name="ER_ID">escaperooms id</param>
        /// <returns>a EscapeRoom</returns>
        public EscapeRoom GetForOwner(int ER_ID) {
            return DBER.GetForOwner(ER_ID);
        }

        /// <summary>
        /// the method to get all the escaperooms 
        /// </summary>
        /// <returns>a list of escaperooms</returns>
        public IEnumerable<EscapeRoom> GetAllForOwner() {
            return DBER.GetAllForOwner();
        }

        /// <summary>
        /// the method to convert a image to bytes saved in a folder
        /// </summary>
        /// <param name="img">a Image</param>
        /// <returns>a byte array of the image</returns>
        private byte[] ConvertImgToBinary ( Image img )
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);    
                
                return ms.ToArray();
            }

        }

        /// <summary>
        /// the method to create a escaperoom whit the parameters 
        /// </summary>
        /// <param name="name">the name off the escape-room</param>
        /// <param name="description">the description of the escape-room</param>
        /// <param name="maxClearTime">the time for how long the customer have the room</param>
        /// <param name="cleanTime">the time for preparing the room to a new customer</param>
        /// <param name="price"> the price</param>
        /// <param name="rating"> the rating there is set to 0 at start</param>
        /// <param name="empId"> Employees id</param>
        /// <param name="img"> a image</param>
        public void CreateRoomOwner (string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId ,byte[] img) {

            if(img != null) {                
                DBER.Create(name, description, maxClearTime, cleanTime, price, rating, empId, img);
            }
            else {
                string path = System.Windows.Forms.Application.StartupPath.Substring(0, (System.Windows.Forms.Application.StartupPath.Length - 10));
                Bitmap bm = new Bitmap(path + "\\Pictures\\MAPMA_PIC.jpg");
                byte[] Dreail = ConvertImgToBinary(bm);

                img = Dreail;
                DBER.Create(name, description, maxClearTime, cleanTime, price, rating, empId, img);
            }
           
        }
        /// <summary>
        /// the method to delete a escape-room
        /// </summary>
        /// <param name="ER_ID">escape-rooms id</param>
        public void DeleteEscapeRoom (int ER_ID){
            DBER.Delete(ER_ID);
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
        public void UpdateRoom (string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, int escId, byte[] img) {
            EmployeeController ec = new EmployeeController();
            EscapeRoom ER = new EscapeRoom() {
                escapeRoomID = escId,
                name = name,
                description = description,
                maxClearTime = maxClearTime,
                cleanTime = cleanTime,
                price = price,
                rating = rating,
                emp = ec.Get(empId),
                Image = img
            };
            
            DBER.Update(ER);
        }

        /// <summary>
        /// a method to give all the time not tacking in that day in that escape-room
        /// </summary>
        /// <param name="ER_ID">escape-rooms id</param>
        /// <param name="Bdate">the date of a day</param>
        /// <returns>a list of the times a customer can chose from an book the time</returns>
        public List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate) {           
            List<Booking> bkl = new List<Booking>();
            EscapeRoom est = new EscapeRoom();
            BookingController bc = new BookingController();
            bkl = bc.CheckBooking(ER_ID, Bdate);            
            TimeSpan time1, time2, time3, time4, time5;
            time1 = new TimeSpan(16, 00, 00);
            time2 = new TimeSpan(18, 00, 00);
            time3 = new TimeSpan(20, 00, 00);
            time4 = new TimeSpan(22, 00, 00);
            time5 = new TimeSpan(00, 00, 00);
            List<TimeSpan> newTimes = new List<TimeSpan>(){
                time1,
                time2,
                time3,
                time4,
                time5
            };
            foreach (var time in bkl) {

                if (time.bookingTime == time1){
                    newTimes.Remove(time1);
                }
                else if(time.bookingTime == time2) {
                    newTimes.Remove(time2);
                }
                else if (time.bookingTime == time3) {
                    newTimes.Remove(time3);
                }
                else if (time.bookingTime == time4) {
                    newTimes.Remove(time4);
                }
                else if (time.bookingTime == time5) {
                    newTimes.Remove(time5);
                }
                else {
                }
            }
            return newTimes;
        }

    }
}
