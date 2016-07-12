using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Chat.Droid
{
    public static class NetWorking
    {
        public static UdpClient ClientUdp;
        public static readonly UdpClient ServerUdp;
        public static readonly int Port = 5637;
        public static void Init()
        {
            UDPSend("Command:PING");


        }


        public static void UDPSend(string message)
        {
            ClientUdp = new UdpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Any,Port);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            ClientUdp.Send(bytes, bytes.Length, ip);
            ClientUdp.Close();
        }

    }
}