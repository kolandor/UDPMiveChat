using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UPDMiveChat
{
    public class Chatting
    {
        static string remoteAddress; // Sending data host
        static int remotePort; // Sending data port
        static int localPort; // The local port for message listening

        public static void StartChatting()
        {
            try
            {
                Console.Write("Input the listening port: "); // Local port
                localPort = Int32.Parse(Console.ReadLine());
                Console.Write("Input the connection adress: ");
                remoteAddress = Console.ReadLine(); // Connection address
                Console.Write("Input the connection port: ");
                remotePort = Int32.Parse(Console.ReadLine()); // Connection port

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendMessage()
        {
            UdpClient sender = new UdpClient(); // UdpClient cretion
            try
            {
                while (true)
                {
                    string message = Console.ReadLine(); // The message reding
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    sender.Send(data, data.Length, remoteAddress, remotePort); // Sending the message
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }
        public static void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient for receiving data
            IPEndPoint remoteIp = null; // Incoming connection address
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // Receiving data
                    string message = Encoding.Unicode.GetString(data);
                    Console.WriteLine("Message: {0}", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                receiver.Close();
            }
        }
    }
}