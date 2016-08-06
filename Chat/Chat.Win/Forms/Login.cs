using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Chat.Win.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            //ChatForm CF = new ChatForm();
            //CF.Show();

            WebRequest request = WebRequest.Create("http://punksky.xyz/games/user/token.json");

            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes("");
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            string temp = responseFromServer;
            responseFromServer = responseFromServer.Remove(0, 10);
            responseFromServer = responseFromServer.Remove(responseFromServer.Length - 2,2);
            //MessageBox.Show(temp + Environment.NewLine + responseFromServer);
            request = WebRequest.Create("http://punksky.xyz/games/user/login.json");

            string Data = "{\"username\":\"" + textBox_UserName.Text + "\",\"password\":\"" + textBox_Password.Text + "\"}";
            //MessageBox.Show(Data);
            request.Method = "POST";
             byteArray = Encoding.UTF8.GetBytes(Data);
            request.Headers.Add("X-CSRF-Token", responseFromServer);
            
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
             dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                Globals.Chatform = new ChatForm();
                response = request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                Globals.Name = textBox_UserName.Text;
                Globals.isLogedin = true;
                Globals.Chatform.Show();
                this.Hide();
            } catch
            {
                MessageBox.Show("failed to login");
            }
                reader.Close();
            dataStream.Close();
            response.Close();

        }
    }
}
