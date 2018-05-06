using System;
using Foundation;
using AppKit;
using System.IO;
using CoreGraphics;

namespace Carol.Views
{
    public partial class ContentView : NSView
    {
        AppDelegate currentDelegate;

        NSAppleScript script;
        NSDictionary errors;
        NSAppleEventDescriptor result;

        CGRect progress;
        float containerHeight;
        nfloat width;

        NSMenu settingsMenu;
        NSMenuItem launch, artwork;
        NSCursor cursor;

        public static event EventHandler QuitButtonClicked;
        public static event EventHandler AboutMenuItemClicked;
        
        #region Constructors

        // Called when created from unmanaged code
        public ContentView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ContentView(NSCoder coder) : base(coder)
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

            LyricsTextView.BackgroundColor = NSColor.Clear;
            LyricsTextView.Font = NSFont.SystemFontOfSize(NSUserDefaults.StandardUserDefaults.FloatForKey("TextSize"), 0.2f);

            // Blur Overlay is the Visual Effect View with Blur and Vibrancy
            BlurOverlay.WantsLayer = true;
            BlurOverlay.Material = NSVisualEffectMaterial.Dark;
            BlurOverlay.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;

            ThumbnailView.WantsLayer = true;
            ThumbnailView.Layer.CornerRadius = 32.0f;

            // Progress bar shows how much of lyrics have you covered. It works with scrollview
            progress = ProgressBar.Frame;

            //Adding observer of Scroll view change in Notification Center. It helps to update the width of progress bar
            MainScroll.ContentView.PostsBoundsChangedNotifications = true;
            NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("boundsChange:"),
            NSView.BoundsChangedNotification, MainScroll.ContentView);

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

            OpenInBrowserButton.AddTrackingArea(new NSTrackingArea(OpenInBrowserButton.Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null));
            ChangeTextSizeButton.AddTrackingArea(new NSTrackingArea(ChangeTextSizeButton.Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null));
            SettingsButton.AddTrackingArea(new NSTrackingArea(SettingsButton.Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null));
            cursor = NSCursor.CurrentSystemCursor;
		}

		public override void ViewDidMoveToWindow()
		{
            base.ViewDidMoveToWindow();

            LyricsTextView.Value = currentDelegate.controller.TrackLyrics.message.body.lyrics.lyrics_body;
            TrackName.StringValue = currentDelegate.controller.Track;
            ArtistName.StringValue = currentDelegate.controller.Artist;

            if (currentDelegate.controller.TrackLyrics.message.body.lyrics.@explicit == 1)
                ExplicitTag.Hidden = false;
            else
                ExplicitTag.Hidden = true;
            
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

            containerHeight = (float)LyricsTextView.Bounds.Height;
		}

        //Observer method called to update the progress bar whenever scrolling occurs 
        [Export("boundsChange:")]
        public void BoundsDidChangeNotification(NSObject sender)
        {
            var notification = sender as NSNotification;
            var view = notification.Object as NSView;
            var position = view.Bounds.Location.Y;

            width = (position * 100) / (containerHeight - MainScroll.Bounds.Height);
            if (width > 4 && width <= 100)
                progress.Width = width;
            else if (width < 0)
                progress.Width = 4;
            else if (width > 100)
                progress.Width = 100;
            ProgressBar.Frame = progress;
        }

        partial void SettingsButtonClick(NSObject sender)
        {
            launch.State = (NSUserDefaults.StandardUserDefaults.BoolForKey("LaunchLogin")) ? NSCellStateValue.On : NSCellStateValue.Off;
            artwork.State = (NSUserDefaults.StandardUserDefaults.BoolForKey("BackgroundArtwork")) ? NSCellStateValue.On : NSCellStateValue.Off;

            NSMenu.PopUpContextMenu(settingsMenu, NSApplication.SharedApplication.CurrentEvent, sender as NSView);
        }

        partial void ChangeTextSizeButtonClick(NSObject sender)
        {
            switch (NSUserDefaults.StandardUserDefaults.FloatForKey("TextSize"))
            {
                case 21.0f:
                    LyricsTextView.Font = NSFont.SystemFontOfSize(27.0f, 0.2f);
                    NSUserDefaults.StandardUserDefaults.SetFloat(27.0f, "TextSize");
                    break;
                case 27.0f:
                    LyricsTextView.Font = NSFont.SystemFontOfSize(32.0f, 0.2f);
                    NSUserDefaults.StandardUserDefaults.SetFloat(32.0f, "TextSize");
                    break;
                case 32.0f:
                    LyricsTextView.Font = NSFont.SystemFontOfSize(21.0f, 0.2f);
                    NSUserDefaults.StandardUserDefaults.SetFloat(21.0f, "TextSize");
                    break;
            }
            containerHeight = (float)LyricsTextView.Bounds.Height;
            NSNotificationCenter.DefaultCenter.PostNotificationName(NSView.BoundsChangedNotification, MainScroll.ContentView);
        }

        partial void OpenInBrowserButtonClick(NSObject sender)
        {
            NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl(currentDelegate.controller.ShareUrl));
            currentDelegate.StatusBar.HidePopover(sender);
        }


        //Method to handle Launch at Login functionality
        [Export("launch:")]         void Launch(NSObject sender)         {
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
            }         }

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
            AboutMenuItemClicked?.Invoke(this, null);
        }

        //Delegating the Quit Menu Item click event to Helpers/StatusBarController.cs
        [Export("quit:")]         void Quit(NSObject sender)         {             QuitButtonClicked?.Invoke(this, null);         }

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
