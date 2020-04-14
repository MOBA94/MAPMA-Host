using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {
    [ServiceContract (Namespace = "http://localhost:8737/Design_Time_Addresses/Customer/")]
    public interface ICustomerServices {

        [OperationContract]
        Customer Get(string username);
    }
}
