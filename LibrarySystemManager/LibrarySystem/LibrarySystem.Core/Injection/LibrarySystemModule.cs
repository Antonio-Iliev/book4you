using Autofac;
using LibrarySystem.ConsoleClient.Commands.Contracts;
using LibrarySystem.ConsoleClient.Core;
using LibrarySystem.ConsoleClient.Core.Contracts;
using LibrarySystem.ConsoleClient.Core.Providers;
using LibrarySystem.Data.Context;
using System.Linq;
using System.Reflection;

namespace LibrarySystem.ConsoleClient.Injection
{
    public class LibrarySystemModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
            this.RegisterCoreComponents(builder);
            this.RegisterCommands(builder);
            this.RegisterServices(builder);
            base.Load(builder);
        }
        public void RegisterCoreComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<LibrarySystemContext>().As<LibrarySystemContext>();
            builder.RegisterType<ConsoleRenderer>().As<IRenderer>();
        }
        public void RegisterCommands(ContainerBuilder builder)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            var commandTypes = currentAssembly.DefinedTypes
                .Where(typeInfo =>
                    typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)))
                .ToList();

            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType())
                  .Named<ICommand>(
                    commandType.Name.ToLower().Replace("command", ""));
            }
        }
        public void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("LibrarySystem.Services"))
                .Where(s => s.Namespace.Contains("Services"))
                .AsImplementedInterfaces();
        }      
    }
}
