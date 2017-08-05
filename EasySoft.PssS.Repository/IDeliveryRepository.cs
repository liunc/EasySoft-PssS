// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：仓储接口库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Repository
{
    using EasySoft.Core.Persistence.Repository;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 交付仓储接口
    /// </summary>
    public interface IDeliveryRepository : IBaseRepository<Delivery, string>
    {
        /// <summary>
        /// 查询交付表信息，用于列表分页显示
        /// </summary>
        /// <param name="startDate">查询开始日期</param>
        /// <param name="endDate">查询结束日期</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回交付数据集合</returns>
        List<Delivery> Search(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int totalCount);

    }
}
