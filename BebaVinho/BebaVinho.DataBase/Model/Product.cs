using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class Product
    {
        public Product()
        {
            this.ClientProducts = new List<ClientProduct>();
            this.ProductDetails = new ProductDetails();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal Price { get; set; }
        public string Region { get; set; }
        public string Descritption { get; set; }
        public string ImagePath { get; set; }
        public int Type { get; set; }
        public short IsActive { get; set; }
        public Nullable<int> AdminProductId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual AdminProduct AdminProduct { get; set; }
        public virtual ProductDetails ProductDetails { get; set; }
        public virtual ICollection<ClientProduct> ClientProducts { get; set; }
    }
}
