using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class Deliver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short IsActive { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public Nullable<int> AdminDeliverId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual AdminDeliver AdminDeliver { get; set; }
    }
}
