﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Fundamentals.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        object Get(string key);
        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        T Get<T>(string key);
        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        void Set(string key, object value);
        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        void Set(string key, object value, DateTime absoluteExpiration);
        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        void Set(string key, object value, TimeSpan slidingExpiration);
        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}
