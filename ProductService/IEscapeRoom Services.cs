using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {

    [ServiceContract (Namespace = "http://localhost:8735/Design_Time_Addresses/Escaperoom/")]
    public interface IEscapeRoom_Services {

        [OperationContract]
        EscapeRoom GetForOwner(int ER_ID);

        [OperationContract]
        IEnumerable<EscapeRoom> GetAllForOwner();

        [OperationContract]
        void CreateEscapeRoom ( string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, byte[] img );

        [OperationContract]
        void DeleteEscapeRoom ( int ER_ID );

        [OperationContract]
        List<TimeSpan> FreeTimes(int ER_ID, DateTime Bdate);

        [OperationContract]
        void Update(string name, string description, decimal maxClearTime, decimal cleanTime, decimal price, decimal rating, int empId, int EscId);
    }
}
