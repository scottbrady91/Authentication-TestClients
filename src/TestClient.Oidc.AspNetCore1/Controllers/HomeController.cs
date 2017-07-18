using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestClient.Oidc.AspNetCore1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Login()
        {
            return Redirect("Index");
        }

        public async Task<ActionResult> Logout()
        {
            await Request.HttpContext.Authentication.SignOutAsync("OpenIdConnect");
            return Redirect("Index");
        }
    }
}
