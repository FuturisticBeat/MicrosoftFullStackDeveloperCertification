namespace StoreApp.Middlewares
{
    public class GlobalLoggingAndExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalLoggingAndExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalLoggingAndExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Adds scoped logging for grouping related requests together and 
        /// adding generic exception handling for any unhandled exceptions.
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            using (_logger.BeginScope("RequestId: {requestId}", context.TraceIdentifier))
            { 
                try
                {
                    _logger.LogInformation("Handling request {requestId}", context.TraceIdentifier);

                    await _next(context);

                    _logger.LogInformation("Finished handling request {requestId}", context.TraceIdentifier);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unhandled exception occured in request {requestId}", context.TraceIdentifier);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync($"An unexpected error occured. Please try again later.");
                }
            }
        }
    }
}
