using HMS.WebApi;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace WebApiAssignment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
