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
	[Register ("EmptyView")]
	partial class EmptyView
	{
		[Outlet]
		AppKit.NSImageView AlbumArtView { get; set; }

		[Outlet]
		AppKit.NSTextField ArtistName { get; set; }

		[Outlet]
		AppKit.NSVisualEffectView BlurOverlay { get; set; }

		[Outlet]
		AppKit.NSImageView InstrumentalTag { get; set; }

		[Outlet]
		AppKit.NSImageView PlayerIcon { get; set; }

		[Outlet]
		AppKit.NSButton SettingsButton { get; set; }

		[Outlet]
		AppKit.NSImageView ThumbnailView { get; set; }

		[Outlet]
		AppKit.NSTextField TrackName { get; set; }

		[Action ("SettingsButtonClick:")]
		partial void SettingsButtonClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
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

			if (InstrumentalTag != null) {
				InstrumentalTag.Dispose ();
				InstrumentalTag = null;
			}

			if (PlayerIcon != null) {
				PlayerIcon.Dispose ();
				PlayerIcon = null;
			}

			if (ThumbnailView != null) {
				ThumbnailView.Dispose ();
				ThumbnailView = null;
			}

			if (SettingsButton != null) {
				SettingsButton.Dispose ();
				SettingsButton = null;
			}
		}
	}
}
