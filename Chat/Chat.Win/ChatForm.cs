using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Common;
namespace Chat.Win
{
    public partial class ChatForm : Form
    {
        
        public ChatForm()
        {
            InitializeComponent();

        }

        public void AppendOutPut(string text)
        {

            Chat_TextBox.AppendText(text);

        }
        

        private void button_Send_Click(object sender, EventArgs e)
        {
            Packet PACK = new Packet();
            PACK.ChatDataIdentifier = DataIdentifier.Message;
            PACK.ChatMessage = textBox1.Text;
            PACK.MessageID = Uilt.Randomgen(10);
            NetWork.Send(PACK);
            textBox1.Text = "";
            NetWork.ChatOutput(PACK);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxFormat_Tick(object sender, EventArgs e)
        {



        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Boot.UDPThread.Abort();
            Application.Exit();

        }
    }
}
