using System;
using System.Collections.Generic;

namespace ProductService.DataAccessLayer {

    interface ICRUD<T> {
        void Create(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id);
    }

}
