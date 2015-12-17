using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public short IsLogged { get; set; }
        public int UserId { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual User User { get; set; }
    }
}
