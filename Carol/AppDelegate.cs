using AppKit;
using Carol.Helpers;
using Foundation;

namespace Carol
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        NSPopover popover;

        public AppDelegate()
        {
            popover = new NSPopover();
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application

            var storyboard = NSStoryboard.FromName("Main", null);
            var controller = storyboard.InstantiateControllerWithIdentifier("PopupController") as ViewController;

            popover.ContentViewController = controller;

            StatusBarController statusBar = new StatusBarController(popover, "StatusBarIcon.png");
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
