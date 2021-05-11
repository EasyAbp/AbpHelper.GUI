// declare window globals
interface Window
{
    dataLayer: any[];
    gtag: (...args: any[]) => void;
}

// declare globals
declare const dataLayer: any[];
declare const gtag: (...args: any[]) => void;

// init globals
window.dataLayer = window.dataLayer || [];
window.gtag = window.gtag || function () { dataLayer.push(arguments); };

// configure first timestamp
gtag("js", new Date());

namespace GoogleAnalyticsInterop
{
    export function configure(trackingId: string, debug: boolean = false): void
    {
        this.debug = debug;
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;

        document.head.appendChild(script);

        gtag("config", trackingId);

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Configured!`);
        }
    }

    export function navigate(trackingId: string, href: string): void
    {
        gtag("config", trackingId, { page_location: href });

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Navigated: '${href}'`);
        }
    }

    export function trackEvent(eventName: string, eventCategory: string, eventLabel: string, eventValue: string)
    {
        gtag("event", eventName, { event_category: eventCategory, event_label: eventLabel, value: eventValue });
        if(this.debug){
            console.log(`[GTAG][Event triggered]: ${eventName}`);
        }
    }
}
