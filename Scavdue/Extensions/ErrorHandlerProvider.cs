﻿using Scavdue.Middleware;

namespace Scavdue.Extensions
{
    public static class ErrorHandlerProvider
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandler>();
        }
    }
}