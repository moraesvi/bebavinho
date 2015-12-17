using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class AdminProduct
    {
        public AdminProduct()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }
        public short IsActive { get; set; }
        public Nullable<int> AdminId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
