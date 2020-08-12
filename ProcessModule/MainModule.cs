using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class MainModule : BaseModule
    {
        public MainModule(BasicModule basic, AgendaModule agenda, ProductModule product)
        {
            Console.WriteLine("---------MainModule---------");
            Console.WriteLine("MainModule AddStep basic");
            AddStep(basic);
            Console.WriteLine("MainModule AddStep agenda");
            AddStep(agenda);
            Console.WriteLine("MainModule AddStep product");
            AddStep(product);
        }

        protected override async Task BeginModule(TransactionCreateContext context)
        {
            Console.WriteLine("-------------Main Module Begins---------------");
            Console.WriteLine("Main Module BeginModule");
            await Task.CompletedTask;
        }

        protected override Task CompleteModule(TransactionCreateContext context)
        {
            Console.WriteLine("Main Module CompleteModule");
            Console.WriteLine("---------------Main Module Ends-------------");
            return Task.CompletedTask;
        }
    }
}
