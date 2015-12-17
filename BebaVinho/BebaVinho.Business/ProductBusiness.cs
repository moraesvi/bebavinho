using BebaVinho.Domain.Interface;
using BebaVinho.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Business
{
    public class ProductBusiness : IProductEntity<Domain.Product>, ITestCase<Domain.Product>
    {
        private IProductEntity<Domain.Product> _objProduct;
        private IProductTestCase<Domain.Product> _objProductTestCase;
        public  ProductBusiness() 
        {
            _objProduct = new Product();
            _objProductTestCase = new BebaVinho.Infrastructure.Product();
        }

        public IEnumerable<Domain.Product> Get
        {
            get
            {
                try
                {
                    IEnumerable<Domain.Product> lstProduct = _objProduct.Get;
                    if (lstProduct == null || lstProduct.Count() == 0)
                    {
                        throw new InvalidOperationException("Não existem produtos cadastrados.");
                    }

                    return lstProduct;
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception(ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public IEnumerable<Domain.Product> GetByIds(int[] ids)
        {
            try
            {
                return _objProduct.GetByIds(ids);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.Product GetByAdminProductId(int adminProductId)
        {
            try
            {
                return _objProduct.GetByAdminProductId(adminProductId);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.ProductProductDetails GetProductProductDetailsById(int id)
        {
            try
            {
                return _objProduct.GetProductProductDetailsById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByName(string name)
        {
            try
            {
                return _objProduct.GetByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        
        Domain.Product ICrudEntity<Domain.Product>.GetByName(string fullname)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByType(int type)
        {
            try
            {
                return _objProduct.GetByType(type);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByDescription(string description)
        {
            try
            {
                return _objProduct.GetByDescription(description);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByPrice(decimal price)
        {
            try
            {
                return _objProduct.GetByPrice(price);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Disable(int id)
        {
            try
            {
                return _objProduct.Disable(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Enable(int id)
        {
            try
            {
                return _objProduct.Enable(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Disable(List<int> ids)
        {
            try
            {
                return _objProduct.Disable(ids);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Enable(List<int> ids)
        {
            try
            {
                return _objProduct.Enable(ids);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.Product GetById(int id)
        {
            try
            {
                return _objProduct.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public List<Domain.Procedure.PrGetProducts> GetByPagination(int skip, int take)
        {
            try
            {
                return _objProduct.GetByPagination(skip, take);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int AddOrUpdate(Domain.Product entity)
        {
            try
            {
                if (string.IsNullOrEmpty(entity.Name)) 
                {
                    throw new InvalidOperationException("O nome do produto é obrigatório.");
                }

                return _objProduct.AddOrUpdate(entity);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.Product AddOrUpdateAndGetEntity(Domain.Product entity)
        {
            try
            {
                return _objProduct.AddOrUpdateAndGetEntity(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Remove(Domain.Product entity)
        {
            try
            {
                return _objProduct.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                return _objProduct.Remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool DeleteDataFromDataBase(Domain.Product entity)
        {
            try
            {
                return _objProductTestCase.DeleteDataFromDataBase(entity.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool DeleteDataFromDataBase(int id)
        {
            try
            {
                return _objProductTestCase.DeleteDataFromDataBase(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool DeleteDataFromDataBase(string fullname)
        {
            try
            {
                return _objProductTestCase.DeleteDataFromDataBase(fullname);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
