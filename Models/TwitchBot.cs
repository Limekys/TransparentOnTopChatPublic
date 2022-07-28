using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TransparentOnTopChat
{
    internal class TwitchBot
    {
        public TwitchClient client;
        ConnectionCredentials credentials = new ConnectionCredentials(Twitch_info.ChannelName, Twitch_info.BotToken);

        public TwitchBot()
        {
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            client = new TwitchClient(customClient);
            client.Initialize(credentials, Twitch_info.ChannelName);
        }

        internal void Disconnect()
        {
            client.Disconnect();

        }

        internal void Connect()
        {
            client.Connect();
        }
    }
}
