using System.Web;
using System.Web.Mvc;

namespace RoboBank.Customer.Service.Isolated
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
