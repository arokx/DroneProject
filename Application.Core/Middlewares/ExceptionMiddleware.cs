using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Application.Core.Utility;

namespace Application.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        // TODO: Add loggin stuff to this middleware
        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception in ExceptionMiddleware.InvokeAsync", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var message = _configuration["Messages:DefaultErrorMessage"];

            if (exception is NotImplementedException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                message = exception.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = exception.Message;
            }

            await ExecuteErrorResponse(context, message);
        }

        private async Task ExecuteErrorResponse(HttpContext context, string message)
        {
            await context.Response.WriteAsync(new AppResponse<object>()
            {
                Data = null,
                Meta = new()
                {
                    IsSucceeded = false,
                    Message = message,
                    HttpErrorCode = context.Response.StatusCode
                }
            }.ToString());
        }
    }
}
