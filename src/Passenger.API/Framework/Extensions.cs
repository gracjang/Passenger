using Microsoft.AspNetCore.Builder;

namespace Passenger.API.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseException(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}