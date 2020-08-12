using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessModule
{
    public class ProcessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProcessService>().As<IProcessService>().InstancePerLifetimeScope();
        }
    }
}
