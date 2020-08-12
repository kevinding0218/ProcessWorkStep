using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessModule
{
    public enum DataAccessResultType
    {
		Failure = 0, //might not be needed currently?
		Success = 1,
		[Obsolete("We handle not founds in the repository layer now.", true)]
		RecordNotFound = 2,
		[Obsolete("We handle already exists / conflict in the repository layers now.", true)]
		RecordAlreadyExists = 3
	}
}
