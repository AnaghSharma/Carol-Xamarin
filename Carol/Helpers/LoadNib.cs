/*
 * Helper class to load and return an NSView object from a Nib file
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2018
 * 
 */

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


        public static T LoadViewFromNib<T>(string filename, NSObject owner) where T : NSView
        {
			//Loading the nib file and getting its items into NSArray
            NSBundle.MainBundle.LoadNibNamed(filename, owner, out xibItems);

            //Traversing the NSArray to look for the item of type T
            for (nuint i = 0; i < xibItems.Count; i++)
            {
                NSObject item = xibItems.GetItem<NSObject>(i);
                if (item is T)
                {
                    index = i;
                    break;
                }
            }

            return Runtime.GetNSObject<T>(xibItems.ValueAt(index));
        }
    }
}
