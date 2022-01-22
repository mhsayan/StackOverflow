using System.ComponentModel.DataAnnotations;
using Autofac;
using StackOverflow.Platform.Exceptions;
using StackOverflow.Platform.Services;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Models.Comment
{
    public class CommentVoteModel
    {
        [Required]
        public Guid QuestionId { get; set; }
        public Guid CommentId { get; set; }
        private ICommentService _commentService;
        private ICommentVoteService _commentVoteService;
        private IProfileService _profileService;
        private ILifetimeScope _scope;

        public CommentVoteModel()
        {

        }

        public CommentVoteModel(ICommentService commentService,
            ICommentVoteService commentVoteService, IProfileService profileService)
        {
            _commentService = commentService;
            _commentVoteService = commentVoteService;
            _profileService = profileService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _commentService = _scope.Resolve<ICommentService>();
            _commentVoteService = _scope.Resolve<ICommentVoteService>();
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

            var userVote = await _commentVoteService.GetUserVote(id);

            if (userVote != null)
            {
                if (userVote.DownVote)
                {
                    userVote.DownVote = false;
                    userVote.UpVote = true;
                }

                _commentVoteService.UpdateVote(userVote);
            }
            else
            {
                var user = await _profileService.GetUserAsync();
                var vote = new BO.CommentVote()
                {
                    UpVote = true,
                    ApplicationUserId = user.Id,
                    CommentId = CommentId
                };

                _commentVoteService.CreateVote(vote);
            }
        }

        public async Task DownVote(Guid id)
        {
            if (id == Guid.Empty)
                throw new InvalidParameterException("Comment id is required to down vote.");

            var userVote = await _commentVoteService.GetUserVote(id);

            if (userVote != null)
            {
                if (userVote.UpVote)
                {
                    userVote.DownVote = true;
                    userVote.UpVote = false;
                }

                _commentVoteService.UpdateVote(userVote);
            }
            else
            {
                var user = await _profileService.GetUserAsync();

                var vote = new BO.CommentVote()
                {
                    DownVote = true,
                    ApplicationUserId = user.Id,
                    CommentId = CommentId
                };

                _commentVoteService.CreateVote(vote);
            }
        }
    }
}
