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
	[Register ("AboutViewController")]
	partial class AboutViewController
	{
		[Outlet]
		Carol.Controls.HyperlinkTextField AboutLink { get; set; }

		[Outlet]
		Carol.Controls.HyperlinkTextField GitHubLink { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GitHubLink != null) {
				GitHubLink.Dispose ();
				GitHubLink = null;
			}

			if (AboutLink != null) {
				AboutLink.Dispose ();
				AboutLink = null;
			}
		}
	}
}
