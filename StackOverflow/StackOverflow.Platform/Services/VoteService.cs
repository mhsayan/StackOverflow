using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public class VoteService : IVoteService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;
        private readonly ICommentService _commentService;
        private IMapper _mapper;

        public VoteService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper, IProfileService profileService,
            ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileService = profileService;
            _commentService = commentService;
        }

        public async Task<BO.Vote?> GetUserVote(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var user = await _profileService.GetUserAsync();

            if (user == null)
            {
                return null;
            }

            var voteEntity = _unitOfWork.Votes.Get(c => c.CommentId == commentId && c.ApplicationUserId == user.Id, "").FirstOrDefault();

            var vote = _mapper.Map<BO.Vote>(voteEntity);

            return vote;
        }

        public void CreateVote(BO.Vote vote)
        {
            if (vote == null)
                throw new InvalidOperationException("New vote creation failed.");

            var voteEntity = _mapper.Map<EO.Vote>(vote);

            _unitOfWork.Votes.Add(voteEntity);
            _unitOfWork.Save();
        }

        public void UpdateVote(BO.Vote vote)
        {
            if (vote == null)
                throw new InvalidOperationException("Vote update failed.");

            var voteEntity = _unitOfWork.Votes.GetById(vote.Id);
            _mapper.Map(vote, voteEntity);

            _unitOfWork.Save();
        }

        public int VoteCount(Guid commentId)
        {
            var upVotes = _unitOfWork.Votes.GetCount(v => v.UpVote == true && v.CommentId == commentId);
            var downVotes = _unitOfWork.Votes.GetCount(v => v.DownVote == true && v.CommentId == commentId);

            return upVotes - downVotes;
        }
    }
}