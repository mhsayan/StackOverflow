using StackOverflow.Data;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public interface IQuestionRepository : IRepository<Question, Guid>
    {
    }
}
