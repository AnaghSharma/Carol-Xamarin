using AppKit;
using Carol.Helpers;
using Foundation;

namespace Carol
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        NSPopover popover;
        public ViewController controller;

        public AppDelegate()
        {
            popover = new NSPopover();
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application

            var storyboard = NSStoryboard.FromName("Main", null);
            controller = storyboard.InstantiateControllerWithIdentifier("PopupController") as ViewController;

            popover.ContentViewController = controller;
            popover.SetAppearance(NSAppearance.GetAppearance(NSAppearance.NameVibrantDark));

            StatusBarController statusBar = new StatusBarController(popover, "StatusBarIcon.png");

            NSUserDefaults.StandardUserDefaults.RegisterDefaults(new NSDictionary(27.0f, "TextSize"));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(new NSDictionary(false, "LaunchLogin"));
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(new NSDictionary(true, "BackgroundArtwork"));
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
