using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface IProductDetailsEntity<T> : ICrudEntity<T> where T : class 
    {
        T GetByProductId(int productDetailsId);
    }
}
