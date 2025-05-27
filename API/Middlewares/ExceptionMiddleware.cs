using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware(IHostEnvironment environment, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, environment);
        }
    }

    private static Task HandleExceptionAsync
        (HttpContext httpContext, Exception exception, IHostEnvironment hostEnvironment)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        var response = hostEnvironment.IsDevelopment()
            ? new ApiErrorResponse(httpContext.Response.StatusCode, exception.Message, exception.StackTrace)
            : new ApiErrorResponse(httpContext.Response.StatusCode, "Internal Server Error", null);
        var option = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        var json = JsonSerializer.Serialize(response, option);
        return httpContext.Response.WriteAsync(json);
    }
}