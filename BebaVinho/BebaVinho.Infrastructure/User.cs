using BebaVinho.DataBase.DataContext;
using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure
{
    public class User : IUserEntity<Domain.User>, ITestCase<Domain.User>
    {
        private BebaVinhoDataContext _dataContext = null;

        private const string ERRO_BUSCAR_DADOS_USER = "Ocorreu um erro ao realizar a busca de usuários.";

        private const string ERRO_ADICIONAR_ATUALIZAR = "Ocorreu um erro ao realizar a inserção ou atualização de um usuário.";

        private const string ERRO_EXCLUIR = "Ocorreu um erro ao realizar a exclusão de um usuário.";

        public User()
        {
            _dataContext = new BebaVinhoDataContext();
        }

        public IEnumerable<Domain.User> Get
        {
            get
            {
                try
                {
                    IList<Domain.User> lstUser = new List<Domain.User>();
                    _dataContext.User.Where(value => value.IsActive == 1).ToList().ForEach(valor =>
                    {
                        bool isAdmin = valor.IsAdmin == 1;
                        lstUser.Add(new Domain.User(valor.Id, valor.Name, valor.Surname, isAdmin));
                    });
                    return lstUser;
                }
                catch (Exception ex)
                {
                    throw new Exception(ERRO_BUSCAR_DADOS_USER, ex.InnerException);
                }
            }
        }

        public Domain.User GetById(int id)
        {
            try
            {
                DataBase.Model.User objUser = _dataContext.User
                                                          .Where(valor => valor.Id == id && valor.IsActive == 1)
                                                          .FirstOrDefault();
                if (objUser != null)
                {
                    bool isAdmin = objUser.IsAdmin == 1;
                    Domain.User objUserDomain = new Domain.User(objUser.Id, objUser.Name, objUser.Surname, isAdmin);
                    return objUserDomain;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_USER, ex.InnerException);
            }
        }

        public Domain.User GetByName(string fullname)
        {
            try
            {
                DataBase.Model.User objUser = _dataContext.User
                                                          .Where(valor => (valor.Name + " " + valor.Surname) == fullname && valor.IsActive == 1)
                                                          .FirstOrDefault();
                if (objUser != null)
                {
                    bool isAdmin = objUser.IsAdmin == 1;
                    Domain.User objUserDomain = new Domain.User(objUser.Id, objUser.Name, objUser.Surname, isAdmin);
                    return objUserDomain;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_BUSCAR_DADOS_USER, ex.InnerException);
            }
        }

        public int AddOrUpdate(Domain.User entity)
        {
            try
            {
                return AddUpdate(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        public Domain.User AddOrUpdateAndGetEntity(Domain.User entity)
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

        public bool Remove(Domain.User entity)
        {
            try
            {
                DataBase.Model.User objUser = _dataContext.User.Find(entity.Id);
                if (objUser != null)
                {
                    objUser.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
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
                DataBase.Model.User objUser = _dataContext.User.Find(id);
                if (objUser != null)
                {
                    objUser.IsActive = 0;
                    return _dataContext.SaveChanges() > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_EXCLUIR, ex.InnerException);
            }
        }

        public bool RemoveAdminProfile(int id)
        {
            DataBase.Model.User objUser = _dataContext.User.Find(id);
            if (objUser != null)
            {
                objUser.IsAdmin = 0;
                return _dataContext.SaveChanges() > 0;
            }
            return false;
        }


        public bool DeleteDataFromDataBase(Domain.User entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new InvalidOperationException("A entidade não pode estar nulo");
                }

                DataBase.Model.User objUser = _dataContext.User.Find(entity.Id);
                if (objUser != null)
                {
                    _dataContext.User.Remove(objUser);
                    return _dataContext.SaveChanges() > 0;
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
                DataBase.Model.User objUser = _dataContext.User.Find(id);
                if (objUser != null)
                {
                    _dataContext.User.Remove(objUser);
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
                List<DataBase.Model.User> lstUser = _dataContext.User
                                                                .Where(valor => (valor.Name + " " + valor.Surname).ToUpper() == fullname.ToUpper()).ToList();
                if (lstUser.Count > 0)
                {
                    lstUser.ForEach(obj =>
                    {
                        _dataContext.User.Remove(obj);
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

        private int AddUpdate(Domain.User entity)
        {
            try
            {
                DataBase.Model.User objUser = _dataContext.User
                                                          .Where(valor => valor.Id == entity.Id || (valor.Name + " " + valor.Surname) == (entity.Name + " " + entity.Surname) && valor.IsActive == 1)
                                                          .FirstOrDefault();

                int isAdmin = entity.IsAdmin ? 1 : 0;

                if (objUser != null)
                {
                    objUser = _dataContext.User.Find(objUser.Id);
                    objUser.Id = objUser.Id;
                    objUser.Name = entity.Name;
                    objUser.Surname = entity.Surname;
                    objUser.IsAdmin = (Int16)isAdmin;
                    objUser.IsActive = objUser.IsActive;
                    objUser.UpdateDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 

                    _dataContext.SaveChanges();

                    return objUser.Id;
                }

                objUser = new DataBase.Model.User();

                objUser.Id = 0;
                objUser.Name = entity.Name;
                objUser.Surname = entity.Surname;
                objUser.IsActive = 1;
                objUser.IsAdmin = (Int16)isAdmin;
                objUser.RegisterDate = DateTime.Now; // TODO: Buscar data do banco GMT+3. 
                _dataContext.User.Add(objUser);

                _dataContext.SaveChanges();

                return objUser.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ERRO_ADICIONAR_ATUALIZAR, ex.InnerException);
            }
        }

        #endregion
    }
}
