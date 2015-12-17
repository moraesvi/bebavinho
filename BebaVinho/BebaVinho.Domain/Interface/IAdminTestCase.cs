using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface IAdminTestCase<T> : ITestCase<T> where T : class
    {
        List<T> GetByAdminId(int adminId);
    }
}
