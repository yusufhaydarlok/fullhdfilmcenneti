using Autofac;
using fullhdfilmcenneti_core.Repositories;
using fullhdfilmcenneti_core.UnitOfWorks;
using fullhdfilmcenneti_repository.Repositories;
using fullhdfilmcenneti_repository.UnitOfWorks;
using System.Reflection;
using System;
using Module = Autofac.Module;
using fullhdfilmcenneti_core.Services;
using fullhdfilmcenneti_service.Services;
using fullhdfilmcenneti_repository;
using fullhdfilmcenneti_service.Mapping;

namespace fullhdfilmcenneti_api.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
