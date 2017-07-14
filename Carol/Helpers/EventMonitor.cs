using AppKit;
using Foundation;
namespace Carol.Helpers
{
    public class EventMonitor
    {
        NSObject monitor;
        NSEventMask mask;
        GlobalEventHandler handler;

        public EventMonitor()
        {
            
        }

        public EventMonitor(NSEventMask mask, GlobalEventHandler handler)
        {
            this.mask = mask;
            this.handler = handler;
        }

        ~EventMonitor()
        {
            Stop();
        }

        public void Start()
        {
            monitor = NSEvent.AddGlobalMonitorForEventsMatchingMask(mask, handler) as NSObject;
        }

        public void Stop()
        {
            if (monitor != null)
            {
                NSEvent.RemoveMonitor(monitor);
                monitor = null;
            }
        }
    }
}
