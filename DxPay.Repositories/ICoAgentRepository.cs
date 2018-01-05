using DxPay.Infrastructure;
using JMP.MDL;

namespace DxPay.Repositories
{
    public interface ICoAgentRepository : IRepository<CoAgent>
    {
        bool UpdateState(int id, int state);

        bool UpdatePwd(int id, string password);

        bool UpdateById(int id, object objUpdate);

        IPagedList<CoAgent> FindJionList(string orderBy, string @where = "", object parameters = null, int pageIndex = 0, int pageSize = 20);

        int FindMax(string sql);
        bool UpdateState(string  id, int state);

    }
}
