using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;

namespace DxPay.Repositories
{
    public class CoCooperationApplicationRepository : GenericRepository<CoCooperationApplication>, ICoCooperationApplicationRepository
    {
        /// <summary>
        /// 修改合作信息状态
        /// </summary>
        /// <param name="state"> 状态 （1：:已抢单 -1 关闭）</param>
        /// <param name="GrabbedDate"> 抢单时间</param>
        /// <param name="GrabbedByName"> 抢单人</param>
        /// <param name="GrabbedById">抢单人Id</param>
        /// <returns></returns>
        public bool UpdateState( int id,int state, DateTime GrabbedDate , string GrabbedByName , int GrabbedById )
        {
            string sql = "";
            if(state==1)
            {
                sql = "update CoCooperationApplication set [State]='" + state + " ', GrabbedDate='" + GrabbedDate + "',GrabbedByName='" + GrabbedByName + "',GrabbedById='" + GrabbedById + "' where id='"+id+"' ";
            }
            else
            {
                sql = " update CoCooperationApplication set[State] = " + state + " where id='" + id + "'";
            }
            var  rows = DbHelperSql.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
