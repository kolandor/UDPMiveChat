using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using UDPMiveChat;
using UDPMiveChat.Connectivity;
using UDPMiveChat.Models;

namespace UDPMiveChat
{
    public class MainWindowViewModel : DependencyObject
    {
        private IList<Message> message;
        private Chatting chatting;

        public MainWindowViewModel()
        {
            message = new List<Message>();
            MessagesCollect = CollectionViewSource.GetDefaultView(message);

            chatting = new Chatting(OnReceiveMessage);
        }
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty)); 

        public string Nickname
        {
            get { return (string)GetValue(NicknameProperty); }
            set { SetValue(NicknameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Nickname.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NicknameProperty =
            DependencyProperty.Register("Nickname", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));


        public string Messages
        {
            get { return (string)GetValue(MessagesProperty); }
            set { SetValue(MessagesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessagesProperty =
            DependencyProperty.Register("Messages", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));

        public ICollectionView MessagesCollect
        {
            get { return (ICollectionView)GetValue(MessagesCollectProperty); }
            set { SetValue(MessagesCollectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessagesCollectProperty =
            DependencyProperty.Register("MessagesCollect", typeof(ICollectionView), typeof(MainWindowViewModel), new PropertyMetadata(null));

        public bool IsLogged
        {
            get { return (bool)GetValue(IsLoggedProperty); }
            set { SetValue(IsLoggedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLogged.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoggedProperty =
            DependencyProperty.Register("IsLogged", typeof(bool), typeof(MainWindowViewModel), new PropertyMetadata(false));

        public ICommand SendMessageCommand => new CommandExecutor(() => { chatting.SendMessage(new Message { Nickname = this.Nickname, Text = this.Messages }); });

        public ICommand LoginCommand => new CommandExecutor(OnLogin);

        public ICommand LogoutCommand => new CommandExecutor(OnLogout);

        private void OnLogin()
        {
            if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(UserName.Trim()))
            {
                return;
            }

            if (chatting != null)
            {
                chatting.StopMessaging();
            }

            Nickname = string.Concat(UserName, ":");
            IsLogged = true;

            chatting.StartMessaging();
        }

        private void OnLogout()
        {
            chatting.StopMessaging();

            IsLogged = false;
        }

        private void OnReceiveMessage(Message receivedMessage)
        {
            // Adding received message to the source list of messages
            message.Add(receivedMessage);

            // Refreshing a collection to update view via bindings
            MessagesCollect.Refresh();
        }
    }
}
