// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：持久组件库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Persistence.Stereotype
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 持久组件缓存类
    /// </summary>
    public class PersistenceCache
    {
        #region 变量

        private static Dictionary<string, string> stringObjects = new Dictionary<string, string>();
        private static Dictionary<string, PropertyInfo[]> propertyInfos = new Dictionary<string, PropertyInfo[]>();
        private static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        #endregion

        #region 方法

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>返回关键字对应的值</returns>
        public static string GetStringObjects(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (stringObjects == null)
                {
                    stringObjects = new Dictionary<string, string>();
                    return string.Empty;
                }
                string value = string.Empty;
                if (stringObjects.TryGetValue(key, out value))
                {
                    return value;
                }
                return string.Empty;
            }
            finally
            {
                if (readerWriterLockSlim.IsReadLockHeld)
                {
                    readerWriterLockSlim.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// 写入对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字值</param>
        public static void SetStringObjects(string key, string value)
        {
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (stringObjects == null)
                {
                    stringObjects = new Dictionary<string, string>();
                }
                if (stringObjects.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("{0} is exists in Persistence Cache."));
                }
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    stringObjects.Add(key, value);
                }
                finally
                {
                    if (readerWriterLockSlim.IsWriteLockHeld)
                    {
                        readerWriterLockSlim.EnterWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.EnterUpgradeableReadLock();
                }
            }
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>返回关键字对应的值</returns>\[
        public static PropertyInfo[] GetPropertyInfos(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (propertyInfos == null)
                {
                    propertyInfos = new Dictionary<string, PropertyInfo[]>();
                    return null;
                }
                PropertyInfo[] value = null;
                if (propertyInfos.TryGetValue(key, out value))
                {
                    return value;
                }
                return null;
            }
            finally
            {
                if (readerWriterLockSlim.IsReadLockHeld)
                {
                    readerWriterLockSlim.ExitReadLock();
                }
            }
        }

        /// <summary>
        /// 写入对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">关键字值</param>
        public static void SetPropertyInfos(string key, PropertyInfo[] value)
        {
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (propertyInfos == null)
                {
                    propertyInfos = new Dictionary<string, PropertyInfo[]>();
                }
                if (propertyInfos.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("{0} is exists in Persistence Cache."));
                }
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    propertyInfos.Add(key, value);
                }
                finally
                {
                    if (readerWriterLockSlim.IsWriteLockHeld)
                    {
                        readerWriterLockSlim.EnterWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.EnterUpgradeableReadLock();
                }
            }
        }

        #endregion
    }
}
