﻿﻿using System;
using System.IO;
using AppKit;
using Carol.Helpers;
using Carol.Models;
using CoreGraphics;
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
        CGRect progress;
        float containerHeight;

        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            lyricsHelper = new LyricsHelper();
            LyricsTextView.BackgroundColor = NSColor.Clear;
			MediaPlayer.WantsLayer = true;
			MediaPlayer.Material = NSVisualEffectMaterial.Dark;
			MediaPlayer.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;
            MediaPlayer.Layer.CornerRadius = 4.0f;

            progress = ProgressBar.Frame;

            MainScroll.ContentView.PostsBoundsChangedNotifications = true;
            NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("boundsChange:"),
            NSView.BoundsChangedNotification, MainScroll.ContentView);
		}

        [Export("boundsChange:")]
        public void BoundsDidChangeNotification(NSObject sender)
        {
            var notification = sender as NSNotification;
            var view = notification.Object as NSView;
            var position = view.Bounds.Location.Y;

            progress.Width = (position * 100) / (containerHeight - MainScroll.Bounds.Height);
            ProgressBar.Frame = progress;
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

            if (result.NumberOfItems == 3)
            {
                var artist = result.DescriptorAtIndex(1).StringValue;
                var track = result.DescriptorAtIndex(2).StringValue;
                var app = result.DescriptorAtIndex(3).StringValue;

                var lyrics = lyricsHelper.GetLyrics(track, artist, (response) =>
                 {
                     TrackLyrics.RootObject tracklyrics = JsonConvert.DeserializeObject<TrackLyrics.RootObject>(response);
                     LyricsTextView.Value = tracklyrics.message.body.lyrics.lyrics_body;
                     TrackName.StringValue = track;
                     ArtistName.StringValue = artist;
                     
                     if (app == "iTunes")
                     {
                         PlayerIcon.Image = new NSImage("icon_itunes.pdf");
                         PlayerName.StringValue = app;
                     }
                     else 
                    {
						 PlayerIcon.Image = new NSImage("icon_spotify.pdf");
						 PlayerName.StringValue = app;
                    }

                    containerHeight = (float)LyricsTextView.Bounds.Height;
                 });
            }
            else if (result.NumberOfItems == 0)
            {
                switch (result.StringValue)
                {
                    case "1":
                        LyricsTextView.Value = "No track playing";
                        break;
                    case "2":
                        LyricsTextView.Value = "No music app is running";
                        break;
                    case "3":
                        LyricsTextView.Value = "You playin' two songs at a time. Livin' in 3017";
                        break;
                }
            }
            else
                LyricsTextView.Value = "Something went wrong. It happens.";
        }
    }
}
