using System;
using Foundation;
using AppKit;

namespace Carol.Views
{
    public partial class ErrorView : NSView
    {
		AppDelegate currentDelegate;
        public static EventHandler RetryButtonClicked;
        #region Constructors

        // Called when created from unmanaged code
        public ErrorView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ErrorView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
			currentDelegate = NSApplication.SharedApplication.Delegate as AppDelegate;

            ViewController.NetworkErrorOccurred += HandleNetworkError;
            ViewController.LyricsNotFoundOccurred += HandleLyricsNotFound;
			ViewController.NothingPlayingFound += HandleNothingPlayingFound;
			ViewController.NoMusicAppRunningFound += HandleNoMusicAppRunningFound;
			ViewController.MultiPlayingFound += HandleMultiPlayingFound;
        }

		#endregion

		partial void RetryButtonClick(NSObject sender)
		{
            RetryButtonClicked?.Invoke(this, null);
		}

        void HandleNetworkError(object sender, EventArgs e)
        {
            ErrorTextView.StringValue = "Looks like there is no internet connection.";
			IllustrationContainer.Image = new NSImage("illustration_no_internet.png");
        }

        void HandleLyricsNotFound(object sender, EventArgs e)
        {
            ErrorTextView.StringValue = "Oops, could not find the lyrics of this song. Please try again later.";
			IllustrationContainer.Image = new NSImage("illustration_not_found.png");
			RetryButton.Hidden = false;
        }

		void HandleNothingPlayingFound(object sender, EventArgs e)
		{
			ErrorTextView.StringValue = "No track is playing. Play something from your awesome collection.";
			IllustrationContainer.Image = new NSImage("illustration_no_music.png");         
		}

		void HandleNoMusicAppRunningFound(object sender, EventArgs e)
        {
			ErrorTextView.StringValue = "No music app is running. Play some music from one of the apps.";
			IllustrationContainer.Image = new NSImage("illustration_no_app.png");
			iTunesButton.Hidden = false;
			SpotifyButton.Hidden = false;
        }

		void HandleMultiPlayingFound(object sender, EventArgs e)
        {
			ErrorTextView.StringValue = "You playin' two songs at a time. Living in 3018.";
			IllustrationContainer.Image = new NSImage("illustration_two_songs.png");
		}

		partial void OpeniTunesButtonClick(NSObject sender)
		{
			if (NSWorkspace.SharedWorkspace.LaunchApplication("/Applications/iTunes.app"))
            {
				currentDelegate.StatusBar.HidePopover(sender);
            }
			else
			{
			    currentDelegate.StatusBar.HidePopover(sender);
				var alert = new NSAlert()
                {
				    MessageText = "Looks like we are not able to launch iTunes app. Make sure it is installed and can be found in Applications folder."
                };
                alert.AddButton("Ok");
			    alert.RunModal();
			}
		}

		partial void OpenSpotifyButtonClick(NSObject sender)
		{
			if (NSWorkspace.SharedWorkspace.LaunchApplication("/Applications/Spotify.app"))
			{
				currentDelegate.StatusBar.HidePopover(sender);
			}
			else
            {
                currentDelegate.StatusBar.HidePopover(sender);
                var alert = new NSAlert()
                {
                    MessageText = "Looks like we are not able to launch Spotify app. Make sure it is installed and can be found in Applications folder."
                };
                alert.AddButton("Ok");
                alert.RunModal();
            }
		}
	}
}
