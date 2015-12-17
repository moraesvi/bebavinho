using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain
{
    public class Deliver
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public int AdminDeliverId { get; set; }

        public AdminDeliver AdminDeliver { get; set; }
    }
}
