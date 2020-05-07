using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {
    [ServiceContract (Namespace = "http://localhost:8734/Design_Time_Addresses/Booking")]
    public interface IBookingServices {

        [OperationContract]
        int Create(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate);

        [OperationContract]
        Booking Get(int EscID, string username, DateTime Bdate);

        [OperationContract]
        IEnumerable<Booking> GetAll();

        [OperationContract]
        void Delete(int EmpID, string username, int ER_ID, TimeSpan bookTime, int AOP, DateTime Bdate);
        
        [OperationContract]
        IEnumerable<Booking> GetAllFromUser(string username);
    }
}
