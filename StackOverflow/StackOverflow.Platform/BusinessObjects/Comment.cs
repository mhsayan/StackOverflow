using StackOverflow.Data;

namespace StackOverflow.Platform.BusinessObjects
{
    public class Comment : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Entities.Question Question { get; set; }
        public string Body { get; set; }
        public bool IsAnswer { get; set; }
        public int TotalVote { get; set; }
        public IList<CommentVote> Votes { get; set; }
    }
}
