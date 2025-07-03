using Movie_Api.Handler;

namespace Movie_Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorResponse = error switch
                {
                    ApiException e =>
                        (e.StatusCode, APIRespone<object>.CreateError(e.Message)),
                    KeyNotFoundException e =>
                        (StatusCodes.Status404NotFound, APIRespone<object>.CreateError("Not found")),
                    _ => (StatusCodes.Status500InternalServerError,
                         APIRespone<object>.CreateError("An internal server error occurred."))
                };

                response.StatusCode = errorResponse.Item1;
                await response.WriteAsJsonAsync(errorResponse.Item2);
            }
        }
    }
}