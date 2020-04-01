using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.DataAccessLayer {
    class CustomerController {
        private DBCustomer DBcus;

        public CustomerController () {
            DBcus = new DBCustomer();
        }
        public void Create(Customer cus) {
            throw new NotImplementedException();
        }

        public void Delete(string username) {
            throw new NotImplementedException();
        }

        public Customer Get(string username) {
            return DBcus.Get(username);
        }

        public IEnumerable<Customer> GetAll() {
            throw new NotImplementedException();
        }

        public void Update(Customer entity) {
            throw new NotImplementedException();
        }
    }
}
