using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Domain.Interface
{
    public interface IProductEntity<T> : ICrudEntity<T> where T : class 
    {
        T GetByAdminProductId(int adminProductId);

        List<Domain.Procedure.PrGetProducts> GetByPagination(int skip, int take);

        Domain.ProductProductDetails GetProductProductDetailsById(int id);

        IEnumerable<T> GetByIds(int [] ids);

        IEnumerable<T> GetByName(string name);

        IEnumerable<T> GetByType(int type);

        IEnumerable<T> GetByDescription(string description);

        IEnumerable<T> GetByPrice(decimal price);

        bool Disable(int id);

        bool Enable(int id);

        bool Disable(List<int> ids);

        bool Enable(List<int> ids);
    }
}
