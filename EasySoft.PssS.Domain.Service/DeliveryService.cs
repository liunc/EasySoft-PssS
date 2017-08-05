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
    using Application.DataTransfer.Cost;
    using Application.DataTransfer.Delivery;
    using Application.DataTransfer.Sale;
    using Core.Persistence.RepositoryImplement;
    using Core.Util;
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 交付领域服务类
    /// </summary>
    public class DeliveryService
    {
        #region 变量

        private IDeliveryRepository deliveryRepository = null;
        private IDeliveryDetailRepository deliveryDetailRepository = null;
        private IPurchaseRepository purchaseRepository = null;
        private ISaleRepository saleRepository = null;
        private ISaleOrderRepository saleOrderRepository = null;
        private ICostRepository costRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryService()
        {
            this.deliveryRepository = new DeliveryRepository();
            this.deliveryDetailRepository = new DeliveryDetailRepository();
            this.purchaseRepository = new PurchaseRepository();
            this.saleRepository = new SaleRepository();
            this.saleOrderRepository = new SaleOrderRepository();
            this.costRepository = new CostRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增交付记录
        /// </summary>
        /// <param name="dto">新增交付数据对象</param>
        public void Add(DeliveryAddDTO dto)
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

                    #region Delivery and Cost

                    Delivery entity = new Delivery();
                    entity.Create(dto.Date, dto.ExpressCompany, dto.ExpressBill, dto.IncludeOrder, dto.Summary, dto.Remark, dto.Costs, dto.Creator);
                    this.deliveryRepository.Insert(trans, entity);
                    foreach(Cost cost in entity.Costs)
                    {
                        this.costRepository.Insert(trans, cost);
                    }

                    #endregion

                    Dictionary<string,Sale> sales = new Dictionary<string, Sale>();
                    Dictionary<string, string> items = new Dictionary<string, string>();
                    foreach (DeliveryDetailAddDTO detail in dto.DeliveryDetails)
                    {
                        #region Purchase
                        Purchase purchase = this.purchaseRepository.Select(trans, detail.PurchaseId);
                        if (purchase == null || purchase.Category != detail.ItemCategory || purchase.Item != detail.Item)
                        {
                            throw new EasySoftException(string.Format("关联的采购单不存在或项目不正确。采购单ID：{0}, 采购项：{1}", purchase.Id, detail.ItemName));
                        }
                        
                        if (purchase.Inventory < detail.DeliveryQuantity)
                        {
                            throw new EasySoftException(string.Format("库存不足。采购单ID：{0}, 采购项：{1}", purchase.Id, detail.ItemName));
                        }

                        purchase.Delivery(detail.DeliveryQuantity, dto.Creator);
                        this.purchaseRepository.Update(trans, purchase);
                        #endregion

                        #region DeliveryDetail
                        DeliveryDetail detailEntity = new DeliveryDetail();
                        detailEntity.Create(entity.Id, purchase.Id, detail.ItemCategory, detail.DeliveryQuantity, detail.PackQuantity, detail.PackUnit, dto.Creator);
                        this.deliveryDetailRepository.Insert(trans, detailEntity);
                        #endregion
                    }

                    #region SaleOrder
                    if (dto.IncludeOrder == Constant.COMMON_Y)
                    {
                        foreach (SaleOrderExpressDTO order in dto.SaleOrders)
                        {
                            SaleOrder orderEntity = this.saleOrderRepository.Select(trans, order.Id);
                            if (orderEntity == null || orderEntity.NeedExpress == Constant.COMMON_N || orderEntity.Item != order.Item || orderEntity.Quantity != order.Quantity || orderEntity.Status != SaleOrderStatus.Ordered)
                            {
                                throw new EasySoftException(string.Format("关联的订单不存在或状态不正确，订单信息Id：{0}, 订单项：{1}", order.Id, order.ItemName));
                            }
                            orderEntity.SentExpress(orderEntity.Id, dto.Creator);
                            this.saleOrderRepository.Update(trans, orderEntity);
                        }
                    }
                    #endregion

                    #region Sale
                    foreach (SaleAddDTO sale in dto.Sales)
                    {
                        Sale saleEntity = new Sale();
                        saleEntity.Create(entity.Id, sale.Item, sale.Quantity, sale.Unit, dto.Creator);
                        this.saleRepository.Insert(trans, saleEntity);
                    }
                    #endregion

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
        /// 删除交付记录
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

                    Delivery oldEntity = this.Select(trans, id);

                    this.costRepository.DeleteByRecordId(trans, id);
                    this.deliveryRepository.Delete(trans, id);

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
        /// 查询交付表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="item">产品项</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<Delivery> Search(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.deliveryRepository.Search(startDate, endDate, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回交付记录</returns>
        public Delivery Select(string id)
        {
            return this.Select(null, id);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回交付记录</returns>
        internal Delivery Select(DbTransaction trans, string id)
        {
            Delivery entity = this.deliveryRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Delivery_NoFoundById);
            }
            return entity;
        }

        /// <summary>
        /// 更新交付信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        internal void Update(DbTransaction trans, Delivery entity)
        {
            this.deliveryRepository.Update(trans, entity);
        }

        #endregion
    }
}
