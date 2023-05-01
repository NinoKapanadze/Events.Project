using Events.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Events.API
{
    public class ApiError : ProblemDetails
    {
        public const string UnhandlerErrorCode = "UnhandledError";
        private HttpContext _httpContext;
        private Exception _exception;

        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }

        public ApiError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;

            //default
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Title = exception.Message;
            LogLevel = LogLevel.Error;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }

        //private void HandleException(ItemNotFoundException exception)
        //{
        //    Code = exception.Code;
        //    Status = (int)HttpStatusCode.NotFound;
        //    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
        //    Title = exception.Message;
        //    LogLevel = LogLevel.Information;
        //    //Log.Error(exception.Message, exception);

        //}

        private void HandleException(UserAlreadyExistsException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.Conflict;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.9";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
            //  Log.Error(exception.Message, exception);
        }

        //private void HandleException(InvalidCurrencyException exception)
        //{
        //    Code = exception.Code;
        //    Status = (int)HttpStatusCode.BadRequest;
        //    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        //    Title = exception.Message;
        //    LogLevel = LogLevel.Information;
        //}

        private void HandleException(InvalidUserException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.NotFound;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Information;
        }

        private void HandleException(Exception exception)
        {
            // Log.Error(exception.Message, exception);
        }
    }
}