using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicFinderBot.Models.Commands
{
    public class GetPlaylistsCommand : Command
    {
        public override string Name => "/show_all_playlists";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var playlists = await ApiHelper.GetAllPlaylists(chatId);
            if (playlists != null)
            {
                List<List<InlineKeyboardButton>> Rows = new List<List<InlineKeyboardButton>>();
                foreach (var playlist in playlists)
                {
                    List<InlineKeyboardButton> Row = new List<InlineKeyboardButton>();
                    Row.Add(new InlineKeyboardButton { Text = playlist, CallbackData = $"/get_tracks_from_playlist {playlist}" });
                    Rows.Add(Row);
                }
                InlineKeyboardMarkup Buttons = new InlineKeyboardMarkup(Rows.ToArray());
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "Your playlists:", replyToMessageId: messageId, replyMarkup: Buttons);
            }
            else
            {
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "You have no playlists! Add them first!", replyToMessageId: messageId);
            }
        }
    }
}
