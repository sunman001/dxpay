/************聚米支付平台__DataTable转换实体类和list集合************/
//描述：转换实体类和list集合
//功能：转换实体类和list集合
//开发者：秦际攀
//开发时间: 2016.03.16
/************聚米支付平台__DataTable转换实体类和list集合************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JMP.TOOL
{
    /// <summary>
    /// DataTable转换实体类和list集合
    /// </summary>
    public static class MdlList
    {
        /// <summary>
        /// 把一个DataTable实例中的所有的行，转换成指定类型的实体，并构造函数一个泛型List。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable table) where T : class
        {
            List<T> list = new List<T>();

            if (!IsEmptyDataTable(table))
            {
                foreach (DataRow row in table.Rows)
                {
                    var obj = ToModels<T>(row);
                    list.Add(obj);
                }
            }

            return list;
        }

        /// <summary>
        /// 判断一个DataTable是否为空表（为Null或没有记录行）。
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool IsEmptyDataTable(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// 把指定行转换成相应类型的实体。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ToModels<T>(DataRow row) where T : class
        {
            try
            {
                var obj = Activator.CreateInstance<T>();

                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (row.Table.Columns.Contains(prop.Name))
                    {
                        prop.SetValue(obj, row.IsNull(prop.Name) ? null : row[prop.Name], null);
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 把DataTable实例中的第1行转换成相应类型的实体。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T ToModel<T>(DataTable table) where T : class
        {
            if (IsEmptyDataTable(table))
            {
                return null;
            }

            return ToModels<T>(table.Rows[0]);
        }
        /// <summary>  
        /// 数据行转DataTable  
        /// </summary>  
        /// <param name="drs">数据行</param>  
        /// <returns></returns>  
        public static DataTable RowsToTable(DataRow[] drs)
        {
            DataTable dt = null;
            if (drs != null && drs.Length > 0)
            {
                dt = drs[0].Table.Clone();
                for (int i = 0; i < drs.Length; i++)
                {
                    dt.ImportRow(drs[i]);
                }
            }
            return dt;
        }

        /// <summary>  
        /// 条件过滤  
        /// </summary>  
        /// <param name="dt">DataTable</param>  
        /// <param name="where">条件</param>  
        /// <returns></returns>  
        public static DataTable TableSelect(DataTable dt, string where)
        {
            DataTable Result = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drs = dt.Select(where);
                Result = RowsToTable(drs);
            }
            return Result;
        }

        /// <summary>  
        /// 字典类型转化为对象(需要强制转换数据类型待解决)  
        /// </summary>  
        /// <param name="dic"></param>  
        /// <returns></returns>  
        public static T DicToObject<T>(Dictionary<string, string> dic) where T : new()
        {
            var md = new T();
            foreach (var d in dic)
            {
                var value = d.Value;
                md.GetType().GetProperty(d.Key).SetValue(md,value,null);
            }
            return md;
        }



        /// <summary> 
        /// 集合转换DataSet 
        /// </summary> 
        /// <param name="list">集合</param> 
        /// <returns></returns> 
        public static DataSet ToDataSet(IList p_List)
        {
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(p_List[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }
    }
}
