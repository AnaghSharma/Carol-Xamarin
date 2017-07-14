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

        public StatusBarController()
        { 
            
        }

        public StatusBarController(string image, NSPopover popover)
        {
            //Initialisation
            statusBar = new NSStatusBar();
            statusItem = statusBar.CreateStatusItem(NSStatusItemLength.Variable);
            popover = new NSPopover();

            button = statusItem.Button;
            button.Image = new NSImage(image)
            {
                Template = true
            };
            button.Action = new ObjCRuntime.Selector("toggle:");
            button.Target = this;

            this.popover = popover;
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
        }

        void HidePopover(NSObject sender)
        {
            popover.PerformClose(sender);
        }
    }
}
