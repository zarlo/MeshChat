using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat.Win
{
    public partial class ChatForm : Form
    {
        public ChatForm()
        {
            InitializeComponent();
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            Globals.NW = new NetWork(Globals.Name);
            Globals.NW.Receivemsg += Receivemsg;
        }

        public void Receivemsg(Common.Packet MSG)
        {

        }

    }
}
