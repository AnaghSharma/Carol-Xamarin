/*
 * Model for track
 * 
 * Author - Anagh Sharma
 * http://www.anaghsharma.com
 * 
 * 2017
 * 
 */

using System;
using System.Collections.Generic;

namespace Carol.Models
{
    public class Tracks
    {
		public class Header
		{
			public int status_code { get; set; }
			public double execute_time { get; set; }
			public int available { get; set; }
		}

		public class PrimaryGenres
		{
			public List<object> music_genre_list { get; set; }
		}

		public class SecondaryGenres
		{
			public List<object> music_genre_list { get; set; }
		}

		public class Track
		{
			public int track_id { get; set; }
			public string track_mbid { get; set; }
			public string track_isrc { get; set; }
			public string track_spotify_id { get; set; }
			public string track_soundcloud_id { get; set; }
			public string track_xboxmusic_id { get; set; }
			public string track_name { get; set; }
			public List<object> track_name_translation_list { get; set; }
			public int track_rating { get; set; }
			public int track_length { get; set; }
			public int commontrack_id { get; set; }
			public int instrumental { get; set; }
			public int @explicit { get; set; }
			public int has_lyrics { get; set; }
			public int has_lyrics_crowd { get; set; }
			public int has_subtitles { get; set; }
			public int has_richsync { get; set; }
			public int num_favourite { get; set; }
			public int lyrics_id { get; set; }
			public int subtitle_id { get; set; }
			public int album_id { get; set; }
			public string album_name { get; set; }
			public int artist_id { get; set; }
			public string artist_mbid { get; set; }
			public string artist_name { get; set; }
			public string album_coverart_100x100 { get; set; }
			public string album_coverart_350x350 { get; set; }
			public string album_coverart_500x500 { get; set; }
			public string album_coverart_800x800 { get; set; }
			public string track_share_url { get; set; }
			public string track_edit_url { get; set; }
			public string commontrack_vanity_id { get; set; }
			public int restricted { get; set; }
			public string first_release_date { get; set; }
			public string updated_time { get; set; }
			public PrimaryGenres primary_genres { get; set; }
			public SecondaryGenres secondary_genres { get; set; }
		}

		public class TrackList
		{
			public Track track { get; set; }
		}

		public class Body
		{
			public List<TrackList> track_list { get; set; }
		}

		public class Message
		{
			public Header header { get; set; }
			public Body body { get; set; }
		}

		public class RootObject
		{
			public Message message { get; set; }
		}
    }
}
