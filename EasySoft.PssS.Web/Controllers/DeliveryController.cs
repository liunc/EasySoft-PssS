// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using EasySoft.Core.Util;
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.Delivery;
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models.Purchase;
    using EasySoft.PssS.Web.Models.PurchaseItem;
    using EasySoft.PssS.Web.Models.SaleOrder;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Application.DataTransfer.Delivery;
    using Application.DataTransfer.Sale;
    using Application.DataTransfer.Cost;
    using Models.CostItem;
    using Models.Cost;

    /// <summary>
    /// 出库控制器类
    /// </summary>
    public class DeliveryController : Controller
    {
        #region 变量

        private PurchaseService purchaseService = null;
        private DeliveryService deliveryService = null;
        private SaleOrderService saleOrderService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryController()
        {
            this.purchaseService = new PurchaseService();
            this.deliveryService = new DeliveryService();
            this.saleOrderService = new SaleOrderService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取Index视图
        /// </summary>
        /// <returns></returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string start, string end, int page = 1)
        {
            DeliveryIndexModel model = new DeliveryIndexModel(start, end, page);
            int totalCount = 0;
            List<Delivery> entities = this.deliveryService.Search(model.StartDate, model.EndDate, model.PageIndex, model.PageSize, ref totalCount);
            Dictionary<string, string> items = ParameterHelper.GetExpressCompany(false);
            foreach (Delivery entity in entities)
            {
                DeliveryPageModel pageModel = new DeliveryPageModel(entity);
                string expressCompany = items[entity.ExpressCompany];
                if (!string.IsNullOrWhiteSpace(expressCompany))
                {
                    pageModel.ExpressCompanyName = expressCompany;
                }
                model.PageData.Add(pageModel);
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult First(string id)
        {
            DeliveryFirstModel model = new DeliveryFirstModel(id);
            DeliveryAddDTO dto;
            if (string.IsNullOrWhiteSpace(model.Id))
            {
                model.NewId();
                dto = new DeliveryAddDTO();
                this.Session[model.Id] = dto;
            }
            else
            {
                if (this.Session[model.Id] == null)
                {
                    this.Session[model.Id] = new DeliveryAddDTO();
                }
                dto = (DeliveryAddDTO)this.Session[model.Id];
            }

            List<DeliveryDetailAddDTO> deliveryProducts = dto.DeliveryDetails;

            List<Purchase> entities = this.purchaseService.GetDeliverable(PurchaseItemCategory.Product);
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false);
            foreach (Purchase entity in entities)
            {
                PurchaseDeliveryModel product = new PurchaseDeliveryModel(entity);
                PurchaseItemCacheModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    product.ItemName = purchaseItem.Name;
                    product.PackUnit = purchaseItem.OutUnit;
                    product.InOutRate = purchaseItem.InOutRate;
                }
                if (deliveryProducts.Count > 0)
                {
                    DeliveryDetailAddDTO deliveryProduct = deliveryProducts.Find(x => x.ItemCategory == PurchaseItemCategory.Product && x.PurchaseId == entity.Id);
                    if (deliveryProduct != null)
                    {
                        if (deliveryProduct.DeliveryQuantity > 0)
                        {
                            product.DeliveryQuantity = deliveryProduct.DeliveryQuantity.ToString("0.##");
                        }
                        if (deliveryProduct.PackQuantity > 0)
                        {
                            product.PackQuantity = deliveryProduct.PackQuantity.ToString("0.##");
                        }
                    }
                }
                model.Products.Add(product);
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostFirst(string id, List<DeliveryDetailAddModel> products)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                validate.CheckList<DeliveryDetailAddModel>(WebResource.PurchaseItemCategory_Product, products);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/Delivery/First";
                    return Json(result);
                }
                DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[id];
                Dictionary<string, SaleAddDTO> saleAddDict = new Dictionary<string, SaleAddDTO>();
                foreach (DeliveryDetailAddModel product in products)
                {
                    product.PostValidate(ref validate);
                    dto.DeliveryDetails.Add(new DeliveryDetailAddDTO
                    {
                        PurchaseId = product.PurchaseId,
                        Item = product.Item,
                        ItemCategory = product.ItemCategory,
                        ItemName = product.ItemName,
                        DeliveryQuantity = product.DeliveryQuantity,
                        PackQuantity = product.PackQuantity,
                        PackUnit = product.PackUnit
                    });
                    if (saleAddDict.ContainsKey(product.Item))
                    {
                        saleAddDict[product.Item].Quantity += product.PackQuantity;
                    }
                    else
                    {
                        saleAddDict.Add(product.Item, new SaleAddDTO { Item = product.Item, ItemName = product.ItemName, Quantity = product.PackQuantity, Unit = product.PackUnit });
                    }
                }
                List<string> summary = new List<string>();
                foreach (KeyValuePair<string, SaleAddDTO> saleAdd in saleAddDict)
                {
                    summary.Add(string.Format("{0}{1}{2}", saleAdd.Value.ItemName, saleAdd.Value.Quantity, saleAdd.Value.Unit));
                    dto.Sales.Add(saleAdd.Value);
                }
                dto.Summary = string.Join("，", summary.ToArray());
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                this.Session[id] = dto;
                result.Data = "/Delivery/Second?id=" + id;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Second(string id)
        {
            DeliverySecondModel model = new DeliverySecondModel(id);
            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
            {
                return RedirectToAction("First", "Delivery");
            }

            DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[model.Id];

            List<DeliveryDetailAddDTO> deliveryPacks = dto.DeliveryDetails;
            List<Purchase> entities = this.purchaseService.GetDeliverable(PurchaseItemCategory.Pack);
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Pack, false);
            foreach (Purchase entity in entities)
            {
                PurchaseDeliveryModel pack = new PurchaseDeliveryModel(entity);
                PurchaseItemCacheModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    pack.ItemName = purchaseItem.Name;
                }
                if (deliveryPacks.Count > 0)
                {
                    DeliveryDetailAddDTO deliveryPack = deliveryPacks.Find(x => x.ItemCategory == PurchaseItemCategory.Pack && x.PurchaseId == entity.Id);
                    if (deliveryPack != null)
                    {
                        if (deliveryPack.DeliveryQuantity > 0)
                        {
                            pack.DeliveryQuantity = deliveryPack.DeliveryQuantity.ToString("0.##");
                        }
                    }
                }
                model.Packs.Add(pack);
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostSecond(string id, List<DeliveryDetailAddModel> packs)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                validate.CheckList<DeliveryDetailAddModel>(WebResource.PurchaseItemCategory_Pack, packs);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/Delivery/First";
                    return Json(result);
                }
                DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[id];
                Dictionary<string, DeliveryDetailAddDTO> packDict = new Dictionary<string, DeliveryDetailAddDTO>();
                Dictionary<string, PurchaseItemCacheModel> purchaseItems = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Pack, false);
                foreach (DeliveryDetailAddModel pack in packs)
                {
                    pack.PostValidate(ref validate);
                    dto.DeliveryDetails.Add(new DeliveryDetailAddDTO
                    {
                        PurchaseId = pack.PurchaseId,
                        Item = pack.Item,
                        ItemCategory = pack.ItemCategory,
                        ItemName = pack.ItemName,
                        DeliveryQuantity = pack.DeliveryQuantity
                    });
                }

                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                this.Session[id] = dto;
                result.Data = "/Delivery/Third?id=" + id;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Third(string id)
        {
            DeliveryThirdModel model = new DeliveryThirdModel(id);
            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
            {
                return RedirectToAction("First", "Delivery");
            }
            DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[model.Id];
            List<SaleOrderExpressDTO> saleOrders = dto.SaleOrders;
            List<SaleOrder> entities = this.saleOrderService.SearchNeedExpress();
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false);
            foreach (SaleOrder entity in entities)
            {
                SaleOrderNeedExpressModel saleOrder = new SaleOrderNeedExpressModel(entity);
                PurchaseItemCacheModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    saleOrder.ItemName = purchaseItem.Name;
                }
                if (saleOrders.Exists(x=>x.Id == entity.Id))
                {
                    saleOrder.Selected = Constant.COMMON_Y;
                }
                    model.SaleOrders.Add(saleOrder);
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostThird(string id, List<SaleOrderExpressModel> orders)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/Delivery/First";
                    return Json(result);
                }

                if (orders != null && orders.Count > 0)
                {
                    DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[id];
                    dto.IncludeOrder = Constant.COMMON_Y;

                    Dictionary<string, SaleOrderExpressModel> orderDict = new Dictionary<string, SaleOrderExpressModel>();
                    foreach (SaleOrderExpressModel order in orders)
                    {
                        order.PostValidate(ref validate);
                        dto.SaleOrders.Add(new SaleOrderExpressDTO { Id = order.Id, Item = order.Item, ItemName = order.ItemName, Quantity = order.Quantity, Unit = order.Unit });
                        if (orderDict.ContainsKey(order.Item))
                        {
                            orderDict[order.Item].Quantity += order.Quantity;
                        }
                        else
                        {
                            orderDict.Add(order.Item, order);
                        }
                    }

                    List<string> summary = new List<string>();
                    foreach (KeyValuePair<string, SaleOrderExpressModel> saleAdd in orderDict)
                    {
                        summary.Add(string.Format("{0}{1}{2}", saleAdd.Value.ItemName, saleAdd.Value.Quantity, saleAdd.Value.Unit));
                        SaleAddDTO saleDto = dto.Sales.Find(x => x.Item == saleAdd.Value.Item);
                        if (saleDto == null)
                        {
                            throw new EasySoftException(string.Format("出库产品中没有包含{0}", saleAdd.Value.ItemName));
                        }
                        else
                        {
                            dto.Sales.RemoveAll(x => x.Item == saleAdd.Value.Item);
                            saleDto.Quantity -= saleAdd.Value.Quantity;
                            if (saleDto.Quantity < 0)
                            {
                                throw new EasySoftException(string.Format("{0}的出库量少于订单数量", saleAdd.Value.ItemName));
                            }
                            dto.Sales.Add(saleDto);
                        }
                    }
                    dto.Summary = string.Format("{0}, 其中包含快递{1}", dto.Summary, string.Join("， ", summary.ToArray()));
                    this.Session[id] = dto;
                }

                result.Data = "/Delivery/Fourth?id=" + id;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Fourth(string id)
        {
            DeliveryFourthModel model = new DeliveryFourthModel(id);
            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
            {
                return RedirectToAction("First", "Delivery");
            }
            DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[model.Id];
            model.CurrentDate = DataConvert.ConvertDateToString(dto.Date);
            model.ExpressCompany = dto.ExpressCompany;
            model.ExpressBill = dto.ExpressBill;
            model.Remark = dto.Remark;
            return View(model);
        }

        /// <summary>
        /// 提交产品采购入库
        /// </summary>
        /// <param name="model">采购入库的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostAdd(string id, DeliveryFourthModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                validate.CheckObjectArgument<DeliveryFourthModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/Delivery/First";
                    return Json(result);
                }

                model.PostValidate(ref validate);

                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                DeliveryAddDTO dto = (DeliveryAddDTO)this.Session[id];

                dto.Date = model.Date;
                dto.ExpressCompany = model.ExpressCompany;
                dto.ExpressBill = model.ExpressBill;
                dto.Remark = model.Remark;
                dto.Creator = this.Session["Mobile"].ToString();
                dto.Costs = new List<CostAddDTO>();
                foreach (CostAddModel cost in model.Costs)
                {
                    dto.Costs.Add(new CostAddDTO { Item = cost.Item, Money = cost.Money });
                }
                this.deliveryService.Add(dto);

                result.Result = true;
                result.Data = "/Delivery/Index";
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        #endregion
    }
}