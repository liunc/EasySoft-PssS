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
    using EasySoft.Core.Util;
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models.Customer;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
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
            CustomerIndexModel model = new CustomerIndexModel(name, groupId, page);
            int totalCount = 0;
            List<Customer> entities = this.customerService.Search(name, groupId, model.PageIndex, model.PageSize, ref totalCount);
            foreach (Customer entity in entities)
            {
                CustomerPageModel pageModel = new CustomerPageModel(entity);
                pageModel.GroupName = this.GetCustomerGroupName(entity.GroupId);
                model.PageData.Add(pageModel);
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        /// <summary>
        /// 获取新增客户视图
        /// </summary>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string source)
        {
            CustomerAddModel model = new CustomerAddModel(source);
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
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerAddModel>("model", model);
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
                result.Data = this.customerService.Add(model.Name, model.Nickname, model.Mobile, model.Address, string.Empty, model.GroupId, this.Session["Mobile"].ToString());
                result.Result = true;
            }
            catch (EasySoftException ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
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
            string val = string.Empty;
            ParameterHelper.GetCustomerGroup().TryGetValue(groupId, out val);
            return val;
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
                this.customerService.Update(model.Id, model.Name, model.Nickname, model.Mobile, model.WeChatId, model.GroupId, this.Session["Mobile"].ToString());
                result.Result = true;
            }
            catch (EasySoftException ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
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
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                }
                else
                {
                    this.customerService.Delete(id);
                    result.Result = true;
                }
            }
            catch (EasySoftException ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        #endregion
    }
}