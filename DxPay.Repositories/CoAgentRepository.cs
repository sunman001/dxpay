using JMP.MDL;
using DxPay.Infrastructure.Dba;
using DxPay.Infrastructure;
using System;

namespace DxPay.Repositories
{
    public class CoAgentRepository : GenericRepository<CoAgent>, ICoAgentRepository
    {
        public bool UpdatePwd(int id, string password)
        {
            var sql = " update CoAgent set Password = '" + password + "' where Id='" + id + "'";
            var rows = DbHelperSql.ExecuteSql(sql);
            return rows > 0;
        }


        public bool UpdateById(int id, object objUpdate)
        {
            return DbHelperExtension.UpdateById<CoAgent>(id, objUpdate);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool UpdateState(int id, int state)
        {
            var sql = " update CoAgent set [State] = " + state + " where id='" + id + "'";
            var rows = DbHelperSql.ExecuteSql(sql);
            return rows > 0;
        }


        public IPagedList<CoAgent> FindJionList(string orderBy, string @where = "", object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var sql = "select a.*,b.AgentPushMoneyRatio as AgentPushMoneyRatio from  CoAgent a  left join  CoServiceFeeRatioGrade b on a.ServiceFeeRatioGradeId=b.Id "+where+"";
            var list = sql.QueryJoinPagedList<CoAgent>(orderBy, pageIndex, pageSize);
            return list;
        }

        public int FindMax(string sql)
        {
            var id = DbHelperSql.GetSingle(sql);
            return Convert.ToInt32( id);
        }

        public bool UpdateState(string id, int state)
        {
            var sql = " update CoAgent set [State] = " + state + " where  Id in (" + id + ")";
            var rows = DbHelperSql.ExecuteSql(sql);
            return rows > 0;
        }
    }

}
