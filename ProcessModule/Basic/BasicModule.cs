using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal sealed class BasicModule : BaseModule
    {
        public BasicModule(BasicOneStep basicStepOne, BasicTwoStep basicStepTwo, BasicThreeStep basicStepThree)
        {
            Console.WriteLine("---------BasicModule---------");
            Console.WriteLine("1.1 - BasicModule AddStep BasicOneStep");
            AddStep(basicStepOne);
            Console.WriteLine("1.2 - BasicModule AddStep BasicTwoStep");
            AddStep(basicStepTwo);
            Console.WriteLine("1.3 - BasicModule AddStep BasicThreeStep");
            AddStep(basicStepThree);
        }

        protected override async Task BeginModule(TransactionCreateContext context)
        {
            Console.WriteLine("-------------Basic Module Begins---------------");
            Console.WriteLine("1 - Basic Module BeginModule");
            await Task.CompletedTask;
        }

        protected override Task CompleteModule(TransactionCreateContext context)
        {
            Console.WriteLine("1 - Basic Module CompleteModule");
            Console.WriteLine("--------------Basic Module Ends--------------");
            return Task.CompletedTask;
        }
    }
}
