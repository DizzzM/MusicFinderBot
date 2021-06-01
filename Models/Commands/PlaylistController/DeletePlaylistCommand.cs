using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFinderBot.Models.Commands
{
    public class DeletePlaylistCommand : Command
    {
        public override string Name => "/delete_playlist ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, await ApiHelper.DeletePlaylist(chatId, value), replyToMessageId: messageId);
        }
    }
}
