using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Comment
{
    public class VoteModel
    {
        [Required]
        public Guid QuestionId { get; set; }
        public Guid CommentId { get; set; }
        public BO.Question Question { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        private ICommentService _commentService;
        private IVoteService _voteService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;
        private IMapper _mapper;

        public VoteModel()
        {

        }

        public VoteModel(IMapper mapper, ICommentService commentService,
            IVoteService voteService, IProfileService profileService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _voteService = voteService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _mapper = _scope.Resolve<IMapper>();
            _commentService = _scope.Resolve<ICommentService>();
            _voteService = _scope.Resolve<IVoteService>();
            _profileService = _scope.Resolve<IProfileService>();
        }

        public void LoadModelData(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var comment = _commentService.GetComment(id);
            QuestionId = comment.QuestionId;
            CommentId = comment.Id;
        }

        public async Task UpVote(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var userVote = await _voteService.GetUserVote(id);

            if (userVote != null)
            {
                if (userVote.DownVote)
                {
                    userVote.DownVote = false;
                    userVote.UpVote = true;
                }

                _voteService.UpdateVote(userVote);
            }
            else
            {
                var user = await _profileService.GetUserAsync();
                var vote = new BO.Vote()
                {
                    UpVote = true,
                    ApplicationUserId = user.Id,
                    CommentId = CommentId
                };

                _voteService.CreateVote(vote);
            }
        }

        public async Task DownVote(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required.");

            var userVote = await _voteService.GetUserVote(id);

            if (userVote != null)
            {
                if (userVote.UpVote)
                {
                    userVote.DownVote = true;
                    userVote.UpVote = false;
                }

                _voteService.UpdateVote(userVote);
            }
            else
            {
                var user = await _profileService.GetUserAsync();

                var vote = new BO.Vote()
                {
                    DownVote = true,
                    ApplicationUserId = user.Id,
                    CommentId = CommentId
                };

                _voteService.CreateVote(vote);
            }
        }
    }
}
