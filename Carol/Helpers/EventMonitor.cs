/*
 * Helper class to close the popover automatically on an external event
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

using AppKit;
using Foundation;
namespace Carol.Helpers
{
    public class EventMonitor
    {
        NSObject monitor;
        NSEventMask mask;
        GlobalEventHandler handler;

        #region Constructors
        public EventMonitor()
        {
            
        }

        public EventMonitor(NSEventMask mask, GlobalEventHandler handler)
        {
            this.mask = mask;
            this.handler = handler;
        }
        #endregion

        // Destructor
        ~EventMonitor()
        {
            Stop();
        }

        /// <summary>
        /// Start monitoring events of a given mask
        /// </summary>
        public void Start()
        {
            monitor = NSEvent.AddGlobalMonitorForEventsMatchingMask(mask, handler) as NSObject;
        }

        /// <summary>
        /// Stop monitoring events and release the resources
        /// </summary>
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
