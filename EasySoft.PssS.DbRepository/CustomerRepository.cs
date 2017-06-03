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
    /// 客户仓储实现类
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer, string>, ICustomerRepository
    {
        #region 方法

        /// <summary>
        /// 批量修改客户分组
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="oldGroupId">旧分组Id</param>
        /// <param name="newGroupId">新分组Id</param>
        public void BatUpdateGroupId(DbTransaction trans, string oldGroupId, string newGroupId)
        {
            string cmdText = string.Format("UPDATE {0} SET [GroupId] = @NewGroupId WHERE [GroupId] = @OldGroupId", this.Resolver.TableName);
            DbParameter[] paras = new DbParameter[] 
            {
                DbHelper.CreateParameter("OldGroupId", DbType.String, 32),
                DbHelper.CreateParameter("NewGroupId", DbType.String, 32)
            };
            paras[0].Value = oldGroupId;
            paras[1].Value = newGroupId;

            if(trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 查询客户表信息，用于列表分页显示
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户数据集合</returns>
        public List<Customer> Search(string name, string groupId, int pageIndex, int pageSize, ref int totalCount)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(name))
            {
                conditions.Add("[Name] LIKE @Name");
                para = DbHelper.CreateParameter("Name", DbType.String, 50);
                para.Value = "%" + name + "%";
                paras.Add(para);
            }
            if (!string.IsNullOrEmpty(groupId))
            {
                conditions.Add("[GroupId] = @GroupId");
                para = DbHelper.CreateParameter("GroupId", DbType.String, 32);
                para.Value = groupId;
                paras.Add(para);
            }
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            string cmdText = string.Format("{0} {1}", this.Resolver.SelectAllCommandText, whereCmdText);
            string totalCmdText = string.Format("{0} {1}", this.Resolver.CountAllCommandText, whereCmdText);
            totalCount = Convert.ToInt32(DbHelper.ExecuteScalar(totalCmdText, paras.ToArray()));
            return this.Paging(cmdText, pageSize, totalCount, pageIndex, "[CreateTime] DESC", paras.ToArray());
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override Customer SetEntity(DbDataReader reader)
        {
            Customer entity = new Customer
            {
                Id = reader["Id"].ToString(),
                Name = reader["Name"].ToString(),
                GroupId = reader["GroupId"].ToString(),
                Nickname = reader["Nickname"].ToString(),
                Mobile = reader["Mobile"].ToString(),
                WeChatId = reader["WeChatId"].ToString()
            };
            entity.SetCreator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"]));
            entity.SetMender(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]));
            return entity;
        }

        #endregion

    }
}
