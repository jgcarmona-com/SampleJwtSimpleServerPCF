using Microsoft.AspNetCore.Mvc;

namespace Sample.API.Controllers
{
    public class HomeController : Controller
    {
        private const string _redirectToSwagger = "~/swagger";
        
        /// <summary>
        /// It redirects to its Swagger documentation site
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return new RedirectResult(_redirectToSwagger);
        }
    }
}
