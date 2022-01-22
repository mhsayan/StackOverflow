using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.UnitOfWorks;
using BO = StackOverflow.Platform.BusinessObjects;
using EO = StackOverflow.Platform.Entities;

namespace StackOverflow.Platform.Services
{
    public class CommentVoteService : ICommentVoteService
    {
        private readonly IPlatformUnitOfWork _unitOfWork;
        private readonly IProfileService _profileService;
        private readonly ICommentService _commentService;
        private IMapper _mapper;

        public CommentVoteService(IPlatformUnitOfWork unitOfWork,
            IMapper mapper, IProfileService profileService,
            ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _profileService = profileService;
            _commentService = commentService;
        }

        public async Task<BO.CommentVote?> GetUserVote(Guid commentId)
        {
            if (commentId == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var user = await _profileService.GetUserAsync();

            if (user == null)
            {
                return null;
            }

            var voteEntity = _unitOfWork.CommentVotes.Get(c => c.CommentId == commentId && c.ApplicationUserId == user.Id, "").FirstOrDefault();

            var vote = _mapper.Map<BO.CommentVote>(voteEntity);

            return vote;
        }

        public void CreateVote(BO.CommentVote vote)
        {
            if (vote == null)
                throw new InvalidOperationException("New vote creation failed.");

            var voteEntity = _mapper.Map<EO.CommentVote>(vote);

            _unitOfWork.CommentVotes.Add(voteEntity);
            _unitOfWork.Save();
        }

        public void UpdateVote(BO.CommentVote vote)
        {
            if (vote == null)
                throw new InvalidOperationException("Vote update failed.");

            var voteEntity = _unitOfWork.CommentVotes.GetById(vote.Id);
            _mapper.Map(vote, voteEntity);

            _unitOfWork.Save();
        }

        public int VoteCount(Guid commentId)
        {
            var upVotes = _unitOfWork.CommentVotes.GetCount(v => v.UpVote == true && v.CommentId == commentId);
            var downVotes = _unitOfWork.CommentVotes.GetCount(v => v.DownVote == true && v.CommentId == commentId);

            return upVotes - downVotes;
        }
    }
}