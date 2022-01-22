using StackOverflow.Data;

namespace StackOverflow.Platform.Entities
{
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        public IList<CommentVote> CommentVotes { get; set; }
    }
}
