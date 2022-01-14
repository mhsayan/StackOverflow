using Autofac;
using StackOverflow.Platform.Contexts;
using StackOverflow.Platform.Repositories;
using StackOverflow.Platform.Services;
using StackOverflow.Platform.UnitOfWorks;

namespace StackOverflow.Platform
{
    public class PlatformModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public PlatformModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlatformDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<PlatformDbContext>().As<IPlatformDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionService>().As<IQuestionService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProfileService>().As<IProfileService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PlatformUnitOfWork>().As<IPlatformUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
