using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {

    [ServiceContract]
    interface IEscapeRoom_Services {

        [OperationContract]
        EscapeRoom GetForOwner(int ER_ID);

        [OperationContract]
        IEnumerable<EscapeRoom> GetAllForOwner();
    }
}
