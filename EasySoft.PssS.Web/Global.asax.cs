using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EasySoft.PssS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
            
        //    Exception ex = Server.GetLastError();
        //    //Log.Error(ex); //记录日志信息  
        //    int httpStatusCode = (ex is HttpException) ? (ex as HttpException).GetHttpCode() : 500; //这里仅仅区分两种错误  
        //    HttpContext httpContext = ((MvcApplication)sender).Context;
        //    httpContext.ClearError();
        //    httpContext.Response.Clear();
        //    httpContext.Response.StatusCode = httpStatusCode;
        //    bool shouldHandleException = true;
        //    HandleErrorInfo errorModel;




        //    //如果是AJAX请求返回json
        //    if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        //    {
        //        filterContext.Result = new JsonResult()
        //        {
        //            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
        //            Data = new
        //            {
        //                error = true,
        //                message = filterContext.Exception.Message
        //            }
        //        };
        //    }












        //    var routeData = new RouteData();
        //    routeData.Values["controller"] = "Common";

        //    switch (httpStatusCode)
        //    {
        //        case 404:
        //            routeData.Values["action"] = "NotFound";
        //            errorModel = new HandleErrorInfo(new Exception(string.Format("No page Found", httpContext.Request.UrlReferrer), ex), "Common", "NotFound");
        //            break;

        //        default:
        //            routeData.Values["action"] = "Error";
        //            Exception exceptionToReplace = 
        //            errorModel = new HandleErrorInfo(exceptionToReplace, "Utility", "Error");
        //            break;
        //    }

        //    if (shouldHandleException)
        //    {
        //        var controller = new UtilityController();
        //        controller.ViewData.Model = errorModel; //通过代码路由到指定的路径  
        //        ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //    }



        //    var httpContext = ((MvcApplication)sender).Context;

        //    var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
        //    var currentController = "";
        //    var currentAction = "";
        //    if (currentRouteData != null)
        //    {
        //        if (currentRouteData.Values["controller"] != null &&
        //            !string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
        //        {
        //            currentController = currentRouteData.Values["controller"].ToString();
        //        }

        //        if (currentRouteData.Values["action"] != null &&
        //            !string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
        //        {
        //            currentAction = currentRouteData.Values["action"].ToString();
        //        }
        //    }

        //    var ex = Server.GetLastError();
        //    var controller = new ErrorController();
        //    var routeData = new RouteData();
        //    var action = "Index";
        //    if (ex is HttpException)
        //    {
        //        var httpEx = ex as HttpException;
        //        switch (httpEx.GetHttpCode())
        //        {
        //            case 404:
        //                action = "NotFound";
        //                break;
        //            default:
        //                action = "Index";
        //                break;
        //        }
        //    }

        //    httpContext.ClearError();
        //    httpContext.Response.Clear();
        //    httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
        //    httpContext.Response.TrySkipIisCustomErrors = true;
        //    routeData.Values["controller"] = "Error";
        //    routeData.Values["action"] = action;

        //    controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
        //    ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
        //}
    }

}
