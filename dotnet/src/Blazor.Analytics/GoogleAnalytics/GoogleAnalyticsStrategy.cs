using System;
using System.Threading.Tasks;
using Blazorx.Analytics.Constants;
using Microsoft.JSInterop;

namespace Blazorx.Analytics.GoogleAnalytics
{
    public sealed class GoogleAnalyticsStrategy : IAnalytics
    {
        private readonly IJSRuntime _jsRuntime;

        private string _trackingId = null;
        public bool _isInitialized = false;
        public bool _debug = false;

        public GoogleAnalyticsStrategy(
            IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void Configure(string trackingId, bool debug)
        {
            _trackingId = trackingId;
            _debug = debug;
        }

        public async Task Initialize(string trackingId)
        {
            if (trackingId == null)
            {
                throw new InvalidOperationException("Invalid TrackingId");
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Configure, trackingId, _debug);

            _trackingId = trackingId;
            _isInitialized = true;
        }

        public async Task TrackNavigation(string uri)
        {
            if (!_isInitialized)
            {
                await Initialize(_trackingId);
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Navigate, _trackingId, uri);
        }

        public async Task TrackEvent(
            string eventName,
            string eventCategory = null,
            string eventLabel = null,
            int? eventValue = null)
        {
            if (!_isInitialized)
            {
                await Initialize(_trackingId);
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.TrackEvent,
                eventName, eventCategory, eventLabel, eventValue);
        }

        public Task TrackEvent(string eventName, int eventValue, string eventCategory = null, string eventLabel = null)
        {
            return TrackEvent (eventName, eventCategory, eventLabel, eventValue);
        }
    }
}
