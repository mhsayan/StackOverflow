using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Question
{
    public class CreateQuestionModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 15)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        private ILifetimeScope _scope;
        private IQuestionService _questionService;
        private IMapper _mapper;

        public CreateQuestionModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public CreateQuestionModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public async Task CreateQuestionAsync()
        {
            var question = _mapper.Map<BO.Question>(this);

            await _questionService.CreateQuestionAsync(question);
        }
    }
}