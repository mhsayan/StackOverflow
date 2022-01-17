using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Comment
{
    public class CommentModel
    {
        [Required]
        public Guid QuestionId { get; set; }
        public BO.Question Question { get; set; }
        [Required]
        [StringLength(10000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        private ICommentService _commentService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public CommentModel()
        {

        }

        public CommentModel(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _commentService = _scope.Resolve<ICommentService>();
        }

        public void Delete(Guid id)
        {
            _commentService.Delete(id);
        }

        public void GetComment(Guid id)
        {
            var comment = _commentService.GetComment(id);
            _mapper.Map(comment, this);
        }

        public void AcceptAnswer(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            _commentService.AcceptAnswer(id);
        }
    }
}
