using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LXTHANG_10117004
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
                name: "Shop",
                url: "xe-dap",
                defaults: new { controller = "Product", action = "Shop", id = UrlParameter.Optional },
                namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
                name: "SaleShop",
                url: "sale-shop",
                defaults: new { controller = "Product", action = "SaleShop", id = UrlParameter.Optional },
                namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "Product Detail",
               url: "chi-tiet/{alias}/{id}",
               defaults: new { controller = "Product", action = "ViewDetail", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );


            routes.MapRoute(
               name: "CategorySupplierShop",
               url: "{alias}/{name}/{idcate}/{idsupp}",
               defaults: new { controller = "Product", action = "CategorySupplierShop", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "CategoryShop",
               url: "loai/{alias}/{id}",
               defaults: new { controller = "Product", action = "CategoryShop", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "ViewCart",
               url: "xem-gio-hang",
               defaults: new { controller = "Cart", action = "ViewCart", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "AboutUs",
               url: "about-us",
               defaults: new { controller = "AboutUs", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
               name: "ViewMyUser",
               url: "thong-tin-tai-khoan",
               defaults: new { controller = "User", action = "ViewMyUser", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
              namespaces: new[] { "LXTHANG_10117004.Controllers" }
           );

            routes.MapRoute(
              name: "Register",
              url: "dang-ky",
              defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
              namespaces: new[] { "LXTHANG_10117004.Controllers" }
           );

            routes.MapRoute(
               name: "Payment",
               url: "thanh-toan",
               defaults: new { controller = "Payment", action = "Payment", id = UrlParameter.Optional },
               namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "LXTHANG_10117004.Controllers" }
            );
        }
    }
}
