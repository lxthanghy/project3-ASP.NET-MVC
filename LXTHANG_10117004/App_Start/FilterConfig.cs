using System.Web;
using System.Web.Mvc;

namespace LXTHANG_10117004
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
