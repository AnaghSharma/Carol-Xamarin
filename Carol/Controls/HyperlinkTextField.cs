/*
 * Custom Text field control that works as a hyperlink
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
    [Register("HyperlinkTextField"), DesignTimeVisible(true)]
    public class HyperlinkTextField : NSTextField
    {
        String href = "";
        NSTrackingArea hoverarea;
        NSCursor cursor;

        [Export("Href"), Browsable(true)]
        public String Href
        {
            get
            {
                return href;
            }

            set => href = value;
        }

        #region Constructors
        public HyperlinkTextField(IntPtr p) : base(p)
        {

        }

        public HyperlinkTextField()
        {
            
        }
        #endregion

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            AttributedStringValue = new NSAttributedString(StringValue, new NSStringAttributes()
            {
                //You can change the color of link after uncommenting the following
                //ForegroundColor = NSColor.Blue,
                UnderlineStyle = NSUnderlineStyle.Single.GetHashCode()
            });

            hoverarea = new NSTrackingArea(Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null);
            AddTrackingArea(hoverarea);
            cursor = NSCursor.CurrentSystemCursor;
        }

        //Method override to open url on click of HyperlinkTextField
        public override void MouseDown(NSEvent theEvent)
        {
            NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl(href));
        }

        //Method override to change cursor to pointing hand on Mouse Enter (Hover)
        public override void MouseEntered(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);

            cursor = NSCursor.PointingHandCursor;
            cursor.Push();
        }

        //Method override to change cursor back to pointing arrow on Mouse Exit
        public override void MouseExited(NSEvent theEvent)
        {
            base.MouseEntered(theEvent);

            cursor.Pop();
        }
    }
}
