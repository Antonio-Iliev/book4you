using Autofac;
using LibrarySystem.ConsoleClient.Core;
using System;
using System.Reflection;

namespace LibrarySystem.ConsoleClient
{
    public class StartUp
    {
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            var engine = container.Resolve<Engine>();
            engine.Start();

        }
    }
}
