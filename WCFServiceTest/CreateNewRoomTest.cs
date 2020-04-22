using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService;

namespace WCFServiceTest
{
    [TestClass]
    public class CreateNewRoomTest
    {
        [TestMethod]
        public void CreateRoom ( )
        {

            //Arrange
            IEscapeRoom_Services ess = new EscapeRoom_Services();
            IEmplyeeServices empSer = new EmplyeeServices();

            ModelLayer.EscapeRoom Es1 = new ModelLayer.EscapeRoom();
            ModelLayer.Employee Em1 = empSer.Get(1);
            Es1.name = "The Dark Room";
            Es1.description = "The Fors is Strong in the darkside";
            Es1.maxClearTime = 180;
            Es1.cleanTime = 90;
            Es1.price = 1200;
            Es1.rating = 0;
            Es1.emp = Em1;

            //Act
            ess.CreateEscapeRoom(Es1.name, Es1.description, Es1.maxClearTime, Es1.cleanTime, Es1.price, Es1.rating, Es1.emp.employeeID);

            //Assert
            List<ModelLayer.EscapeRoom> escapeRooms = new List<ModelLayer.EscapeRoom>();
            escapeRooms = (List<ModelLayer.EscapeRoom>)ess.GetAllForOwner();
            bool found = false;
            int i = 0;
            ModelLayer.EscapeRoom EsR = new ModelLayer.EscapeRoom();

            while (i < escapeRooms.Count && !found)
            {
                if (escapeRooms.ElementAt(i).name.Equals(Es1.name))
                {
                    EsR = escapeRooms.ElementAt(i);
                    found = true;
                }
                else
                {
                    i++;
                }
            }

            Assert.IsTrue(found);

            if (EsR.escapeRoomID != 0)
            {
                ess.DeleteEscapeRoom(EsR.escapeRoomID);
            }
        }
    }
}
