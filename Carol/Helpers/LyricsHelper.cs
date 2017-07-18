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

        public LyricsHelper()
        {
            client = new HttpClient();
            apikey = SecretsReader.GetSecrets();
        }

		public async Task GetTrackId(string track, string artist)
		{
            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.search?q_track={0}&q_artist={1}&apikey={2}", track, artist, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
				Tracks.RootObject trackresult = JsonConvert.DeserializeObject<Tracks.RootObject>(await response.Content.ReadAsStringAsync());
				trackid = trackresult.message.body.track_list[0].track.track_id;
			}
		}

        public async Task GetLyrics(string track, string artist, Action<string> onSuccess)
        {
            await GetTrackId(track, artist);

            var uri = new Uri(String.Format("https://api.musixmatch.com/ws/1.1/track.lyrics.get?track_id={0}&apikey={1}", trackid, apikey));
			var response = await client.GetAsync(uri.AbsoluteUri);
			if (response.IsSuccessStatusCode)
			{
				onSuccess(await response.Content.ReadAsStringAsync());
			}
        }
	}
}
