using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPMiveChat.Connectivity;
using UDPMiveChat.Models;

namespace UPDMiveChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Chatting chat = new Chatting((receivedMessage) =>
            {
                Console.WriteLine("{0}: {1}", receivedMessage.Nickname, receivedMessage.Text);
            });

            Console.Write("Input your nickname: ");
            string nickName = Console.ReadLine();
            chat.StartMessaging();
            Console.WriteLine("Welcome to MiveChat! Say hello to your new friends!");

            Message message = new Message();

            string messageToSend = null;

            while (true)
            {
                messageToSend = Console.ReadLine();

                message.Text = messageToSend;
                message.Nickname = nickName;

                // Checking semding message for whitespaces and NULL. If messagecontains only spoaces it also won't be sent
                if (!string.IsNullOrEmpty(messageToSend) && !string.IsNullOrEmpty(messageToSend.Trim()))
                    chat.SendMessage(message);
            }
        }
    }
}
