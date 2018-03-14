// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Carol.Views
{
	[Register ("ContentView")]
	partial class ContentView
	{
		[Outlet]
		AppKit.NSImageView AlbumArtView { get; set; }

		[Outlet]
		AppKit.NSTextField ArtistName { get; set; }

		[Outlet]
		AppKit.NSVisualEffectView BlurOverlay { get; set; }

		[Outlet]
		AppKit.NSImageView ExplicitTag { get; set; }

		[Outlet]
		AppKit.NSTextView LyricsTextView { get; set; }

		[Outlet]
		AppKit.NSScrollView MainScroll { get; set; }

		[Outlet]
		AppKit.NSImageView PlayerIcon { get; set; }

		[Outlet]
		AppKit.NSBox ProgressBackground { get; set; }

		[Outlet]
		AppKit.NSBox ProgressBar { get; set; }

		[Outlet]
		AppKit.NSImageView ThumbnailView { get; set; }

		[Outlet]
		AppKit.NSTextField TrackName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}

			if (ProgressBackground != null) {
				ProgressBackground.Dispose ();
				ProgressBackground = null;
			}

			if (MainScroll != null) {
				MainScroll.Dispose ();
				MainScroll = null;
			}

			if (LyricsTextView != null) {
				LyricsTextView.Dispose ();
				LyricsTextView = null;
			}

			if (AlbumArtView != null) {
				AlbumArtView.Dispose ();
				AlbumArtView = null;
			}

			if (BlurOverlay != null) {
				BlurOverlay.Dispose ();
				BlurOverlay = null;
			}

			if (TrackName != null) {
				TrackName.Dispose ();
				TrackName = null;
			}

			if (ArtistName != null) {
				ArtistName.Dispose ();
				ArtistName = null;
			}

			if (ThumbnailView != null) {
				ThumbnailView.Dispose ();
				ThumbnailView = null;
			}

			if (PlayerIcon != null) {
				PlayerIcon.Dispose ();
				PlayerIcon = null;
			}

			if (ExplicitTag != null) {
				ExplicitTag.Dispose ();
				ExplicitTag = null;
			}
		}
	}
}
