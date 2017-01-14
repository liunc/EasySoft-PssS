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

namespace EasySoft.PssS.Web.Filters
{
    /*
    System.Web.Mvc.IAuthorizationFilter、
    System.Web.Mvc.IActionFilter、
    System.Web.Mvc.IResultFilter、
    System.Web.Mvc.IExceptionFilter、
    System.Web.Mvc.Filters.IAuthenticationFilter。*/


    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if(httpContext.Session == null || httpContext.Session["Moblie"] == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(this.Roles))
            {
                string[] roles = this.Roles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string role = httpContext.Session["Role"].ToString();
                if (!roles.Contains(role))
                {
                    return false;
                }
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}