using System;
using System.Collections.Generic;
using System.ServiceModel;
using ModelLayer;

namespace ProductService {

    [ServiceContract]
    public interface IProductService {

        [OperationContract]
        List<Product> GetAll();

    }
}
