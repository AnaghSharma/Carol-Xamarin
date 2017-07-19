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
		AppKit.NSTextField LyricsText { get; set; }

		[Outlet]
		AppKit.NSTextView LyricsTextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LyricsText != null) {
				LyricsText.Dispose ();
				LyricsText = null;
			}

			if (LyricsTextView != null) {
				LyricsTextView.Dispose ();
				LyricsTextView = null;
			}
		}
	}
}
