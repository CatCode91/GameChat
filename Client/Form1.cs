using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private const string _adress = "127.0.0.1";
        private const int _port = 8050;
        private Socket _socket;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void SendMessage()
        {
          
        }
        // получение сообщений
        public void ReceiveMessage()
        {
           
        }

        public void Disconnect()
        {
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Parse(_adress), _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(localIPEndPoint);
           // byte[] msg = Encoding.UTF8.GetBytes("Мяу!");
           // server.Send(msg);
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            _socket.Close();
        }

    }
}
