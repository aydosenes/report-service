using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Middlewares
{
    public static class Extensions
    {
        public static IApplicationBuilder UseBaseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BaseMiddleware>();
        }

    }
}
