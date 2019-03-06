using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using NkBolg.Common.Dapper;
using IContainer = Autofac.IContainer;

namespace NkBlog.IOC
{
    public static class AutofacExt
    {
        private static IContainer _container;
        public static IContainer InitAutofac(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            services.AddDapper(options =>
            {
                options.ConnectionString = config.GetConnectionString("MySqlConnection");
                options.DatabaseType = DatabaseType.MySql;
            });

            var builder = new ContainerBuilder();

            var service = Assembly.Load("NkBlog.Services");
            builder.RegisterAssemblyTypes(service).AsImplementedInterfaces();

            var repository = Assembly.Load("NkBlog.Repository");
            builder.RegisterAssemblyTypes(repository).AsImplementedInterfaces();

            builder.Populate(services);

            _container = builder.Build();
            return _container;

        }
    }
}
