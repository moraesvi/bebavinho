using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class AdminDeliver : ICrudEntity<Domain.AdminDeliver>, ITestCase<Domain.AdminDeliver>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_ADMDELIVER = "Ocorreu um erro ao realizar a busca de administradores.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um administrador.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um administrador.";

        public AdminDeliver(BebaVinhoDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<Domain.AdminDeliver> Get
        {
            get 
            {
                try
                {
                    IList<Domain.AdminDeliver> lstAdminDeliver = new List<Domain.AdminDeliver>();
                    _dataContext.AdminDeliver.ToList().ForEach(valor =>
                    {
                        lstAdminDeliver.Add(new Domain.AdminDeliver()
                        {
                            Id = valor.Id,
                            AdminId = valor.AdminId,
                        });
                    });
                    return lstAdminDeliver;
                }
                catch (Exception ex) 
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
                }  
            }
        }

        public Domain.AdminDeliver GetById(int id)
        {
            try
            {
                Domain.AdminDeliver objAdminDeliverDomain = new Domain.AdminDeliver();
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Where(valor => valor.Id == id)
                                                                                       .FirstOrDefault();
                objAdminDeliverDomain.Id = objAdminDeliver.Id;
                objAdminDeliverDomain.AdminId = objAdminDeliver.AdminId;
                return objAdminDeliverDomain;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
            };
        }

        public Domain.AdminDeliver GetByName(string fullname)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(Domain.AdminDeliver entity)
        {
            try
            {
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Where(valor => valor.Id == entity.Id)
                                                                               .FirstOrDefault();
                if (objAdminDeliver != null)
                {
                    objAdminDeliver = _dataContext.AdminDeliver.Find(entity.Id);
                    objAdminDeliver.Id = entity.Id;
                    objAdminDeliver.AdminId = entity.AdminId;
                    objAdminDeliver.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    _dataContext.SaveChanges();

                    return objAdminDeliver.Id;
                }

                objAdminDeliver = new DataBase.Model.AdminDeliver();

                objAdminDeliver.Id = entity.Id;
                objAdminDeliver.AdminId = entity.AdminId;
                objAdminDeliver.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.AdminDeliver.Add(objAdminDeliver);

                _dataContext.SaveChanges();

                return objAdminDeliver.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public Domain.AdminDeliver AddOrUpdateAndGetEntity(Domain.AdminDeliver entity)
        {
            try
            {
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Where(valor => valor.Id == entity.Id)
                                                                               .FirstOrDefault();
                if (objAdminDeliver != null)
                {
                    objAdminDeliver = _dataContext.AdminDeliver.Find(entity.Id);
                    objAdminDeliver.Id = entity.Id;
                    objAdminDeliver.AdminId = entity.AdminId;
                    objAdminDeliver.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    return GetById(objAdminDeliver.Id);
                }

                objAdminDeliver = new DataBase.Model.AdminDeliver();

                objAdminDeliver.Id = entity.Id;
                objAdminDeliver.AdminId = entity.AdminId;
                objAdminDeliver.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.AdminDeliver.Add(objAdminDeliver);

                int id = _dataContext.SaveChanges();

                return GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public bool Remove(Domain.AdminDeliver entity)
        {
            try
            {
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Find(entity.Id);
                objAdminDeliver.IsActive = 0;
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
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Find(id);
                objAdminDeliver.IsActive = 0;
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }


        public bool DeleteDataFromDataBase(Domain.AdminDeliver entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new InvalidOperationException("A entidade não pode estar nulo");
                }

                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Find(entity.Id);
                if (objAdminDeliver != null)
                {
                    _dataContext.AdminDeliver.Remove(objAdminDeliver);
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
                DataBase.Model.AdminDeliver objAdminDeliver = _dataContext.AdminDeliver.Find(id);
                if (objAdminDeliver != null)
                {
                    _dataContext.AdminDeliver.Remove(objAdminDeliver);
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
            throw new NotImplementedException();
        }
    }
}
