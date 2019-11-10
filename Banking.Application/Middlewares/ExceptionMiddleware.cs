using Banking.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Banking.Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var code = HttpStatusCode.InternalServerError;

            if (exception is ValidationException || exception is DataNotFoundException)
                code = HttpStatusCode.NotFound;

            context.Response.StatusCode = (int)code;
            var result = JsonConvert.SerializeObject(new
            {
                status_code = context.Response.StatusCode,
                message = exception.Message
            });
            return context.Response.WriteAsync(result);
        }
    }
}
