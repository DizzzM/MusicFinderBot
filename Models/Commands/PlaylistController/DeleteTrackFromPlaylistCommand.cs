using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFinderBot.Models.Commands
{
    public class DeleteTrackFromPlaylistCommand : Command
    {
        public override string Name => "/delete_track_from_playlist ";
        public override async Task Execute(string value, long chatId, int messageId)
        {
            var Values = value.Split(' ');
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, await ApiHelper.DeleteTrackFromPlaylist(chatId, value.Remove(value.Length - Values.Last().Length - 1), Values.Last()), replyToMessageId: messageId);
        }
    }
}
