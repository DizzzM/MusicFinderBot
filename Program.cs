using MusicFinderBot.Models;
namespace MusicFinderBot
{
    class Program
    {
        static void Main(string[] args)
        {
            BotSettings.InitializeClient();
            BotSettings.Start();
        }
    }
}
