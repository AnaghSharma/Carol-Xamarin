/*
 * Custom Popover control with adjustable background color
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

using System;
using System.ComponentModel;
using AppKit;
using Foundation;

namespace Carol.Controls
{
    // DesignTimeVisible(true) makes sure that the class is visible in Xcode at design time
    [Register("PopoverView"), DesignTimeVisible(true)]
    public class PopoverView : NSView
    {

        #region Constructors
        public PopoverView()
        {

        }

        public PopoverView(IntPtr p) : base(p)
        {

        }
        #endregion

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

                //You can change the background color of popover below. Here it takes the color as rgba
                backgroundView.Layer.BackgroundColor = new CoreGraphics.CGColor(0.07f, 0.07f, 0.07f, 1.0f);
                backgroundView.AutoresizingMask = (NSViewResizingMask.HeightSizable | NSViewResizingMask.WidthSizable);
                frameView.AddSubview(backgroundView, NSWindowOrderingMode.Below, frameView);
            }
        }
    }
}
