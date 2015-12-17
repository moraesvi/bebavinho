using BebaVinho.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaVinho.Infrastructure.P.O.C.O
{
    public class EntityCrud<T> : ICrudEntity<T> where T : class, ICrudEntity<T> 
    {
        private ICrudEntity<T> _objEntity = null;

        public EntityCrud(T objEntity) 
        {
            _objEntity = objEntity;
        }

        public IEnumerable<T> Get
        {
            get 
            {
                try
                {
                    return _objEntity.Get;
                }
                catch (Exception ex) 
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _objEntity.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public T GetByName(string name)
        {
            try
            {
                return _objEntity.GetByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public int AddOrUpdate(T entity)
        {
            try
            {
                return _objEntity.AddOrUpdate(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public T AddOrUpdateAndGetEntity(T entity)
        {
            try
            {
                return _objEntity.AddOrUpdateAndGetEntity(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                return _objEntity.Remove(entity);
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
                return _objEntity.Remove(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
