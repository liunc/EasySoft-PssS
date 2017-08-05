// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------

namespace EasySoft.PssS.Domain.Entity
{
    using Core.Persistence;
    using Core.Util;
    using System;
    using System.Data;

    /// <summary>
    /// 快递公司领域实体类
    /// </summary>
    [Table("dbo.ExpressCompany")]
    public class ExpressCompany : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Column(Name = "Name", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Name { get; private set; }

        /// <summary>
        /// 获取或设置编码
        /// </summary>
        [Column(Name = "Code", DataType = DbType.String, Size = Constant.STRING_LENGTH_10, AllowEdit = false)]
        public string Code { get; private set; }
        
        /// <summary>
        /// 获取或设置是否有效
        /// </summary>
        [Column(Name = "IsValid", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string IsValid { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; set; }

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExpressCompany()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public ExpressCompany(string id, string name, string code, string isValid, string remark, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.Name = name;
            this.Code = code;
            this.IsValid = IsValid;
            this.Remark = remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建快递公司
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="creator">创建人</param>
        public void Create(string name, string code, string creator)
        {
            base.Create(creator);
            this.Name = name;
            this.Code = code;
            this.IsValid = Constant.COMMON_Y;
        }

        /// <summary>
        /// 修改快递公司
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="mender">修改人</param>
        public void Update(string name, string isValid, string mender)
        {
            base.Update(mender);
            this.Name = name;
            this.IsValid = isValid;
        }

        #endregion
    }
}
