using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// 缓存辅助类
    /// </summary>
    public static class CacheHelper
    {
        private static readonly ICacheOperation _CacheDefault = new AspnetCachedOperation();

        /// <summary>
        /// 默认缓存管理实例
        /// </summary>
        public static ICacheOperation CacheDefault
        {
            get
            {
                return _CacheDefault;
            }
        }

        /// <summary>
        /// 获取缓存键时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string key)
        {
            return DicTime[key];
        }

        /// <summary>
        /// 某键上次缓存时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsExistDateime(string key)
        {
            return DicTime.ContainsKey(key);
        }

        /// <summary>
        /// 设置缓存时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        public static void SetDateTime(string key, DateTime time)
        {
            DicTime[key] = time;
        }

        private static readonly Dictionary<string, DateTime> DicTime = new Dictionary<string, DateTime>(100);

        /// <summary>
        /// 中等缓存时间
        /// </summary>
        public const int SecondMiddle = 30;
    }

    /// <summary>
    /// 缓存辅助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class CacheHelper<T>
    {
        /// <summary>
        /// 运行方法，加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="f"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static T CacheFunRun(string key, Func<T> f, int second)
        {
            if (!CacheHelper.IsExistDateime(key))
            {
                CacheHelper.SetDateTime(key, DateTime.Now);
            }


            if (CacheHelper.CacheDefault.KeyExists(key))
            {
                if (CacheHelper.GetDateTime(key).AddSeconds(second) > DateTime.Now)
                {
                    return (T)CacheHelper.CacheDefault.Get(key);
                }
                else
                {
                    CacheHelper.SetDateTime(key, DateTime.Now);
                    var data = f();
                    CacheHelper.CacheDefault.Set(key, data);
                    return data;
                }
            }
            else
            {
                var data = f();
                CacheHelper.CacheDefault.Set(key, data);
                return data;
            }
        }

    }
}
