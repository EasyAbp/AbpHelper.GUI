using Blazorx.Analytics.GoogleAnalytics;
using Microsoft.Extensions.DependencyInjection;

namespace Blazorx.Analytics
{
    public static class GoogleAnalyticsExtensions
    {
        public static IServiceCollection AddGoogleAnalytics(this IServiceCollection services) => AddGoogleAnalytics(services, null, false);
        public static IServiceCollection AddGoogleAnalytics(this IServiceCollection services, string trackingId) => AddGoogleAnalytics(services, trackingId, false);
        public static IServiceCollection AddGoogleAnalytics(this IServiceCollection services, bool debug) => AddGoogleAnalytics(services, null, debug);

        public static IServiceCollection AddGoogleAnalytics(
            this IServiceCollection services,
            string trackingId,
            bool debug)
        {
            return services.AddScoped<IAnalytics>(p =>
            {
                var googleAnalytics = ActivatorUtilities.CreateInstance<GoogleAnalyticsStrategy>(p);

                if (trackingId != null)
                {
                    googleAnalytics.Configure(trackingId, debug);
                }

                return googleAnalytics;
            });
        }
    }
}
