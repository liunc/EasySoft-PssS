// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-05-13
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
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Web.Filters;
    using Models;
    using Models.Customer;
    using Models.CustomerGroup;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// 客户控制器类
    /// </summary>
    public class CustomerController : Controller
    {
        #region 变量

        private CustomerService customerService = null;
        private CustomerAddressService customerAddressService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerController()
        {
            this.customerService = new CustomerService();
            this.customerAddressService = new CustomerAddressService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取客户分组视图
        /// </summary>
        /// <param name="name">查询的客户名称</param>
        /// <param name="groupId">查询的客户分组Id</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图对象</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string name, string groupId, int page = 1)
        {
            CustomerIndexModel model = new CustomerIndexModel(page);
            model.QueryName = name;
            model.QueryGroupId = groupId;
            int totalCount = 0;
            List<Customer> entities = this.customerService.Search(name, groupId, model.PageIndex, model.PageSize, ref totalCount);
            foreach (Customer entity in entities)
            {
                entity.GroupId = this.GetCustomerGroupName(entity.GroupId);
                model.PageData.Add(new CustomerPageModel(entity));
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        /// <summary>
        /// 获取新增客户视图
        /// </summary>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add()
        {
            CustomerAddModel model = new CustomerAddModel();
            return View(model);
        }

        /// <summary>
        /// 添加客户分组
        /// </summary>
        /// <param name="model">新增客户分组数据视图模型对象</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(CustomerAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                List<string> errorMessages = new List<string>();
                if (!ValidateHelper.CheckObjectArgument<CustomerAddModel>("model", model, ref errorMessages))
                {
                    result.BuilderErrorMessage(errorMessages[0]);
                    return Json(result);
                }
                model.PostValidate(ref errorMessages);
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }
                this.customerService.Add(model.Name, model.Nickname, model.Mobile, model.Address, string.Empty, model.GroupId, this.Session["Moblie"].ToString());
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
        /// 获取GroupDetail视图
        /// </summary>
        /// <param name="id">客户Id</param>
        /// <param name="name">查询的客户名称</param>
        /// <param name="groupId">查询的客户分组Id</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Detail(string id, string name, string groupId, int page = 1)
        {
            Customer entity = this.customerService.Select(id);
            CustomerDetailModel model = new CustomerDetailModel(entity);
            model.Group = this.GetCustomerGroupName(model.Group);
            if (string.IsNullOrWhiteSpace(model.WeChatId))
            {
                model.WeChatId = WebResource.Common_None;
            }
            model.QueryName = name;
            model.QueryGroupId = groupId;
            model.PageIndex = page;
            return View(model);
        }

        /// <summary>
        /// 获取客户分组名称
        /// </summary>
        /// <param name="groupId">分组Id</param>
        /// <returns>返回客户分组名称</returns>
        private string GetCustomerGroupName(string groupId)
        {
            ValueTextModel keyValue = ParameterHelper.GetCustomerGroup().Find(i => i.Value == groupId);
            if (keyValue != null)
            {
                return keyValue.Text;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取编辑视图
        /// </summary>
        /// <param name="id">客户Id</param>
        /// <param name="name">查询的客户名称</param>
        /// <param name="groupId">查询的客户分组Id</param>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Edit(string id, string name, string groupId, int page = 1)
        {
            Customer entity = this.customerService.Select(id);
            CustomerEditModel model = new CustomerEditModel(entity);
            if (string.IsNullOrWhiteSpace(model.WeChatId))
            {
                model.WeChatId = WebResource.Common_None;
            }
            model.QueryName = name;
            model.QueryGroupId = groupId;
            model.PageIndex = page;
            return View(model);
        }

        /// <summary>
        /// 修改客户分组
        /// </summary>
        /// <param name="model">新增客户分组数据视图模型对象</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Update(CustomerEditModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerEditModel>("model", model);
                if (!validate.Result.Success)
                {
                    result.BuilderErrorMessage(validate.Result.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(validate);
                if (!validate.Result.Success)
                {
                    result.BuilderErrorMessage(validate.Result.ErrorMessages);
                    return Json(result);
                }
                this.customerService.Update(model.Id, model.Name, model.Nickname, model.Mobile, model.WeChatId, model.GroupId, this.Session["Moblie"].ToString());
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
        /// 提交删除客户
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
                if (validate.Result.Success)
                {
                    this.customerService.Delete(id);
                    result.Result = true;
                }
                else
                {
                    result.BuilderErrorMessage(validate.Result.ErrorMessages);
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