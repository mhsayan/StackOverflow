using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Question
{
    public class QuestionDetailsModel
    {
        [Required]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ApplicationUserId { get; set; }
        public IList<BO.Comment> Comments { get; set; }
        public bool Owner { get; set; }
        public bool Moderator { get; set; }
        public bool IsAuthenticated { get; set; }
        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Comment { get; set; }
        private ILifetimeScope _scope;
        private IQuestionService _questionService;
        private ICommentService _commentService;
        private ICommentVoteService _commentVoteService;
        private IProfileService _profileService { get; set; }
        private IMapper _mapper;


        public QuestionDetailsModel()
        {
        }

        public virtual void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionService = _scope.Resolve<IQuestionService>();
            _mapper = _scope.Resolve<IMapper>();
            _commentService = _scope.Resolve<ICommentService>();
            _profileService = _scope.Resolve<IProfileService>();
            _commentVoteService = _scope.Resolve<ICommentVoteService>();
        }

        public QuestionDetailsModel(IQuestionService questionService, IMapper mapper,
            ICommentService commentService, IProfileService profileService,
            ICommentVoteService commentVoteService)
        {
            _questionService = questionService;
            _mapper = mapper;
            _commentService = commentService;
            _profileService = profileService;
            _commentVoteService = commentVoteService;
        }

        public void GetQuestionDetailsAsync(Guid questionId)
        {
            if (questionId == Guid.Empty)
                throw new InvalidParameterException("Question id is needed to get question details.");

            var question = _questionService.GetQuestion(questionId);

            foreach (var comment in question.Comments)
            {
                comment.TotalVote = _commentVoteService.VoteCount(comment.Id);
            }

            _mapper.Map(question, this);
        }

        public void AddComment()
        {
            _commentService.CreateCommentAsync(Comment, Id);
        }

        public async Task GetOwnerStatusAsync()
        {
            var user = await _profileService.GetUserAsync();

            if (user != null)
            {
                if (user.Id == ApplicationUserId)
                    Owner = true;
            }
        }

        public async Task GetModeratorStatusAsync()
        {
            var user = await _profileService.GetUserAsync();

            if (user != null)
            {
                var claim = await _profileService.GetRolesAsync(user);

                if (claim.Contains("Moderator"))
                    Moderator = true;
            }
        }

        public void Delete(Guid questionId)
        {
            if (questionId == Guid.Empty)
                throw new InvalidParameterException("Question id is needed to delete the question.");

            _questionService.Delete(questionId);
        }

        public void LoadUserAuthentication()
        {
            IsAuthenticated = _profileService.IsAuthenticated();
        }
    }
}