using StackOverflow.Data;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public interface ICommentVoteRepository : IRepository<CommentVote, Guid>
    {
    }
}
