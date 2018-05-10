using AppKit;
using Carol.Helpers;
using Foundation;

namespace Carol
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
		NSStoryboard storyboard;
        NSPopover popover;
        public ViewController controller;
        NSDictionary defaultSettings;
        StatusBarController statusBar;
        
        public StatusBarController StatusBar => statusBar;
		public NSStoryboard Storyboard => storyboard;

        public AppDelegate()
        {
            popover = new NSPopover();

            //Loading the default preferences from a .plist file
            defaultSettings = new NSDictionary("DefaultPreferences.plist");
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application

            storyboard = NSStoryboard.FromName("Main", null);
            controller = storyboard.InstantiateControllerWithIdentifier("PopupController") as ViewController;

            popover.ContentViewController = controller;
            popover.SetAppearance(NSAppearance.GetAppearance(NSAppearance.NameVibrantDark));

            statusBar = new StatusBarController(popover, "StatusBarIcon.png");

            //Registering the default settings loaded from the .plist file in the constructor
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(defaultSettings);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
