using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain
{
    public class ClientLogHistory
    {
        public int Id { get; set; }

        public bool EmailSent { get; set; }

        public int ClientId { get; set; }

        public int LogHistoryId { get; set; }

        public LogHistory LogHistory { get; set; }

        public Client Client { get; set; }
    }
}
