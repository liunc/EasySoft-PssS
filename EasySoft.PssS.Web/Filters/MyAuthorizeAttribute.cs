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
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] AuthRoles = null;

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
            if (this.AuthRoles != null && this.AuthRoles.Length > 0)
            {
                string[] roles = httpContext.Session["Roles"].ToString().Split(new char[] {',' }, StringSplitOptions.RemoveEmptyEntries);
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
            for(int i = 0; i < arrayA.Length; i++)
            {
                for(int j = 0; j < arrayB.Length; j++)
                {
                    if(arrayA[i] == arrayB[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}