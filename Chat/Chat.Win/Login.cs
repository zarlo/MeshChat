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



namespace Chat.Win
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


            responseFromServer = responseFromServer.Remove(0, 10);
            responseFromServer = responseFromServer.Remove(responseFromServer.Length - 2,2);
            MessageBox.Show(responseFromServer);
            request = WebRequest.Create("http://punksky.xyz/games/user/login.json");

            string Data = "{\"username\":\"" + textBox_UserName.Text + "\",\"password\":\"" + textBox_Password.Text + "\"}";
            MessageBox.Show(Data);
            request.Method = "POST";
             byteArray = Encoding.UTF8.GetBytes(Data);
            request.Headers.Add("X-CSRF-Token", responseFromServer);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
             dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
             response = request.GetResponse();
            dataStream = response.GetResponseStream();
             reader = new StreamReader(dataStream);
             responseFromServer = reader.ReadToEnd();
            MessageBox.Show(responseFromServer);
            reader.Close();
            dataStream.Close();
            response.Close();

        }
    }
}
