using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class ClientProduct
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}
