using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicFinderBot.Models
{
    public static class ApiHelper
    {
        public static HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("https://musicfindapi.azurewebsites.net/api/"),
            Timeout = TimeSpan.FromMilliseconds(-1)
        };

        //SEARCH section

        public static async Task<List<Track>> GetTrackList(string Value)
        {
            var TrackList = new List<Track>();
            var Content = await Client.GetAsync(Client.BaseAddress + $"search/tracks?value={Value}");
            TrackList = JsonConvert.DeserializeObject<List<Track>>(await Content.Content.ReadAsStringAsync());

            return TrackList;
        }
        public static async Task<Track> GetLyrics(Track Track)
        {
            var json = JsonConvert.SerializeObject(Track);
            var Body = new StringContent(json, Encoding.UTF8, "application/json");
            var Content = await Client.PostAsync(Client.BaseAddress + $"search/lyrics",Body);
            Track = JsonConvert.DeserializeObject<Track>(await Content.Content.ReadAsStringAsync());

            return Track;
        }
        public static async Task<List<Track>> GetRelatedArtists(string id)
        {
            var Content = await Client.GetAsync(Client.BaseAddress + $"search/related?id={id}");
            return JsonConvert.DeserializeObject<List<Track>>(await Content.Content.ReadAsStringAsync());
        }
        
        //FAVOURITE section

        public static async Task<string> AddToFavourite(long ChatId, string TrackId)
        {
            var response = await Client.PostAsync(Client.BaseAddress + $"favourite/add?ChatId={ChatId}&TrackId={TrackId}", new StringContent("", Encoding.UTF8, "application/json"));
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());
            
            switch(content)
            {
                case HttpStatusCode.OK:
                    {
                        return $"This track was added to Favourite list";
                    }
                case HttpStatusCode.NotFound:
                    {
                        return $"Error! This track has been already added to Favourite list";
                    }
                default:
                    {
                        return $"Error {content} appeared!";
                    }
            }
        }
        public static async Task<List<Track>> GetFavouriteList(long ChatId)
        {
            var TrackList = new List<Track>();
            var Content = await Client.GetAsync(Client.BaseAddress + $"favourite/get?ChatId={ChatId}");
            TrackList = JsonConvert.DeserializeObject<List<Track>>(await Content.Content.ReadAsStringAsync());

            return TrackList;
        }
        public static async Task<string> DeleteFromFavourite(long ChatId, string TrackId)
        {
            var response = await Client.DeleteAsync(Client.BaseAddress + $"favourite/delete?ChatId={ChatId}&TrackId={TrackId}");
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());

            switch (content)
            {
                case HttpStatusCode.OK:
                    {
                        return $"This track was deleted from Favourite list";
                    }
                case HttpStatusCode.NotFound:
                    {
                        return $"This track was not found Favourite list";
                    }
                default:
                    {
                        return "Unknown error occured!";
                    }
            }
        }


        //PLAYLIST section

        public static async Task<string> AddTrackToPlaylist(long ChatId, string Name, string TrackId)
        {
            var response = await Client.PostAsync(Client.BaseAddress + $"playlists/add/track?ChatId={ChatId}&Name={Name}&TrackId={TrackId}", new StringContent("", Encoding.UTF8, "application/json"));
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());

            switch (content)
            {
                case HttpStatusCode.OK:
                    {
                        return $"This track was added to playlist {Name}";
                    }
                case HttpStatusCode.NotFound:
                    {
                        return $"Error! This track has been already added to playlist {Name}";
                    }
                case HttpStatusCode.BadRequest:
                    {
                        return $"Error! There is no {Name} playlist in my memory";
                    }
                default: return "Unknown error occured!";
            }
        }
        public static async Task<string> DeleteTrackFromPlaylist(long ChatId, string Name, string TrackId)
        {
            var response = await Client.DeleteAsync(Client.BaseAddress + $"playlists/delete/track?ChatId={ChatId}&Name={Name}&TrackId={TrackId}");
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());
            switch(content)
            {
                case HttpStatusCode.BadRequest: return $"Playlist {Name} was not found";
                case HttpStatusCode.NotFound: return $"Such track was not found in {Name}";
                case HttpStatusCode.OK: return "Track was successfully deleted!";
                default: return "Unknown error occured!";
            }
        }
        public static async Task<List<Track>> GetTracksFromPlaylist(long ChatId,string Name)
        {
            var TrackList = new List<Track>();
            var Content = await Client.GetAsync(Client.BaseAddress + $"playlists/get/playlist?ChatId={ChatId}&Name={Name}");
            TrackList = JsonConvert.DeserializeObject<List<Track>>(await Content.Content.ReadAsStringAsync());

            return TrackList;
        }
        public static async Task<string> AddPlaylist(long ChatId, string Name)
        {
            var response = await Client.PostAsync(Client.BaseAddress + $"playlists/add/playlist?ChatId={ChatId}&Name={Name}", new StringContent("", Encoding.UTF8, "application/json"));
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());

            switch (content)
            {
                case HttpStatusCode.OK:
                    {
                        return $"Playlist {Name} was added";
                    }
                case HttpStatusCode.NotFound:
                    {
                        return $"Playlist {Name} was already added";
                    }
                default: return "Unknown error occured!";
            }
        }
        public static async Task<string> DeletePlaylist(long ChatId, string Name)
        {
            var response = await Client.DeleteAsync(Client.BaseAddress + $"playlists/delete/playlist?ChatId={ChatId}&Name={Name}");
            var content = JsonConvert.DeserializeObject<HttpStatusCode>(await response.Content.ReadAsStringAsync());
            switch(content)
            {
                case HttpStatusCode.NotFound:  return "This playlist was not found";
                case HttpStatusCode.OK: return "Playlist was successfully deleted";
                default: return "Unknown error occured!";
            }
        } //done
        public static async Task<List<string>> GetAllPlaylists(long ChatId)
        {
            var PlaylistList = new List<string>();
            var Content = await Client.GetAsync(Client.BaseAddress + $"playlists/get/all?ChatId={ChatId}");
            PlaylistList = JsonConvert.DeserializeObject<List<string>>(await Content.Content.ReadAsStringAsync());

            return PlaylistList;
        } //done
    }
}

