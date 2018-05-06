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
        NSWindow aboutWindow;
        NSStoryboard storyboard;
        NSWindowController windowController;

        #region Constructors
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

            ContentView.AboutMenuItemClicked += HandleAboutMenuItemClicked;
            ContentView.QuitButtonClicked += HandleQuitButtonClicked;

            EmptyView.AboutMenuItemClicked += HandleAboutMenuItemClicked;
            EmptyView.QuitButtonClicked += HandleQuitButtonClicked;

            storyboard = NSStoryboard.FromName("Main", null);
            windowController = storyboard.InstantiateControllerWithIdentifier("AboutWindow") as NSWindowController;
        }
        #endregion

        //Destructor
        ~StatusBarController()
        {
            ContentView.AboutMenuItemClicked -= HandleAboutMenuItemClicked;
            ContentView.QuitButtonClicked -= HandleQuitButtonClicked;

            EmptyView.AboutMenuItemClicked -= HandleAboutMenuItemClicked;
            EmptyView.QuitButtonClicked -= HandleQuitButtonClicked;
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

        /// <summary>
        /// Handles the about menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void HandleAboutMenuItemClicked(object sender, System.EventArgs e)
        {
            HidePopover(sender as NSObject);

            aboutWindow = windowController.Window;
            aboutWindow.Title = "";
            aboutWindow.TitlebarAppearsTransparent = true;
            aboutWindow.MovableByWindowBackground = true;

            windowController.ShowWindow(sender as NSObject);
        }

        /// <summary>
        /// Handles the quit button click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void HandleQuitButtonClicked(object sender, System.EventArgs e)
        {
            HidePopover(sender as NSObject);
            var alert = new NSAlert()
            {
                MessageText = "Are you sure you want to Quit Carol?"
            };
            alert.AddButton("Quit");
            alert.AddButton("Cancel");
            var retValue = alert.RunModal();
            if (retValue == 1000)
                NSApplication.SharedApplication.Terminate((sender as NSObject));
        }
    }
}
