/*
 * Helper class to get the lyrics of a track
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Carol.Models;
using Newtonsoft.Json;

namespace Carol.Helpers
{
    public class LyricsHelper
    {
        private readonly HttpClient client;
        private readonly String apikey;
        private int trackid;
        private string artist_name, share_url;

        public LyricsHelper()
        {
            client = new HttpClient();
            apikey = SecretsReader.GetSecrets();
        }

        /// <summary>
        /// Method to get the track id of currently playing track
        /// </summary>
        /// <returns>Trackid.</returns>
        /// <param name="track">Track Name.</param>
        /// <param name="artist">Artist Name.</param>
		public async Task GetTrackId(string track, string artist)
		{
            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.search?q_track={0}&q_artist={1}&apikey={2}", track, artist, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
				Tracks.RootObject trackresult = JsonConvert.DeserializeObject<Tracks.RootObject>(await response.Content.ReadAsStringAsync());
				trackid = trackresult.message.body.track_list[0].track.track_id;
                artist_name = trackresult.message.body.track_list[0].track.artist_name;
                share_url = trackresult.message.body.track_list[0].track.track_share_url;
			}
		}

        /// <summary>
        /// Method to get the lyrics of a track
        /// </summary>
        /// <returns>Lyrics of the track.</returns>
        /// <param name="track">Track.</param>
        /// <param name="artist">Artist.</param>
        /// <param name="onSuccess">Action.</param>
        public async Task GetLyrics(string track, string artist, Action<string, string, string> onSuccess)
        {
            await GetTrackId(track, artist);

            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id={0}&apikey={1}", trackid, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
                onSuccess(await response.Content.ReadAsStringAsync(), artist_name, share_url);
			}
        }
	}
}
