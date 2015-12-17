using BebaVinho.Domain.Interface;
using BebaVinho.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Business
{
    public class ProductDetailsBusiness : IProductDetailsEntity<Domain.ProductDetails>, ITestCase<Domain.ProductDetails>
    {
        private IProductDetailsEntity<Domain.ProductDetails> _objProductDetails;
        private IProductTestCase<Domain.Product> _objProductDetailsTestCase;
        public ProductDetailsBusiness() 
        {
            _objProductDetails = new ProductDetails();
            _objProductDetailsTestCase = new BebaVinho.Infrastructure.ProductDetails();
        }

        public IEnumerable<Domain.ProductDetails> Get
        {
            get
            {
                try
                {
                    return _objProductDetails.Get;
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

        public Domain.ProductDetails GetByProductId(int productDetailsId)
        {
            try
            {
                return _objProductDetails.GetByProductId(productDetailsId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.ProductDetails GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Domain.ProductDetails GetByName(string fullname)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(Domain.ProductDetails entity)
        {
            throw new NotImplementedException();
        }

        public Domain.ProductDetails AddOrUpdateAndGetEntity(Domain.ProductDetails entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Domain.ProductDetails entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDataFromDataBase(Domain.ProductDetails entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDataFromDataBase(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDataFromDataBase(string fullname)
        {
            throw new NotImplementedException();
        }
    }
}
