using Autofac;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models
{
    public class IndexModel
    {
        private ILifetimeScope _scope;
        private IQuestionService _questionService;

        public IndexModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
        }

        public IndexModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IList<BO.Question> GetQuestionListAsync()
        {
            return _questionService.GetQuestionListAsync();
        }
    }
}