using System;
using System.Collections.Generic;
using ModelLayer;
using ProductService.ControlLayer;

namespace ProductService {
     
    public class ProductService : IProductService {
        public List<Product> GetAll() {

            ProductController pc = new ProductController();

            List<Product> foundProducts = (List<Product>)pc.GetAll();

            return foundProducts;
        }
    }
}
