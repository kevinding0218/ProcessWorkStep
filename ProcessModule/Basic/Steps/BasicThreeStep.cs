using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class BasicThreeStep : BaseStep
    {
        protected override bool ShouldRunStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.3.1 - Basic Step Three ShouldRunStep");
            return true;
        }

        protected override Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("1.3.2 - Basic Step Three Begin Step");
            return Task.CompletedTask;
        }

        protected override Task ProcessData(TransactionCreateContext context)
        {
            Console.WriteLine("1.3.3 - Basic Step Three ProcessData");
            return Task.CompletedTask;
        }

        protected override async Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("1.3.4 - Basic Step Three AdjustData");
            await Task.CompletedTask;
        }
    }
}
