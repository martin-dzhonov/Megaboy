using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Exceptions
{
    public class InvalidHealthException : ApplicationException
    {
        public InvalidHealthException(string msg)
            : base(msg)
        {
            
        }
    }
}
