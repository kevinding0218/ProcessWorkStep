using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    public class ProcessService : IProcessService
    {
        private readonly MainModule _mainModule;

        public ProcessService(ILifetimeScope scope)
        {
            _mainModule = scope.Resolve<MainModule>();
        }

        public async Task StartProcess()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Process Service StartProcess");
            TransactionCreateContext context = TransactionCreateContext.CreateNewContext();
            await _mainModule.Process(context);
        }
    }
}
