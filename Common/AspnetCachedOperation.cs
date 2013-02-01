using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    /// <summary>
    /// Asp.Net缓存操作
    /// </summary>
    public class AspnetCachedOperation : ICacheOperation
    {
        // private ICacheManager cache = CacheFactory.GetCacheManager();

        #region ICacheOperation Members

        /// <summary>
        /// 新增键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            return Add(key, value);
        }

        /// <summary>
        /// 删除键值对
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            if (cache[key] == null)
                return false;
            cache.Remove(key);
            return true;
        }

        readonly Dictionary<string, object> cache = new Dictionary<string, object>();


        readonly DateTime begin = DateTime.Now.AddMinutes(3);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return this.cache[key];
        }

        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            if (DateTime.Now > begin)
            {
                cache.Remove(key);
                return false;
            }
            return cache.Keys.Contains(key);
        }

        /// <summary>
        /// 设置键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string key, object value)
        {
            cache.Remove(key);
            cache.Add(key, value);
            return true;
        }

        #endregion
    }
}
