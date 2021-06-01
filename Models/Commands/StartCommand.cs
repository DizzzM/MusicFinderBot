using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFinderBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            var Message = "Hello! I am MusicFinder Bot! I can find any track for everything you want" +
                "\n\nType:" +
                "\n     '/find (value) to start searching for track which is connected with the value you entered'" +
                "\n     '/get_favourite_list' to show tracks that were preiously added to Favourite list";
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, Message, replyToMessageId: messageId);
        }
    }
}
