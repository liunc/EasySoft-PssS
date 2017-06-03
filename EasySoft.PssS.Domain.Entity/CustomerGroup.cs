// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-05-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Entity
{
    using Core.Util;
    using EasySoft.Core.Persistence;
    using System.Data;

    /// <summary>
    /// 客户分组领域实体类
    /// </summary>
    [Table("dbo.CustomerGroup")]
    public class CustomerGroup : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Column(Name = "Name", DataType = DbType.String, Size = 50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置是否为默认设置
        /// </summary>
        [Column(Name = "IsDefault", DataType = DbType.String, Size = 1)]
        public string IsDefault { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = 120)]
        public string Remark { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 添加新分组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string remark, string creator)
        {
            this.NewId();
            this.IsDefault = "N";
            this.SetCreator(creator);
            this.Update(name, remark, creator);
        }

        /// <summary>
        /// 更新分组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="remark">备注</param>
        /// <param name="mender">修改人</param>
        public void Update(string name, string remark, string mender)
        {
            this.Name = name.Trim();
            this.Remark = DataConvert.ConvertNullToEmptyString(remark);
            this.SetMender(mender);
        }

        public bool CanDelete()
        {
            return this.IsDefault == Constant.COMMON_N;
        }

        #endregion
    }
}
