using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Services
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(BO.Question question);
        IList<BO.Question> GetQuestionListAsync();
        BO.Question GetQuestion(Guid id);
        void Delete(Guid id);
        void EditQuestion(BO.Question question);
    }
}
