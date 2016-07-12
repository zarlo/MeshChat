using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Chat.Common;

namespace Chat.Win.TCP
{
    public class Server
    {
        
        
        private ArrayList clientList;
        private Socket serverSocket;
        private byte[] dataStream = new byte[1024];
       
        

        private struct Client
        {
            public EndPoint endPoint;
            public string name;
        }

        public Server(int Port)
        {
            
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Tcp);
            
            IPEndPoint server = new IPEndPoint(IPAddress.Any, Port);

            serverSocket.Bind(server);

            IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);

            EndPoint epSender = (EndPoint)clients;

            serverSocket.BeginReceiveFrom(dataStream, 0, dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(ReceiveData), epSender);


        }


        private void ReceiveData(IAsyncResult asyncResult)
        {
            try
            {
                byte[] data;

                Packet receivedData = new Packet(dataStream);

                Packet sendData = new Packet();

                IPEndPoint clients = new IPEndPoint(IPAddress.Any, 0);

                EndPoint epSender = (EndPoint)clients;

                serverSocket.EndReceiveFrom(asyncResult, ref epSender);

                sendData = receivedData;

                switch (receivedData.ChatDataIdentifier)
                {
                    case DataIdentifier.Message:
                        //sendData.ChatMessage = string.Format("{0}: {1}", receivedData.ChatName, receivedData.ChatMessage);
                        break;

                    case DataIdentifier.LogIn:
                        Client client = new Client();
                        client.endPoint = epSender;
                        client.name = receivedData.ChatName;

                        this.clientList.Add(client);

                        sendData.ChatMessage = string.Format("-- {0} is online --", receivedData.ChatName);
                        break;

                    case DataIdentifier.LogOut:
                        foreach (Client c in this.clientList)
                        {
                            if (c.endPoint.Equals(epSender))
                            {
                                this.clientList.Remove(c);
                                break;
                            }
                        }

                        sendData.ChatMessage = string.Format("-- {0} has gone offline --", receivedData.ChatName);
                        break;
                }

                data = sendData.GetDataStream();

                foreach (Client client in this.clientList)
                {
                    if (client.endPoint != epSender || sendData.ChatDataIdentifier != DataIdentifier.Command)
                    {
                        serverSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, client.endPoint, new AsyncCallback(this.SendData), client.endPoint);
                    }
                    else
                    {
                        //{COMMAND}
                    }
                }

                serverSocket.BeginReceiveFrom(dataStream, 0, dataStream.Length, SocketFlags.None, ref epSender, new AsyncCallback(ReceiveData), epSender);
                       
            }
            catch (Exception ex)
            {
             //   MessageBox.Show("ReceiveData Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SendData(IAsyncResult asyncResult)
        {
            try
            {
                serverSocket.EndSend(asyncResult);
            }
            catch (Exception ex)
            {
             //   MessageBox.Show("SendData Error: " + ex.Message, "UDP Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
