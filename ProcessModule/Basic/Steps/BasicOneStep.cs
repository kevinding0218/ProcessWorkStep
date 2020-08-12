using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class BasicOneStep : BaseStep
    {
        // virtual method from BaseStep
        protected override bool ShouldRunStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.1.1 - Basic Step One ShouldRunStep");
            return true;
        }

        // virtual method from BaseStep (optional)
        protected override Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.1.2 - Basic Step One Begin Step");
            return Task.CompletedTask;
        }

        // abstract method from BaseStep 
        protected override Task ProcessData(TransactionCreateContext context)
        {
            Console.WriteLine("1.1.3 - Basic Step One ProcessData");
            return Task.CompletedTask;
        }

        protected override async Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("1.1.4 - Basic Step One AdjustData");
            await Task.CompletedTask;
        }
    }
}
