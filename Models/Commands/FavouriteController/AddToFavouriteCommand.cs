using System.Threading.Tasks;

namespace MusicFinderBot.Models.Commands
{
    public class AddToFavouriteCommand : Command
    {
        public override string Name => "/add_to_favourite ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, await ApiHelper.AddToFavourite(chatId, value), replyToMessageId: messageId);
        }
    }
}
