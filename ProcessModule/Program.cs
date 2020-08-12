using Autofac;
using System;
using System.Reflection;

namespace ProcessModule
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(thisAssembly)
                   .Where(t => t.Name.EndsWith("Step") || t.Name.EndsWith("Module"))
                   .Except<BaseModule>()
                   .Except<BaseStep>();
            builder.RegisterType<ProcessService>().As<IProcessService>().InstancePerLifetimeScope();
            return builder.Build();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var scope = CompositionRoot().BeginLifetimeScope())
            {
                ProcessService ps = new ProcessService(scope);
                ps.StartProcess();
            }            
        }
    }
}
