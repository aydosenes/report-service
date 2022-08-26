using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Application.Middlewares
{
    public class BaseMiddleware
    {
        private readonly RequestDelegate _next;

        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
        }

    }
}
