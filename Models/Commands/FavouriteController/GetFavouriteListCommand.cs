using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicFinderBot.Models.Commands
{
    public class GetFavouriteListCommand : Command
    {
        InlineKeyboardButton SpotifyButton = new InlineKeyboardButton { Text = "Open Spotify" };
        InlineKeyboardButton LyricsButton = new InlineKeyboardButton { Text = "Lyrics" };
        InlineKeyboardButton DeleteButton = new InlineKeyboardButton { Text = "Delete from Favourite" };

        public override string Name => "/get_favourite_list";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var TrackList = await ApiHelper.GetFavouriteList(chatId);
            if(TrackList==null)
            {
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "Seems like you haven`t added tracks to your Favourite list yet(", replyToMessageId: messageId);
            }
            else
            foreach (var Track in TrackList)
            {
                List<InlineKeyboardButton> _row1 = new List<InlineKeyboardButton>();
                List<InlineKeyboardButton> _row2 = new List<InlineKeyboardButton>();

                SpotifyButton.Url = Track.SpotifyURL;
                DeleteButton.CallbackData = $"/delete_from_favourite {Track.Id}";
                _row1.Add(SpotifyButton);
                _row2.Add(DeleteButton);

                if (Track.Lyrics != "Not found")
                {
                    LyricsButton.Url = Track.Lyrics;
                    _row1.Add(LyricsButton);
                }

                var keyboard = new InlineKeyboardMarkup(new[] { _row1.ToArray(), _row2.ToArray() });

                var Description = 
                    $"Name:\n  {Track.Name}" +
                    $"\nArtist:\n  {Track.Artist}";

                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, Description, replyMarkup: keyboard, replyToMessageId: messageId);
            }
        }
    }
}
