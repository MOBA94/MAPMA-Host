using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ModelLayer {

    [DataContract]
    public class Product {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public Product() {
        }

        public Product(string title, decimal price) {
            Title = title;
            Price = price;
        }

        public Product(int id, string title, decimal price) : this(title, price) {
            Id = id;
        }

        public override string ToString() {
            return $"{Id} {Title} ({Price})";
        }

    }
}
