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
    using Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Xml;

    /// <summary>
    /// 成本配置仓储实现类
    /// </summary>
    public class CostItemRepository : BaseRepository<CostItem, string>, ICostItemRepository
    {

        #region 方法

        /// <summary>
        /// 判断是否存在代码相同的成本项
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id，新增时为空字符串</param>
        /// <param name="code">成本项代码</param>
        /// <returns>返回布尔值</returns>
        public bool HasSameCode(DbTransaction trans, string id, string code)
        {
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = DbHelper.CreateParameter("Code", DbType.String, Constant.STRING_LENGTH_10);
            para.Value = code;
            paras.Add(para);
            string cmdText = string.Format("{0} WHERE [Code] = @Code", this.Resolver.CountAllCommandText);
            if (!string.IsNullOrWhiteSpace(id))
            {
                cmdText += " AND [Id] <> @Id";
                para = DbHelper.CreateParameter("Id", DbType.String, Constant.STRING_LENGTH_32);
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
        /// 查询成本项表信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回成本项数据集合</returns>
        public List<CostItem> Search(string category, string isValid)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                para = DbHelper.CreateParameter("Category", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = category;
                paras.Add(para);
            }
            if (!string.IsNullOrEmpty(isValid))
            {
                conditions.Add("[IsValid] = @IsValid");
                para = DbHelper.CreateParameter("IsValid", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = isValid;
                paras.Add(para);
            }
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }

            string cmdText = string.Format("{0} {1} ORDER BY [OrderNumber]", this.Resolver.SelectAllCommandText, whereCmdText);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, paras.ToArray());

            List<CostItem> entities = new List<CostItem>();
            while (reader.Read())
            {
                entities.Add(this.SetEntity(reader));
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entities;
        }

        /// <summary>
        /// 查询成本项表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回成本项数据集合</returns>
        public List<CostItem> Search(string category, int pageIndex, int pageSize, ref int totalCount)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                para = DbHelper.CreateParameter("Category", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = category;
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
        protected override CostItem SetEntity(DbDataReader reader)
        {
            return new CostItem
            (
                reader["Id"].ToString(),
                reader["Name"].ToString(),
                reader["Code"].ToString(),
                reader["Category"].ToString(),
                reader["IsValid"].ToString(),
                Convert.ToInt16(reader["OrderNumber"]),
                reader["Remark"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"]),
                reader["Mender"].ToString(),
                Convert.ToDateTime(reader["ModifyTime"])
            );
        }

        #endregion

    }
}
