using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
namespace BulkyBook.Api.MiddleWares
{
    public class AuthenticationMiddleware
    {
      
        private readonly IConfiguration Configuration;
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            Configuration = configuration;
        }
        //Invoke is the defualt executed method 
        public async Task Invoke(HttpContext httpContext)
        {
            
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                httpContext.Response.StatusCode = 401;
                await WriteResponse(httpContext.Response, "The token for accessing this endpoint is not available");
                return;
            }
            string extractedToken = authHeader["Bearer ".Length..].Trim();
            string tokenFromConfiguration = Configuration["BearerToken"] ?? "";
            // Use a secure method of comparing strings to prevent timing attacks
            if (!SecureCompare(tokenFromConfiguration, extractedToken))
            {
                httpContext.Response.StatusCode = 401;
                await WriteResponse(httpContext.Response, "The authentication token is incorrect: Unauthorized access");
                return;
            }
            await _next(httpContext);
        }
        private static bool SecureCompare(string a, string? b)
        {
            return !string.IsNullOrEmpty(b) && CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
        }
        private static async Task WriteResponse(HttpResponse response, string message)
        {
            response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new { error = message });
            await response.WriteAsync(result);
        }
    }
}