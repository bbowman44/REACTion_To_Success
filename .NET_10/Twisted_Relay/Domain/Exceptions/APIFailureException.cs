using System;
using System.Collections.Generic;
using System.Text;

namespace Twisted.Domain.Exceptions
{
    public class APIFailureException : Exception
    {
        //implement this is the future for gathering the information about what was sent to the request and why it failed.
        //This should gather all information like headers, params, body, etc. and return it tot he front end on what was input so debugging is not always necessary and it's transparent
        public APIFailureException() : base() 
        { 
        }
        public APIFailureException(string message) : base(message) 
        { 
        }
        public APIFailureException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }
}
