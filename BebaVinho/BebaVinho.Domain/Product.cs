using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal Price { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int Type { get; set; }

        public int? AdminProductId { get; set; }

        public int? ProductDetailsId { get; set; }

        public AdminProduct AdminProduct { get; set; } 

    }
}
