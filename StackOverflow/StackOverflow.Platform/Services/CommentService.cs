using System.Globalization;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public class CommentService : ICommentService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;
        private IMapper _mapper;

        public CommentService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper,
            IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileService = profileService;
        }

        public void CreateCommentAsync(string commentBody, Guid questionId)
        {
            var comment = new EO.Comment();
            comment.Body = commentBody;
            comment.QuestionId = questionId;

            _unitOfWork.Comments.Add(comment);
            _unitOfWork.Save();
        }

        public IList<BO.Question> GetQuestionListAsync()
        {
            var questionEntities = _unitOfWork.Questions.GetAll();
            var questions = new List<BO.Question>();

            foreach (var entity in questionEntities)
            {
                var question = _mapper.Map<BO.Question>(entity);
                questions.Add(question);
            }

            return questions;
        }

        public BO.Question GetQuestionAsync(Guid id)
        {
            var questionEntity = _unitOfWork.Questions.Get(q => q.Id == id, "Comments").FirstOrDefault();
            var question = _mapper.Map<BO.Question>(questionEntity);

            return question;
        }

        public void Delete(Guid id)
        {
            _unitOfWork.Comments.Remove(id);
            _unitOfWork.Save();
        }

        public BO.Comment GetComment(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var commentEntity = _unitOfWork.Comments.Get(q => q.Id == id, "").FirstOrDefault();
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