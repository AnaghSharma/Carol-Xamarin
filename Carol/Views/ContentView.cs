using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Carol.Views
{
    public partial class ContentView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ContentView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ContentView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion
    }
}
