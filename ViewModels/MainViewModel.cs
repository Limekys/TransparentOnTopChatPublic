using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace TransparentOnTopChat.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //public static SettingsWindow settings;

        private ChatMessage currentMessage;
        //private MainWindow uiInstance;

        public static int messagesHistory = 20;

        public static ObservableCollection<ChatMessage> Messages { get; set; }
        public ChatMessage CurrentMessage
        {
            get { return currentMessage; }
            set
            {
                currentMessage = value;
                OnPropertyChanged("CurrentMessage");
            }
        }

        public MainViewModel()
        {
            Messages = new ObservableCollection<ChatMessage>();


            var _message = new ChatMessage
            {
                Username = "Hello, This is init message!",
                Message = "",
                ColorValue = "#0000FF"
            };
            Messages.Insert(0, _message);

            /*
            if (settings == null)
            {
                settings = new SettingsWindow();
                settings.Show();
            }
            else
            {
                settings.Activate();
            }
            */

            TwitchBot bot = new TwitchBot();
            bot.client.OnLog += Client_OnLog;
            bot.client.OnJoinedChannel += Client_OnJoinedChannel;
            bot.client.OnMessageReceived += Client_OnMessageReceived;
            bot.client.OnConnected += Client_OnConnected;
            bot.Connect();
        }

        static void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            //if (e.ChatMessage.Message.Contains("badword"))
            //client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
            var _message = new ChatMessage
            {
                Username = e.ChatMessage.DisplayName + ": ",
                Message = e.ChatMessage.Message,
                ColorValue = e.ChatMessage.ColorHex
            };

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                //Delete last message
                if (Messages.Count > messagesHistory)
                    Messages.RemoveAt(Messages.Count - 1);
                //Add new
                Messages.Insert(0, _message);
            });
        }

        static void Client_OnLog(object sender, OnLogArgs e)
        {
            //Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");

        }

        static void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            //Console.WriteLine($"Connected to {e.AutoJoinChannel}");
            var _message = new ChatMessage
            {
                Username = "Succesfully connected to Twitch!",
                Message = "",
                ColorValue = "#00FF00"
            };

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                Messages.Insert(0, _message);
            });
        }

        static void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            //Console.WriteLine("Hey guys! I am a bot connected via TwitchLib!");
            //client.SendMessage(e.Channel, "Hey guys! I am a bot connected via TwitchLib!");
        }

        void SaveSettings()
        {
            /*
            if (settings == null)
            {
                settings.Close();
            }
            */
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
