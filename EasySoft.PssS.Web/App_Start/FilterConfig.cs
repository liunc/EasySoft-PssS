using EasySoft.PssS.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace EasySoft.PssS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
