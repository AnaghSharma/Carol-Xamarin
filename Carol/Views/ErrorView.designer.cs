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
	[Register ("ErrorView")]
	partial class ErrorView
	{
		[Outlet]
		AppKit.NSTextField ErrorTextView { get; set; }

		[Outlet]
		AppKit.NSImageView IllustrationContainer { get; set; }

		[Outlet]
		AppKit.NSButton iTunesButton { get; set; }

		[Outlet]
		AppKit.NSButton RetryButton { get; set; }

		[Outlet]
		AppKit.NSButton SpotifyButton { get; set; }

		[Action ("OpeniTunesButtonClick:")]
		partial void OpeniTunesButtonClick (Foundation.NSObject sender);

		[Action ("OpenSpotifyButtonClick:")]
		partial void OpenSpotifyButtonClick (Foundation.NSObject sender);

		[Action ("RetryButtonClick:")]
		partial void RetryButtonClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ErrorTextView != null) {
				ErrorTextView.Dispose ();
				ErrorTextView = null;
			}

			if (IllustrationContainer != null) {
				IllustrationContainer.Dispose ();
				IllustrationContainer = null;
			}

			if (RetryButton != null) {
				RetryButton.Dispose ();
				RetryButton = null;
			}

			if (iTunesButton != null) {
				iTunesButton.Dispose ();
				iTunesButton = null;
			}

			if (SpotifyButton != null) {
				SpotifyButton.Dispose ();
				SpotifyButton = null;
			}
		}
	}
}
