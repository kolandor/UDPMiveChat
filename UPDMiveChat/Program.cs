using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPMiveChat.Connectivity;

namespace UPDMiveChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Chatting chat = new Chatting();


                Console.Write("Input your nickname: ");
                string nickName = Console.ReadLine();
                chat.Login(nickName);
                Console.WriteLine("Welcome to MiveChat! Say hello to your new friends!");
            

            string message = null;

            Thread receiveMessageThread = new Thread(() => 
            {
                while(true)
                {
                    chat.ReceiveMessages();
                    if (!chat.IsMessageEmpty)
                        Console.WriteLine(chat.PopMessage());
                }               
            });

           // Thread sendingThread = new Thread(() =>
            //{
                //loginThread.Wait();
                while (true)
                {
                    Console.Write("Enter new message here:");
                    message = Console.ReadLine();
                    chat.SendMessage(message);
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
