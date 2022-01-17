using System.ComponentModel.DataAnnotations;
using Autofac;
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
        private ICommentService _commentService;
        private IVoteService _voteService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;

        public VoteModel()
        {

        }

        public VoteModel(ICommentService commentService,
            IVoteService voteService, IProfileService profileService)
        {
            _commentService = commentService;
            _voteService = voteService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
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
                throw new InvalidParameterException("Comment id is required to up vote.");

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
                throw new InvalidParameterException("Comment id is required to down vote.");

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
