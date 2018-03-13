using System;
using Foundation;
using AppKit;

namespace Carol.Views
{
    public partial class LoadingView : NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public LoadingView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public LoadingView(NSCoder coder) : base(coder)
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

            Loader.StartAnimation(null);
        }
	}
}
