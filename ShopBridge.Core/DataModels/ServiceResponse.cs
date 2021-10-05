using System.Collections.Generic;
using System.Net;

namespace ShopBridge.Core.DataModels
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(HttpStatusCode statusCode, T data, IEnumerable<Error> errors = null)
        {
            Data = data;
            HttpStatusCode = (int)statusCode;
            Errors = errors;
        }

        public int HttpStatusCode { get; }

        public T Data { get; }

        public IEnumerable<Error> Errors { get; }

    }

    public class ServiceResponse
    {
        public ServiceResponse(HttpStatusCode statusCode)
        {

        }
    }
}
