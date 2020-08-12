using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class ProductModule : BaseModule
    {
        public ProductModule(ProductOneStep productStepOne)
        {
            Console.WriteLine("---------ProductModule---------");
            Console.WriteLine("3.1 - ProductModule AddStep ProductOneStep");
            AddStep(productStepOne);
        }

        protected override async Task BeginModule(TransactionCreateContext context)
        {
            Console.WriteLine("-------------Product Module Begins---------------");
            Console.WriteLine("3 - Product Module BeginModule");
            await Task.CompletedTask;
        }

        protected override Task CompleteModule(TransactionCreateContext context)
        {
            Console.WriteLine("3 - Product Module CompleteModule");
            Console.WriteLine("-------------Product Module Ends---------------");
            return Task.CompletedTask;
        }
    }
}
