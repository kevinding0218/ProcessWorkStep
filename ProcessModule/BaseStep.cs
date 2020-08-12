using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    internal abstract class BaseStep : IProcessable
    {
        protected abstract bool ShouldRunStep(TransactionCreateContext context);

        /// An extra optional async check for ShouldRunStep.  If overridden, this is combined with the result of ShouldRunStep.
        protected virtual Task<bool> ShouldRunStepAsync(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep ShouldRunStepAsync");
            return Task.FromResult(true);
        }

        protected abstract Task ProcessData(TransactionCreateContext context);

        protected virtual void CreateDefaults(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep CreateDefaults");
        }

        protected virtual Task BeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("--------------BaseStep Begins--------------");
            Console.WriteLine("BS - BaseStep BeginStep");
            return Task.CompletedTask;
        }

        protected virtual Task AdjustData(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep AdjustData");
            return Task.CompletedTask;
        }


        private async Task RealBeginStep(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep RealBeginStep");
            if (context.SimultaneousSteps > 0)
            {
                throw new Exception("Detected an attempt to run steps in parallel. Do not run steps in parallel.");
            }
            context.SimultaneousSteps++;
            context.LastAttemptedStep = GetType().Name;
            await context.PersistProgress();
            await BeginStep(context);
        }

        private async Task RealCompleteStep(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep RealCompleteStep");
            await CompleteStep(context);
            context.StepsCompleted++;
            var stepName = GetType().Name;
            context.LastSuccessfulStep = stepName;
            await context.PersistProgress();
            var stats = new Dictionary<string, object>
            {
                ["instanceId"] = context.InstanceId,
                ["percentComplete"] = context.PercentComplete,
                ["stepName"] = stepName
            };
            var stepTime = context.FinishTimingAndGetStepElapsedMilliseconds(stepName);
            if (stepTime != -1)
            {
                stats["stepElapsedTimeMs"] = stepTime;
            }
            context.SimultaneousSteps--;
            if (context.SimultaneousSteps != 0)
            {
                throw new Exception("somehow the copy process has become internally inconsistent");
            }
        }

        protected virtual Task CompleteStep(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep CompleteStep");
            Console.WriteLine("--------------BaseStep Ends--------------");
            return Task.CompletedTask;
        }

        public async Task Process(TransactionCreateContext context)
        {
            Console.WriteLine("BS - BaseStep Process");
            if (context.AttemptingResume)
            {
                // skip steps until we've reached our resume point
                if (context.ResumeAt != GetType().Name)
                {
                    context.StepsCompleted++;
                    return;
                }
                context.AttemptingResume = false;
            }

            await RealBeginStep(context);

            if (ShouldRunStep(context) && await ShouldRunStepAsync(context))
            {
                await ProcessData(context);
            }

            await RealCompleteStep(context);
        }

        public int GetStepCount()
        {
            return 1;
        }
    }
}
