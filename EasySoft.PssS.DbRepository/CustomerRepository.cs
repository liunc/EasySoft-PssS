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
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.PssS.Application.DataTransfer.Customer;
    using EasySoft.PssS.Domain.Entity;
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

            if (trans == null)
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
            return this.Paging(cmdText, pageSize, totalCount, pageIndex, "[Name]", paras.ToArray());
        }

        /// <summary>
        /// 提供下单选择客户的数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <returns>返回数据</returns>
        public List<CustomerForOrderDTO> GetCustomerListForOrder(string name, string groupId)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter parameter = null;
            if (!string.IsNullOrEmpty(name))
            {
                conditions.Add("A.[Name] LIKE @Name");
                parameter = DbHelper.CreateParameter("Name", DbType.String, 50);
                parameter.Value = "%" + name + "%";
                parameters.Add(parameter);
            }
            if (!string.IsNullOrEmpty(groupId))
            {
                conditions.Add("A.[GroupId] = @GroupId");
                parameter = DbHelper.CreateParameter("GroupId", DbType.String, 32);
                parameter.Value = groupId;
                parameters.Add(parameter);
            }
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            string cmdText = string.Format("SELECT A.[Id], A.[Name], A.[Nickname], A.[Mobile], A.[GroupId], B.[Id] AddressId, B.[Address], B.[Linkman], B.[Mobile] LinkmanMobile, B.[IsDefault] FROM [dbo].[Customer] A JOIN [dbo].[CustomerAddress] B ON A.[Id] = B.[CustomerId] {0} ORDER BY A.[Name] ASC, B.[IsDefault] DESC", whereCmdText);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, parameters.ToArray());

            List<CustomerForOrderDTO> entities = new List<CustomerForOrderDTO>();
            int index = -1;
            string lastId = string.Empty;
            while (reader.Read())
            {
                string id = reader["Id"].ToString();
                CustomerAddressDTO addressDto = new CustomerAddressDTO
                {
                    Id = reader["AddressId"].ToString(),
                    Address = reader["Address"].ToString(),
                    Linkman = reader["Linkman"].ToString(),
                    Mobile = reader["LinkmanMobile"].ToString(),
                    IsDefault = reader["IsDefault"].ToString()
                };
                if (id == lastId)
                {
                    entities[index].CustomerAddressList.Add(addressDto);
                }
                else
                {
                    CustomerForOrderDTO dto = new CustomerForOrderDTO
                    {
                        Id = id,
                        Name = reader["Name"].ToString(),
                        Nickname = reader["Nickname"].ToString(),
                        Mobile = reader["Mobile"].ToString(),
                        GroupId = reader["GroupId"].ToString(),
                        CustomerAddressList = new List<CustomerAddressDTO>()
                    };
                    dto.CustomerAddressList.Add(addressDto);
                    entities.Add(dto);
                    index++;
                    lastId = id;
                }
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entities;
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
            return new Customer
            (
                reader["Id"].ToString(),
                reader["Name"].ToString(),
                reader["Nickname"].ToString(),
                reader["Mobile"].ToString(),
                reader["WeChatId"].ToString(),
                reader["GroupId"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"]),
                reader["Mender"].ToString(),
                Convert.ToDateTime(reader["ModifyTime"])
            );
        }

        #endregion

    }
}
