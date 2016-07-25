using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Win
{
    public class Boot
    {
        public static Thread UDPThread;
       
        public static void PreInit()
        {
            UDPThread = new Thread(new ThreadStart(UDP.Receive));
            UDPThread.Start();
            
        }
        
        public static void Init()
        {
            Common.Packet Ping = new Common.Packet();
            Ping.ChatDataIdentifier = Common.DataIdentifier.Message;
            Ping.ChatMessage = "PING";
            Ping.ChatName = "{}";
            
            UDP.Send(Ping);
        }

        public static void PostInit()
        {

            

        }
        

    }
}
