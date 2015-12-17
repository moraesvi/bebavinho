using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface ITestCase<T> where T : class
    {
        bool DeleteDataFromDataBase(T entity);

        bool DeleteDataFromDataBase(int id);

        bool DeleteDataFromDataBase(string fullname);
    }
}
