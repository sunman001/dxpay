using System;
using System.Collections.Generic;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;

namespace DxPay.Services
{
    public class CoCooperationApplicationService : GenericService<CoCooperationApplication>, ICoCooperationApplicationService
    {
        private readonly ICoCooperationApplicationRepository _repository;
        public CoCooperationApplicationService(ICoCooperationApplicationRepository repository) : base(repository)
        {
            _repository = repository;
        }
       
        /// <summary>
        /// 根据条件查询信息
        /// </summary>
        /// <param name="top"></param>
        /// <param name="orderBy"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<CoCooperationApplication> FindByClause(int top, string orderBy, string @where = "")
        {
            return _repository.FindByClause(top, orderBy, @where);
        }

       /// <summary>
       /// 分页查询信息
       /// </summary>
       /// <param name="orderBy"></param>
       /// <param name="status"></param>
       /// <param name="sea_name"></param>
       /// <param name="type"></param>
       /// <param name="searchDesc"></param>
       /// <param name="parameters"></param>
       /// <param name="pageIndex"></param>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        public IPagedList<CoCooperationApplication> FindPagedList(string orderBy, string status, string sea_name, int type, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            orderBy = "  ID desc";
            string where = " 1=1 ";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        where += "  and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        where += " and EmailAddress like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        where += " and MobilePhone like  '%" + sea_name + "%' ";
                        break;
                }

            }

            if (!string.IsNullOrEmpty(status))
            {
                where += " and [State]='" + status + "' ";
            }
            else
            {
                where += "and [State]=0 ";
            }
            if (searchDesc == 1)
            {
                orderBy = "  id  ";
            }
            else
            {
                orderBy = " id desc ";
            }
            return _repository.FindPagedList(orderBy, where, parameters, pageIndex, pageSize);

        }
        /// <summary>
        /// 分页查询（我的代理商）
        /// </summary>
        /// <param name="orderBy">排序字段 </param>
        /// <param name="status"> 状态</param>
        /// <param name="sea_name"> 查询字段</param>
        /// <param name="type"> 类型</param>
        /// <param name="searchDesc"> 排序方式</param>
        /// <param name="userid"> 商务ID</param>
        /// <param name="parameters">参数 </param>
        /// <param name="pageIndex"> 当前页</param>
        /// <param name="pageSize"> 每页显示数量</param>
        /// <returns></returns>
        public IPagedList<CoCooperationApplication> FindPagedList(string orderBy, string status, string sea_name, int type, int searchDesc, int userid, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            orderBy = "ID desc";
            string where = " 1=1 ";
            if (type > 0 && !string.IsNullOrEmpty(sea_name))
            {
                switch (type)
                {
                    case 1:
                        where += "  and Name like '%" + sea_name + "%' ";
                        break;
                    case 2:
                        where += " and EmailAddress like '%" + sea_name + "%' ";
                        break;
                    case 5:
                        where += " and MobilePhone like  '%" + sea_name + "%' ";
                        break;
                }
            }

            if (!string.IsNullOrEmpty(status))
            {
                where += " and [State]='" + status + "' ";
            }
            else
            {
                where += " and [State] in (1,2)";
            }
           
            where += " and GrabbedById='" + userid + "'";
            if (searchDesc == 1)
            {
                orderBy = "  id  ";
            }
            else
            {
                orderBy = " id desc ";
            }
            return _repository.FindPagedList(orderBy, where, parameters, pageIndex, pageSize);
        }
        public bool UpdateState( int id, int state, DateTime GrabbedDate, string GrabbedByName, int GrabbedById)
        {
            return _repository.UpdateState( id,state, GrabbedDate, GrabbedByName, GrabbedById);
        }
    }
}
