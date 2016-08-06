using Chat.Common;
using System;
using System.Net;
using System.Net.Sockets;

namespace Chat.Win.TCP
{
    public class Client
    {

        private Socket clientSocket;

        private string name;

        private EndPoint epServer;

        private byte[] dataStream = new byte[1024];

        public Client(string ip )
        {
            Init(ip, 5899);
        }

        public Client(string ip, int port)
        {
            Init(ip, port);
        }

        private void Init(string ip, int port)
        {
            try
            {

                Packet sendData = new Packet();
                sendData.ChatName = this.name;
                sendData.ChatMessage = null;
                sendData.ChatDataIdentifier = DataIdentifier.LogIn;
                sendData.MessageID = Uilt.Randomgen(10);

                this.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress serverIP = IPAddress.Parse(ip.Trim());

                IPEndPoint server = new IPEndPoint(serverIP, port);

                epServer = (EndPoint)server;

                byte[] data = sendData.GetDataStream();

                clientSocket.BeginSendTo(data, 0, data.Length, SocketFlags.None, epServer, new AsyncCallback(SendData), null);

                this.dataStream = new byte[1024];

                clientSocket.BeginReceiveFrom(dataStream, 0, dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(ReceiveData), null);
            }
            catch (Exception ex)
            {
                //    MessageBox.Show("Connection Error: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void Send(Packet Message)
        {
            
            // Get packet as byte array
            byte[] byteData = Message.GetDataStream();

            // Send packet to the server
            clientSocket.BeginSendTo(byteData, 0, byteData.Length, SocketFlags.None, epServer, new AsyncCallback(this.SendData), null);
                        
        }

    

        private void SendData(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
             //   MessageBox.Show("Send Data: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);

                Packet receivedData = new Packet(dataStream);

                if (receivedData.ChatMessage != null)
                {
                    Globals.NW.Receive(receivedData);
                }

                // Reset data stream
                dataStream = new byte[1024];

                clientSocket.BeginReceiveFrom(dataStream, 0, dataStream.Length, SocketFlags.None, ref epServer, new AsyncCallback(ReceiveData), null);
            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
             //   MessageBox.Show("Receive Data: " + ex.Message, "UDP Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
