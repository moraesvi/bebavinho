using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain
{
    public class ProductProductDetails
    {
        public int ProductId { get; set; }

        public int ProductDetailsId { get; set; }

        public string Title { get; set; }

        public string Region { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public string Detail { get; set; }

        public string SmallDetail { get; set; }

        public string PDImagePath1 { get; set; }

        public string PDImagePath2 { get; set; }
    }
}
