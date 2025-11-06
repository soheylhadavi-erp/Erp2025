using Common.Application;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace Common.Infrastructure.Middlewares
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = context.User?.FindFirst(ClaimTypes.Name)?.Value;

            CurrentUser.Set(userId, userName);

            await _next(context);
        }
    }
}
