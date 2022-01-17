using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models
{
    public class IndexModel
    {
        public string Title { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string FirstName { get; set; }
        private ILifetimeScope _scope;
        private IQuestionService _questionService;
        private IMapper _mapper;

        public IndexModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public IndexModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public IList<BO.Question> GetQuestionListAsync()
        {
            return _questionService.GetQuestionListAsync();
        }
    }
}