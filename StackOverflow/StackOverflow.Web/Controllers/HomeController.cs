using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models;
using System.Diagnostics;
using Autofac;

namespace StackOverflow.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger,
            ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            var model = _scope.Resolve<IndexModel>();
            model.Resolve(_scope);
            ViewBag.Questions = model.GetQuestionListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}