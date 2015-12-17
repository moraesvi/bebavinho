using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain
{
    public class Login
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsLogged { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
