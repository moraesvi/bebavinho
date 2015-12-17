using BebaVinho.Business;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BebaVinho.MVC.Models.Product
{
    public class ProductCrudModel
    {
        private ProductBusiness _productBusiness;
        public ProductCrudModel() 
        {
            _productBusiness = new ProductBusiness();
        }

        public IEnumerable<Domain.Product> GetProducts()
        {
            try
            {
                return _productBusiness.Get;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetProductsWithPathImage()
        {
            try
            {
                int countImagePath = 0;
                IEnumerable<Domain.Product> lstProduct = _productBusiness.Get;

                lstProduct.ToList().ForEach(value =>
                {
                    countImagePath++;
                    if (countImagePath == 12)
                    {
                        countImagePath = 1;
                    }

                    if (countImagePath > 9)
                        value.ImagePath = string.Concat("/Content/Images/Wines/vinho" + countImagePath + ".png");
                    else
                        value.ImagePath = string.Concat("/Content/Images/Wines/vinho0" + countImagePath + ".png");
                });

                return lstProduct;
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
                return _productBusiness.GetProductProductDetailsById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByIds(int [] ids)
        {
            try
            {
                IEnumerable<Domain.Product> lstProduct = _productBusiness.GetByIds(ids);

                return lstProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Procedure.PrGetProducts> GetByPagination(int skip, int take) 
        {
            try
            {
                IEnumerable<Domain.Procedure.PrGetProducts> lstProduct = _productBusiness.GetByPagination(skip, take);

                return lstProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}