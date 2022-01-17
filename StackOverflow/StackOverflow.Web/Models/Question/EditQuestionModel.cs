using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Question
{
    public class EditQuestionModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 15)]
        public string Title { get; set; }
        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        private ILifetimeScope _scope;
        private IQuestionService _questionService;
        private IMapper _mapper;

        public EditQuestionModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public EditQuestionModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public void LoadModelData(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Question id is missing when loading model data.");

            var question = _questionService.GetQuestion(id);

            _mapper.Map(question, this);
        }

        public void Edit()
        {
            var question = _mapper.Map<BO.Question>(this);

            _questionService.EditQuestion(question);
        }
    }
}