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

        private byte[] ConvertImgToBinary ( Image img )
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);    
                
                return ms.ToArray();
            }

        }

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

        public void DeleteEscapeRoom (int ER_ID){
            DBER.Delete(ER_ID);
        }

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
