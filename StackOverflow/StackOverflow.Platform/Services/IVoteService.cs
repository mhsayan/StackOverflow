using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Services
{
    public interface IVoteService
    {
        Task<BO.Vote?> GetUserVote(Guid commentId);
        void CreateVote(BO.Vote vote);
        void UpdateVote(BO.Vote vote);
        int VoteCount(Guid commentId);
    }
}
