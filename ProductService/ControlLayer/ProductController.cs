using System;
using System.Collections.Generic;
using ModelLayer;
using ProductService.DataAccessLayer;

namespace ProductService.ControlLayer {
    public class ProductController : ICRUD<Product> {

        private DbProduct _dbProduct;

        public ProductController() {
            _dbProduct = new DbProduct();
        }

        public void Create(Product prodEntity) {
            _dbProduct.Create(prodEntity);
        }

        public void Delete(int id) {
            throw new NotImplementedException();
        }

        public Product Get(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll() {
            return _dbProduct.GetAll();
        }

        public void Update(Product entity) {
            throw new NotImplementedException();
        }
    }
}
