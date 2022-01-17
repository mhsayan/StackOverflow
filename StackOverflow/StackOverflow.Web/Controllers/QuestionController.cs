using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.Question;

namespace StackOverflow.Web.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            model.Resolve(_scope);
            model.GetQuestionDetailsAsync(id);
            await model.GetOwnerStatusAsync();
            await model.GetModeratorStatusAsync();
            model.LoadUserAuthentication();

            return View(model);
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

                return RedirectToAction(nameof(Index), "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "New Question Create failed.");

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult AddComment(QuestionDetailsModel model)
        {
            if (!ModelState.IsValid)
                RedirectToAction(nameof(Index), "Home");

            try
            {
                model.Resolve(_scope);
                model.AddComment();

                return RedirectToAction(nameof(Details), "Question", new { id = model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "Failed to add a new comment.");

                return RedirectToAction(nameof(Details), "Question", new { id = model.Id });
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = _scope.Resolve<EditQuestionModel>();
            model.Resolve(_scope);
            model.LoadModelData(id);


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditQuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                model.Resolve(_scope);
                model.Edit();

                return RedirectToAction(nameof(Details), "Question", new { id = model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _logger.LogError(ex, "Failed to edit the question.");

                return RedirectToAction(nameof(Details), "Question", new { id = model.Id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<QuestionDetailsModel>();
            model.Resolve(_scope);
            model.Delete(id);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
