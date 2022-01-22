using StackOverflow.Data;
using StackOverflow.Membership.Entities;

namespace StackOverflow.Platform.Entities
{
    public class CommentVote : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
