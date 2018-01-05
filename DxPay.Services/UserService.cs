using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;

namespace DxPay.Services
{
    public class UserService : GenericService<jmp_user>, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<jmp_user> FindListBySql(string where, string orderBy)
        {
            return _repository.FindListBySql(where,orderBy);
        }

        public IPagedList<jmp_user> FindPagedListBySql(string orderBy, string sql, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            return _repository.FindPagedListBySql(orderBy, sql, parameters, pageIndex, pageSize);
        }


        
    }
}
