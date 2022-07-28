using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransparentOnTopChat
{
    public class ChatMessage
    {
        private string username;
        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set 
            { 
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private string colorValue;
        public string ColorValue
        {
            get { return colorValue; }
            set 
            { 
                colorValue = value;
                OnPropertyChanged("ColorValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
