using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public const string ErrorMessage = "The Server has encountered an error.";

        public const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;

        public InternalServerErrorException() 
            : base(ErrorMessage)
        {
        }
    }
}
