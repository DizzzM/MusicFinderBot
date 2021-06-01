using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace MusicFinderBot.Models
{
    public static class Bot
    {
        public async static void OnMessage(object sender, MessageEventArgs e)
        {
            var Message = e.Message;

            if (Message.Text != null)
            {
                var chatId = Message.Chat.Id;
                var messageId = Message.MessageId;
                var messageText = Message.Text;
                await CommandList(messageText, chatId, messageId);

            }
        }
        public async static void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var Message = e.CallbackQuery.Message;
            if (Message != null)
            {
                var chatId = Message.Chat.Id;
                var messageId = Message.MessageId;
                var queryText = e.CallbackQuery.Data;
                await CommandList(queryText, chatId, messageId);
            }
        }
        static async Task CommandList(string messageText, long chatId, int messageId)
        {
            var flag = false;
            foreach (var command in BotSettings.Commands)
            {
                if (command.Contains(messageText))
                {
                    await command.Execute(messageText.Replace(command.Name,""), chatId, messageId);
                    flag = true;
                    break;
                }
            }
            if (!flag)
                await BotSettings.TelegramClient.SendTextMessageAsync(chatId, "Sorry, I`am too stupid to interact with you(", replyToMessageId: messageId);
        }
    }
}

