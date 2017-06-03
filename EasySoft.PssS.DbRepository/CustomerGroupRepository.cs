// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：数据库仓储类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.DbRepository
{
    using Core.Persistence.RepositoryImplement;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 客户分组仓储实现类
    /// </summary>
    public class CustomerGroupRepository : BaseRepository<CustomerGroup, string>, ICustomerGroupRepository
    {
        #region 方法

        /// <summary>
        /// 判断是否存在名称相同的分组
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id，新增时为空字符串</param>
        /// <param name="name">分组名称</param>
        /// <returns>返回布尔值</returns>
        public bool HasSameName(DbTransaction trans, string id, string name)
        {
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = DbHelper.CreateParameter("Name", DbType.String, 50);
            para.Value = name;
            paras.Add(para);
            string cmdText = string.Format("{0} WHERE [Name] = @Name", this.Resolver.CountAllCommandText);
            if (!string.IsNullOrWhiteSpace(id))
            {
                cmdText += " AND [Id] <> @Id";
                para = DbHelper.CreateParameter("Id", DbType.String, 32);
                para.Value = id;
                paras.Add(para);
            }

            object obj = null;
            if (trans == null)
            {
                obj = DbHelper.ExecuteScalar(cmdText, paras.ToArray());
            }
            else
            {
                obj = DbHelper.ExecuteScalar(trans, cmdText, paras.ToArray());
            }
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            return Convert.ToInt32(obj) == 0 ? false : true;
        }

        /// <summary>
        /// 判断是否默认分组
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id，新增时为空字符串</param>
        /// <returns>返回布尔值</returns>
        public string GetDefaultGroupId(DbTransaction trans)
        {
            string cmdText = string.Format("SELECT [Id] FROM {0} WHERE [IsDefault] = 'Y'", this.Resolver.TableName);

            object obj = null;
            if (trans == null)
            {
                obj = DbHelper.ExecuteScalar(cmdText);
            }
            else
            {
                obj = DbHelper.ExecuteScalar(trans, cmdText);
            }
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        /// <summary>
        /// 查询客户分组表信息，用于列表分页显示
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户分组数据集合</returns>
        public List<CustomerGroup> Search(int pageIndex, int pageSize, ref int totalCount)
        {
            totalCount = Convert.ToInt32(DbHelper.ExecuteScalar(this.Resolver.CountAllCommandText));
            return this.Paging(this.Resolver.SelectAllCommandText, pageSize, totalCount, pageIndex, "[CreateTime] ASC");
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override CustomerGroup SetEntity(DbDataReader reader)
        {
            CustomerGroup entity = new CustomerGroup
            {
                Id = reader["Id"].ToString(),
                Name = reader["Name"].ToString(),
                IsDefault = reader["IsDefault"].ToString(),
                Remark = reader["Remark"].ToString()
            };
            entity.SetCreator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"]));
            entity.SetMender(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]));
            return entity;
        }

        #endregion

    }
}
