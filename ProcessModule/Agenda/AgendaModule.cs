using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class AgendaModule : BaseModule
    {
        public AgendaModule(AgendaOneStep agendaStepOne, AgendaTwoStep agendaStepTwo)
        {
            Console.WriteLine("---------AgendaModule---------");
            Console.WriteLine("2.1 - AgendaModule AddStep AgendaOneStep");
            AddStep(agendaStepOne);
            Console.WriteLine("2.2 - AgendaModule AddStep AgendaTwoStep");
            AddStep(agendaStepTwo);
        }

        protected override async Task BeginModule(TransactionCreateContext context)
        {
            Console.WriteLine("-------------Agenda Module Begins---------------");
            Console.WriteLine("2 - Agenda Module BeginModule");
            await Task.CompletedTask;
        }

        protected override Task CompleteModule(TransactionCreateContext context)
        {
            Console.WriteLine("2 - Agenda Module CompleteModule");
            Console.WriteLine("---------------Agenda Module Ends-------------");
            return Task.CompletedTask;
        }
    }
}
