using System;
using Foundation;
using AppKit;
using System.IO;

namespace Carol.Views
{
    public partial class EmptyView : NSView
    {
        AppDelegate currentDelegate;

        NSAppleScript script;
        NSDictionary errors;
        NSAppleEventDescriptor result;

        NSMenu settingsMenu;
        NSMenuItem launch, artwork;
        NSCursor cursor;

		NSWindowController windowController;
        NSWindow aboutWindow;

        #region Constructors

        // Called when created from unmanaged code
        public EmptyView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public EmptyView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

		#endregion

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            currentDelegate = NSApplication.SharedApplication.Delegate as AppDelegate;

            // Blur Overlay is the Visual Effect View with Blur and Vibrancy
            BlurOverlay.WantsLayer = true;
            BlurOverlay.Material = NSVisualEffectMaterial.Dark;
            BlurOverlay.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;

            ThumbnailView.WantsLayer = true;
            ThumbnailView.Layer.CornerRadius = 32.0f;

            #region Settings Menu
            settingsMenu = new NSMenu();

            artwork = new NSMenuItem("Background Artwork", new ObjCRuntime.Selector("artwork:"), "");
            launch = new NSMenuItem("Launch at Login", new ObjCRuntime.Selector("launch:"), "");
            NSMenuItem about = new NSMenuItem("About", new ObjCRuntime.Selector("about:"), "");
            NSMenuItem quit = new NSMenuItem("Quit Carol", new ObjCRuntime.Selector("quit:"), "q");


            settingsMenu.AddItem(artwork);
            settingsMenu.AddItem(launch);
            settingsMenu.AddItem(about);
            settingsMenu.AddItem(NSMenuItem.SeparatorItem);
            settingsMenu.AddItem(quit);
            #endregion

            SettingsButton.AddTrackingArea(new NSTrackingArea(SettingsButton.Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null));
            cursor = NSCursor.CurrentSystemCursor;

			windowController = currentDelegate.Storyboard.InstantiateControllerWithIdentifier("AboutWindow") as NSWindowController;
        }

        public override void ViewDidMoveToWindow()
        {
            base.ViewDidMoveToWindow();

            TrackName.StringValue = currentDelegate.controller.Track;
            ArtistName.StringValue = currentDelegate.controller.Artist;

            if (currentDelegate.controller.App == "iTunes")
            {
                PlayerIcon.Image = new NSImage("icon_itunes.pdf");
                script = new NSAppleScript(File.ReadAllText("Scripts/GetAlbumArtiTunes.txt"));
                result = script.ExecuteAndReturnError(out errors);
                NSImage cover = new NSImage(result.Data);
                AlbumArtView.Image = cover;
                ThumbnailView.Image = cover;
            }
            else
            {
                PlayerIcon.Image = new NSImage("icon_spotify.pdf");
                script = new NSAppleScript(File.ReadAllText("Scripts/GetAlbumArtSpotify.txt"));
                result = script.ExecuteAndReturnError(out errors);
                NSUrl artworkurl = new NSUrl(result.StringValue);
                NSImage cover = new NSImage(artworkurl);
                AlbumArtView.Image = cover;
                ThumbnailView.Image = cover;
            }
        }

        partial void SettingsButtonClick(NSObject sender)
        {
            launch.State = (NSUserDefaults.StandardUserDefaults.BoolForKey("LaunchLogin")) ? NSCellStateValue.On : NSCellStateValue.Off;
            artwork.State = (NSUserDefaults.StandardUserDefaults.BoolForKey("BackgroundArtwork")) ? NSCellStateValue.On : NSCellStateValue.Off;

            NSMenu.PopUpContextMenu(settingsMenu, NSApplication.SharedApplication.CurrentEvent, sender as NSView);
        }

        //Method to handle Launch at Login functionality
        [Export("launch:")]
        void Launch(NSObject sender)
        {
            if (!NSUserDefaults.StandardUserDefaults.BoolForKey("LaunchLogin"))
            {
                script = new NSAppleScript(File.ReadAllText("Scripts/LoginAdd.txt"));
                script.ExecuteAndReturnError(out errors);
                NSUserDefaults.StandardUserDefaults.SetBool(true, "LaunchLogin");
            }
            else
            {
                script = new NSAppleScript(File.ReadAllText("Scripts/LoginRemove.txt"));
                script.ExecuteAndReturnError(out errors);
                NSUserDefaults.StandardUserDefaults.SetBool(false, "LaunchLogin");
            }
        }

        //Method to show/hide album artwork in background
        [Export("artwork:")]
        void Artwork(NSObject sender)
        {
            if (NSUserDefaults.StandardUserDefaults.BoolForKey("BackgroundArtwork"))
            {
                AlbumArtView.Hidden = true;
                NSUserDefaults.StandardUserDefaults.SetBool(false, "BackgroundArtwork");
            }
            else
            {
                AlbumArtView.Hidden = false;
                NSUserDefaults.StandardUserDefaults.SetBool(true, "BackgroundArtwork"); ;
            }
        }

        //Delegating the About Menu Item click event to Helpers/StatusBarController.cs
        [Export("about:")]
        void About(NSObject sender)
        {
			currentDelegate.StatusBar.HidePopover(sender);

            aboutWindow = windowController.Window;
            aboutWindow.Title = "";
            aboutWindow.TitlebarAppearsTransparent = true;
            aboutWindow.MovableByWindowBackground = true;

            windowController.ShowWindow(sender as NSObject);
        }

        //Delegating the Quit Menu Item click event to Helpers/StatusBarController.cs
        [Export("quit:")]
        void Quit(NSObject sender)
        {
			currentDelegate.StatusBar.HidePopover(sender);
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

        //Method override to change cursor to pointing hand on Mouse Enter (Hover)
        public override void MouseEntered(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);

            cursor = NSCursor.PointingHandCursor;
            cursor.Push();
        }

        //Method override to change cursor to pointing hand on Mouse Exit
        public override void MouseExited(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);

            cursor.Pop();
        }
	}
}
