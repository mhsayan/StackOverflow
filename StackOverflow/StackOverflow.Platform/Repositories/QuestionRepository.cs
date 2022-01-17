using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public class QuestionRepository : Repository<Question, Guid>,
        IQuestionRepository
    {
        public QuestionRepository(IPlatformDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
