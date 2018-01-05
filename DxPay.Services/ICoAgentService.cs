using JMP.MDL;
using DxPay.Infrastructure;

namespace DxPay.Services
{
    public interface ICoAgentService : IService<CoAgent>
    {
       IPagedList<CoAgent> FindPagedList(string orderBy, int s_type, string s_keys, string  status , string  AuditState,  int userid,int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20);

        bool UpdateState(int id, int state);

        bool UpdateState(string  id, int state);
        bool UpdatePwd(int id, string password);

        /// <summary>
        /// 解冻代理商
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <returns></returns>
        bool Lock(int id);
        /// <summary>
        /// 冻结代理商
        /// </summary>
        /// <param name="id">代理商ID</param>
        /// <returns></returns>
        bool UnLock(int id);

        bool UpdateById(int id, object objUpdate);

        IPagedList<CoAgent> FindJionList(string orderBy, int s_type, string s_keys, string status, string AuditState, int userid, int searchDesc, object parameters = null, int pageIndex = 0, int pageSize = 20);

        int FindMax(string sql);
    }
}
