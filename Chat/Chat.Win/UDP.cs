using System;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace Chat.Win
{
    public  class UDP
    {
        public static Thread UDPThread;
        public UDP()
        {

            UDPThread = new Thread(new ThreadStart(this.Receive));
            UDPThread.Start();

        }

        public void Send(Common.Packet MSG)
        {
            
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 5899);

            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            sock.EnableBroadcast = true;
            try
            {
                
                sock.SendTo(MSG.GetDataStream(), iep);

            }
            catch (SystemException ex) {

                MessageBox.Show(ex.ToString());

            }
                        
            sock.Close();

        }

        public void Receive()
        {
            while (true) {

                UdpClient receivingUdpClient = new UdpClient();

                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 5899);
                try
                {

                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                    
                    Globals.NW.Receive(new Common.Packet(receiveBytes));

                    receivingUdpClient.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
