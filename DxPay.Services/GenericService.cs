using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;

namespace DxPay.Services
{
    public abstract class GenericService<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        protected GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<T> FindAll()
        {
            return _repository.FindAll();
        }

        public IPagedList<T> FindPagedList(string orderBy, string @where="", object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            return _repository.FindPagedList(orderBy, where,parameters, pageIndex, pageSize);
        }

        public virtual T FindById(int id)
        {
            return _repository.FindById(id);
        }

        public virtual int Insert(T entity)
        {
            return _repository.Insert(entity);
        }

        public virtual bool Update(T entity)
        {
            return _repository.Update(entity);
        }

        public virtual bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public virtual bool Delete(string ids)
        {
            return _repository.Delete(ids);
        }

    }
}
