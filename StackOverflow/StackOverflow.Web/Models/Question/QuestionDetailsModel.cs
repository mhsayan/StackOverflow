using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Question
{
    public class QuestionDetailsModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 15)]
        public string Title { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ApplicationUserId { get; set; }
        public IList<BO.Comment> Comments { get; set; }
        private ILifetimeScope _scope;
        private IQuestionService _questionService;
        private IMapper _mapper;


        public QuestionDetailsModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        public QuestionDetailsModel(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public BO.Question GetQuestionDetailsAsync(Guid id)
        {
            var test = _questionService.GetQuestionAsync(id);

            //Comments = new List<BO.Comment>();

            var question = _mapper.Map(test, this);


            return _questionService.GetQuestionAsync(id);
        }
    }
}