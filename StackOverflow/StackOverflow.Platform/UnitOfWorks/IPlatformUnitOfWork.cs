using StackOverflow.Data;
using StackOverflow.Platform.Repositories;

namespace StackOverflow.Platform.UnitOfWorks
{
    public interface IPlatformUnitOfWork : IUnitOfWork
    {
        IQuestionRepository Questions { get; }
        ICommentRepository Comments { get; }
        ICommentVoteRepository CommentVotes { get; }
    }
}