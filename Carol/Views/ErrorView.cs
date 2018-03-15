using System;
using Foundation;
using AppKit;

namespace Carol.Views
{
    public partial class ErrorView : NSView
    {
        public static EventHandler RetryButtonClicked;
        #region Constructors

        // Called when created from unmanaged code
        public ErrorView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ErrorView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

		#endregion

		partial void RetryButtonClick(NSObject sender)
		{
            RetryButtonClicked?.Invoke(this, null);
		}
	}
}
