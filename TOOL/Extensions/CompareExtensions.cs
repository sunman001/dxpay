using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JMP.Model;

namespace TOOL.Extensions
{
    /// <summary>
    /// 实体变更跟踪器
    /// </summary>
    public class EntityModifiedTracker
    {
        /// <summary>
        /// 变更的属性详情集合
        /// </summary>
        private readonly List<Modified> _modifiedProperties;
        public EntityModifiedTracker()
        {
            _modifiedProperties = new List<Modified>();
        }
        /// <summary>
        /// 变更的属性详情集合
        /// </summary>
        public List<Modified> ModifiedProperties
        {
            get { return _modifiedProperties; }
        }

        /// <summary>
        /// 添加变更记录到变更集合中
        /// </summary>
        /// <param name="modified">实体变更属性实体</param>
        public void Add(Modified modified)
        {
            _modifiedProperties.Add(modified);
        }

        /// <summary>
        /// 获取所有被修改字段的详情
        /// </summary>
        /// <returns></returns>
        public string Message
        {
            get
            {
                if (_modifiedProperties.Count(x => !x.IsOriginalString) <= 0)
                {
                    return "操作了修改按钮，无数据更改！";
                }
                return string.Join(" , ", _modifiedProperties.Where(x => x.NewValue != x.OldValue).Select(x => string.Format("{0}:{1}从{2}修改为:{3}", string.IsNullOrEmpty(x.Label) ? x.PropertyName : x.Label, "[" + x.PropertyName + "]", x.OldValue, x.NewValue)));
            }
        }
    }

    /// <summary>
    /// 实体创建跟踪器(记录实体的每个属性及对应值)
    /// </summary>
    public class EntityCreateTracker
    {
        /// <summary>
        /// 被创建的实体的属性详情集合
        /// </summary>
        private readonly List<Creator> _createdProperties;
        /// <summary>
        /// 实体创建跟踪器(记录实体的每个属性及对应值)
        /// </summary>
        public EntityCreateTracker()
        {
            _createdProperties = new List<Creator>();
        }
        /// <summary>
        /// 被创建的实体的属性详情集合
        /// </summary>
        public List<Creator> CreatedProperties
        {
            get { return _createdProperties; }
        }

        /// <summary>
        /// 向实体创建跟踪器中添加一条实体属性详情数据
        /// </summary>
        /// <param name="prop">实体创建属性实体</param>
        public void Add(Creator prop)
        {
            _createdProperties.Add(prop);
        }

        /// <summary>
        /// 获取创建实体对应字段的详情
        /// </summary>
        /// <returns></returns>
        public string Message
        {
            get
            {
                return string.Join(" , ", _createdProperties.Select(x => string.Format("{0}:{1}值:{2}", string.IsNullOrEmpty(x.Label) ? x.PropertyName : x.Label, "[" + x.PropertyName + "]", x.Value)));
            }
        }
    }

    /// <summary>
    /// 实体变更属性实体 
    /// </summary>
    public class Modified
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 标签名
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 原始值
        /// </summary>
        public object OldValue { get; set; }
        /// <summary>
        /// 修改后的新值
        /// </summary>
        public object NewValue { get; set; }
        /// <summary>
        /// 是否为原始值的序列化字符串
        /// </summary>
        public bool IsOriginalString { get; set; }
    }

    /// <summary>
    /// 实体创建属性实体
    /// </summary>
    public class Creator
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 标签名
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 属性对应的值
        /// </summary>
        public object Value { get; set; }
    }
    public static class CompareExtensions
    {
        /// <summary>
        /// 对比两个实例的属性值,如果目标实例属性的值和源实例属性的值不相等,则将源实例属性的值赋给目标实体的属性
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="distObject">目标实例</param>
        /// <param name="sourceObject">源实例</param>
        /// <param name="ignoreProperties">需要忽略的属性值(被忽略的属性将不会把源实例的属性数据赋给目标实例的属性)</param>
        /// <returns></returns>
        public static T CompareObject<T>(this T distObject, T sourceObject, string[] ignoreProperties = null)
        {
            var disType = distObject.GetType();
            var disProps = disType.GetProperties();

            var sourceType = sourceObject.GetType();
            var sourceProps = sourceType.GetProperties();

            foreach (var disProp in disProps)
            {
                if (ignoreProperties != null && ignoreProperties.Length > 0 && ignoreProperties.Contains(disProp.Name))
                {
                    continue;
                }
                if (sourceProps.All(x => x.Name != disProp.Name)) continue;
                var disValue = disProp.GetValue(distObject, null);
                var sourceValue = sourceType.GetProperty(disProp.Name).GetValue(sourceObject, null);
                if (disValue == null && sourceValue == null)
                {
                    continue;
                }
                if (disValue == null)
                {
                    disProp.SetValue(distObject, sourceValue, null);
                    continue;
                }
                if (!disValue.Equals(sourceValue))
                {
                    disProp.SetValue(distObject, sourceValue, null);
                }
            }
            return distObject;
        }

        /// <summary>
        /// 对比两个实例的属性值,如果目标实例属性的值和源实例属性的值不相等,则将源实例属性的值赋给目标实体的属性
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="distObject">目标实例</param>
        /// <param name="sourceObject">源实例</param>
        /// <param name="modifiedRoot"></param>
        /// <param name="ignoreProperties">需要忽略的属性值(被忽略的属性将不会把源实例的属性数据赋给目标实例的属性)</param>
        /// <returns></returns>
        public static T CompareObject<T>(this T distObject, T sourceObject, out EntityModifiedTracker modifiedRoot, string[] ignoreProperties = null)
        {
            modifiedRoot = new EntityModifiedTracker();
            var disType = distObject.GetType();
            var disProps = disType.GetProperties();

            var sourceType = sourceObject.GetType();
            var sourceProps = sourceType.GetProperties();

            foreach (var disProp in disProps)
            {
                if (ignoreProperties != null && ignoreProperties.Length > 0 && ignoreProperties.Contains(disProp.Name))
                {
                    continue;
                }
                if (sourceProps.All(x => x.Name != disProp.Name)) continue;
                var disValue = disProp.GetValue(distObject, null);
                var sourceValue = sourceType.GetProperty(disProp.Name).GetValue(sourceObject, null);
                if (disValue == null && sourceValue == null)
                {
                    continue;
                }
                if (disValue == null)
                {
                    disProp.SetValue(distObject, sourceValue, null);
                    modifiedRoot.Add(new Modified
                    {
                        PropertyName = disProp.Name,
                        OldValue = null,
                        NewValue = sourceValue
                    });
                    disProp.SetValue(distObject, sourceValue, null);
                    continue;
                }
                if (!disValue.Equals(sourceValue))
                {
                    modifiedRoot.Add(new Modified
                    {
                        PropertyName = disProp.Name,
                        OldValue = disValue,
                        NewValue = sourceValue
                    });
                    disProp.SetValue(distObject, sourceValue, null);
                }
            }
            return distObject;
        }

        /// <summary>
        /// 对比两个实例的属性值,如果目标实例属性的值和源实例属性的值不相等,则将源实例属性的值赋给目标实体的属性
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="distObject">目标实例[原始值]</param>
        /// <param name="sourceObject">源实例[修改后的值]</param>
        /// <param name="ignoreProperties">需要忽略的属性值(被忽略的属性将不会把源实例的属性数据赋给目标实例的属性)</param>
        /// <returns></returns>
        public static EntityModifiedTracker GetModifiedTracker<T>(this T distObject, T sourceObject, string[] ignoreProperties = null)
        {
            var mod = new EntityModifiedTracker();
            var disType = distObject.GetType();
            var disProps = disType.GetProperties();

            var sourceType = sourceObject.GetType();
            var sourceProps = sourceType.GetProperties();

            foreach (var disProp in disProps)
            {
                var attr = (EntityTrackerAttribute)disProp.GetCustomAttribute(typeof(EntityTrackerAttribute), false);
                if (attr != null && attr.Ignore)
                {
                    var ignore = attr.Ignore;
                    continue;
                }
                if (ignoreProperties != null && ignoreProperties.Length > 0 && ignoreProperties.Contains(disProp.Name))
                {
                    continue;
                }
                if (sourceProps.All(x => x.Name != disProp.Name)) continue;
                var disValue = disProp.GetValue(distObject, null);
                var sourceValue = sourceType.GetProperty(disProp.Name).GetValue(sourceObject, null);
                if (disValue == null && sourceValue == null)
                {
                    continue;
                }
                if (disValue.Equals(sourceValue))
                {
                    continue;
                }
                var c = new Modified
                {
                    PropertyName = disProp.Name,
                    OldValue = disValue,
                    NewValue = sourceValue
                };
                if (attr != null && !string.IsNullOrEmpty(attr.Label))
                {
                    c.Label = attr.Label;
                }
                mod.Add(c);
            }
            return mod;
        }

        /// <summary>
        /// 对比两个实例的属性值,如果目标实例属性的值和源实例属性的值不相等,则将源实例属性的值赋给目标实体的属性
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="distObject">目标实例[原始值]</param>
        /// <param name="sourceObject">源实例[修改后的值]</param>
        /// <returns></returns>
        public static EntityModifiedTracker GetModifiedTracker<T>(this T distObject, T sourceObject)
        {
            var mod = new EntityModifiedTracker();
            var disType = distObject.GetType();
            var disProps = disType.GetProperties();

            var sourceType = sourceObject.GetType();
            var sourceProps = sourceType.GetProperties();

            foreach (var disProp in disProps)
            {
                var attr = (EntityTrackerAttribute)disProp.GetCustomAttribute(typeof(EntityTrackerAttribute), false);
                if (attr != null && attr.Ignore)
                {
                    continue;
                }
                if (sourceProps.All(x => x.Name != disProp.Name)) continue;
                var disValue = disProp.GetValue(distObject, null);
                var sourceValue = sourceType.GetProperty(disProp.Name).GetValue(sourceObject, null);
                if (disValue == null && sourceValue == null)
                {
                    continue;
                }

                if (disValue != null && disValue.Equals(sourceValue) && disValue.ToString().Replace(" ", "") == sourceValue.ToString().Replace(" ", ""))
                {
                    continue;
                }
                var c = new Modified
                {
                    PropertyName = disProp.Name,
                    OldValue = disValue,
                    NewValue = sourceValue
                };
                if (attr != null && !string.IsNullOrEmpty(attr.Label))
                {
                    c.Label = attr.Label;
                }
                mod.Add(c);
            }
            try
            {
                mod.Add(new Modified
                {
                    IsOriginalString = true,
                    Label = "original_entity",
                    PropertyName= "original_entity",
                    OldValue = JMP.TOOL.JsonHelper.Serialize(distObject),
                    NewValue= JMP.TOOL.JsonHelper.Serialize(sourceObject)
                });
            }
            catch { }
            return mod;
        }

        /// <summary>
        /// 获取实体属性及对应的值
        /// </summary>
        /// <typeparam name="T">泛型实体对象</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public static EntityCreateTracker GetCreateEntityPropTracker<T>(this T entity)
        {
            var mod = new EntityCreateTracker();
            var props = entity.GetType().GetProperties();
            foreach (var pi in props)
            {
                var attr = (EntityTrackerAttribute)pi.GetCustomAttribute(typeof(EntityTrackerAttribute), false);
                if (attr != null && attr.Ignore)
                {
                    continue;
                }
                var c = new Creator
                {
                    PropertyName = pi.Name,
                    Value = pi.GetValue(entity, null),
                    Label = ""
                };

                if (attr != null && !string.IsNullOrEmpty(attr.Label))
                {
                    c.Label = attr.Label;
                }
                mod.Add(c);
            }
            return mod;
        }
    }
}