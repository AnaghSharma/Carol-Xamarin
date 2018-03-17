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
		AppKit.NSButton RetryButton { get; set; }

		[Action ("RetryButtonClick:")]
		partial void RetryButtonClick (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ErrorTextView != null) {
				ErrorTextView.Dispose ();
				ErrorTextView = null;
			}

			if (RetryButton != null) {
				RetryButton.Dispose ();
				RetryButton = null;
			}
		}
	}
}
