using Autofac;
using Movie_App.Application.Services;
using Movie_App.Infrastructure.Services;

namespace Movie_App.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public InfrastructureModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserMovieService>().As<IUserMovieService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}