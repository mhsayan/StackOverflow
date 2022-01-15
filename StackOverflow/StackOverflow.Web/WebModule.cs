using Autofac;
using StackOverflow.Web.Models;
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

            base.Load(builder);
        }
    }
}
