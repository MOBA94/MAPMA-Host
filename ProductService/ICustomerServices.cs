using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {
    [ServiceContract]
    interface ICustomerServices {

        [OperationContract]
        Customer Get(string username);
    }
}
