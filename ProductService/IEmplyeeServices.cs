using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {

    [ServiceContract(Namespace = "http://localhost:8736/Design_Time_Addresses/Employee/")]
    public interface IEmplyeeServices {

        [OperationContract]
        Employee Get(int id);

        [OperationContract]
        IEnumerable<Employee> GetAll();
    }
}
