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
    using EasySoft.PssS.Web.Models.CustomerGroup;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// 客户分组控制器类
    /// </summary>
    public class CustomerGroupController : Controller
    {
        #region 变量

        private CustomerGroupService customerGroupService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupController()
        {
            this.customerGroupService = new CustomerGroupService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取客户分组视图
        /// </summary>
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图对象</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(int page = 1)
        {
            CustomerGroupIndexModel model = new CustomerGroupIndexModel(page);
            int totalCount = 0;
            List<CustomerGroup> entities = this.customerGroupService.Search(model.PageIndex, model.PageSize, ref totalCount);
            foreach (CustomerGroup entity in entities)
            {
                model.PageData.Add(new CustomerGroupPageModel(entity));
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        /// <summary>
        /// 获取AddCustomerGroup视图
        /// </summary>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加客户分组
        /// </summary>
        /// <param name="model">新增客户分组数据视图模型对象</param>
        /// <returns>返回视图</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(CustomerGroupAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerGroupAddModel>("model", model);
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
                this.customerGroupService.Add(model.Name, model.Remark, this.Session["Mobile"].ToString());
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
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Detail(string id, int page = 1)
        {
            CustomerGroup entity = this.customerGroupService.Select(id);
            CustomerGroupEditModel model = new CustomerGroupEditModel(entity);
            if (string.IsNullOrWhiteSpace(model.Remark))
            {
                model.Remark = WebResource.Common_None;
            }
            model.PageIndex = page;
            return View(model);
        }

        /// <summary>
        /// 获取GroupEdit视图
        /// </summary>
        /// <returns>返回视图</returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Edit(string id, int page = 1)
        {
            CustomerGroup entity = this.customerGroupService.Select(id);
            CustomerGroupEditModel model = new CustomerGroupEditModel(entity);
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
        public ActionResult Update(CustomerGroupEditModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                Validate validate = new Validate();
                validate.CheckObjectArgument<CustomerGroupEditModel>("model", model);
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
                this.customerGroupService.Update(model.Id, model.Name, model.Remark, this.Session["Mobile"].ToString());
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
        /// 提交删除客户分组
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
                    this.customerGroupService.Delete(id);
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