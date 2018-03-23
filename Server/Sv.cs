using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class Sv : Form
    {
        Socket server;
        Socket client;
        IPEndPoint ipServer;
        public Sv()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipServer = new IPEndPoint(IPAddress.Any, 9050);
            server.Bind(ipServer);
            server.Listen(5);
            client = server.Accept();
            textBox1.Text = (client.RemoteEndPoint).ToString();
            byte[] data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add("Client: " + Encoding.ASCII.GetString(data));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            listBox1.Items.Add("Server: " + text);
            textBox2.Text = "";
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(text);
            client.Send(data);
            data = new byte[1024];
            client.Receive(data);
            listBox1.Items.Add("Client: " + Encoding.ASCII.GetString(data));
        }
    }
}
