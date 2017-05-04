using System.Web;
using System.Web.Mvc;

namespace TestClient.Saml2p
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
