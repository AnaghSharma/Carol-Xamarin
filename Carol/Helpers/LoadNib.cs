using System;
using AppKit;
using Foundation;
using ObjCRuntime;

namespace Carol.Helpers
{
    public static class LoadNib
    {
        static NSArray xibItems;
        static nuint index;


        public static NSView LoadViewFromNib<T>(string filename, NSObject owner) where T : NSView
        {
            NSBundle.MainBundle.LoadNibNamed(filename, owner, out xibItems);

            for (nuint i = 0; i < 2; i++)
            {
                NSObject item = xibItems.GetItem<NSObject>(i);
                if (item is T)
                {
                    index = i;
                }
            }

            return Runtime.GetNSObject<T>(xibItems.ValueAt(index));
        }
    }
}
