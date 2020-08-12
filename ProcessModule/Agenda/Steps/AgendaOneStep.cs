using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class AgendaOneStep : BaseStep
    {
        protected override bool ShouldRunStep(TransactionCreateContext context)
        {
            Console.WriteLine("2.1.1 - Agenda Step One ShouldRunStep");
            return true;
        }

        protected override Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("2.1.2 - Agenda Step One Begin Step");
            return Task.CompletedTask;
        }

        protected override Task ProcessData(TransactionCreateContext context)
        {
            Console.WriteLine("2.1.3 - Agenda Step One ProcessData");
            return Task.CompletedTask;
        }

        protected override async Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("2.1.4 - Agenda Step One AdjustData");
            await Task.CompletedTask;
        }
    }
}
