using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public class CommentVoteRepository : Repository<CommentVote, Guid>,
        ICommentVoteRepository
    {
        public CommentVoteRepository(IPlatformDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
