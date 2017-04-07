using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasySoft.Core.DbRepository
{
    public static class DbObjectCache
    {
        private static Dictionary<string, string> stringCache = new Dictionary<string, string>();
        private static Dictionary<string, List<DbParameter>> dbParameterCache = new Dictionary<string, List<DbParameter>>();
        private static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();

        public static string GetStringByKey(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();

                if (stringCache == null)
                {
                    return string.Empty;
                }
                if (stringCache.ContainsKey(key))
                {
                    return stringCache[key];
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

        public static void SetStringValue(string key, string value)
        {
            try
            {
                readerWriterLockSlim.ExitWriteLock();
                if (stringCache == null)
                {
                    stringCache = new Dictionary<string, string>();
                }
                stringCache.Add(key, value);
            }
            finally
            {
                if (readerWriterLockSlim.IsWriteLockHeld)
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
            }
        }

        public static List<DbParameter> GetDbParameterListByKey(string key)
        {
            try
            {
                readerWriterLockSlim.EnterReadLock();

                if (dbParameterCache == null)
                {
                    return new List<DbParameter>();
                }
                if (dbParameterCache.ContainsKey(key))
                {
                    return dbParameterCache[key];
                }
                return new List<DbParameter>();
            }
            finally
            {
                if (readerWriterLockSlim.IsReadLockHeld)
                {
                    readerWriterLockSlim.ExitReadLock();
                }
            }
        }

        public static void SetDbParameterList(string key, List<DbParameter> value)
        {
            try
            {
                readerWriterLockSlim.ExitWriteLock();
                if (dbParameterCache == null)
                {
                    dbParameterCache = new Dictionary<string, List<DbParameter>>();
                }
                dbParameterCache.Add(key, value);
            }
            finally
            {
                if (readerWriterLockSlim.IsWriteLockHeld)
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
            }
        }
    }
}
