using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UDPMiveChat.Models;

namespace UDPMiveChat.Connectivity
{
    public class Chatting
    {
        private Action<Message> receiveCallbackAction;
        IPAddress brodcastIp = IPAddress.Broadcast;
        int applicationPort = 8001;

        /*string remoteAddress = "235.5.5.1";//  IPAddress.Any.ToString();// IPAddress.Broadcast.ToString();//"235.5.5.1"; // Sending data host
        int remotePort = 8001; // Sending data port
        int localPort = 8001; // The local port for message listening
        string userName = null;
        string message = null;
        const int TTL = 20;
        IPAddress groupAddress;
        UdpClient client;*/
        public Chatting(Action<Message> callback)
        {
            receiveCallbackAction = callback;
        }
        public void StartMessaging(string name)
        {
            try
            {
                userName = name;
                client = new UdpClient(localPort);
                client.JoinMulticastGroup(groupAddress, TTL);
                string firstMessage = userName + " has entered the chat";
                byte[] data = Encoding.Unicode.GetBytes(firstMessage);
                client.Send(data, data.Length, remoteAddress, remotePort);

                //Thread receiveThread = new Thread(ReceiveMessages);
               // receiveThread.Start();
            }
            catch(Exception ex)
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
                udp.Send(data, data.Length, brodcastIp.ToString(), applicationPort); // Sending the message
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
                    var ipEndPoint = new IPEndPoint(brodcastIp, applicationPort);
                    byte[] data = udpClient.Receive(ref ipEndPoint);

                    string receivedString = Encoding.Unicode.GetString(data);

                    Message receivedMessge = JsonConvert.DeserializeObject<Message>(receivedString);

                    receiveCallbackAction(receivedMessge);
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
            finally
            {
                client.Close();
            }
        }







        /*public bool IsMessageEmpty { get { return string.IsNullOrEmpty(message); } }
        public string PopMessage()
        {
            string returnMessage = message;
            message = null;
            return returnMessage;
        }
        public void ExitChat()
        {
            try
            {
                string lastMessage = userName + "has left the chat";
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, remoteAddress, remotePort);
                client.DropMulticastGroup(groupAddress);
                alive = false;
                client.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Close()
        {
            try
            {
                if (alive)
                    ExitChat();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
    }
}