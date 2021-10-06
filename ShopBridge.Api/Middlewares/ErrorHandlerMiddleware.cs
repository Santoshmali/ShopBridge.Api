using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShopBridge.Core;
using ShopBridge.Core.DataModels;
using ShopBridge.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopBridge.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger.ThrowIfNull(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                ServiceResponse<string> serviceResponse = null;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ShopBridgeAppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        serviceResponse = new ServiceResponse<string>(HttpStatusCode.BadGateway, "", new List<Error>() { new Error() { Title = error.Message } });
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        serviceResponse = new ServiceResponse<string>(HttpStatusCode.NotFound, "", new List<Error>() { new Error() { Title = error.Message } });
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        serviceResponse = new ServiceResponse<string>(HttpStatusCode.InternalServerError, "", new List<Error>() { new Error() { Title = error.Message } });
                        break;
                }

                // Log message
                _logger.LogError(error, error.Message);

                var result = JsonSerializer.Serialize(serviceResponse);
                await response.WriteAsync(result);
            }
        }
    }
}
