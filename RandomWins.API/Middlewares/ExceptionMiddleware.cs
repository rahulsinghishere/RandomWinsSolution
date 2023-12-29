using Microsoft.AspNetCore.Authentication;
using System;

namespace RandomWins.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly Dictionary<Type, Action<HttpContext, Exception>> _exceptiondictionary = default!;
        public ExceptionMiddleware()
        {
            _exceptiondictionary.Add(typeof(ArgumentNullException), (context, exception) =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response
                    .WriteAsJsonAsync(exception.Message)
                    .GetAwaiter()
                    .GetResult();
            });
            _exceptiondictionary.Add(typeof(NotImplementedException), (context, exception) =>
            {
                context.Response.StatusCode = StatusCodes.Status501NotImplemented;
                context.Response
                    .WriteAsJsonAsync(exception.Message)
                    .GetAwaiter()
                    .GetResult();
            });
            _exceptiondictionary.Add(typeof(Exception), (context, exception) => 
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response
                    .WriteAsJsonAsync(exception.Message)
                    .GetAwaiter()
                    .GetResult();
            });
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                if(_exceptiondictionary.TryGetValue(ex.GetType(),out var value))
                {
                    value.Invoke(context, ex);
                }
            }
        }
    }
}

