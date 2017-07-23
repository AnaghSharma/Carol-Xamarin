// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Carol
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField ArtistName { get; set; }

		[Outlet]
		AppKit.NSTextView LyricsTextView { get; set; }

		[Outlet]
		AppKit.NSVisualEffectView MediaPlayer { get; set; }

		[Outlet]
		AppKit.NSImageView PlayerIcon { get; set; }

		[Outlet]
		AppKit.NSTextField PlayerName { get; set; }

		[Outlet]
		AppKit.NSTextField TrackName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ArtistName != null) {
				ArtistName.Dispose ();
				ArtistName = null;
			}

			if (LyricsTextView != null) {
				LyricsTextView.Dispose ();
				LyricsTextView = null;
			}

			if (MediaPlayer != null) {
				MediaPlayer.Dispose ();
				MediaPlayer = null;
			}

			if (TrackName != null) {
				TrackName.Dispose ();
				TrackName = null;
			}

			if (PlayerIcon != null) {
				PlayerIcon.Dispose ();
				PlayerIcon = null;
			}

			if (PlayerName != null) {
				PlayerName.Dispose ();
				PlayerName = null;
			}
		}
	}
}
