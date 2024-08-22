using Microsoft.AspNetCore.Mvc;

namespace Blog.WEB.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
