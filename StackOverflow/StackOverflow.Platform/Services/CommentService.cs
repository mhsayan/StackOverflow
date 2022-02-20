using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IPlatformUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void CreateCommentAsync(string commentBody, Guid questionId)
        {
            var comment = new EO.Comment
            {
                Body = commentBody,
                QuestionId = questionId
            };

            _unitOfWork.Comments.Add(comment);
            _unitOfWork.Save();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.Comments.Remove(id);
            _unitOfWork.Save();
        }

        public void Delete(IList<BO.Comment> comments)
        {
            foreach (var comment in comments)
            {
                _unitOfWork.Comments.Remove(comment.Id);
            }

            _unitOfWork.Save();
        }

        public BO.Comment GetComment(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var commentEntity = _unitOfWork.Comments.Get(q => q.Id == id, "CommentVotes").FirstOrDefault();
            var comment = _mapper.Map<BO.Comment>(commentEntity);

            return comment;
        }

        public void AcceptAnswer(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var comment = _unitOfWork.Comments.GetById(id);
            comment.IsAnswer = true;

            _unitOfWork.Save();
        }
    }
}