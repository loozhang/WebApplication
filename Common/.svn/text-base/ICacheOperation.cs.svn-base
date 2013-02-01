namespace Common
{
    /// <summary>
    /// 缓存操作接口
    /// </summary>
    public interface ICacheOperation
    {
        /// <summary>
        /// 新增键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool Add(string key, object value);

        /// <summary>
        /// 删除键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool Delete(string key);
        /// <summary>
        /// 获取键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// 判断键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool KeyExists(string key);

        /// <summary>
        /// 设置键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool Set(string key, object value);

    }
}
