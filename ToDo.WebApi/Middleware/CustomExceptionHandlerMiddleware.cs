using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDo.Application.Common.Exceptions;

namespace ToDo.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            ProblemDetails problemDetails;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    var errors = validationException.Errors
                        .GroupBy(error => error.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );
                    problemDetails = new ValidationProblemDetails(errors)
                    {
                        Status = (int)code,
                        Title = "Validation Failed",
                        Instance = context.Request.Path
                    };
                    break;
                case NotFoundException notFoundExceptions:
                    code = HttpStatusCode.NotFound;
                    problemDetails = new ProblemDetails
                    {
                        Type = "https://httpstatuses.com/404",
                        Title = "Resource Not Found",
                        Status = (int)code,
                        Detail = notFoundExceptions.Message,
                        Instance = context.Request.Path
                    };
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    problemDetails = new ProblemDetails
                    {
                        Type = "https://httpstatuses.com/500",
                        Title = "Internal Server Error",
                        Status = (int)code,
                        Detail = "An unexpected error occurred.",
                        Instance = context.Request.Path
                    };
                    break;
            }

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/problem+json";

            return context.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType(), context.RequestAborted);
        }
    }
}
