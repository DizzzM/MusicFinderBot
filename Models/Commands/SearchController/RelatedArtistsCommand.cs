using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MusicFinderBot.Models.Commands
{
    class RelatedArtistsCommand : Command
    {
        public override string Name => "/related_artists ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var Message = "Related artists:\n";
            var Artists = await ApiHelper.GetRelatedArtists(value);
            int count = 0;
            foreach(var Artist in Artists)
            {
                if (count < 10)
                    Message += $"    {Artist.Artist}\n";
                else break;
                count++;
            }
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, Message, replyToMessageId: messageId);
        }
    }
}
