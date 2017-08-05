// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域服务类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Service
{
    using EasySoft.PssS.Application.DataTransfer.Sale;
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.Core.Util;
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 销售订单领域服务类
    /// </summary>
    public class SaleOrderService
    {
        #region 变量

        private ISaleOrderRepository saleOrderRepository = null;
        private ICustomerAddressRepository customerAddressRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderService()
        {
            this.saleOrderRepository = new SaleOrderRepository();
            this.customerAddressRepository = new CustomerAddressRepository();
        }

        #endregion

        #region 方法

        public void Insert(SaleOrderAddDTO dto)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }

                    foreach (SaleOrderDetailAddDTO detail in dto.SaleOrderDetails)
                    {
                        SaleOrder entity = new SaleOrder();
                        entity.Create(dto.Date, detail.CustomerId, detail.Address, detail.Mobile, detail.Linkman, dto.Item, dto.Quantity, dto.Unit, detail.NeedExpress, dto.Price, 0, dto.Remark, dto.Creator);
                        this.saleOrderRepository.Insert(trans, entity);
                    }

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="item">产品项</param>
        /// <param name="unit">单位</param>
        /// <param name="price">单价</param>
        /// <param name="quantity">数量</param>
        /// <param name="actualAmount">实收金额</param>
        /// <param name="needExpress">需要快递</param>
        /// <param name="remark">备注</param>
        /// <param name="addressId">客户地址Id</param>
        /// <param name="creator">创建人</param>
        public void Insert(DateTime date, string item, string unit, decimal price, decimal quantity, decimal actualAmount, string needExpress, string remark, string addressId, string creator)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }

                    CustomerAddress customerAddress = this.customerAddressRepository.Select(trans, addressId);
                    if (customerAddress == null)
                    {
                        throw new EasySoftException(BusinessResource.SaleOrder_CustomerAddressInvalid);
                    }
                    SaleOrder entity = new SaleOrder();
                    entity.Create(date, customerAddress.CustomerId, customerAddress.Address, customerAddress.Mobile, customerAddress.Linkman, item, quantity, unit, needExpress, price, actualAmount, remark, creator);
                    this.saleOrderRepository.Insert(trans, entity);

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除销售订单记录
        /// </summary>
        /// <param name="id">Id</param>
        public void Update(string id, string status, decimal actualAmount, string mender)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }

                    SaleOrder oldEntity = this.Select(trans, id);
                    if (oldEntity.Status == SaleOrderStatus.Finished)
                    {
                        throw new EasySoftException(BusinessResource.Common_NotAllowEdit);
                    }
                    oldEntity.Update(status, actualAmount, mender);
                    this.saleOrderRepository.Update(trans, oldEntity);

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除销售订单记录
        /// </summary>
        /// <param name="id">Id</param>
        public void Delete(string id)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }

                    SaleOrder oldEntity = this.Select(trans, id);
                    if (oldEntity.Status != SaleOrderStatus.Ordered)
                    {
                        throw new EasySoftException(BusinessResource.Common_NotAllowDelete);
                    }
                    this.saleOrderRepository.Delete(trans, id);

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询销售订单表信息，用于列表分页显示
        /// </summary>
        /// <param name="item">产品项</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<SaleOrder> Search(string item, string status, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.saleOrderRepository.Search(item, status, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 查询需要快递的销售订单表信息
        /// </summary>
        /// <returns>返回销售订单数据集合</returns>
        public List<SaleOrder> SearchNeedExpress()
        {
            return this.saleOrderRepository.SearchNeedExpress();
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回销售订单记录</returns>
        public SaleOrder Select(string id)
        {
            return this.Select(null, id);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回销售订单记录</returns>
        internal SaleOrder Select(DbTransaction trans, string id)
        {
            SaleOrder entity = this.saleOrderRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Purchase_NoFoundById);
            }
            return entity;
        }

        #endregion
    }
}
