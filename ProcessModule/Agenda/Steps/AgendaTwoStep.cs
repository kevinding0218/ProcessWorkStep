using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class AgendaTwoStep : BaseStep
    {
        protected override bool ShouldRunStep(TransactionCreateContext context)
        {
            Console.WriteLine("2.2.1 - Agenda Step Two ShouldRunStep");
            return true;
        }

        protected override Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("2.2.2 - Agenda Step Two Begin Step");
            return Task.CompletedTask;
        }

        protected override Task ProcessData(TransactionCreateContext context)
        {
            Console.WriteLine("2.2.3 - Agenda Step Two ProcessData");
            return Task.CompletedTask;
        }

        protected override async Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("2.2.4 - Agenda Step Two AdjustData");
            await Task.CompletedTask;
        }
    }
}
