using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface ICrudEntity<T> where T : class
    {
        IEnumerable<T> Get { get; }

        T GetById(int id);

        T GetByName(string fullname);

        int AddOrUpdate(T entity);

        T AddOrUpdateAndGetEntity(T entity);

        bool Remove(T entity);

        bool Remove(int id);
    }
}
