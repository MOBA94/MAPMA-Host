using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {

    [ServiceContract]
    interface IEmplyeeServices {

        [OperationContract]
        Employee Get(int id);
    }
}
