using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Models.Comment;

namespace StackOverflow.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ILifetimeScope _scope;

        public CommentController(ILogger<CommentController> logger,
            ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Create(CommentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "New Question Create failed.");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var model = _scope.Resolve<CommentModel>();
            model.Resolve(_scope);
            model.GetComment(id);
            model.Delete(id);

            return RedirectToAction("Details", "Question", new { id = model.QuestionId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(Guid id)
        {
            var model = _scope.Resolve<CommentModel>();
            model.Resolve(_scope);
            model.GetComment(id);
            model.AcceptAnswer(id);

            return RedirectToAction("Details", "Question", new { id = model.QuestionId });
        }
    }
}
