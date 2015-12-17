using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class Client
    {
        public Client()
        {
            this.ClientLogHistories = new List<ClientLogHistory>();
            this.ClientProducts = new List<ClientProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public short ReceiveProductUpdates { get; set; }
        public int AdminClientId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual AdminClient AdminClient { get; set; }
        public virtual ICollection<ClientLogHistory> ClientLogHistories { get; set; }
        public virtual ICollection<ClientProduct> ClientProducts { get; set; }
    }
}
