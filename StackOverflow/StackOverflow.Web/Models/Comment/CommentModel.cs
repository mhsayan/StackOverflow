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
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required to delete the comment.");

            _commentService.Delete(id);
        }

        public void GetComment(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required to get comment.");

            var comment = _commentService.GetComment(id);
            _mapper.Map(comment, this);
        }

        public void AcceptAnswer(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required to accept the answer.");

            _commentService.AcceptAnswer(id);
        }
    }
}
