/*
 * Helper class to create and maintain a Status Bar Item
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

using AppKit;
using Foundation;
using Carol.Views;

namespace Carol.Helpers
{
    public class StatusBarController : NSObject
    {
        readonly NSStatusBar statusBar;
        readonly NSStatusItem statusItem;
        readonly NSPopover popover;
        NSStatusBarButton button;
        EventMonitor eventMonitor;
              
        #region Constructor
        public StatusBarController()
        { 
            
        }

        public StatusBarController(NSPopover popover, string image)
        {
            //Initialisation
            statusBar = new NSStatusBar();
            statusItem = statusBar.CreateStatusItem(NSStatusItemLength.Variable);

            button = statusItem.Button;
            button.Image = new NSImage(image)
            {
                Template = true
            };

            button.Action = new ObjCRuntime.Selector("toggle:");
            button.Target = this;

            this.popover = popover;

			eventMonitor = new EventMonitor((NSEventMask.LeftMouseDown | NSEventMask.RightMouseDown), MouseEventHandler);
			eventMonitor.Start();
        }

        #endregion

        [Export("toggle:")]
        void TogglePopover(NSObject sender)
        {
            if (popover.Shown)
                HidePopover(sender);
            else ShowPopover(sender);
        }

        void ShowPopover(NSObject sender)
        {
            button = statusItem.Button;
            popover.Show(button.Bounds, button, NSRectEdge.MaxYEdge);
            eventMonitor.Start();
        }

        public void HidePopover(NSObject sender)
        {
            popover.PerformClose(sender);
            eventMonitor.Stop();
        }

        /// <summary>
        /// Hides popover on external mouse click
        /// </summary>
        /// <param name="_event">Event.</param>
        void MouseEventHandler(NSEvent _event)
        {
            if (popover.Shown)
                HidePopover(_event);
        }
    }
}
