using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace MusicFinderBot.Models.Commands
{
    public class AddToPlaylistCommand : Command
    {
        public override string Name => "/add_to_playlist ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var Values = value.Split(' ');
            if (Values.Length > 1)
            {
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, await ApiHelper.AddTrackToPlaylist(chatId, value.Remove(value.Length - Values.Last().Length - 1), Values.Last()), replyToMessageId: messageId);
            }
            else
            {
                var playlists = await ApiHelper.GetAllPlaylists(chatId);
                if (playlists != null && playlists.Count > 0)
                {
                    List<List<InlineKeyboardButton>> Rows = new List<List<InlineKeyboardButton>>();
                    foreach (var playlist in await ApiHelper.GetAllPlaylists(chatId))
                    {
                        List<InlineKeyboardButton> Row = new List<InlineKeyboardButton>();
                        Row.Add(new InlineKeyboardButton { Text = playlist, CallbackData = $"/add_to_playlist { playlist} {value}" });
                        Rows.Add(Row);
                    }
                    InlineKeyboardMarkup Buttons = new InlineKeyboardMarkup(Rows.ToArray());
                    await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "Choose where to add:", replyToMessageId: messageId, replyMarkup: Buttons);
                }
                else
                {
                    await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "You have no playlists! Add them first!", replyToMessageId: messageId);
                }

            }
            
        }
    }
}
