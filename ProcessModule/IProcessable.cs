using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    /// <summary> Components of the copy process responsible for the business logic and procedures needed to perform the copy </summary>
    internal interface IProcessable
    {
        /// <summary> Performs data manipulations based on its own type and the provided context </summary>
        ///  <param name="context">The context needed for the processable to do its thing.</param>
        Task Process(TransactionCreateContext context);

        /// <summary> Returns the number of steps in the processable. </summary>
        int GetStepCount();
    }
}
