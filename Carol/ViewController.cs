using System;
using System.IO;
using AppKit;
using Foundation;

namespace Carol
{
    public partial class ViewController : NSViewController
    {
        NSAppleScript script;
        NSDictionary errors;
        NSAppleEventDescriptor result;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

		public override void ViewDidAppear()
		{
			base.ViewDidAppear();

			var getCurrentSongScript = File.ReadAllText("Scripts/GetCurrentSong.txt");
			script = new NSAppleScript(getCurrentSongScript);
			result = script.ExecuteAndReturnError(out errors);

			var artist = result.DescriptorAtIndex(1).StringValue;
			var track = result.DescriptorAtIndex(2).StringValue;

			Console.WriteLine(track + " - " + artist);
		}
    }
}
