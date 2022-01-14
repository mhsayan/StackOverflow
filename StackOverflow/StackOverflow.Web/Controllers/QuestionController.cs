using Autofac;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.Question;

namespace StackOverflow.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly ILifetimeScope _scope;

        public QuestionController(ILogger<QuestionController> logger,
            ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateQuestionModel model)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                model.Resolve(_scope);
                await model.CreateQuestionAsync();

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "New Question Create failed.");

                return View(model);
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
