using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DxPay.Infrastructure.Dba
{
    public class AdoUtil
    {

        public static void SetParSize(DbParameter par)
        {
            var size = par.Size;
            if (size < 4000)
            {
                par.Size = 4000;
            }
        }


        public static SqlParameter[] GetParameters(object whereObj, PropertyInfo[] propertyInfo = null)
        {
            var sqlParameterKeyWord = "@";
            var listParams = new List<SqlParameter>();
            if (whereObj == null) return listParams.ToArray();
            var type = whereObj.GetType();
            var isDic = type.IsIn(PubConst.DicArraySO, PubConst.DicArraySS);
            if (isDic)
            {
                if (type == PubConst.DicArraySO)
                {
                    var newObj = (Dictionary<string, object>)whereObj;
                    var pars = newObj.Select(it => new SqlParameter(sqlParameterKeyWord + it.Key, it.Value));
                    var sqlParameters = pars as SqlParameter[] ?? pars.ToArray();
                    foreach (var par in sqlParameters)
                    {
                        SetParSize(par);
                    }
                    listParams.AddRange(sqlParameters);
                }
                else
                {
                    var newObj = (Dictionary<string, string>)whereObj;
                    var pars = newObj.Select(it => new SqlParameter(sqlParameterKeyWord + it.Key, it.Value));
                    var sqlParameters = pars as SqlParameter[] ?? pars.ToArray();
                    foreach (var par in sqlParameters)
                    {
                        SetParSize(par);
                    }
                    listParams.AddRange(sqlParameters);
                }
            }
            else
            {
                var propertiesObj = propertyInfo ?? type.GetProperties();
                foreach (var r in propertiesObj)
                {
                    var value = r.GetValue(whereObj, null);
                    if (r.PropertyType.IsEnum)
                    {
                        value = Convert.ToInt64(value);
                    }
                    if (value == null || value.Equals(DateTime.MinValue)) value = DBNull.Value;
                    if (r.Name.ToLower().Contains("hierarchyid"))
                    {
                        var par = new SqlParameter(sqlParameterKeyWord + r.Name, SqlDbType.Udt)
                        {
                            UdtTypeName = "HIERARCHYID",
                            Value = value
                        };
                        listParams.Add(par);
                    }
                    else
                    {
                        var par = new SqlParameter(sqlParameterKeyWord + r.Name, value);
                        SetParSize(par);
                        if (value == DBNull.Value)
                        {//防止文件类型报错
                            //SetSqlDbType(r, par);
                        }
                        listParams.Add(par);
                    }
                }
            }
            return listParams.ToArray();
        }
    }
}
