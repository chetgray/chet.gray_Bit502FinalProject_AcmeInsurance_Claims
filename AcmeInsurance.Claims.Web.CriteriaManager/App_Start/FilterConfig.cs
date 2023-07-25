using System.Web.Mvc;

namespace AcmeInsurance.Claims.Web.CriteriaManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
