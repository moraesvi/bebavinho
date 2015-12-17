using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class LogHistory
    {
        public LogHistory()
        {
            this.ClientLogHistories = new List<ClientLogHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Descritption { get; set; }
        public short Type { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual ICollection<ClientLogHistory> ClientLogHistories { get; set; }
    }
}
