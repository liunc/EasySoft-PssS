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
namespace EasySoft.PssS.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http.Controllers;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Filters;
    using System.Web.Mvc;

    /// <summary>
    /// 自定义认证属性类
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] AuthRoles { get; set; }

        /// <summary>
        /// 重写时，提供一个入口点用于进行自定义授权检查。
        /// </summary>
        /// <param name="httpContext">HTTP 上下文，它封装有关单个 HTTP 请求的所有 HTTP 特定的信息。</param>
        /// <returns>如果用户已经过授权，则为 true；否则为 false。</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (httpContext.Session == null || httpContext.Session["Moblie"] == null)
            {
                httpContext.Session["Moblie"] = "13510129341";
                httpContext.Session["Roles"] = "Admin";
                httpContext.Session["Name"] = "刘年超";
                // return false;
            }
            if (this.AuthRoles != null && this.AuthRoles.Length > 0)
            {
                string[] roles = httpContext.Session["Roles"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (!this.ExistsSameElement(this.AuthRoles, roles))
                {
                    return false;
                }
            }
            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// 判断两个数组中是否存在相同的元素
        /// </summary>
        /// <param name="arrayA">数组A</param>
        /// <param name="arrayB">数组B</param>
        /// <returns>存在相同元素返回true，否则false</returns>
        private bool ExistsSameElement(string[] arrayA, string[] arrayB)
        {
            for (int i = 0; i < arrayA.Length; i++)
            {
                for (int j = 0; j < arrayB.Length; j++)
                {
                    if (arrayA[i] == arrayB[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}