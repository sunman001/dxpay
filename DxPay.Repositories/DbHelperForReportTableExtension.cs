using System.Text;
using DxPay.Dba.Extensions;
using DxPay.Infrastructure;
using DxPay.Infrastructure.Dba;
using System.Collections.Generic;

namespace DxPay.Repositories
{
    public static class DbHelperForReportTableExtension
    {
        /// <summary>
        /// 查询分页数据集合(内部处理分页逻辑)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPagedList<T> QueryPagedList<T>(this string sql, string orderBy = "", int pageIndex = 0, int pageSize = 20) where T : class
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat(@";WITH T0 AS(
	{0}
),
T1 AS (
	SELECT T0.*,RowNum=ROW_NUMBER() OVER (ORDER BY T0.{1}) FROM T0
),
T2 AS(
	SELECT MAX(RowNum) AS RowNumTotalCount FROM T1
)
SELECT * FROM T1,T2 WHERE T1.RowNum BETWEEN {2} AND {3}", sql, orderBy, pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);

            var ds = DbHelperSql.Query(sqlBuilder.ToString());
            var list = ds.Tables[0].ToList<T>();
            var totalCount = ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["RowNumTotalCount"] : 0;
            var items = new PagedList<T>(list, pageIndex, pageSize, int.Parse(totalCount.ToString()));

            return items;
        }

        /// <summary>
        /// 查询分页数据集合(直接执行SQL语句)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IPagedList<T> QueryPagedList<T>(this string sql, int pageIndex = 0, int pageSize = 20) where T : class
        {
            var ds = DbHelperSql.Query(sql);
            var list = ds.Tables[0].ToList<T>();
            var totalCount = ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["RowNumTotalCount"] : 0;
            var items = new PagedList<T>(list, pageIndex, pageSize, int.Parse(totalCount.ToString()));

            return items;
        }

        public static IEnumerable<T> QueryList<T>(this string sql) where T : class
        {
            var ds = DbHelperSql.Query(sql);
            var list = ds.Tables[0].ToList<T>();
            return list;
        }
    }
}
