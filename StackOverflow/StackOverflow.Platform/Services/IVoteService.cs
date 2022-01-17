using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

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
