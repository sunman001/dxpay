using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace JMP.TOOL
{
    public class CacheHelper
    {
        /// <summary>
        /// CacheHelper对象唯一标识
        /// </summary>
        private static readonly object _object = new object();
        /// <summary>
        /// 缓存数据唯一标识
        /// </summary>
        private static readonly object _objectCache = new object();
        private static CacheHelper cacheHelper = null;
        private CacheHelper()
        { }
        /// <summary>
        /// 单线程使用
        /// </summary>
        public static CacheHelper Single
        {
            get
            {
                if (cacheHelper == null)
                {
                    cacheHelper = new CacheHelper();
                }
                return cacheHelper;
            }
        }
        /// <summary>
        /// 多线程使用
        /// </summary>
        public static CacheHelper SingelLock
        {
            get
            {
                if (cacheHelper == null)
                {
                    lock (_object)
                    {
                        if (cacheHelper == null)
                            cacheHelper = new CacheHelper();
                    }
                }
                return cacheHelper;
            }
        }

        /// <summary>
        /// 判断是否已经缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsCache(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("缓存键不能为空");
            try
            {
                if (MemoryCache.Default[key] == null)
                {
                    return false;
                }
            }
            catch
            {

            }

            return true;
        }

        /// <summary>
        /// 释放默认缓存
        /// </summary>
        public static void ReleaseCache()
        {
            MemoryCache.Default.Dispose();
        }
        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <typeparam name="T">缓存值</typeparam>
        /// <param name="key">缓存key值</param>
        /// <param name="time">缓存时间</param>
        /// <returns>返回缓存值</returns>
        public static T CacheObjectLocak<T>(T t, string key, double time = 0.0)
        {

            if ((object)t == null) throw new ArgumentNullException("缓存数据不能为空");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("缓存键不能为空");
            T objectValue = default(T);
            try
            {
                if (MemoryCache.Default[key] == null)
                {
                    lock (_objectCache)
                    {
                        if (MemoryCache.Default[key] == null)
                        {
                            objectValue = t;
                            var item = new CacheItem(key, objectValue);
                            var policy = new CacheItemPolicy();
                            if (time > 0)
                            {
                                policy.AbsoluteExpiration =
                                DateTimeOffset.Now.AddMinutes(time);
                            }
                            policy.Priority = CacheItemPriority.Default;
                            MemoryCache.Default.Add(item, policy);
                        }
                    }
                }
                else
                {
                    objectValue = (T)MemoryCache.Default[key];
                }
            }
            catch (Exception ex)
            {
                MemoryCache.Default.Dispose();
                throw new Exception("缓存失败，错误信息:" + ex.Message);
            }
            return objectValue;
        }

        /// <summary>
        /// 更新缓存数据
        /// </summary>
        /// <typeparam name="T">缓存值</typeparam>
        /// <param name="key">缓存key值</param>
        /// <param name="time">缓存时间</param>
        /// <returns>返回缓存值</returns>
        public static T UpdateCacheObjectLocak<T>(T t, string key, double time = 0.0)
        {

            if ((object)t == null) throw new ArgumentNullException("缓存数据不能为空");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("缓存键不能为空");
            T objectValue = default(T);
            try
            {
                if (MemoryCache.Default[key] == null)
                {
                    lock (_objectCache)
                    {
                        if (MemoryCache.Default[key] == null)
                        {
                            objectValue = t;
                            var item = new CacheItem(key, objectValue);
                            var policy = new CacheItemPolicy();
                            if (time > 0)
                            {
                                policy.AbsoluteExpiration =
                                DateTimeOffset.Now.AddMinutes(time);
                            }
                            policy.Priority = CacheItemPriority.Default;
                            MemoryCache.Default.Add(item, policy);
                        }
                    }
                }
                else
                {

                    lock (_objectCache)
                    {
                        MemoryCache.Default.Remove(key);
                        if (MemoryCache.Default[key] == null)
                        {
                            objectValue = t;
                            var item = new CacheItem(key, objectValue);
                            var policy = new CacheItemPolicy();
                            if (time > 0)
                            {
                                policy.AbsoluteExpiration =
                                DateTimeOffset.Now.AddMinutes(time);
                            }
                            policy.Priority = CacheItemPriority.Default;
                            MemoryCache.Default.Add(item, policy);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MemoryCache.Default.Dispose();
                throw new Exception("缓存失败，错误信息:" + ex.Message);
            }
            return objectValue;
        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCaChe<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("Key键不能为空!");
            if (MemoryCache.Default[key] == null) throw new Exception("key键没有缓存数据!");
            return (T)MemoryCache.Default[key];
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        /// <typeparam name="T">缓存值</typeparam>
        /// <param name="key">缓存key值</param>
        /// <param name="time">缓存时间</param>
        /// <returns>返回缓存值</returns>
        public static void CacheObject(string key, object value, double time = 0.0)
        {

            if (value == null) throw new ArgumentNullException("缓存数据不能为空");
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("缓存键不能为空");

            if (MemoryCache.Default[key] == null)
            {
                lock (_objectCache)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, value);
                        var policy = new CacheItemPolicy();
                        if (time > 0)
                        {
                            policy.AbsoluteExpiration =
                            DateTimeOffset.Now.AddMinutes(time);
                        }
                        policy.Priority = CacheItemPriority.Default;
                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

        }

        public static object GetCaChe(string key)
        {
            return MemoryCache.Default[key];
        }
    }
}
