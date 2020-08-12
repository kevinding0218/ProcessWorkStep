using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class BasicTwoStep : BaseStep
    {
        protected override bool ShouldRunStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.2.1 - Basic Step Two ShouldRunStep");
            return true;
        }

        protected override Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.2.2 - Basic Step Two Begin Step");
            return Task.CompletedTask;
        }

        protected override Task ProcessData(TransactionCreateContext context)
        {
            Console.WriteLine("1.2.3 - Basic Step Two ProcessData");
            return Task.CompletedTask;
        }

        protected override async Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("1.2.4 - Basic Step Two AdjustData");
            await Task.CompletedTask;
        }
    }
}
