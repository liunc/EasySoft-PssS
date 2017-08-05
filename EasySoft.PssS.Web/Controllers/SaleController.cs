//// ----------------------------------------------------------
//// 系统名称：EasySoft PssS
//// 项目名称：Web
//// 创 建 人：刘年超
//// 创建时间：2017-01-15
//// ----------------------------------------------------------
//// 修改记录：
//// 
//// 
//// ----------------------------------------------------------
//// 版权所有：易则科技工作室 
//// ----------------------------------------------------------
//namespace EasySoft.PssS.Web.Controllers
//{
//    using Core.Util;
//    using Domain.Entity;
//    using Domain.ValueObject;
//    using Domain.Service;
//    using Filters;
//    using Models.SaleOrder;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Web;
//    using System.Web.Mvc;
//    using Models.PurchaseItem;
//    using Application.DataTransfer.Sale;
//    using Core.ViewModel;
//    using Resources;
//    using Models.CustomerAddress;

//    public class SaleController : Controller
//    {
//        #region 变量

//        private SaleService saleOrderService = null;

//        #endregion

//        #region 构造函数

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public SaleController()
//        {
//            this.saleOrderService = new SaleOrderService();
//            this.customerAddressService = new CustomerAddressService();
//        }

//        #endregion

//        /// <summary>
//        /// 获取Index视图
//        /// </summary>
//        /// <param name="page">当前页索引</param>
//        /// <returns>返回视图</returns>
//        [Route("SaleOrder/Index")]
//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult Index(string item, int page = 1)
//        {
//            Validate validate = new Validate();

//            SaleOrderIndexModel model = new SaleOrderIndexModel(page);
//            model.SelectedItem = item;
//            int totalCount = 0;
//            List<SaleOrder> entities = this.saleOrderService.Search(item, model.PageIndex, model.PageSize, ref totalCount);
//            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false);
//            foreach (SaleOrder entity in entities)
//            {
//                SaleOrderPageModel pageModel = new SaleOrderPageModel(entity);
//                PurchaseItemCacheModel purchaseItem = items[entity.Item];
//                if (purchaseItem != null)
//                {
//                    pageModel.ItemName = purchaseItem.Name;
//                }
//                model.PageData.Add(pageModel);
//            }
//            model.TotalCount = totalCount;
//            return View(model);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult First(string id, string linkMan)
//        {
//            SaleOrderFirstModel model = new SaleOrderFirstModel(id, linkMan);
//            SaleOrderAddDTO dto;
//            if (string.IsNullOrWhiteSpace(model.Id))
//            {
//                model.NewId();
//                model.DateString = model.CurrentDate;
//                model.Unit = "---";
//                dto = new SaleOrderAddDTO();
//                dto.Creator = this.Session["Mobile"].ToString();
//                this.Session[model.Id] = dto;
//            }
//            else
//            {
//                if (this.Session[model.Id] == null)
//                {
//                    this.Session[model.Id] = new SaleOrderAddDTO();
//                }
//                dto = (SaleOrderAddDTO)this.Session[model.Id];
//                model.DateString = DataConvert.ConvertDateToString(dto.Date);
//                model.Item = dto.Item;
//                model.Quantity = dto.Quantity;
//                model.Unit = dto.Unit;
//                model.Remark = dto.Remark;
//            }
//            return View(model);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult PostFirst(string id, SaleOrderFirstModel saleOrder, string linkMan)
//        {
//            JsonResultModel result = new JsonResultModel();
//            try
//            {
//                Validate validate = new Validate();
//                validate.CheckStringArgument(WebResource.Field_Id, id, true);
//                validate.CheckObjectArgument<SaleOrderFirstModel>("saleOrder", saleOrder);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }

//                if (this.Session[id] == null)
//                {
//                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
//                    result.Data = "/SaleOrder/First";
//                    return Json(result);
//                }
//                saleOrder.PostValidate(ref validate);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }
//                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
//                dto.Date = saleOrder.Date;
//                dto.Item = saleOrder.Item;
//                dto.Quantity = saleOrder.Quantity;
//                dto.Unit = saleOrder.Unit;
//                dto.Price = saleOrder.Price;
//                this.Session[id] = dto;
//                result.Data = "/SaleOrder/Second?id=" + id + "&linkMan=" + linkMan;
//                result.Result = true;
//            }
//            catch (Exception ex)
//            {
//                result.BuilderErrorMessage(ex.Message);
//            }
//            return Json(result);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult Second(string id, string linkMan)
//        {
//            SaleOrderSecondModel model = new SaleOrderSecondModel(id, linkMan);
//            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
//            {
//                return RedirectToAction("First", "SaleOrder");
//            }

//            SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[model.Id];

//            List<SaleOrderDetailAddDTO> saleOrderDetails = dto.SaleOrderDetails;
//            List<CustomerAddress> entities = this.customerAddressService.SearchByLinkMan(linkMan);

//            foreach (CustomerAddress entity in entities)
//            {
//                CustomerAddressSelectModel address = new CustomerAddressSelectModel(entity);
//                if (saleOrderDetails.Count > 0)
//                {
//                    SaleOrderDetailAddDTO saleOrderDetail = saleOrderDetails.Find(x => x.Id == entity.Id);
//                    if (saleOrderDetail != null)
//                    {
//                        address.Selected = Constant.COMMON_Y;
//                        address.NeedExpress = saleOrderDetail.NeedExpress;
//                    }
//                }
//                model.CustomerAddress.Add(address);
//            }
//            return View(model);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult Selected(string id, SaleOrderDetailAddModel model)
//        {
//            JsonResultModel result = new JsonResultModel();
//            try
//            {
//                Validate validate = new Validate();
//                validate.CheckStringArgument(WebResource.Field_Id, id, true);
//                validate.CheckObjectArgument<SaleOrderDetailAddModel>("model", model);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }

//                if (this.Session[id] == null)
//                {
//                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
//                    result.Data = "/SaleOrder/First";
//                    return Json(result);
//                }
//                model.PostValidate(ref validate);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }
//                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
//                SaleOrderDetailAddDTO saleOrder = dto.SaleOrderDetails.Find(x => x.CustomerId == model.CustomerId);
//                if (saleOrder != null)
//                {
//                    dto.SaleOrderDetails.RemoveAll(x => x.Id == model.Id);
//                }
//                if (model.Selected == Constant.COMMON_Y)
//                {
//                    dto.SaleOrderDetails.Add(new SaleOrderDetailAddDTO { CustomerId = model.CustomerId, Linkman = model.Linkman, Mobile = model.Mobile, Address = model.Address, NeedExpress = model.NeedExpress, Selected = model.Selected, Id = model.Id });
//                }
//                this.Session[id] = dto;
//                result.Result = true;
//            }
//            catch (Exception ex)
//            {
//                result.BuilderErrorMessage(ex.Message);
//            }
//            return Json(result);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult PostSecond(string id, string linkMan)
//        {
//            JsonResultModel result = new JsonResultModel();
//            try
//            {
//                Validate validate = new Validate();
//                validate.CheckStringArgument(WebResource.Field_Id, id, true);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }

//                if (this.Session[id] == null)
//                {
//                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
//                    result.Data = "/SaleOrder/First";
//                    return Json(result);
//                }
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }
//                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
//                validate.CheckList<SaleOrderDetailAddDTO>(WebResource.Field_Id, dto.SaleOrderDetails);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }
                
//                result.Data = "/SaleOrder/Third?id=" + id + "&linkMan=" + linkMan;
//                result.Result = true;
//            }
//            catch (Exception ex)
//            {
//                result.BuilderErrorMessage(ex.Message);
//            }
//            return Json(result);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult Third(string id, string linkMan)
//        {
//            SaleOrderThirdModel model = new SaleOrderThirdModel(id, linkMan);
//            if (string.IsNullOrWhiteSpace(model.Id) || this.Session[model.Id] == null)
//            {
//                return RedirectToAction("First", "SaleOrder");
//            }
//            SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[model.Id];

//            List<SaleOrderDetailAddDTO> saleOrderDetails = dto.SaleOrderDetails;
//            foreach (SaleOrderDetailAddDTO entity in saleOrderDetails)
//            {
//                model.CustomerAddress.Add(new CustomerAddressSelectModel
//                {
//                    Id = entity.Id,
//                    CustomerId = entity.CustomerId,
//                    Linkman = entity.Linkman,
//                    Mobile = entity.Mobile,
//                    Address = entity.Address,
//                    NeedExpress = entity.NeedExpress,
//                    Selected = entity.Selected
//                });
//            }
//            return View(model);
//        }

//        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
//        public ActionResult PostAdd(string id)
//        {
//            JsonResultModel result = new JsonResultModel();
//            try
//            {
//                Validate validate = new Validate();
//                validate.CheckStringArgument(WebResource.Field_Id, id, true);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }

//                if (this.Session[id] == null)
//                {
//                    result.BuilderErrorMessage(WebResource.Message_DeliverySessionOut);
//                    result.Data = "/SaleOrder/First";
//                    return Json(result);
//                }
//                SaleOrderAddDTO dto = (SaleOrderAddDTO)this.Session[id];
//                validate.CheckList<SaleOrderDetailAddDTO>(WebResource.Field_Id, dto.SaleOrderDetails);
//                if (validate.IsFailed)
//                {
//                    result.BuilderErrorMessage(validate.ErrorMessages);
//                    return Json(result);
//                }
//                this.saleOrderService.Insert(dto);
//                result.Result = true;
//            }
//            catch (Exception ex)
//            {
//                result.BuilderErrorMessage(ex.Message);
//            }
//            return Json(result);
//        }
//    }
//}