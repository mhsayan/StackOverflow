using Autofac;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.Comment;
using StackOverflow.Web.Models.Question;

namespace StackOverflow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IndexModel>().AsSelf();
            builder.RegisterType<CreateQuestionModel>().AsSelf();
            builder.RegisterType<QuestionDetailsModel>().AsSelf();
            builder.RegisterType<CommentModel>().AsSelf();
            builder.RegisterType<VoteModel>().AsSelf();
            builder.RegisterType<EditQuestionModel>().AsSelf();

            base.Load(builder);
        }
    }
}
