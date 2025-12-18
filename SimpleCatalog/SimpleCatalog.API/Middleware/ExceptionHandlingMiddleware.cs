using Microsoft.AspNetCore.Mvc;

namespace SimpleCatalog.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger <ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger,RequestDelegate next)
        {
            _logger = logger;
            _next = next;
            
        }


        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }


            catch (Exception execption) 
            {
                _logger.LogError(
                         execption, "Exception occurred:{Message}", execption.Message);

                var problemDetails = new ProblemDetails
                {
                     Status=StatusCodes.Status500InternalServerError,
                     Title="Server error",
                      Detail=execption.Message

                    
                };

                context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);


            }
        }
    }
}
