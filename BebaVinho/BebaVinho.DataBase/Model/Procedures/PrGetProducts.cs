using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.DataBase.Model.Procedures
{
    public class PrGetProducts
    {
        public int Id { get; set; }

        public int TotalRegisters { get; set; }

        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal Price { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public int Type { get; set; }

        public short IsActive { get; set; }

        public int AdminProductId { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
