using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDPMiveChat.Connectivity
{
    public class Chatting
    {
        string remoteAddress; // Sending data host
        int remotePort; // Sending data port
        int localPort; // The local port for message listening
        string message = null;
        public void StartChatting(string address, int rmPort, int lcPort)
        {
            try
            {

                localPort = lcPort;

                remoteAddress = address; // Connection address

                remotePort = rmPort; // Connection port

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }

        public void SendMessage(string message)
        {
            UdpClient sender = new UdpClient(); // UdpClient cretion
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                sender.Send(data, data.Length, remoteAddress, remotePort); // Sending the message
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sender.Close();
            }
        }
        public void ReceiveMessage()
        {
            UdpClient receiver = new UdpClient(localPort); // UdpClient for receiving data
            IPEndPoint remoteIp = null; // Incoming connection address
            try
            {
                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp); // Receiving data
                    message = Encoding.Unicode.GetString(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                receiver.Close();
            }
        }

        public bool IsMessageEmpty { get { return string.IsNullOrEmpty(message); } }

        public string PopMessage()
        {
            string returnMessage = message;
            message = null;
            return returnMessage;
        }
    }
}