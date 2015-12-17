using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class Admin
    {
        public Admin()
        {
            this.AdminClients = new List<AdminClient>();
            this.AdminDelivers = new List<AdminDeliver>();
            this.AdminProducts = new List<AdminProduct>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public short Status { get; set; }
        public Nullable<int> Count { get; set; }
        public short IsActive { get; set; }
        public int UserId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AdminClient> AdminClients { get; set; }
        public virtual ICollection<AdminDeliver> AdminDelivers { get; set; }
        public virtual ICollection<AdminProduct> AdminProducts { get; set; }
    }
}
