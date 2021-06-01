using MusicFinderBot.Models.Commands;
using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace MusicFinderBot.Models
{
    public static class BotSettings
    {
        public static TelegramBotClient TelegramClient;
        private static readonly string Key = @""; // insert your Telegram Bot Key

        private static List<Command> _commands;
        public static IReadOnlyList<Command> Commands { get => _commands.AsReadOnly(); }
        public static void InitializeClient()
        {
            TelegramClient = new TelegramBotClient(Key);
            //Add here new commands
            _commands = new List<Command>();
            _commands.Add(new FindCommand());
            _commands.Add(new AddToFavouriteCommand());
            _commands.Add(new GetFavouriteListCommand());
            _commands.Add(new DeleteFromFavouriteCommand());
            _commands.Add(new StartCommand());
            _commands.Add(new RelatedArtistsCommand());
            _commands.Add(new DeleteTrackFromPlaylistCommand());
            _commands.Add(new DeletePlaylistCommand());
            _commands.Add(new AddPlaylistCommand());
            _commands.Add(new AddToPlaylistCommand());
            _commands.Add(new GetPlaylistsCommand());
            _commands.Add(new GetTracksFromPlaylistCommand());
        }
        public static void Start()
        {
            TelegramClient.StartReceiving();
            TelegramClient.OnMessage += Bot.OnMessage; //Event handler for messages
            TelegramClient.OnCallbackQuery += Bot.OnCallbackQuery; //Event handler for buttons

            Console.ReadLine();
        }
    }
}
