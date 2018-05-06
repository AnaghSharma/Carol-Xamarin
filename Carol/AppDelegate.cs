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
        NSDictionary defaultSettings;
        StatusBarController statusBar;

        public StatusBarController StatusBar => statusBar;

        public AppDelegate()
        {
            popover = new NSPopover();

            defaultSettings = new NSDictionary("DefaultPreferences.plist");
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application

            var storyboard = NSStoryboard.FromName("Main", null);
            controller = storyboard.InstantiateControllerWithIdentifier("PopupController") as ViewController;

            popover.ContentViewController = controller;
            popover.SetAppearance(NSAppearance.GetAppearance(NSAppearance.NameVibrantDark));

            statusBar = new StatusBarController(popover, "StatusBarIcon.png");

            NSUserDefaults.StandardUserDefaults.RegisterDefaults(defaultSettings);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
