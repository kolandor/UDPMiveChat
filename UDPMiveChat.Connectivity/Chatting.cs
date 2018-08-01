using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDPMiveChat.Models;
using System.ComponentModel;

namespace UDPMiveChat.Connectivity
{
    public class Chatting : IDisposable
    {
        private Action<Message> receiveCallbackAction;
        private IPAddress multicastIp = IPAddress.Parse("239.255.255.255");
        private int applicationPort = 8001;
        private UdpClient chatClient;
        private Thread receiveThread;

        public Chatting(Action<Message> callback)
        {
            receiveCallbackAction = callback;
        }

        /// <summary>
        /// Message starting
        /// </summary>
        public void StartMessaging()
        {
            try
            {
                if (chatClient != null)
                    return;
                var host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress hostIp = null;

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        hostIp = ip;
                    }
                }

                if (hostIp == null)
                    throw new Exception("The IP address hasn't been found");

                IPEndPoint endPoint = new IPEndPoint(hostIp, applicationPort);
                chatClient = new UdpClient(endPoint);
                chatClient.JoinMulticastGroup(multicastIp);

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.Start(chatClient);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function provides the messaging abbility
        /// </summary>
        /// <param name="message">The "message parameter is an object of the class Message which contains
        /// message and nickname"</param>
        public void SendMessage(Message message)
        {
            try
            {
                UdpClient udp = new UdpClient();
                string resultMessage = JsonConvert.SerializeObject(message);
                byte[] data = Encoding.Unicode.GetBytes(resultMessage);
                udp.Send(data, data.Length, multicastIp.ToString(), applicationPort); // Sending the message
                udp.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function is working on receiving messages
        /// </summary>
        /// <param name="client">It accepts udp client to work with as a parameter</param>
        private void ReceiveMessages(object client)
        {
            try
            {
                while (true)
                {
                    var udpClient = (UdpClient)client;

                    var ipEndPoint = new IPEndPoint(IPAddress.Any, applicationPort);


                    byte[] data = udpClient.Receive(ref ipEndPoint);

                    string receivedString = Encoding.Unicode.GetString(data);

                    Message receivedMessge = JsonConvert.DeserializeObject<Message>(receivedString);

                    receiveCallbackAction.Invoke(receivedMessge);
                }
            }
            catch (ObjectDisposedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Stopps communication with other connected users
        /// </summary>
        /// <param name="lastMessage">Last message will be sent before disconnecting. By default it is set to null. If final message is empty it won't be sent</param>
        public void StopMessaging(Message lastMessage = null)
        {
            if (chatClient != null)
            {
                if (lastMessage != null)
                {
                    SendMessage(lastMessage);
                }

                chatClient.DropMulticastGroup(multicastIp);

                receiveThread.Abort();
                chatClient.Close();

                receiveThread = null;
                chatClient = null;
            }
        }

        public void Dispose()
        {
            StopMessaging();
        }
    }
}