using DxPay.Infrastructure;
using JMP.DbName;

namespace DxPay.Repositories
{
    public abstract class ReportRepositoryBase
    {
        /// <summary>
        /// 报表数据库名称
        /// </summary>
        protected readonly string ReportDbName = PubDbName.dbtotal;
        protected readonly string BaseDbName = PubDbName.dbbase;
        protected readonly string DeviceDbName = PubDbName.dbdevice;

        /// <summary>
        /// 根据SQL语句查询并返回IPagedList数据集合
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="orderBy">排序</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        protected virtual IPagedList<T> ReadPagedList<T>(string sql, string orderBy, int pageIndex = 0, int pageSize = 20) where T : class
        {
            var list = sql.QueryPagedList<T>(orderBy, pageIndex, pageSize);
            return list;
        }

        /// <summary>
        /// 根据SQL语句查询并返回IPagedList数据集合
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="sql">SQL语句</param>
        /// <param name="pageIndex">当前页索引(从0开始)</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        protected virtual IPagedList<T> ReadPagedList<T>(string sql,int pageIndex = 0, int pageSize = 20) where T : class
        {
            var list = sql.QueryPagedList<T>(pageIndex, pageSize);
            return list;
        }


    }
}
