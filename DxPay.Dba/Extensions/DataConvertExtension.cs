using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace DxPay.Dba.Extensions
{
    /// <summary>
    /// 数据转换静态扩展类
    /// </summary>
    public static class DataConvertExtension
    {
        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 将DataTable转换成泛型集合
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="table">DataTable</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table, Type type = null) where T : class
        {
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
            {
                throw new NullReferenceException("对象无默认构造函数");
            }

            var list = new List<T>();
            //属性修饰符约束
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //读取对象属性的名称和数据类型
            var objFieldNames = typeof(T).GetProperties(flags)
                .Select(item => new
                {
                    item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //读取DataTable中每列的名称和数据类型
            var dtlFieldNames = table.Columns.Cast<DataColumn>()
                .Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                var obj = default(T);
                try
                {
                    obj = Activator.CreateInstance<T>();
                }
                catch
                {
                    if (type != null)
                    {
                        obj = (T)FormatterServices.GetSafeUninitializedObject(type);
                    }
                }
                if (obj == null)
                {
                    continue;
                }
                var tType = obj.GetType();
                foreach (var prop in objFieldNames)
                {
                    if (!dtlFieldNames.Any(x => x.Name.Equals(prop.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        continue;
                    }
                    var propertyInfo = tType.GetProperty(prop.Name);
                    var rowValue = row[prop.Name];
                    if (propertyInfo == null) continue;
                    var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                    var safeValue = (rowValue == null || DBNull.Value.Equals(rowValue)) ? null : Convert.ChangeType(rowValue, t);
                    propertyInfo.SetValue(obj, safeValue, null);
                }
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// 将DataTable中的第一行转换成泛型对象
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="table">DataTable</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataTable table, Type type = null) where T : class
        {
            if (typeof(T).GetConstructor(Type.EmptyTypes) == null)
            {
                throw new NullReferenceException("对象无默认构造函数");
            }
            var obj = default(T);
            try
            {
                obj = Activator.CreateInstance<T>();
            }
            catch
            {
                if (type != null)
                {
                    obj = (T)FormatterServices.GetSafeUninitializedObject(type);
                }
            }

            var tType = obj.GetType();
            //属性修饰符约束
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //读取对象属性的名称和数据类型
            var objFieldNames = typeof(T).GetProperties(flags)
                .Select(item => new
                {
                    item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //读取DataTable中每列的名称和数据类型
            var dtlFieldNames = table.Columns.Cast<DataColumn>()
                .Select(item => new
                {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();
            if (table.Rows == null || table.Rows.Count == 0)
            {
                return obj;
            }

            var row = table.Rows[0];
            foreach (var prop in objFieldNames)
            {
                if (!dtlFieldNames.Any(x => x.Name.Equals(prop.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    continue;
                }
                var propertyInfo = tType.GetProperty(prop.Name);
                var rowValue = row[prop.Name];
                if (propertyInfo == null) continue;
                var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                var safeValue = (rowValue == null || DBNull.Value.Equals(rowValue)) ? null : Convert.ChangeType(rowValue, t);
                propertyInfo.SetValue(obj, safeValue, null);
            }

            return obj;
        }
    }
}
