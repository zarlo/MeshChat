using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Chat.Win
{

    public class ConnectTCP
    {

        public TCP.Client TC;

        public string Name;

        public ConnectTCP(string name, string ip)
        {

            Name = name;
            TC = new TCP.Client(ip);

        }

    }

    public class NetWork
    {
        private static int iServers = 0;
        private static int iServersLimit = 3;
        private static Boolean isTCP = false;
        private static ConnectTCP[] Servers = new ConnectTCP[iServersLimit];



        public static void Send(Common.Packet MSG)
        {
            if (isTCP)
            {
                for (int i = 0; i <= iServers - 1; i++)
                {
                    Servers[i].TC.Send(MSG);
                }
            }
            UDP.Send(MSG);

        }

        private static void OverFlow()
        {

            ConnectTCP[] temp = new ConnectTCP[iServersLimit];

            for (int i = 0; i <= iServersLimit - 1; i++)
            {
                temp[i] = Servers[i];

            }

            Servers = new ConnectTCP[iServersLimit + 5];
            iServers = 0;

            for (int i = 0; i <= iServersLimit - 1; i++)
            {
                Servers[i] = temp[i];
            }

            iServers = iServersLimit - 1;
            iServersLimit = iServersLimit + 5;
        }

        public static void add(ConnectTCP conn)
        {
            isTCP = true;
            if (iServers != iServersLimit)
            {
                Servers[iServers] = conn;
            }
            else
            {
                OverFlow();
                Servers[iServers] = conn;
            }
            iServers++;
        }

        public static void AutoJoin()
        {


        }

        public static Boolean hasReceive(string ID)
        {

            return false;

        }

        public static void Receive(Common.Packet Packet)
        {
            if (!hasReceive(Packet.MessageID))
            {
                if (Packet.ChatDataIdentifier == Common.DataIdentifier.Command)
                {
                    if (Packet.ChatMessage.StartsWith("PONG"))
                    {

                    }
                }
                else
                {
                    ChatOutput(Packet);
                }
            }
        }
        public static void ChatOutput(Common.Packet Packet)
        {
            
            try
            {   
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


    }
}
