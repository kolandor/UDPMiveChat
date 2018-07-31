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

            // Thread sendingThread = new Thread(() =>
            //{
            //loginThread.Wait();
            string messageToSend = null;

            while (true)
            {
                //if(messageToSend != null)
                //Console.WriteLine("Enter new message here:");
                messageToSend = Console.ReadLine();
                message.Text = messageToSend;
                message.Nickname = nickName;
                if (!string.IsNullOrEmpty(messageToSend))
                    chat.SendMessage(message);
                //messageToSend = null;
            }
            //});
            //sendingThread.Start();
            /*while (true)
            {
                if (!chat.IsMessageEmpty)
                    Console.WriteLine(chat.PopMessage());
            }*/

        }
    }
}
