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

namespace UDPMiveChat
{
    public class MainWindowViewModel : DependencyObject
    {
        private IList<Message> message;

        public MainWindowViewModel()
        {
            message = new List<Message>();
            MessagesCollect = CollectionViewSource.GetDefaultView(message);
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

        public ICommand AddMessageCommand => new CommandExecutor(() => { message.Add(new Message { Nickname = this.Nickname, Messages = this.Messages }); MessagesCollect.Refresh(); });

        public ICommand SetUserNameCommand => new CommandExecutor(() => { Nickname = string.Concat(UserName, ":"); IsLogged = !string.IsNullOrEmpty(Nickname); });
    }
}
