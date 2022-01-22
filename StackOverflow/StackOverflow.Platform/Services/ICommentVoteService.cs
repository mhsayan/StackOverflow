using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Services
{
    public interface ICommentVoteService
    {
        Task<BO.CommentVote?> GetUserVote(Guid commentId);
        void CreateVote(BO.CommentVote commentVote);
        void UpdateVote(BO.CommentVote commentVote);
        int VoteCount(Guid commentId);
    }
}
