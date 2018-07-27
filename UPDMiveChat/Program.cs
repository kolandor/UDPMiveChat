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
            string message = null;
            Console.Write("Input IP address: ");
            string address = Console.ReadLine();
            Console.Write("Input removed port: ");
            int rmPort = System.Convert.ToInt32(Console.ReadLine());
            Console.Write("Input local port: ");
            int lcPort = System.Convert.ToInt32(Console.ReadLine());

            Chatting chat = new Chatting();
            chat.StartChatting(address, rmPort, lcPort);

            new Thread(() =>
            {
                while (true)
                {
                    message = Console.ReadLine();
                    chat.SendMessage(message);
                }
            }).Start();
            new Thread(() =>
            {
                while (true)
                {
                    if (!chat.IsMessageEmpty)
                        Console.WriteLine("New message: " + chat.PopMessage());
                }
            }).Start();
        }
    }
}
