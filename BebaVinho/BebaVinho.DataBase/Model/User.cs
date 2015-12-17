using System;
using System.Collections.Generic;

namespace BebaVinho.DataBase.Model
{
    public partial class User
    {
        public User()
        {
            this.Admins = new List<Admin>();
            this.Logins = new List<Login>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short IsActive { get; set; }
        public short IsAdmin { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
