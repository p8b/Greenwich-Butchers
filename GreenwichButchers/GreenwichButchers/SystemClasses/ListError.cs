using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenwichButchers.SystemClasses
{
    // This class is used to create a list of error object which
    // has the Status (true = passed, false = Failed)
    // and a list of "CustomeError" objects.
    // The default constructor is used to create a new and empty "CustomError" List
    public class ListError
    {
        public bool Status { get; set; }

        public List<CustomError> ErrorList { get; set; } = new List<CustomError>();
    }

    // This class is used to create a error number and error message
    public class CustomError
    {
        public int ErrNumber { get; set; }
        public string ItemErrorMsg { get; set; }
    }
}
