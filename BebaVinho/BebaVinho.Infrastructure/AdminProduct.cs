using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class AdminProduct : IAdminEntity<Domain.AdminProduct>, IAdminTestCase<Domain.AdminProduct>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_ADMDELIVER = "Ocorreu um erro ao realizar a busca de administradores.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um administrador.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um administrador.";

        public AdminProduct()
        {
            _dataContext = new BebaVinhoDataContext();
        }

        public IEnumerable<Domain.AdminProduct> Get
        {
            get
            {
                try
                {
                    IList<Domain.AdminProduct> lstAdminProduct = new List<Domain.AdminProduct>();
                    _dataContext.AdminClient.ToList().ForEach(valor =>
                    {
                        lstAdminProduct.Add(new Domain.AdminProduct()
                        {
                            Id = valor.Id,
                            //AdminId = valor.AdminId,
                        });
                    });
                    return lstAdminProduct;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
                }
            }
        }

        public Domain.AdminProduct GetById(int id)
        {
            try
            {
                Domain.AdminProduct objAdminProductDomain = new Domain.AdminProduct();
                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct
                                                                          .Where(value => value.Id == id && value.IsActive == 1)
                                                                          .FirstOrDefault();
                if (objAdminProduct != null)
                {
                    objAdminProductDomain.Id = objAdminProduct.Id;
                    objAdminProductDomain.AdminId = objAdminProduct.AdminId;
                    return objAdminProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
            }
        }

        public Domain.AdminProduct GetByName(string fullname)
        {
            throw new NotImplementedException();
        }

        public Domain.AdminProduct GetByAdminId(int adminId)
        {
            try
            {
                Domain.AdminProduct objAdminProductDomain = new Domain.AdminProduct();
                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct
                                                                          .Where(value => value.AdminId == adminId && value.IsActive == 1)
                                                                          .FirstOrDefault();
                if (objAdminProduct != null)
                {
                    objAdminProductDomain.Id = objAdminProduct.Id;
                    objAdminProductDomain.AdminId = objAdminProduct.AdminId;
                    return objAdminProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
            }
        }

        public int AddOrUpdate(Domain.AdminProduct entity)
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

        public Domain.AdminProduct AddOrUpdateAndGetEntity(Domain.AdminProduct entity)
        {
            try
            {
                return GetById(AddUpdate(entity));
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public bool Remove(Domain.AdminProduct entity)
        {
            try
            {
                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct.Find(entity.Id);
                objAdminProduct.IsActive = 0;
                _dataContext.SaveChanges();
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
                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct.Find(id);
                objAdminProduct.IsActive = 0;
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool RemoveProfile(int adminId)
        {
            try
            {
                if (adminId == 0)
                {
                    throw new InvalidOperationException("É obrigatório informar o id do administrador");
                }

                var objAdminProduct = _dataContext.AdminProduct
                                                  .Where(value => value.AdminId == adminId)
                                                  .FirstOrDefault();

                if (objAdminProduct != null)
                {
                    objAdminProduct.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
                }

                return false;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool ActiveProfile(int adminId)
        {
            try
            {
                if (adminId == 0)
                {
                    throw new InvalidOperationException("É obrigatório informar o id do administrador");
                }

                var objAdminProduct = _dataContext.AdminProduct
                                                  .Where(value => value.AdminId == adminId)
                                                  .FirstOrDefault();

                if (objAdminProduct != null)
                {
                    objAdminProduct.IsActive = 1;
                    return _dataContext.SaveChanges() > 0;
                }

                return false;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        List<Domain.AdminProduct> IAdminTestCase<Domain.AdminProduct>.GetByAdminId(int adminId)
        {
            try
            {
                Domain.AdminProduct objAdminProductDomain = new Domain.AdminProduct();
                List<Domain.AdminProduct> lstAdminProductDomain = new List<Domain.AdminProduct>();
                List<DataBase.Model.AdminProduct> lstAdminProduct = _dataContext.AdminProduct
                                                                                .Where(value => value.AdminId == adminId)
                                                                                .ToList();
                if (lstAdminProduct.Count > 0)
                {
                    lstAdminProduct.ForEach(value =>
                    {
                        objAdminProductDomain.Id = value.Id;
                        objAdminProductDomain.AdminId = value.AdminId;
                        lstAdminProductDomain.Add(objAdminProductDomain);
                    });
                    return lstAdminProductDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
            }
        }

        public bool DeleteDataFromDataBase(Domain.AdminProduct entity)
        {
            try
            {
                if (entity == null)
                    return false;

                List<DataBase.Model.AdminProduct> lstAdminProduct = _dataContext.AdminProduct
                                                                                .Where(value => value.Id == entity.Id)
                                                                                .ToList();
                if (lstAdminProduct != null)
                {
                    lstAdminProduct.ForEach(value =>
                    {
                        _dataContext.AdminProduct.Remove(value);
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

        public bool DeleteDataFromDataBase(int id)
        {
            try
            {
                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct.Find(id);
                if (objAdminProduct != null)
                {
                    _dataContext.AdminProduct.Remove(objAdminProduct);
                    return _dataContext.SaveChanges() > 0;
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
            throw new NotImplementedException();
        }

        private int AddUpdate(Domain.AdminProduct entity)
        {
            try
            {
                if (entity.Admin == null && (!entity.AdminId.HasValue || entity.AdminId == 0))
                {
                    throw new InvalidOperationException("Erro ao inserir o perfil 'administrador de produtos', administrador não encontrado.");
                }

                int? adminId = entity.Admin == null ? entity.AdminId : entity.Admin.Id;

                DataBase.Model.AdminProduct objAdminProduct = _dataContext.AdminProduct
                                                                          .Where(value => value.Id == entity.Id && value.IsActive == 1)
                                                                          .FirstOrDefault();
                if (objAdminProduct != null)
                {
                    objAdminProduct = _dataContext.AdminProduct.Find(entity.Id);
                    objAdminProduct.Id = objAdminProduct.Id;
                    objAdminProduct.Admin = _dataContext.Admin.Find(adminId);
                    objAdminProduct.AdminId = adminId;
                    objAdminProduct.IsActive = objAdminProduct.IsActive;
                    objAdminProduct.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    _dataContext.SaveChanges();

                    return objAdminProduct.Id;
                }

                objAdminProduct = new DataBase.Model.AdminProduct();

                objAdminProduct.Id = entity.Id;
                objAdminProduct.Admin = _dataContext.Admin.Find(adminId);
                objAdminProduct.AdminId = adminId;
                objAdminProduct.IsActive = 1;
                objAdminProduct.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.AdminProduct.Add(objAdminProduct);

                _dataContext.SaveChanges();

                return objAdminProduct.Id;
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
    }
}
