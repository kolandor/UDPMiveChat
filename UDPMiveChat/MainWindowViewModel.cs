using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UDPMiveChat;

namespace UDPMiveChat
{
    public class MainWindowViewModel : DependencyObject
    {
        public string UserName { get; set; }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));

        public ICommand SetUserName => CommandExecutor.ExecuteFunction(ButtonLogin);

        public static MainWindowViewModel Instance { get { return new MainWindowViewModel(); } }

        public string UserNameTextBox
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        private async Task ButtonLogin()
        {
            UserName = UserNameTextBox;
        }

        public string ChatTextBox
        {
            get { return (string)GetValue(MessageTextBoxProperty); }
            set { SetValue(MessageTextBoxProperty, value); }
        }

        public static readonly DependencyProperty ChatTextBoxProperty =
            DependencyProperty.Register("ChatTextBox", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));

        public ICommand SendToChat => CommandExecutor.ExecuteFunction(ButtonSend);

        public string MessageTextBox
        {
            get { return (string)GetValue(MessageTextBoxProperty); }
            set { SetValue(MessageTextBoxProperty, value); }
        }

        public static readonly DependencyProperty MessageTextBoxProperty =
            DependencyProperty.Register("MessageTextBox", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));

        private async Task ButtonSend()
        {
            ChatTextBox += MessageTextBox + "\n";
        }
    }
}
