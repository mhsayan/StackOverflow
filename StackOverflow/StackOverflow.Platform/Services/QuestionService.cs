using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public QuestionService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}