using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Web.Configurations;

namespace Web.Extensions;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddCustomRateLimit(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //For my confused future self, here I read the configurations
        var settings = configuration
            .GetSection("RateLimit")
            .Get<RateLimitSettings>();
        //For the pibes, it's like middleware
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            // main limiter, It's from the entire app, global.
            // The counter is shared among all users; the idea is that it does not exceed the total number of queries, regardless of who makes them.
            options.GlobalLimiter =
                PartitionedRateLimiter.Create<HttpContext, string>(
                    context =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            "global",
                            _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = settings!.GlobalLimit,
                                Window = TimeSpan.FromMinutes(1)
                            }));
            // limit por user
            options.AddPolicy("PerUser", context =>
            {
                var userId =
                //It checks if the user is identified; if not, it uses the connection's IP address as a reference,
                //and if that's not possible, it assigns "unidentified." The only problem is that unidentified users share the same bucket.
                    context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "unidentified";

                return RateLimitPartition.GetFixedWindowLimiter(
                    userId,
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = settings!.UserLimit,
                        Window = TimeSpan.FromMinutes(1)
                    });
            });
            //For heavy endpoints, such as adding songs to the relational database (yes, we know it's wrong to add them, professor, feign ignorance)
            options.AddPolicy("HeavyEndpoint", context =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        "heavy",
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = settings!.EndpointLimit,
                            Window = TimeSpan.FromMinutes(1)
                        });
                });
        });

        return services; // si se rompe devuelve un HTTP 429 Too Many Requests
    }
}