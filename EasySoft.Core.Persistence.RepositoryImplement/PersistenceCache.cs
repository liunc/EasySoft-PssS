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
namespace EasySoft.Core.Persistence.RepositoryImplement
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading;

    /// <summary>
    /// 持久组件缓存类
    /// </summary>
    public class PersistenceCache
    {
        #region 变量

        private static Dictionary<string, string> stringObjects = new Dictionary<string, string>();
        private static Dictionary<string, List<string>> listObjects = new Dictionary<string, List<string>>();
        private static Dictionary<string, Dictionary<string, DbParameter>> dbParameters = new Dictionary<string, Dictionary<string, DbParameter>>();
        private static Dictionary<string, PersistenceResolver> persistenceResolvers = new Dictionary<string, PersistenceResolver>();
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
                        readerWriterLockSlim.ExitWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.ExitUpgradeableReadLock();
                }
            }
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>返回关键字对应的值</returns>
        public static List<string> GetListObjects(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (listObjects == null)
                {
                    listObjects = new Dictionary<string, List<string>>();
                    return null;
                }
                List<string> value = null;
                if (listObjects.TryGetValue(key, out value))
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
        public static void SetListObjects(string key, List<string> value)
        {
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (listObjects == null)
                {
                    listObjects = new Dictionary<string, List<string>>();
                }
                if (listObjects.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("{0} is exists in Persistence Cache."));
                }
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    listObjects.Add(key, value);
                }
                finally
                {
                    if (readerWriterLockSlim.IsWriteLockHeld)
                    {
                        readerWriterLockSlim.ExitWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.ExitUpgradeableReadLock();
                }
            }
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>返回关键字对应的值</returns>\[
        public static Dictionary<string, DbParameter> GetDbParameters(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (dbParameters == null)
                {
                    dbParameters = new Dictionary<string, Dictionary<string, DbParameter>>();
                    return null;
                }
                Dictionary<string, DbParameter> value = null;
                if (dbParameters.TryGetValue(key, out value))
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
        public static void SetDbParameters(string key, Dictionary<string, DbParameter> value)
        {
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (dbParameters == null)
                {
                    dbParameters = new Dictionary<string, Dictionary<string, DbParameter>>();
                }
                if (dbParameters.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("{0} is exists in Persistence Cache."));
                }
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    dbParameters.Add(key, value);
                }
                finally
                {
                    if (readerWriterLockSlim.IsWriteLockHeld)
                    {
                        readerWriterLockSlim.ExitWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.ExitUpgradeableReadLock();
                }
            }
        }

        /// <summary>
        /// 获取对象值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>返回关键字对应的值</returns>
        public static PersistenceResolver GetPersistenceResolvers(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();
                if (persistenceResolvers == null)
                {
                    persistenceResolvers = new Dictionary<string, PersistenceResolver>();
                    return null;
                }
                PersistenceResolver value = null;
                if (persistenceResolvers.TryGetValue(key, out value))
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
        public static void SetPersistenceResolvers(string key, PersistenceResolver value)
        {
            try
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();

                if (persistenceResolvers == null)
                {
                    persistenceResolvers = new Dictionary<string, PersistenceResolver>();
                }
                if (persistenceResolvers.ContainsKey(key))
                {
                    throw new ArgumentException(string.Format("{0} is exists in Persistence Cache."));
                }
                try
                {
                    readerWriterLockSlim.EnterWriteLock();
                    persistenceResolvers.Add(key, value);
                }
                finally
                {
                    if (readerWriterLockSlim.IsWriteLockHeld)
                    {
                        readerWriterLockSlim.ExitWriteLock();
                    }
                }
            }
            finally
            {
                if (readerWriterLockSlim.IsUpgradeableReadLockHeld)
                {
                    readerWriterLockSlim.ExitUpgradeableReadLock();
                }
            }
        }

        #endregion
    }
}
