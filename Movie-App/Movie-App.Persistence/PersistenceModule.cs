using Autofac;
using Movie_App.Persistence.Repository;

namespace Movie_App.Persistence
{
    public class PersistenceModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>()
        .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}