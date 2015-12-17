using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class AdminClient : ICrudEntity<Domain.AdminClient>, ITestCase<Domain.AdminClient>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_ADMDELIVER = "Ocorreu um erro ao realizar a busca de administradores.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um administrador.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um administrador.";

        public IEnumerable<Domain.AdminClient> Get
        {
            get 
            {
                try
                {
                    IList<Domain.AdminClient> lstAdminClient = new List<Domain.AdminClient>();
                    _dataContext.AdminClient.ToList().ForEach(valor =>
                    {
                        lstAdminClient.Add(new Domain.AdminClient()
                        {
                            Id = valor.Id,
                            //AdminId = valor.AdminId,
                        });
                    });
                    return lstAdminClient;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
                }  
            }
        }

        public Domain.AdminClient GetById(int id)
        {
            try
            {
                Domain.AdminClient objAdminClientDomain = new Domain.AdminClient();
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Where(valor => valor.Id == id)
                                                                                    .FirstOrDefault();
                objAdminClientDomain.Id = objAdminClient.Id;
                //objAdminClientDomain.AdminId = objAdminClient.AdminId;
                return objAdminClientDomain;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADMDELIVER, ex.InnerException);
            }
        }

        public Domain.AdminClient GetByName(string fullname)
        {
            throw new NotImplementedException();
        }

        public int AddOrUpdate(Domain.AdminClient entity)
        {
            try
            {
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Where(valor => valor.Id == entity.Id)
                                                                                    .FirstOrDefault();
                if (objAdminClient != null)
                {
                    objAdminClient = _dataContext.AdminClient.Find(entity.Id);
                    objAdminClient.Id = entity.Id;
                    objAdminClient.AdminId = entity.AdminId;
                    objAdminClient.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    _dataContext.SaveChanges();

                    return objAdminClient.Id;
                }

                objAdminClient = new DataBase.Model.AdminClient();

                objAdminClient.Id = entity.Id;
                objAdminClient.AdminId = entity.AdminId;
                objAdminClient.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.AdminClient.Add(objAdminClient);

                _dataContext.SaveChanges();

                return objAdminClient.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public Domain.AdminClient AddOrUpdateAndGetEntity(Domain.AdminClient entity)
        {
            try
            {
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Where(valor => valor.Id == entity.Id)
                                                                                     .FirstOrDefault();
                if (objAdminClient != null)
                {
                    objAdminClient = _dataContext.AdminClient.Find(entity.Id);
                    objAdminClient.Id = entity.Id;
                    objAdminClient.AdminId = entity.AdminId;
                    objAdminClient.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    return GetById(objAdminClient.Id);
                }

                objAdminClient = new DataBase.Model.AdminClient();

                objAdminClient.Id = entity.Id;
                objAdminClient.AdminId = entity.AdminId;
                objAdminClient.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.AdminClient.Add(objAdminClient);

                int id = _dataContext.SaveChanges();

                return GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public bool Remove(Domain.AdminClient entity)
        {
            try
            {
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Find(entity.Id);
                objAdminClient.IsActive = 0;
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
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Find(id);
                objAdminClient.IsActive = 0;
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }


        public bool DeleteDataFromDataBase(Domain.AdminClient entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new InvalidOperationException("A entidade não pode estar nulo");
                }

                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Find(entity.Id);
                if (objAdminClient != null)
                {
                    _dataContext.AdminClient.Remove(objAdminClient);
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
                DataBase.Model.AdminClient objAdminClient = _dataContext.AdminClient.Find(id);
                if (objAdminClient != null)
                {
                    _dataContext.AdminClient.Remove(objAdminClient);
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
