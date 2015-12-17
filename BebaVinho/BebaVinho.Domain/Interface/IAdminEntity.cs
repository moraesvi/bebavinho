using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface IAdminEntity<T> : ICrudEntity<T> where T : class
    {
        T GetByAdminId(int adminId);

        bool RemoveProfile(int adminId);

        bool ActiveProfile(int adminId);
    }
}
