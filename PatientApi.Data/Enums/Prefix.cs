using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientApi.Data.Enums
{
    public enum Prefix
    {
        None, // none
        Equal, // **eq**: equal
        NotEqual, // **ne**: not equal
        GreaterThan, // **gt**: greater than
        LessThan, // **lt**: less than
        GreaterOrEqual, // **ge**: greater or equal
        LessOrEqual, // **le**: less or equal
        StartsAfter, // **sa**: starts after
        EndsBefore, // **eb**: ends before
        Approximately // **ap**: approximately
    }
}
