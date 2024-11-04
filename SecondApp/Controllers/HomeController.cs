using Microsoft.AspNetCore.Mvc;
using SecondApp.Data;
using SecondApp.Models;
using System.Diagnostics;

namespace SecondApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var events = _context.GetEvents();
            return View(events);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult ShortCodes()
        {
            return View();
        }
        public IActionResult Single()
        {
            return View();
        }
    }
}
