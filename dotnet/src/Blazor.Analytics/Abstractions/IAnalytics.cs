using System.Threading.Tasks;

namespace Blazorx.Analytics
{
    public interface IAnalytics
    {
        Task Initialize(string trackingId);

        Task TrackNavigation(string uri);

        Task TrackEvent(string eventName, string eventCategory = null, string eventLabel = null, int? eventValue = null);
        Task TrackEvent(string eventName, int eventValue, string eventCategory = null, string eventLabel = null);
    }
}
