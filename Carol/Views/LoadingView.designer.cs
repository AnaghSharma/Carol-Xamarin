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
	[Register ("LoadingView")]
	partial class LoadingView
	{
		[Outlet]
		AppKit.NSProgressIndicator Loader { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Loader != null) {
				Loader.Dispose ();
				Loader = null;
			}
		}
	}
}
