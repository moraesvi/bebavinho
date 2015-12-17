using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface IUserEntity<T> : ICrudEntity<T> where T : class
    {
        bool RemoveAdminProfile(int id);
    }
}
