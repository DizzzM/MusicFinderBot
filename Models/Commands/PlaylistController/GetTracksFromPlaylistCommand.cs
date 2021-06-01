using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicFinderBot.Models.Commands
{
    public class GetTracksFromPlaylistCommand : Command
    {
        InlineKeyboardButton SpotifyButton = new InlineKeyboardButton { Text = "Open Spotify" };
        InlineKeyboardButton LyricsButton = new InlineKeyboardButton { Text = "Lyrics" };
        InlineKeyboardButton DeleteTrackButton = new InlineKeyboardButton { Text = "Delete from playlist" };
        InlineKeyboardButton DeletePlaylistButton = new InlineKeyboardButton { Text = "Delete playlist" };

        public override string Name => "/get_tracks_from_playlist ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var TrackList = await ApiHelper.GetTracksFromPlaylist(chatId, value);
            if (TrackList == null)
            {
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, $"Playlist {value} is empty!", replyToMessageId: messageId);
            }
            else
            {
                foreach (var _track in TrackList)
                {
                    List<InlineKeyboardButton> _row1 = new List<InlineKeyboardButton>();
                    List<InlineKeyboardButton> _row2 = new List<InlineKeyboardButton>();
                    var Track = await ApiHelper.GetLyrics(_track);

                    SpotifyButton.Url = Track.SpotifyURL;
                    DeleteTrackButton.CallbackData = $"/delete_track_from_playlist {value} {Track.Id}";
                    _row1.Add(SpotifyButton);
                    _row2.Add(DeleteTrackButton);

                    if (Track.Lyrics != "Not found")
                    {
                        LyricsButton.Url = Track.Lyrics;
                        _row1.Add(LyricsButton);
                    }

                    var keyboard = new InlineKeyboardMarkup(new[] { _row1.ToArray(), _row2.ToArray() });

                    var Description =
                          $"Name:\n        {Track.Name}" +
                        $"\nArtist:\n        {Track.Artist}";

                    await BotSettings.TelegramClient.SendTextMessageAsync(chatId, Description, replyMarkup: keyboard, replyToMessageId: messageId);

                }
            }
            List<InlineKeyboardButton> row = new List<InlineKeyboardButton>();
            DeletePlaylistButton.CallbackData = $"/delete_playlist {value}";
            row.Add(DeletePlaylistButton);
            InlineKeyboardMarkup button = new InlineKeyboardMarkup(row.ToArray());
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "You can delete this playlist", replyMarkup: button, replyToMessageId: messageId);

        }
    }
}
