// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-05-19
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
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models.CustomerAddress;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// 客户地址控制器类
    /// </summary>
    public class CustomerAddressController : Controller
    {
        #region 变量

        private CustomerAddressService customerAddressService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressController()
        {
            this.customerAddressService = new CustomerAddressService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取客户分组视图
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <param name="name">查询的客户名称</param>
        /// <param name="groupId">查询的客户分组Id</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图对象</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string customerId, string name, string groupId, int page = 1)
        {
            CustomerAddressIndexModel model = new CustomerAddressIndexModel { PageIndex = page, QueryName = name, QueryGroupId = groupId, CustomerId = customerId };

            List<CustomerAddress> entities = this.customerAddressService.SearchByCustomerId(customerId);
            foreach (CustomerAddress entity in entities)
            {
                model.Addresses.Add(new CustomerAddressPageModel(entity));
            }
            return View(model);
        }

        /// <summary>
        /// 获取AddCustomerAddress视图
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <param name="name">查询的客户名称</param>
        /// <param name="groupId">查询的客户分组Id</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string customerId, string name, string groupId, int page = 1)
        {
            CustomerAddressAddModel model = new CustomerAddressAddModel { CustomerId = customerId, QueryName = name, QueryGroupId = groupId, PageIndex = page };
            return View(model);
        }

        /// <summary>
        /// 添加客户分组
        /// </summary>
        /// <param name="model">新增客户分组数据视图模型对象</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostAdd(CustomerAddressAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerAddressAddModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                this.customerAddressService.Add(model.CustomerId, model.Linkman, model.Mobile, model.Address, model.IsDefault ? Constant.COMMON_Y : Constant.COMMON_N, this.Session["Mobile"].ToString());
                result.Result = true;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 获取一条客户地址数据
        /// </summary>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Select(string id)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckStringArgument("id", id, true);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                CustomerAddress entity = this.customerAddressService.Select(id);
                result.Result = true;
                result.Data = new CustomerAddressEditModel(entity).ToString();
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 添加客户分组
        /// </summary>
        /// <param name="model">新增客户分组数据视图模型对象</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Update(CustomerAddressEditModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerAddressEditModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                this.customerAddressService.Update(model.Id, model.Linkman, model.Mobile, model.Address, this.Session["Mobile"].ToString());
                result.Result = true;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 设置为默认地址
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult SetDefault(string id)
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
                    this.customerAddressService.SetDefault(id, this.Session["Mobile"].ToString());
                    result.Result = true;
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="id">Id</param>
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
                    this.customerAddressService.Delete(id);
                    result.Result = true;
                }
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