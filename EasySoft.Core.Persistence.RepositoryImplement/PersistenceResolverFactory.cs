// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据库仓储实现库
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

    /// <summary>
    /// Persistence Resolve工厂类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    internal class PersistenceResolverFactory
    {

        #region 属性

        /// <summary>
        /// 获取PersistenceResolve
        /// </summary>
        public static PersistenceResolver CreateResolver(Type entityType)
        {
            string key = string.Format("PersistenceResolver_{0}", entityType.ToString());
            PersistenceResolver resolver = PersistenceCache.GetPersistenceResolvers(key);
            if (resolver == null)
            {
                string[] temps = DbHelper.ProviderName.Split(new char[] { '.' });
                if (temps.Length > 0)
                {
                    Type type = Type.GetType(string.Format("EasySoft.Core.Persistence.RepositoryImplement.{0}PersistenceResolver", temps[temps.Length - 1]));
                    resolver = (PersistenceResolver)Activator.CreateInstance(type);
                    resolver.EntityType = entityType;
                }
                if (resolver == null)
                {
                    throw new ArgumentNullException("PersistenceResolve");
                }
                PersistenceCache.SetPersistenceResolvers(key, resolver);
            }

            return resolver;
        }

        #endregion
    }
}
