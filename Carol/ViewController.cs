﻿using System;
using System.IO;
using AppKit;
using Carol.Helpers;
using Carol.Models;
using Foundation;
using Newtonsoft.Json;

namespace Carol
{
    public partial class ViewController : NSViewController
    {
        NSAppleScript script;
        NSDictionary errors;
        NSAppleEventDescriptor result;
        LyricsHelper lyricsHelper;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            lyricsHelper = new LyricsHelper();
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

            LyricsText.StringValue = "";
			var getCurrentSongScript = File.ReadAllText("Scripts/GetCurrentSong.txt");
			script = new NSAppleScript(getCurrentSongScript);
			result = script.ExecuteAndReturnError(out errors);

			var artist = result.DescriptorAtIndex(1).StringValue;
			var track = result.DescriptorAtIndex(2).StringValue;

            var lyrics = lyricsHelper.GetLyrics(track, artist,(response) => 
            {
                TrackLyrics.RootObject tracklyrics = JsonConvert.DeserializeObject<TrackLyrics.RootObject>(response);
                LyricsText.StringValue = tracklyrics.message.body.lyrics.lyrics_body;
            });
        }
    }
}
