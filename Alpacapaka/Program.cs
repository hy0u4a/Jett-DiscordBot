using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace Alpacapaka
{
    class Program
    {
        DiscordSocketClient client;
        CommandService commands;

        static void Main(string[] args)
        {
            new Program().BotMain().GetAwaiter().GetResult();
        }

        public async Task BotMain()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose
            });

            commands = new CommandService(new CommandServiceConfig()
            {
                LogLevel = LogSeverity.Verbose
            });

            client.Log += OnClientLogReceived;
            commands.Log += OnClientLogReceived;

            await client.LoginAsync(TokenType.Bot, "Your Token");
            await client.StartAsync();

            client.MessageReceived += OnClientMessage;

            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

            await Task.Delay(-1);
        }

        private Task OnClientLogReceived(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task OnClientMessage(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message == null) return;

            int pos = 0;

            if (!(message.HasStringPrefix("@제트", ref pos) ||
                message.HasMentionPrefix(client.CurrentUser, ref pos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(client, message);

            var result = await commands.ExecuteAsync(
                context: context,
                argPos: pos,
                services: null);
        }
    }
}
