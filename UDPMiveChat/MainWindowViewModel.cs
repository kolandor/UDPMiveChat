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
        public static MainWindowViewModel Instance { get { return new MainWindowViewModel(); } }



        public string Login
        {
            get { return (string)GetValue(LoginProperty); }
            set { SetValue(LoginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Login.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginProperty =
            DependencyProperty.Register("Login", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(string.Empty));



        public string LoginLabel
        {
            get { return (string)GetValue(LoginLabelProperty); }
            set { SetValue(LoginLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoginLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginLabelProperty =
            DependencyProperty.Register("LoginLabel", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata("Your nickname"));

        //public ICommand LoginCommand => 

    }
}
