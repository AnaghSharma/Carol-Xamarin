﻿﻿using System;
using System.IO;
using AppKit;
using Carol.Helpers;
using Carol.Helpers.StateMachine;
using Carol.Models;
using Carol.Views;
using Foundation;
using Newtonsoft.Json;

namespace Carol
{
    public partial class ViewController : NSViewController
    {
        ViewStateMachine stateMachine;

        NSAppleScript script;
        NSDictionary errors;
        NSAppleEventDescriptor result;

        private String artist, track, app, track_share_url;

        LyricsHelper lyricsHelper;
        TrackLyrics.RootObject tracklyrics;

        public String Artist => artist;
        public String Track => track;
        public String App => app;
        public String ShareUrl => track_share_url;

        public TrackLyrics.RootObject TrackLyrics => tracklyrics;

        public static event EventHandler NetworkErrorOccurred;
        public static event EventHandler LyricsNotFoundOccurred;
		public static event EventHandler NothingPlayingFound;
		public static event EventHandler NoMusicAppRunningFound;
		public static event EventHandler MultiPlayingFound;

        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        ~ViewController()
        {
            ErrorView.RetryButtonClicked -= HandleRetryButtonClick;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Loading the dark theme from Dark.car file in Resources
            View.Appearance = new NSAppearance(@"Dark", null);

            //Setting the initial state of the state machine
            stateMachine = new ViewStateMachine(States.Idle);
            stateMachine.SetupInitialView();

            lyricsHelper = new LyricsHelper();

            ErrorView.RetryButtonClicked += HandleRetryButtonClick;
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

            FetchLyrics();
        }

        private void FetchLyrics()
        {
            stateMachine.StartLoading();

            if (Reachability.IsNetworkAvailable())
            {
                script = new NSAppleScript(File.ReadAllText("Scripts/GetCurrentSong.txt"));
                result = script.ExecuteAndReturnError(out errors);

                //The NumberofItems property is being used to handle different use cases. Check GetCurrentSong.txt in Scripts folder to know more
                if (result.NumberOfItems == 3)
                {
                    artist = result.DescriptorAtIndex(1).StringValue;
                    track = result.DescriptorAtIndex(2).StringValue;
                    app = result.DescriptorAtIndex(3).StringValue;

                    var lyrics = lyricsHelper.GetLyrics(track, artist, (response, artist_name, share_url) =>
                    {
                        tracklyrics = JsonConvert.DeserializeObject<TrackLyrics.RootObject>(response);

                        if (tracklyrics.message.body.lyrics.instrumental == 0)
                        {
                            track_share_url = share_url;
                            stateMachine.ShowContent();
                        }
                        else
                            stateMachine.ShowEmpty();
                    });
                }
                else if (result.NumberOfItems == 0)
                {
                    switch (result.StringValue)
                    {
						//Transitioning to error state of state machine and calling the corresponding delegate method
                        //These delegates method are being used so that one view can be reused for various error cases.
                        case "1":
                            stateMachine.ShowError();
							NothingPlayingFound?.Invoke(this, null);
                            break;
                        case "2":
                            stateMachine.ShowError();
							NoMusicAppRunningFound?.Invoke(this, null);
                            break;
                        case "3":
                            stateMachine.ShowError();
							MultiPlayingFound?.Invoke(this, null);
                            break;
                    }
                }
                else
				{
					stateMachine.ShowError();
					LyricsNotFoundOccurred?.Invoke(this, null);
				}   
            }

            else
            {
                stateMachine.ShowError();
                NetworkErrorOccurred?.Invoke(this, null);
            }
        }

        void HandleRetryButtonClick(object sender, EventArgs e)
        {
            FetchLyrics();
        }
    }
}
