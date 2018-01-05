using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DxPay.Dba.Extensions;

namespace DxPay.Infrastructure.Dba
{
    public class DxQueryable<T> : ISqlQueryable<T>
    {
        private readonly PropertyMapping _propertyMapping;
        private readonly List<string> _whereList = new List<string>();
        private readonly List<SqlParameter> _dbParameters = new List<SqlParameter>();
        private string _orderBy;

        public DxQueryable(PropertyMapping propertyMapping)
        {
            _propertyMapping = propertyMapping;
        }

        public string WhereClause
        {
            get { return _whereList.Count <= 0 ? "" : string.Format(" WHERE {0}", string.Join(" AND ", _whereList)); }
        }


        public string ToSql
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendFormat("SELECT {0} FROM {1}", _propertyMapping.PropertiesString, _propertyMapping.ClassName);
                sb.AppendFormat(WhereClause);
                sb.AppendFormat(OrderByColumns);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 分页SQL语句
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string ToPagingSql(int pageIndex, int pageSize = 20)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageSize < 0)
            {
                pageSize = 20;
            }
            var sb = new StringBuilder();
            sb.AppendFormat(@";WITH T AS(
                        SELECT {0} ,RowNum=ROW_NUMBER() OVER ({1}) FROM {2} {3}
                    ),
                    T2 AS(
	                    SELECT MAX(RowNum) AS RowNumTotalCount FROM T
                    )
                    SELECT * FROM T,T2 WHERE T.RowNum BETWEEN {4} AND {5}", _propertyMapping.PropertiesString, OrderByColumns, _propertyMapping.ClassName, WhereClause, pageIndex * pageSize +1, (pageIndex +1) * pageSize);
            return sb.ToString();
        }

        public string OrderByColumns
        {
            get { return _orderBy; }
        }

        public List<SqlParameter> DbParameters
        {
            get { return _dbParameters; }
        }
        public ISqlQueryable<T> Where(string where, object para = null)
        {
            if (string.IsNullOrEmpty(where))
            {
                return this;
            }
            _whereList.Add(where);
            if (para != null)
            {
                _dbParameters.AddRange(AdoUtil.GetParameters(para));
            }
            return this;
        }

        public ISqlQueryable<T> OrderBy(string orderBy)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                _orderBy = " ORDER BY " + orderBy;
            }
            return this;
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
