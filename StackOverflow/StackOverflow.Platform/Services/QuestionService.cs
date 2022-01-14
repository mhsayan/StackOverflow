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
        private IMapper _mapper;

        public QuestionService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper,
            IProfileService profileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileService = profileService;
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
    }
}