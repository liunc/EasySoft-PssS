using EasySoft.Core.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySoft.Core.DbRepository
{
    public class SqlClientBaseRepository<T> : BaseRepository<T>
    {
        protected string EntityName
        {
            get;
            private set;
        }

        public SqlClientBaseRepository()
        {
            this.EntityName = default(T).ToString();
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        /// <param name="entity">数据实体泛型对象</param>
        /// <returns>返回数据表名称</returns>
        private string GetTableName(T entity)
        {
            string key = string.Format("{0}_TableName", this.EntityName);
            string name = DbObjectCache.GetStringByKey(key);
            if (string.IsNullOrWhiteSpace(name))
            {
                object[] customAttributes = entity.GetType().GetCustomAttributes(typeof(DbTableAttribute), true);
                if (!((customAttributes == null) || customAttributes.Length.Equals(0)))
                {
                    DbTableAttribute attribute = customAttributes[0] as DbTableAttribute;
                    name = attribute.Name;
                    DbObjectCache.SetStringValue(key, name);
                }
            }
            return name;
        }
    }
}
