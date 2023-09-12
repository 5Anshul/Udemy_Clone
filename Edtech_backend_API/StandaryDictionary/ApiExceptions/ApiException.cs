using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Edtech_backend_API.StandaryDictionary.ApiExceptions
{
    /// <summary>
    /// ApiException custom exception class is designed to handle exceptions related to API operations and includes additional information about the 
    /// HTTP status code associated with the exception
    /// </summary>
    public class ApiException:Exception
    { 
        public ApiException() : base() { }

        //constructor takes a string parameter message and passes it to the base class's constructor. 
        // It allows us to create an instance of ApiException with a custom error message but the status code is not specified in this case.
        public ApiException(string message) : base(message) { }

        // constructor takes two parameters: `HttpStatusCode StatusCode` and `string message`. It calls the base class's constructor with the provided error message. 
        //Additionally, it assigns the numeric value of the `StatusCode` enum to the `StatusCode` property of the `ApiException` class. 
        //This constructor is used to create an instance of `ApiException` with a specific HTTP status code and a custom error message.
        public ApiException(HttpStatusCode StatusCode, string message) : base(message)
        {
            this.StatusCode = (int)StatusCode;
        }

        //This is a public property named `StatusCode` of type `int` within the `ApiException` class. 
        //It allows you to get or set the HTTP status code associated with the exception instance.
        //This property provides more information about the nature of the exception and can be accessed after catching an instance of `ApiException`.
        public int StatusCode { get; set; }
    }
}
