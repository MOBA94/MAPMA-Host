using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {
    [ServiceContract]
    interface IBookingServices {

        [OperationContract]
        void Create(int EmpID, string username, int ER_ID, DateTime bookTime, int AOP, DateTime Bdate);
    }
}
