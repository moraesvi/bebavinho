using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class ProductDetails : IProductDetailsEntity<Domain.ProductDetails>, IProductTestCase<Domain.Product>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_PRODUCT_DETAILS = "Ocorreu um erro ao realizar a busca do detalhe do produto.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização do detalhe do produto.";

        private const string ERRO_DESABILITAR_PRODUCT_DETAILS = "Ocorreu um erro ao desabilitar o detalhe do produto.";

        private const string ERRO_HABILITAR_PRODUCT_DETAILS = "Ocorreu um erro ao habilitar o detalhe do produto.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão do detalhe do  produto.";

        public ProductDetails() 
        {
            _dataContext = new BebaVinhoDataContext();
        }

        public IEnumerable<Domain.ProductDetails> Get
        {
            get
            {
                try
                {
                    List<Domain.ProductDetails> lstProductDetailDomain = new List<Domain.ProductDetails>();
                    List<DataBase.Model.ProductDetails> lstProductDetail = _dataContext.ProductDetails
                                                                                       .Where(value => value.IsActive == 1)
                                                                                       .ToList();
                    if (lstProductDetail != null && lstProductDetail.Count > 0)
                    {
                        lstProductDetail.ForEach(value =>
                        {
                            lstProductDetailDomain.Add(new Domain.ProductDetails()
                            {
                                Id = value.Id,
                                Detail = value.Detail,
                                ImagePath1 = value.ImagePath1,
                                ImagePath2 = value.ImagePath2,
                                ProductId = value.ProductId
                            });
                        });
                        return lstProductDetailDomain;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT_DETAILS, ex.InnerException);
                }
            }
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

        public Domain.ProductDetails GetByProductId(int productDetailsId)
        {
            try
            {
                Domain.ProductDetails objProductDetailsDomain = new Domain.ProductDetails();
                DataBase.Model.ProductDetails objProductDetails = _dataContext.ProductDetails
                                                                              .Where(value => value.ProductId == productDetailsId && value.IsActive == 1)
                                                                              .FirstOrDefault();
                if (objProductDetails != null)
                {
                    objProductDetailsDomain.Id = objProductDetails.Id;
                    objProductDetailsDomain.Detail = objProductDetails.Detail;
                    objProductDetailsDomain.SmallDetail = objProductDetails.SmallDetail;
                    objProductDetailsDomain.ImagePath1 = objProductDetails.ImagePath1;
                    objProductDetailsDomain.ImagePath2 = objProductDetails.ImagePath2;

                    return objProductDetailsDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_DESABILITAR_PRODUCT_DETAILS, ex.InnerException);
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

        public bool DeleteDataFromDataBase(Domain.Product entity)
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
