using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Repositories
{
    public class VoteRepository : Repository<Vote, Guid>,
        IVoteRepository
    {
        public VoteRepository(IPlatformDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
