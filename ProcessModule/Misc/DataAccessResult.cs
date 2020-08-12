using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessModule
{
    public struct DataAccessResult
    {
        public readonly DataAccessResultType ResultType;
        public readonly int RecordCount;
        public readonly string Message;

        public DataAccessResult(DataAccessResultType type = DataAccessResultType.Success, int count = 0, string message = "")
        {
            ResultType = type;
            RecordCount = count;
            Message = message;
        }
    }
}
