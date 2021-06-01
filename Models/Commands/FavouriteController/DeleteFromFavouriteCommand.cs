using System.Threading.Tasks;

namespace MusicFinderBot.Models.Commands
{
    public class DeleteFromFavouriteCommand : Command
    {
        public override string Name => "/delete_from_favourite ";

        public override async Task Execute(string value, long chatId, int messageId)
        {
            await BotSettings.TelegramClient.SendTextMessageAsync(chatId, await ApiHelper.DeleteFromFavourite(chatId, value), replyToMessageId: messageId);
        }
    }
}
