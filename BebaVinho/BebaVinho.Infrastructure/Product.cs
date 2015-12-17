using BebaVinho.DataBase.DataContext;
using BebaVinho.DataBase.Model.Procedures;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class Product : IProductEntity<Domain.Product>, IProductTestCase<Domain.Product>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_PRODUCT = "Ocorreu um erro ao realizar a busca de produtos.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um produto.";

        private const string ERRO_DESABILITAR_PRODUTO = "Ocorreu um erro ao desabilitar um produto.";

        private const string ERRO_HABILITAR_PRODUTO = "Ocorreu um erro ao habilitar um produto.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um produto.";

        public Product()
        {
            _dataContext = new BebaVinhoDataContext();
        }

        public IEnumerable<Domain.Product> Get
        {
            get
            {
                try
                {
                    List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                    List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                          .Where(value => value.IsActive == 1)
                                                                          .ToList();
                    if (lstProduct != null && lstProduct.Count > 0)
                    {
                        lstProduct.ForEach(value =>
                        {
                            lstProductDomain.Add(new Domain.Product()
                            {
                                Id = value.Id,
                                Name = value.Name,
                                Description = value.Descritption,
                                OldPrice = value.OldPrice,
                                Price = value.Price,
                                Region = value.Region,
                                Type = value.Type,
                                AdminProductId = value.AdminProductId
                            });
                        });
                        return lstProductDomain;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
                }
            }
        }

        public IEnumerable<Domain.Product> GetByIds(int[] ids)
        {
            try
            {
                List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => ids.Contains(value.Id) && value.IsActive == 1)
                                                                      .ToList();
                if (lstProduct != null && lstProduct.Count > 0)
                {
                    lstProduct.ForEach(value =>
                    {
                        lstProductDomain.Add(new Domain.Product()
                        {
                            Id = value.Id,
                            Name = value.Name,
                            Description = value.Descritption,
                            OldPrice = value.OldPrice,
                            Price = value.Price,
                            Region = value.Region,
                            Type = value.Type,
                            AdminProductId = value.AdminProductId
                        });
                    });

                    return lstProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public List<Domain.Procedure.PrGetProducts> GetByPagination(int skip, int take)
        {
            try
            {
                var sqlParameterSkip = new System.Data.SqlClient.SqlParameter("@SKIP", skip);
                var sqlParameterTake = new System.Data.SqlClient.SqlParameter("@TAKE", take);
                List<Domain.Procedure.PrGetProducts> lstPrGetProductsDomain = new List<Domain.Procedure.PrGetProducts>();
                IEnumerable<PrGetProducts> lstPrGetProducts = _dataContext.Database.SqlQuery<PrGetProducts>("PR_GET_PRODUCTS @SKIP, @TAKE", sqlParameterSkip, sqlParameterTake);
                lstPrGetProducts.ToList().ForEach(value =>
                {
                    Domain.Procedure.PrGetProducts objPrGetProducts = new Domain.Procedure.PrGetProducts();
                    objPrGetProducts.Id = value.Id;
                    objPrGetProducts.TotalRegisters = value.TotalRegisters;
                    objPrGetProducts.Name = value.Name;
                    objPrGetProducts.OldPrice = value.OldPrice;
                    objPrGetProducts.Price = value.Price;
                    objPrGetProducts.Region = value.Region;
                    objPrGetProducts.Type = value.Type;
                    objPrGetProducts.Description = value.Description;
                    objPrGetProducts.ImagePath = value.ImagePath;
                    objPrGetProducts.AdminProductId = value.AdminProductId;
                    lstPrGetProductsDomain.Add(objPrGetProducts);
                });
                return lstPrGetProductsDomain;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public Domain.ProductProductDetails GetProductProductDetailsById(int id)
        {
            try
            {
                Domain.ProductProductDetails objProductDomain = new Domain.ProductProductDetails();

                var productQuery = from p in _dataContext.Product
                                   join pd in _dataContext.ProductDetails on p.Id equals pd.ProductId
                                   select new Domain.ProductProductDetails()
                                   {
                                       ProductId = p.Id,
                                       ProductDetailsId = pd.Id,
                                       Title = p.Name,
                                       Region = p.Region,
                                       Price = p.Price,
                                       OldPrice = p.OldPrice,
                                       PDImagePath1 = pd.ImagePath1,
                                       PDImagePath2 = pd.ImagePath2,
                                       Detail = pd.Detail,
                                       SmallDetail = pd.SmallDetail
                                   };

                return productQuery.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public Domain.Product GetById(int id)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == id && value.IsActive == 1)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProductDomain.Id = objProduct.Id;
                    objProductDomain.Name = objProduct.Name;
                    objProductDomain.Description = objProduct.Descritption;
                    objProductDomain.Price = objProduct.Price;
                    objProductDomain.Type = objProduct.Type;
                    objProductDomain.AdminProductId = objProduct.AdminProductId;

                    return objProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public Domain.Product GetByAdminProductId(int adminProductId)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.AdminProductId == adminProductId && value.IsActive == 1)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProductDomain.Id = objProduct.Id;
                    objProductDomain.AdminProductId = objProduct.AdminProductId;
                    objProductDomain.Name = objProduct.Name;
                    objProductDomain.Price = objProduct.Price;
                    objProductDomain.Type = objProduct.Type;
                    objProductDomain.Description = objProduct.Descritption;
                    objProductDomain.AdminProductId = objProduct.AdminProductId;

                    return objProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        IEnumerable<Domain.Product> IProductEntity<Domain.Product>.GetByName(string name)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => value.Name == name && value.IsActive == 1)
                                                                      .ToList();
                if (lstProduct.Count > 0)
                {
                    lstProduct.ForEach(obj =>
                    {
                        objProductDomain.Id = obj.Id;
                        objProductDomain.Name = obj.Name;
                        objProductDomain.Description = obj.Descritption;
                        objProductDomain.Price = obj.Price;
                        objProductDomain.Type = obj.Type;
                        objProductDomain.AdminProductId = obj.AdminProductId;
                        lstProductDomain.Add(objProductDomain);
                    });

                    return lstProductDomain.AsEnumerable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        Domain.Product ICrudEntity<Domain.Product>.GetByName(string fullname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Domain.Product> GetByType(int type)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => value.Type == type && value.IsActive == 1)
                                                                      .ToList();
                if (lstProduct.Count > 0)
                {
                    lstProduct.ForEach(obj =>
                    {
                        objProductDomain.Id = obj.Id;
                        objProductDomain.Name = obj.Name;
                        objProductDomain.Description = obj.Descritption;
                        objProductDomain.Price = obj.Price;
                        objProductDomain.Type = obj.Type;
                        objProductDomain.AdminProductId = obj.AdminProductId;
                        lstProductDomain.Add(objProductDomain);
                    });

                    return lstProductDomain.AsEnumerable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByDescription(string description)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => value.Descritption == description && value.IsActive == 1)
                                                                      .ToList();
                if (lstProduct.Count > 0)
                {
                    lstProduct.ForEach(obj =>
                    {
                        objProductDomain.Id = obj.Id;
                        objProductDomain.Name = obj.Name;
                        objProductDomain.Description = obj.Descritption;
                        objProductDomain.Price = obj.Price;
                        objProductDomain.Type = obj.Type;
                        objProductDomain.AdminProductId = obj.AdminProductId;
                        lstProductDomain.Add(objProductDomain);
                    });

                    return lstProductDomain.AsEnumerable();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public IEnumerable<Domain.Product> GetByPrice(decimal price)
        {
            try
            {
                Domain.Product objProductDomain = new Domain.Product();
                List<Domain.Product> lstProductDomain = new List<Domain.Product>();
                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => value.Price == price && value.IsActive == 1)
                                                                      .ToList();
                if (lstProduct.Count > 0)
                {
                    lstProduct.ForEach(obj =>
                    {
                        objProductDomain.Id = obj.Id;
                        objProductDomain.Name = obj.Name;
                        objProductDomain.Description = obj.Descritption;
                        objProductDomain.Price = obj.Price;
                        objProductDomain.Type = obj.Type;
                        objProductDomain.AdminProductId = obj.AdminProductId;
                        lstProductDomain.Add(objProductDomain);
                    });

                    return lstProductDomain.AsEnumerable();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_PRODUCT, ex.InnerException);
            }
        }

        public int AddOrUpdate(Domain.Product entity)
        {
            try
            {
                return AddUpdate(entity);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public Domain.Product AddOrUpdateAndGetEntity(Domain.Product entity)
        {
            try
            {
                return GetById(AddUpdate(entity));
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public bool Remove(Domain.Product entity)
        {
            try
            {
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == entity.Id && value.IsActive == 1)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProduct.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool Remove(int id)
        {
            try
            {
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == id && value.IsActive == 1)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProduct.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool Disable(int id)
        {
            try
            {
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == id && value.IsActive == 1)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProduct.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_DESABILITAR_PRODUTO, ex.InnerException);
            }
        }

        public bool Enable(int id)
        {
            try
            {
                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == id)
                                                                .FirstOrDefault();
                if (objProduct != null)
                {
                    objProduct.IsActive = 1;
                    return _dataContext.SaveChanges() > 0;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_HABILITAR_PRODUTO, ex.InnerException);
            }
        }

        public bool Disable(List<int> ids)
        {
            try
            {
                if (ids == null)
                    return false;
                if (ids.Count == 0)
                    return false;

                ids.ForEach(id =>
                {
                    DataBase.Model.Product objProduct = _dataContext.Product
                                                                    .Where(value => value.Id == id && value.IsActive == 1)
                                                                    .FirstOrDefault();
                    if (objProduct != null)
                    {
                        objProduct.IsActive = 0;
                    }

                    _dataContext.SaveChanges();
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_DESABILITAR_PRODUTO, ex.InnerException);
            }
        }

        public bool Enable(List<int> ids)
        {
            try
            {
                if (ids == null)
                    return false;
                if (ids.Count == 0)
                    return false;

                ids.ForEach(id =>
                {
                    DataBase.Model.Product objProduct = _dataContext.Product
                                                                    .Where(value => value.Id == id && value.IsActive == 1)
                                                                    .FirstOrDefault();
                    if (objProduct != null)
                    {
                        objProduct.IsActive = 1;
                    }

                    _dataContext.SaveChanges();
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_HABILITAR_PRODUTO, ex.InnerException);
            }
        }


        public bool DeleteDataFromDataBase(Domain.Product entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                DataBase.Model.Product objProduct = _dataContext.Product.Find(entity.Id);
                if (objProduct != null)
                {
                    _dataContext.Product.Remove(objProduct);
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDataFromDataBase(int id)
        {
            try
            {
                DataBase.Model.Product objProduct = _dataContext.Product.Find(id);
                if (objProduct != null)
                {
                    _dataContext.Product.Remove(objProduct);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteDataFromDataBase(string fullname)
        {
            try
            {
                if (string.IsNullOrEmpty(fullname))
                    return false;

                List<DataBase.Model.Product> lstProduct = _dataContext.Product
                                                                      .Where(value => value.Name.ToUpper() == fullname.ToUpper())
                                                                      .ToList();
                if (lstProduct.Count > 0)
                {
                    lstProduct.ForEach(obj =>
                    {
                        _dataContext.Product.Remove(obj);
                    });
                    return _dataContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Metodos Privados

        private int AddUpdate(Domain.Product entity)
        {
            try
            {
                if (entity.AdminProduct == null && (!entity.AdminProductId.HasValue || entity.AdminProductId == 0))
                {
                    throw new InvalidOperationException("Erro ao inserir um novo produto, administrador não encontrado.");
                }

                DataBase.Model.Product objProduct = _dataContext.Product
                                                                .Where(value => value.Id == entity.Id && value.IsActive == 1)
                                                                .FirstOrDefault();

                int? adminProductId = entity.AdminProduct == null ? entity.AdminProductId : entity.AdminProduct.Id;

                if (objProduct != null)
                {
                    objProduct = _dataContext.Product.Find(entity.Id);
                    objProduct.Id = entity.Id;
                    objProduct.Name = entity.Name;
                    objProduct.Descritption = entity.Description;
                    objProduct.Price = entity.Price;
                    objProduct.IsActive = objProduct.IsActive;
                    objProduct.Type = entity.Type;
                    objProduct.AdminProductId = adminProductId;
                    objProduct.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    _dataContext.SaveChanges();

                    return objProduct.Id;
                }

                objProduct = new DataBase.Model.Product();

                objProduct.Id = 0;
                objProduct.Name = entity.Name;
                objProduct.Descritption = entity.Description;
                objProduct.Price = entity.Price;
                objProduct.Type = entity.Type;
                objProduct.IsActive = 1;
                objProduct.AdminProductId = adminProductId;
                objProduct.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.Product.Add(objProduct);

                _dataContext.SaveChanges();

                return objProduct.Id;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        #endregion
    }
}
