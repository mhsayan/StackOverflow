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
        public int Upvote { get; set; }
        public int DownVote { get; set; }
    }
}
