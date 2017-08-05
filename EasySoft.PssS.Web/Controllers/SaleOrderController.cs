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
    using Core.Util;
    using Domain.Entity;
    using Domain.ValueObject;
    using Domain.Service;
    using Filters;
    using Models.SaleOrder;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models.PurchaseItem;
    using Application.DataTransfer.Sale;
    using Core.ViewModel;
    using Resources;
    using Models.CustomerAddress;
    using Application.DataTransfer.Customer;

    public class SaleOrderController : Controller
    {
        #region 变量

        private SaleOrderService saleOrderService = null;
        private CustomerAddressService customerAddressService = null;
        private CustomerService customerService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderController()
        {
            this.saleOrderService = new SaleOrderService();
            this.customerAddressService = new CustomerAddressService();
            this.customerService = new CustomerService();
        }

        #endregion

        /// <summary>
        /// 获取Index视图
        /// </summary>
        /// <param name="item">查询项</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [Route("SaleOrder/Index")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string item, string status, int page = 1)
        {
            Validate validate = new Validate();

            IndexModel model = new IndexModel(page);
            model.SelectedItem = item;
            model.QueryStatus = status;
            int totalCount = 0;
            List<SaleOrder> entities = this.saleOrderService.Search(item, status, model.PageIndex, model.PageSize, ref totalCount);
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false);
            Dictionary<string, string> statuss = ParameterHelper.GetSaleOrderStatus();
            foreach (SaleOrder entity in entities)
            {
                SaleOrderPageModel pageModel = new SaleOrderPageModel(entity);
                PurchaseItemCacheModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    pageModel.ItemName = purchaseItem.Name;
                }
                pageModel.StatusName = statuss[pageModel.Status];
                model.PageData.Add(pageModel);
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        #region 
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult First(string id, string linkMan)
        {
            SaleOrderFirstModel model = new SaleOrderFirstModel(id, linkMan);
            SaleOrderAddDTO dto;
            if (string.IsNullOrWhiteSpace(model.Id))
            {
                model.NewId();
                model.DateString = model.CurrentDate;
                model.Unit = "---";
                dto = new SaleOrderAddDTO();
                dto.Creator = this.Session["Mobile"].ToString();
                this.Session[model.Id] = dto;
            }
            else
            {
                if (this.Session[model.Id] == null)
                {
                    this.Session[model.Id] = new SaleOrderAddDTO();
                }
                dto = (SaleOrderAddDTO)this.Session[model.Id];
                model.DateString = DataConvert.ConvertDateToString(dto.Date);
                model.Item = dto.Item;
                model.Quantity = dto.Quantity;
                model.Unit = dto.Unit;
                model.Remark = dto.Remark;
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostFirst(string id, SaleOrderFirstModel saleOrder, string linkMan)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                validate.CheckObjectArgument<SaleOrderFirstModel>("saleOrder", saleOrder);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/SaleOrder/First";
                    return Json(result);
                }
                saleOrder.PostValidate(ref validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
                dto.Date = saleOrder.Date;
                dto.Item = saleOrder.Item;
                dto.Quantity = saleOrder.Quantity;
                dto.Unit = saleOrder.Unit;
                dto.Price = saleOrder.Price;
                this.Session[id] = dto;
                result.Data = "/SaleOrder/Second?id=" + id + "&linkMan=" + linkMan;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Second(string id, string linkMan)
        {
            SaleOrderSecondModel model = new SaleOrderSecondModel(id, linkMan);
            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
            {
                return RedirectToAction("First", "SaleOrder");
            }

            SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[model.Id];

            List<SaleOrderDetailAddDTO> saleOrderDetails = dto.SaleOrderDetails;
            List<CustomerAddress> entities = this.customerAddressService.SearchByLinkMan(linkMan);

            foreach (CustomerAddress entity in entities)
            {
                CustomerAddressSelectModel address = new CustomerAddressSelectModel(entity);
                if (saleOrderDetails.Count > 0)
                {
                    SaleOrderDetailAddDTO saleOrderDetail = saleOrderDetails.Find(x => x.Id == entity.Id);
                    if (saleOrderDetail != null)
                    {
                        address.Selected = Constant.COMMON_Y;
                        address.NeedExpress = saleOrderDetail.NeedExpress;
                    }
                }
                model.CustomerAddress.Add(address);
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Selected(string id, SaleOrderDetailAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                validate.CheckObjectArgument<SaleOrderDetailAddModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                if (this.Session[id] == null)
                {
                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
                    result.Data = "/SaleOrder/First";
                    return Json(result);
                }
                model.PostValidate(ref validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
                SaleOrderDetailAddDTO saleOrder = dto.SaleOrderDetails.Find(x => x.CustomerId == model.CustomerId);
                if (saleOrder != null)
                {
                    dto.SaleOrderDetails.RemoveAll(x => x.Id == model.Id);
                }
                if (model.Selected == Constant.COMMON_Y)
                {
                    dto.SaleOrderDetails.Add(new SaleOrderDetailAddDTO { CustomerId = model.CustomerId, Linkman = model.Linkman, Mobile = model.Mobile, Address = model.Address, NeedExpress = model.NeedExpress, Selected = model.Selected, Id = model.Id });
                }
                this.Session[id] = dto;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostSecond(string id, string linkMan)
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
                    result.Data = "/SaleOrder/First";
                    return Json(result);
                }
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
                validate.CheckList<SaleOrderDetailAddDTO>(WebResource.Field_Id, dto.SaleOrderDetails);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                result.Data = "/SaleOrder/Third?id=" + id + "&linkMan=" + linkMan;
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Third(string id, string linkMan)
        {
            SaleOrderThirdModel model = new SaleOrderThirdModel(id, linkMan);
            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
            {
                return RedirectToAction("First", "SaleOrder");
            }
            SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[model.Id];

            List<SaleOrderDetailAddDTO> saleOrderDetails = dto.SaleOrderDetails;
            foreach (SaleOrderDetailAddDTO entity in saleOrderDetails)
            {
                model.CustomerAddress.Add(new CustomerAddressSelectModel
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Linkman = entity.Linkman,
                    Mobile = entity.Mobile,
                    Address = entity.Address,
                    NeedExpress = entity.NeedExpress,
                    Selected = entity.Selected
                });
            }
            return View(model);
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostAdd(string id)
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
                    result.Data = "/SaleOrder/First";
                    return Json(result);
                }
                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
                validate.CheckList<SaleOrderDetailAddDTO>(WebResource.Field_Id, dto.SaleOrderDetails);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                this.saleOrderService.Insert(dto);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }
        #endregion

        /// <summary>
        /// 获取SearchCustomer视图
        /// </summary>
        /// <param name="groupId">选中的客户分组Id</param>
        /// <param name="name">客户名称</param>
        /// <returns>返回视图</returns>
        [Route("SaleOrder/SearchCustomer")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult SearchCustomer(string groupId, string name)
        {
            ViewBag.QueryGroupId = groupId;
            ViewBag.QueryName = name;
            return View();
        }

        /// <summary>
        /// 获取SelectCustomerAddress视图
        /// </summary>
        /// <param name="groupId">选中的客户分组Id</param>
        /// <param name="name">客户名称</param>
        /// <returns>返回视图</returns>
        [Route("SaleOrder/SelectCustomerAddress")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult SelectCustomerAddress(string groupId, string name)
        {
            SelectCustomerAddressModel model = new SelectCustomerAddressModel(groupId, name);
            model.Customers = this.customerService.GetCustomerListForOrder(name, groupId);
            return View(model);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="addressId">客户地址Id</param>
        /// <param name="groupId">选中的客户分组Id</param>
        /// <param name="name">客户名称</param>
        /// <returns>返回视图</returns>
        [Route("SaleOrder/Add")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string addressId, string groupId, string name)
        {
            AddModel model = new AddModel(addressId, groupId, name);
            return View(model);
        }

        /// <summary>
        /// 提交添加新订单数据
        /// </summary>
        /// <param name="model">订单数据</param>
        /// <returns>返回Json数据视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult AddOrder(AddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<AddModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(ref validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                this.saleOrderService.Insert(model.Date, model.Item, model.Unit, model.Price, model.Quantity, model.ActualAmount, model.NeedExpress ? Constant.COMMON_Y : Constant.COMMON_N, model.Remark, model.AddressId, this.Session["Mobile"].ToString());
                result.Data = "/SaleOrder/Index";
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="id">客户地址Id</param>
        /// <param name="item">查询项</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Detail(string id, string item, string status, int page)
        {
            Validate validate = new Validate();
            validate.CheckStringArgument(WebResource.Field_Id, id, true);
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }
            DetailModel model = new DetailModel(this.saleOrderService.Select(id), item, status, page);
            Dictionary<string, PurchaseItemCacheModel> purchaseItemDict = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false);
            PurchaseItemCacheModel purchaseItem = purchaseItemDict[model.Item];
            if (purchaseItem != null)
            {
                model.ItemName = purchaseItem.Name;
            }
            Dictionary<string, string> statusDict = ParameterHelper.GetSaleOrderStatus();
            model.StatusName = statusDict[model.Status] ?? model.Status;
            return View(model);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="id">客户地址Id</param>
        /// <param name="item">查询项</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Edit(string id, string item, string status, int page)
        {
            Validate validate = new Validate();
            validate.CheckStringArgument(WebResource.Field_Id, id, true);
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }
            DetailModel model = new DetailModel(this.saleOrderService.Select(id), item, status, page);
            return View(model);
        }

        /// <summary>
        /// 提交修改订单数据
        /// </summary>
        /// <param name="model">订单数据</param>
        /// <returns>返回Json数据视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Save(DetailModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<DetailModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(ref validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                this.saleOrderService.Update(model.Id, model.Status, model.ActualAmount, this.Session["Mobile"].ToString());
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        /// <summary>
        /// 提交修改采购记录
        /// </summary>
        /// <param name="model">修改采购的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Delete(string id)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                }
                else
                {
                    this.saleOrderService.Delete(id);
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }
    }
}