using System;
using System.Collections.Generic;

namespace Chat.Win
{

    public class ConnectTCP
    {

        public TCP.Client TC;

        public string Name { get; private set; }

        public ConnectTCP(string name, string ip)
        {

            Name = name;
            TC = new TCP.Client(ip);

        }

    }

    public class NetWork
    {
        private static Boolean isTCP = false;
        private static List<ConnectTCP> Servers = new List<ConnectTCP>();

        public delegate void ReceiveMSG(Common.Packet MSG);
        public event ReceiveMSG Receivemsg;

        public string Name { get; private set; }

        private UDP udp;

        public NetWork(string name)
        {
            udp = new UDP();
            Name = name;
            Common.Packet Ping = new Common.Packet();
            Ping.ChatDataIdentifier = Common.DataIdentifier.Command;
            Ping.ChatMessage = "PING";
            Ping.ChatName = name;

            udp.Send(Ping);

            Ping.ChatDataIdentifier = Common.DataIdentifier.LogIn;
            Ping.ChatMessage = "";
            Ping.ChatName = name;

            Send(Ping);

        }

        public void Send(Common.Packet MSG)
        {
            MSG.ChatName = Name;
            if (isTCP)
            {
                for (int i = 0; i <= Servers.Count - 1; i++)
                {
                    Servers[i].TC.Send(MSG);
                }
            }
            udp.Send(MSG);
        }

        public void add(ConnectTCP conn)
        {
            isTCP = true;
            Servers.Add(conn);
        }

        public void AutoJoin()
        {



        }

        public Boolean hasReceive(string ID)
        {

            return false;

        }

        public void Receive(Common.Packet Packet)
        {
            if (!hasReceive(Packet.MessageID))
            {
                if (Packet.ChatDataIdentifier == Common.DataIdentifier.Command)
                {
                    
                }
                else
                {
                    Receivemsg(Packet);
                }
            }
        }
    }
}
