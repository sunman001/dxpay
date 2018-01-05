using System;
using DxPay.Infrastructure;
using DxPay.Repositories;
using JMP.MDL;

namespace DxPay.Services
{
    public class CoAgentService : GenericService<CoAgent>, ICoAgentService
    {
        private readonly ICoAgentRepository _repository;
        public CoAgentService(ICoAgentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 查询代理商信息
        /// </summary>
        /// <param name="orderBy"> 排序字段</param>
        /// <param name="s_type"> 查询类型</param>
        /// <param name="s_keys">关键字</param>
        /// <param name="status"> 账号状态</param>
        /// <param name="AuditState">审核状态</param>
        /// <param name="userid"> 代理商ID</param>
        /// <param name="searchDesc">排序方式</param>
        /// <param name="parameters">参数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示数量</param>
        /// <returns></returns>
        public IPagedList<CoAgent> FindPagedList(string orderBy, int s_type, string s_keys, string status, string AuditState, int userid, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {

            orderBy = "id desc";
            string where = "1=1";
            if (s_type > 0 && !string.IsNullOrEmpty(s_keys))
            {
                switch (s_type)
                {
                    case 1:
                        where += "  and LoginName like '%" + s_keys + "%' ";
                        break;
                    case 2:
                        where += " and DisplayName like '%" + s_keys + "%' ";
                        break;
                    case 3:
                        where += " and MobilePhone like  '%" + s_keys + "%' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(status))
            {
                where += " and [State]='" + status + "'";
            }

            if (!string.IsNullOrEmpty(AuditState))
            {
                where += " and AuditState='" + AuditState + "' ";
            }
            where += " and OwnerId='" + userid + "'";

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
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdatePwd(int id, string password)
        {
            return _repository.UpdatePwd(id, password);
        }

        /// <summary>
        /// 冻结代理商
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <returns></returns>
        public bool Lock(int id)
        {
            return _repository.UpdateById(id, new { State = 1 });
        }

        /// <summary>
        /// 解冻代理商
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <returns></returns>
        public bool UnLock(int id)
        {
            return _repository.UpdateById(id, new { State = 0 });
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateState(int id, int state)
        {
            return _repository.UpdateState(id, state);
        }

        public bool UpdateById(int id, object objUpdate)
        {
            return _repository.UpdateById(id,objUpdate);
        }

        public IPagedList<CoAgent> FindJionList(string orderBy, int s_type, string s_keys, string status, string AuditState, int userid, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20)
        {

            orderBy = "  id desc";
            string where = " where 1=1";
            if (s_type > 0 && !string.IsNullOrEmpty(s_keys))
            {
                switch (s_type)
                {
                    case 1:
                        where += "  and LoginName like '%" + s_keys + "%' ";
                        break;
                    case 2:
                        where += " and DisplayName like '%" + s_keys + "%' ";
                        break;
                    case 3:
                        where += " and MobilePhone like  '%" + s_keys + "%' ";
                        break;

                }

            }
            if (!string.IsNullOrEmpty(status))
            {
                where += " and [State]='" + status + "'";
            }

            if (!string.IsNullOrEmpty(AuditState))
            {
                where += " and AuditState='" + AuditState + "' ";
            }
            where += " and OwnerId='" + userid + "'";

            if (searchDesc == 1)
            {
                orderBy = "    id  ";
            }
            else
            {
                orderBy = " id desc ";
            }
            return _repository.FindJionList(orderBy, where, parameters, pageIndex, pageSize);
        }

        public int FindMax(string sql)
        {
            return _repository.FindMax(sql);
        }

        public bool UpdateState(string id, int state)
        {
            return _repository.UpdateState(id, state);
        }
    }
}
