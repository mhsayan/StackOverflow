using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public class CommentRepository : Repository<Comment, Guid>,
        ICommentRepository
    {
        public CommentRepository(IPlatformDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
