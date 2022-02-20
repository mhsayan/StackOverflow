using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;
        private readonly ICommentService _commentService;
        private IMapper _mapper;

        public QuestionService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper, IProfileService profileService,
            ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileService = profileService;
            _commentService = commentService;
        }

        public async Task CreateQuestionAsync(BO.Question question)
        {
            if (question == null)
                throw new InvalidParameterException("Received null business object.");

            var user = await _profileService.GetUserAsync();

            question.ApplicationUserId = user.Id;
            question.CreateDate = DateTime.Now;

            var questionEntity = _mapper.Map<EO.Question>(question);

            _unitOfWork.Questions.Add(questionEntity);
            _unitOfWork.Save();
        }

        public void EditQuestion(BO.Question question)
        {
            if (question == null)
                throw new InvalidParameterException("Received null business object.");

            var questionEntity = _unitOfWork.Questions.GetById(question.Id);

            if (questionEntity != null)
            {
                _mapper.Map(question, questionEntity);
                _unitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Question Edit failed.");
        }

        public IList<BO.Question> GetQuestionListAsync()
        {
            var questionEntities = _unitOfWork.Questions.GetAll();
            var questionList = from q in questionEntities
                               orderby q.CreateDate descending
                               select q;

            var questions = new List<BO.Question>();

            foreach (var entity in questionList)
            {
                var question = _mapper.Map<BO.Question>(entity);
                questions.Add(question);
            }

            return questions;
        }

        public BO.Question? GetQuestion(Guid id)
        {
            var questionEntity = _unitOfWork.Questions.Get(q => q.Id == id, "Comments").FirstOrDefault();
            var question = _mapper.Map<BO.Question>(questionEntity);

            return question;
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Received empty question id.");

            var questions = GetQuestion(id);

            if (questions != null && questions.Comments.Count != 0)
            {
                _commentService.Delete(questions.Comments);
            }
            
            _unitOfWork.Questions.Remove(questions.Id);
            _unitOfWork.Save();
        }
    }
}