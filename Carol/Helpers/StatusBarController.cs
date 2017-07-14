using AppKit;
using Foundation;

namespace Carol.Helpers
{
    public class StatusBarController : NSObject
    {
        readonly NSStatusBar statusBar;
        readonly NSStatusItem statusItem;
        readonly NSPopover popover;
        NSStatusBarButton button;
        EventMonitor eventMonitor;

        public StatusBarController()
        { 
            
        }

        public StatusBarController(NSPopover popover)
        {
            //Initialisation
            statusBar = new NSStatusBar();
            statusItem = statusBar.CreateStatusItem(NSStatusItemLength.Variable);

            button = statusItem.Button;
            //button.Image = new NSImage(image)
            //{
            //    Template = true
            //};
            button.Title = "Carol";
            button.Action = new ObjCRuntime.Selector("toggle:");
            button.Target = this;

            this.popover = popover;

			eventMonitor = new EventMonitor((NSEventMask.LeftMouseDown | NSEventMask.RightMouseDown), MouseEventHandler);
			eventMonitor.Start();
        }

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

        void HidePopover(NSObject sender)
        {
            popover.PerformClose(sender);
            eventMonitor.Stop();
        }

        void MouseEventHandler(NSEvent _event)
        {
            if (popover.Shown)
                HidePopover(_event);
        }
    }
}
