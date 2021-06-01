using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicFinderBot.Models.Commands
{
    public class FindCommand : Command
    {
        InlineKeyboardButton SpotifyButton = new InlineKeyboardButton { Text = "Open Spotify" };
        InlineKeyboardButton LyricsButton = new InlineKeyboardButton { Text = "Lyrics" };
        InlineKeyboardButton FavouriteButton = new InlineKeyboardButton { Text = "Add to Favourite" };
        InlineKeyboardButton RelatedArtistsButton = new InlineKeyboardButton { Text = "Show related artists" };
        InlineKeyboardButton AddToPlaylistButton = new InlineKeyboardButton { Text = "AddToPlaylist" };

        public override string Name => "/find ";
        public override async Task Execute(string value, long chatId, int messageId)
        {
            if (value == "")
            {
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "There is no value entered! Try again", replyToMessageId: messageId);
            }
            else
            {
                var TrackList = await ApiHelper.GetTrackList(value);
                foreach (var _track in TrackList)
                {
                    List<InlineKeyboardButton> _row1 = new List<InlineKeyboardButton>();
                    List<InlineKeyboardButton> _row2 = new List<InlineKeyboardButton>();
                    List<InlineKeyboardButton> _row3 = new List<InlineKeyboardButton>();
                    List<InlineKeyboardButton> _row4 = new List<InlineKeyboardButton>();
                    var Track = await ApiHelper.GetLyrics(_track);

                    SpotifyButton.Url = Track.SpotifyURL;
                    FavouriteButton.CallbackData = $"/add_to_favourite {Track.Id}";
                    RelatedArtistsButton.CallbackData = "/related_artists " + Track.ArtistId;
                    AddToPlaylistButton.CallbackData = "/add_to_playlist " + Track.Id; ;
                    _row1.Add(SpotifyButton);
                    _row2.Add(FavouriteButton);
                    _row3.Add(RelatedArtistsButton);
                    _row4.Add(AddToPlaylistButton);

                    if (Track.Lyrics != "Not found")
                    {
                        LyricsButton.Url = Track.Lyrics;
                        _row1.Add(LyricsButton);
                    }
                   var keyboard = new InlineKeyboardMarkup(new[] {_row1.ToArray(), _row2.ToArray(), _row3.ToArray(), _row4.ToArray() } );

                    var Description =
                        $"Name:\n  {Track.Name}" +
                        $"\nArtist:\n  {Track.Artist}";

                    await BotSettings.TelegramClient.SendPhotoAsync(chatId, Track.ImageURL, Description, replyMarkup: keyboard, replyToMessageId: messageId);
                }
            }
        }
    }
}
