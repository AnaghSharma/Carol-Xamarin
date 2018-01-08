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
        NSTrackingArea hoverarea;
        NSMenu settingsMenu;
        NSMenuItem launch;
        bool isLoginItem;
        public static event EventHandler QuitButtonClicked;
        public static event EventHandler AboutMenuItemClicked;

        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            lyricsHelper = new LyricsHelper();
            LyricsTextView.BackgroundColor = NSColor.Clear;

            // Media Player is the Visual Effect View with Blur and Vibrancy
            //MediaPlayer.WantsLayer = true;
            //MediaPlayer.Material = NSVisualEffectMaterial.Dark;
            //MediaPlayer.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;
            //MediaPlayer.Layer.CornerRadius = 4.0f;

            BlurOverlay.WantsLayer = true;
            BlurOverlay.Material = NSVisualEffectMaterial.Dark;
            BlurOverlay.BlendingMode = NSVisualEffectBlendingMode.WithinWindow;

            // Progress bar shows how much of lyrics have you covered. It works with scrollview
            progress = ProgressBar.Frame;

            //Adding observer of Scroll view change in Notification Center. It helps to update the width of progress bar
            MainScroll.ContentView.PostsBoundsChangedNotifications = true;
            NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("boundsChange:"),
            NSView.BoundsChangedNotification, MainScroll.ContentView);

            #region Settings Menu
            settingsMenu = new NSMenu();
            hoverarea = new NSTrackingArea(SettingsButton.Bounds, NSTrackingAreaOptions.MouseEnteredAndExited | NSTrackingAreaOptions.ActiveAlways, this, null);
            SettingsButton.AddTrackingArea(hoverarea);

            launch = new NSMenuItem("Launch at Login", new ObjCRuntime.Selector("launch:"), "");
            NSMenuItem about = new NSMenuItem("About", new ObjCRuntime.Selector("about:"), "");
            NSMenuItem quit = new NSMenuItem("Quit Carol", new ObjCRuntime.Selector("quit:"), "q");


            settingsMenu.AddItem(launch);
            settingsMenu.AddItem(about);
            settingsMenu.AddItem(NSMenuItem.SeparatorItem);
            settingsMenu.AddItem(quit);
            #endregion

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

            //The NumberofItems property is being used to handle different use cases. Check GetCurrentSong.txt in Scripts folder to know more
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
                         
                         
                         var getAlbumArt = File.ReadAllText("Scripts/GetAlbumArtiTunes.txt");
                         script = new NSAppleScript(getAlbumArt);
                         result = script.ExecuteAndReturnError(out errors);
                         NSImage cover = new NSImage(result.Data);
                         AlbumArtView.Image = cover;
                     }
                     else 
                    {
						 PlayerIcon.Image = new NSImage("icon_spotify.pdf");
						
                         
                         var getAlbumArt = File.ReadAllText("Scripts/GetAlbumArtSpotify.txt");
                         script = new NSAppleScript(getAlbumArt);
                         result = script.ExecuteAndReturnError(out errors);
                         NSUrl artworkurl = new NSUrl(result.StringValue);
                         NSImage cover = new NSImage(artworkurl);
                         AlbumArtView.Image = cover;
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

        //Observer method called to update the progress bar whenever scrolling occurs 
        [Export("boundsChange:")]
        public void BoundsDidChangeNotification(NSObject sender)
        {
            var notification = sender as NSNotification;
            var view = notification.Object as NSView;
            var position = view.Bounds.Location.Y;

            var width = (position * 100) / (containerHeight - MainScroll.Bounds.Height);
            if (width > 4 && width <= 100)
                progress.Width = width;
            else if (width < 0)
                progress.Width = 4;
            else if (width > 100)
                progress.Width = 100;
            ProgressBar.Frame = progress;
        }


        partial void SettingsButtonClick(NSObject sender)
        {
            var current = NSApplication.SharedApplication.CurrentEvent;

            //Check if the app is in login items of macOS or not
            var checkLoginItemsScript = File.ReadAllText("Scripts/LoginCheck.txt");
            script = new NSAppleScript(checkLoginItemsScript);
            result = script.ExecuteAndReturnError(out errors);
            isLoginItem = result.BooleanValue;

            if (!isLoginItem)
            {
                launch.State = NSCellStateValue.Off;
            }
            else if (isLoginItem)
                launch.State = NSCellStateValue.On;
            
            NSMenu.PopUpContextMenu(settingsMenu, current, sender as NSView);
        }

        //Method to handle Launch at Login functionality
        [Export("launch:")]         void Launch(NSObject sender)         {
            if (!isLoginItem)
            {
                var addToLoginScript = File.ReadAllText("Scripts/LoginAdd.txt");
                script = new NSAppleScript(addToLoginScript);
                script.ExecuteAndReturnError(out errors);
            }
            else
            {
                var removeFromLoginScript = File.ReadAllText("Scripts/LoginRemove.txt");
                script = new NSAppleScript(removeFromLoginScript);
                script.ExecuteAndReturnError(out errors);
            }         }
         //Delegating the About Menu Item click event to Helpers/StatusBarController.cs
        [Export("about:")]
        void About(NSObject sender)
        {
            AboutMenuItemClicked?.Invoke(this, null);
        }

        //Delegating the Quit Menu Item click event to Helpers/StatusBarController.cs
        [Export("quit:")]         void Quit(NSObject sender)         {             QuitButtonClicked?.Invoke(this, null);         }
    }
}
