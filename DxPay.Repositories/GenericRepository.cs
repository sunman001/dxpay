using System.Collections.Generic;
using System.Data;
using System.Text;
using DxPay.Infrastructure.Dba;
using System;
using DxPay.Dba;
using DxPay.Dba.Extensions;
using DxPay.Infrastructure;
using JMP.DbName;

namespace DxPay.Repositories
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly string ReportDbName = PubDbName.dbtotal;
        protected readonly string BaseDbName = PubDbName.dbbase;
        protected readonly string DeviceDbName = PubDbName.dbdevice;

        public virtual T FindById(int id)
        {
            var type = typeof(T);
            var propMapping = type.Mapping();
            var strSql = new StringBuilder();
            strSql.AppendFormat("SELECT {0}", propMapping.PropertiesString);
            strSql.AppendFormat(" FROM {0}", propMapping.ClassName);
            strSql.AppendFormat(" WHERE {0}=@PrimaryKey", propMapping.PrimaryKey);
            var paraPrimaryKey = SqlParameterFactory.GetParameter;
            paraPrimaryKey.ParameterName = "@PrimaryKey";
            paraPrimaryKey.Value = id;
            paraPrimaryKey.DbType = DbType.String;
            var ds = DbHelperSql.Query(strSql.ToString(), paraPrimaryKey);
            return ds.Tables[0].ToEntity<T>();
        }

        public virtual IEnumerable<T> FindAll()
        {
            var type = typeof(T);
            var propMapping = type.Mapping();

            var strSql = new StringBuilder();
            strSql.AppendFormat("SELECT {0}", propMapping.PropertiesString);
            strSql.AppendFormat(" FROM {0}", propMapping.ClassName);

            var ds = DbHelperSql.Query(strSql.ToString());
            return ds.Tables[0].ToList<T>();
        }

        public IEnumerable<T> FindByClause(int top, string orderBy, string @where = "", object parameters = null)
        {
            var type = typeof(T);
            var propMapping = type.Mapping();

            var dxQueryable = new DxQueryable<T>(propMapping);
            dxQueryable.Where(@where, parameters);
            dxQueryable.OrderBy(orderBy);
            var ds = DbHelperSql.Query(dxQueryable.ToSql, dxQueryable.DbParameters.ToArray());
            return ds.Tables[0].ToList<T>();
        }

        public IPagedList<T> FindPagedList(string orderBy, string @where="", object parameters = null, int pageIndex = 0, int pageSize = 20)
        {
            var type = typeof(T);
            var propMapping = type.Mapping();

            var dxQueryable = new DxQueryable<T>(propMapping);
            dxQueryable.Where(@where, parameters);
            dxQueryable.OrderBy(orderBy);
            var ds = DbHelperSql.Query(dxQueryable.ToPagingSql(pageIndex, pageSize), dxQueryable.DbParameters.ToArray());
            var list = ds.Tables[0].ToList<T>();
            var totalCount = ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0]["RowNumTotalCount"] : 0;
            var items = new PagedList<T>(list, pageIndex, pageSize, int.Parse(totalCount.ToString()));
            return items;
        }

        public virtual int Insert(T entity)
        {
            var type = typeof(T);
            var sql = type.GenerateInsertSqlString();
            var obj = DbHelperSql.GetSingle(sql, AdoUtil.GetParameters(entity));
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        public virtual bool Update(T entity)
        {
            var type = typeof(T);
            var sql = type.GenerateUpdateSqlString();
            var rows = DbHelperSql.ExecuteSql(sql, AdoUtil.GetParameters(entity));
            return rows > 0;
        }

        public virtual bool Delete(int id)
        {
            var type = typeof(T);
            var sql = type.GenerateDeleteSqlString(id);
            var rows = DbHelperSql.ExecuteSql(sql);
            return rows > 0;
        }

        public virtual bool Delete(string ids)
        {
            var type = typeof(T);
            var sql = type.GenerateBatchDeleteSqlString(ids);
            var rows = DbHelperSql.ExecuteSql(sql);
            return rows > 0;
        }
    }
}
