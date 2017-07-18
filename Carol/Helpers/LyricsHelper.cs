﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Carol.Helpers
{
    public class LyricsHelper
    {
        private readonly HttpClient client;
        private readonly String apikey;

        public LyricsHelper()
        {
            client = new HttpClient();
            apikey = SecretsReader.GetSecrets();
        }

		public async Task GetSongId(string track, string artist, Action<string> onSuccess)
		{
            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.search?q_track={0}&q_artist={1}&apikey={2}", track, artist, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
				onSuccess(await response.Content.ReadAsStringAsync());
			}
		}

        public async Task GetLyrics(string trackid, Action<string> onSuccess)
        {
            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id={0}&apikey={1}", trackid, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
				onSuccess(await response.Content.ReadAsStringAsync());
			}
        }
	}
}
