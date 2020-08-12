using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProcessModule
{
    public interface IGeneralDataAccess
    {
        /// <summary> Puts the passed in event into the database </summary>
		/// <param name="toInsert">The event to be added</param>
		Task<DataAccessResult> AddEvent();

        /// <summary> Persists changes to the record in EVENT being modified </summary>
		Task<DataAccessResult> UpdateEvent();
    }
}
