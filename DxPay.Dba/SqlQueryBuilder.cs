using System;
using System.Collections.Generic;
using System.Linq;
using DxPay.Dba.Extensions;

namespace DxPay.Dba
{
    public static class SqlQueryBuilder
    {
        /// <summary>
        /// 读取类的字段并生成SQL插入语句
        /// </summary>
        /// <param name="type">泛型对象</param>
        /// <returns>以逗号连接的对象所有属性名称字符串</returns>
        public static string GenerateInsertSqlString(this Type type)
        {
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var autoIncrementColumnName = type.GetIncrementColumnName();
            var props = type.GetMappedPropertyNameList().Where(x => x != autoIncrementColumnName).ToList();
            if (props == null || props.Count <= 0)
            {
                throw new ArgumentNullException(string.Format("对象[{0}]没有可用的映射属性", type.Name));
            }
            var insert = string.Format("INSERT INTO {0} ({1})VALUES ({2});select @@IDENTITY", type.Name, string.Join(",", props), string.Join(",", props.Select(x => "@" + x)));
            return insert;
        }

        /// <summary>
        /// 读取类的字段并生成SQL更新语句
        /// </summary>
        /// <param name="type">泛型对象</param>
        /// <returns>以逗号连接的对象所有属性名称字符串</returns>
        public static string GenerateUpdateSqlString(this Type type)
        {
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var primaryKey = type.GetPrimaryKey();
            var autoIncrementColumnName = type.GetIncrementColumnName();
            if (string.IsNullOrEmpty(autoIncrementColumnName))
            {
                autoIncrementColumnName = primaryKey;
            }
            if (string.IsNullOrEmpty(autoIncrementColumnName))
            {
                throw new NullReferenceException(string.Format("请为对象[{0}]指定标识列或主键", type.Name));
            }
            var props = type.GetMappedPropertyNameList().Where(x => x != autoIncrementColumnName).ToList();
            if (props == null || props.Count <= 0)
            {
                throw new ArgumentNullException(string.Format("对象[{0}]没有可用的映射属性", type.Name));
            }
            var sql = string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", type.Name, string.Join(",", props.Select(x => x + "=@" + x)), primaryKey);
            return sql;
        }

        /// <summary>
        /// 根据指定的自定义对象生成更新语句(可指定要更新的字段),&lt; 注:请在调用此方法时指定SqlParameters &gt;
        /// </summary>
        /// <param name="objUpdate">自定义的更新对象</param>
        /// <param name="type">要更新的实体对象</param>
        /// <param name="id">主键对应的值</param>
        /// <returns></returns>
        public static string GeneratePartialUpdateSqlById<T>(this Type type, object id, object objUpdate)
        {
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var primaryKey = type.GetPrimaryKey();
            var autoIncrementColumnName = type.GetIncrementColumnName();
            var props = type.GetMappedPropertyNameList().ToList();
            var upType = objUpdate.GetType();
            if (upType == null)
            {
                throw new NullReferenceException("自定义更新对象为空");
            }
            var needUpdateProps = upType.GetProperties().Where(x => x.Name != autoIncrementColumnName).Select(x => x.Name);
            var needUpdates = needUpdateProps.Where(updateProp => props.Any(x => string.Equals(x, updateProp, StringComparison.CurrentCultureIgnoreCase))).ToList();
            var sql = string.Format("UPDATE {0} SET {1} WHERE {2}=@{2}", type.Name, string.Join(",", needUpdates.Select(x => x + "=@" + x)), primaryKey);
            return sql;
        }

        /// <summary>
        /// 读取类的字段并生成SQL删除语句
        /// </summary>
        /// <param name="type">泛型对象</param>
        /// <param name="id"></param>
        /// <returns>以逗号连接的对象所有属性名称字符串</returns>
        public static string GenerateDeleteSqlString(this Type type, object id)
        {
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var primaryKey = type.GetPrimaryKey();

            var props = type.GetMappedPropertyNameList();
            if (props == null || props.Count <= 0)
            {
                throw new ArgumentNullException(string.Format("对象[{0}]没有可用的映射属性", type.Name));
            }
            var sql = string.Format("DELETE FROM {0} WHERE {1}={2}", type.Name, primaryKey, id);
            return sql;
        }

        /// <summary>
        /// 读取类的字段并生成SQL批量删除语句
        /// </summary>
        /// <param name="type">泛型对象</param>
        /// <param name="ids"></param>
        /// <returns>以逗号连接的对象所有属性名称字符串</returns>
        public static string GenerateBatchDeleteSqlString(this Type type, string ids)
        {
            if (type == null)
            {
                throw new NullReferenceException();
            }
            var primaryKey = type.GetPrimaryKey();

            var props = type.GetMappedPropertyNameList();
            if (props == null || props.Count <= 0)
            {
                throw new ArgumentNullException(string.Format("对象[{0}]没有可用的映射属性", type.Name));
            }
            var sql = string.Format("DELETE FROM {0} WHERE {1} IN ({2})", type.Name, primaryKey, ids);
            return sql;
        }
    }
}
