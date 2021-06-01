using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MusicFinderBot.Models
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract Task Execute(string value, long chatId, int messageId);
        public bool Contains(string command)
        {
            return command.Contains(Name)&&command.StartsWith(Name);
        }
    }
}
