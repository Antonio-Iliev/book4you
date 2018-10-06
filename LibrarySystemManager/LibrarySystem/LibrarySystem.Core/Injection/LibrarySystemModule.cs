﻿using Autofac;
using LibrarySystem.ConsoleClient.Core;
using LibrarySystem.ConsoleClient.Core.Contracts;
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
        }
        public void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Namespace.Contains("Command"))
                .AsImplementedInterfaces();
        }
        public void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("LibrarySystem.Services"))
                .Where(s => s.Namespace.Contains("Services"))
                .AsImplementedInterfaces();
        }
        
    }
}
