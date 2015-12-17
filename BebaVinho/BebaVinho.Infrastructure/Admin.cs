using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BebaVinho.Infrastructure
{
    //TODO: Alterar para buscar de resources(globalização).
    public class Admin : ICrudEntity<Domain.Admin>, ITestCase<Domain.Admin>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_ADM = "Ocorreu um erro ao realizar a busca de administradores.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um administrador.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um administrador.";

        public Admin()
        {
            _dataContext = new BebaVinhoDataContext();
        }

        public IEnumerable<Domain.Admin> Get
        {
            get
            {
                try
                {
                    IList<Domain.Admin> lstAdmin = new List<Domain.Admin>();
                    _dataContext.Admin.Where(value => value.IsActive == 1).ToList().ForEach(valor =>
                    {
                        lstAdmin.Add(new Domain.Admin()
                        {
                            Id = valor.Id,
                            UserId = valor.UserId,
                            FullName = valor.FullName,
                            Status = valor.Status.ToString(),
                            Count = valor.Count
                        });
                    });
                    return lstAdmin;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_ADM, ex.InnerException);
                }
            }
        }

        public Domain.Admin GetById(int id)
        {
            try
            {
                Domain.Admin objAdminDomain = new Domain.Admin();
                DataBase.Model.Admin objAdmin = _dataContext.Admin
                                                            .Where(valor => valor.Id == id && valor.IsActive == 1)
                                                            .FirstOrDefault();
                if (objAdmin != null)
                {
                    objAdminDomain.Id = objAdmin.Id;
                    objAdminDomain.UserId = objAdmin.UserId;
                    objAdminDomain.FullName = objAdmin.FullName;
                    objAdminDomain.Status = objAdmin.Status.ToString();
                    objAdminDomain.Count = objAdmin.Count;
                    return objAdminDomain;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADM, ex.InnerException);
            }
        }

        public Domain.Admin GetByName(string fullname)
        {
            try
            {
                Domain.Admin objAdminDomain = new Domain.Admin();
                DataBase.Model.Admin objAdmin = _dataContext.Admin
                                                            .Where(valor => valor.FullName == fullname && valor.IsActive == 1)
                                                            .FirstOrDefault();
                if (objAdmin != null)
                {
                    objAdminDomain.Id = objAdmin.Id;
                    objAdminDomain.UserId = objAdmin.UserId;
                    objAdminDomain.FullName = objAdmin.FullName;
                    objAdminDomain.Status = objAdmin.Status.ToString();
                    objAdminDomain.Count = objAdmin.Count;
                    return objAdminDomain;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_ADM, ex.InnerException);
            }
        }

        public int AddOrUpdate(Domain.Admin entity)
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Domain.Admin AddOrUpdateAndGetEntity(Domain.Admin entity)
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
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Remove(Domain.Admin entity)
        {
            try
            {
                DataBase.Model.Admin objAdmin = _dataContext.Admin.Find(entity.Id);
                if (objAdmin != null)
                {
                    objAdmin.IsActive = 0;
                    _dataContext.SaveChanges();
                    return true;
                }
                return false;
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
                DataBase.Model.Admin objAdmin = _dataContext.Admin.Find(id);
                objAdmin.IsActive = 0;
                _dataContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool DeleteDataFromDataBase(Domain.Admin entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new InvalidOperationException("A entidade não pode estar nulo");
                }

                DataBase.Model.Admin objAdmin = _dataContext.Admin.Find(entity.Id);
                if (objAdmin != null)
                {
                    _dataContext.Admin.Remove(objAdmin);
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
                DataBase.Model.Admin objAdmin = _dataContext.Admin.Find(id);
                if (objAdmin != null)
                {
                    _dataContext.Admin.Remove(objAdmin);
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
                List<DataBase.Model.Admin> lstAdmin = _dataContext.Admin.Where(valor => valor.FullName.ToUpper() == fullname.ToUpper()).ToList();
                if (lstAdmin.Count > 0)
                {
                    lstAdmin.ForEach(obj =>
                    {
                        _dataContext.Admin.Remove(obj);
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

        private int AddUpdate(Domain.Admin entity) 
        {
            if (entity.User == null && entity.UserId == 0)
            {
                throw new InvalidOperationException("Erro ao inserir o adminisrador, usuário não encontrado.");
            }

            if (!entity.User.IsAdmin)
            {
                throw new InvalidOperationException("Erro ao inserir o adminisrador, o usuário necessita ser administrador.");
            }

            DataBase.Model.Admin objAdmin = _dataContext.Admin
                                                        .Where(valor => (valor.Id == entity.Id || valor.FullName == entity.FullName) && valor.IsActive == 1)
                                                        .FirstOrDefault();
            DataBase.Model.User objUser = new DataBase.Model.User();

            if (objAdmin != null)
            {
                objAdmin = _dataContext.Admin.Find(objAdmin.Id);
                objAdmin.User = objAdmin.User;
                objAdmin.UserId = objAdmin.UserId;
                objAdmin.FullName = objAdmin.FullName;
                objAdmin.Count = objAdmin.Count;
                objAdmin.IsActive = objAdmin.IsActive;
                objAdmin.Status = (short)(!string.IsNullOrEmpty(entity.Status) ? 1 : 0); // TODO: Criar enumerador de status.
                objAdmin.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3.

                _dataContext.SaveChanges();

                return objAdmin.Id;
            }

            objAdmin = new DataBase.Model.Admin();

            int userId = entity.User == null ? entity.UserId : entity.User.Id;

            objUser = _dataContext.User.Find(userId);

            objAdmin.Id = 0;
            objAdmin.User = objUser;
            objAdmin.UserId = userId;
            objAdmin.Status = 1; // TODO: Criar enumerador de status.
            objAdmin.Count = entity.Count;
            objAdmin.FullName = entity.FullName;
            objAdmin.IsActive = 1;
            objAdmin.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3.
            _dataContext.Admin.Add(objAdmin);

            _dataContext.SaveChanges();

            return objAdmin.Id;
        }

        #endregion
    }
}
