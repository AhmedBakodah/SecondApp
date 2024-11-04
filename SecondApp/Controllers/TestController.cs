using Microsoft.AspNetCore.Mvc;

namespace SecondApp.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index(string id, int? age)
        {
             return View(model:$"Hollo world MVC Core, my name : {id} - age {age}");
        }
    }
}
