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
		}

		public override void ViewDidMoveToWindow()
		{
            base.ViewDidMoveToWindow();

            LyricsTextView.Value = currentDelegate.controller.TrackLyrics.message.body.lyrics.lyrics_body;
            TrackName.StringValue = currentDelegate.controller.Track;
            ArtistName.StringValue = currentDelegate.controller.Artist;
            //track_share_url = currentDelegate.controller.ShareUrl;

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
	}
}
