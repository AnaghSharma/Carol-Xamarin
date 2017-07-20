using System;
using System.ComponentModel;
using AppKit;
using Foundation;

namespace Carol.Controls
{
    [Register("PopoverView"), DesignTimeVisible(true)]
    public class PopoverView : NSView
    {
        public PopoverView()
        {

        }

        public PopoverView(IntPtr p) : base(p)
        {

        }

        public override void ViewDidMoveToWindow()
        {
            base.ViewDidMoveToWindow();

            var frameView = Window.ContentView.Superview;
            if (frameView != null)
            {
                var backgroundView = new NSView(frameView.Bounds)
                {
                    WantsLayer = true
                };
                backgroundView.Layer.BackgroundColor = new CoreGraphics.CGColor(0.07f, 0.07f, 0.07f, 1.0f);
                backgroundView.AutoresizingMask = (NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable);
                frameView.AddSubview(backgroundView, NSWindowOrderingMode.Below, frameView);
            }
        }
    }
}
