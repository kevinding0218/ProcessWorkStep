using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal abstract class BaseModule : IProcessable
    {
        private readonly List<IProcessable> _steps = new List<IProcessable>();
        private int _totalSteps = 0;

        protected void AddStep(IProcessable step)
        {
            Console.WriteLine("BM - BaseModule AddStep - " + step.GetType().Name);
            _steps.Add(step);
            _totalSteps += step.GetStepCount();
        }

        protected virtual Task BeginModule(TransactionCreateContext context)
        {
            Console.WriteLine("--------------BaseModule Begins--------------");
            Console.WriteLine("BM - BaseModule BeginModule");
            return Task.CompletedTask;
        }

        protected virtual Task CompleteModule(TransactionCreateContext context)
        {
            Console.WriteLine("BM - BaseModule CompleteModule");
            Console.WriteLine("--------------BaseModule Ends--------------");
            return Task.CompletedTask;
        }

        public async Task Process(TransactionCreateContext context)
        {
            Console.WriteLine("BM - BaseModule Process");
            context.ModuleDepth++;
            Console.WriteLine("BM - BaseModule BeginModule");
            await BeginModule(context);
            foreach (var step in _steps)
            {
                var stepName = step.GetType().Name;
                if (stepName.EndsWith("Module"))
                {
                    Console.WriteLine("-------------Next Module---------------");
                } else
                {
                    Console.WriteLine("-------------Next Step---------------");
                }                
                Console.WriteLine(stepName);
                await step.Process(context);
            }
            Console.WriteLine("BM - BaseModule CompleteModule");
            await CompleteModule(context);
            context.ModuleDepth--;
        }

        public int GetStepCount()
        {
            return _totalSteps;
        }
    }
}
