using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class ClientLogHistory
    {
        public int Id { get; set; }
        public short EmailSent { get; set; }
        public int LogHistoryId { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual LogHistory LogHistory { get; set; }
    }
}
