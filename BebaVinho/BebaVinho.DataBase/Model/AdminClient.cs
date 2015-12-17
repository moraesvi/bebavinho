using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class AdminClient
    {
        public AdminClient()
        {
            this.Clients = new List<Client>();
        }

        public int Id { get; set; }
        public short IsActive { get; set; }
        public int AdminId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
