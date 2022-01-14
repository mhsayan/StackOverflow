using Autofac;
using StackOverflow.Web.Models.Question;

namespace StackOverflow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateQuestionModel>().AsSelf();

            base.Load(builder);
        }
    }
}
