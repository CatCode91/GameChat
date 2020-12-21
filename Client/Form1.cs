using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private const string _adress = "127.0.0.1";
        private const int _port = 8050;
        private Socket _socket;
        private NetworkStream _stream;
        private delegate void InvokeDelegate();
        private CancellationTokenSource _cts;

        public Form1()
        {
            InitializeComponent();
        }

        // получение сообщений
        public void ReceiveMessage(CancellationToken ct)
        {
            IMessage message = new Message();
            while (true)
            {
                //буфер для размера сообщения
                byte[] buffer = new byte[2];
                //читаем входящий поток
                int readBytes = _stream.Read(buffer, 0, 2);
                //если посылка пуста - выходим
                if (readBytes == 0)
                    break;

                //запоминаем сколько весит сообщение
                message.Size = BitConverter.ToInt16(buffer, 0);

                //буфер для логина приславшего
                buffer = new byte[4];
                //читаем входящий поток
                readBytes = _stream.Read(buffer, 0, 4);
                //если посылка пуста - выходим
                if (readBytes == 0)
                    break;
                //запоминаем сколько весит сообщение
                message.Login = BitConverter.ToInt32(buffer, 0);


                //буфер для логина приславшего
                buffer = new byte[message.Size];
                //читаем входящий поток
                readBytes = _stream.Read(buffer, 0, message.Size);
                //если посылка пуста - выходим
                if (readBytes == 0)
                    break;
                //запоминаем сколько весит сообщение
                message.Text = Encoding.UTF8.GetString(buffer);

                MessageBox.Show($"Получено от {message.Login} {message.Text}");
            }
        }


        private void btn_connect_Click(object sender, EventArgs e)
        {
            ConnectToServer();
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendMessage(textBox1.Text);
        }

        private void ConnectToServer()
        {
            IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Parse(_adress), _port);
            _cts = new CancellationTokenSource();

            if (_socket == null)
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(localIPEndPoint);
                _stream = new NetworkStream(_socket);
                this.Text = _socket.GetHashCode().ToString();
                Task.Run(() => ReceiveMessage(_cts.Token));
            }
        }

        private void SendMessage(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return;
            }

            if (_socket != null)
            {
                //кодируем текст в байтовый массив
                byte[] message = Encoding.UTF8.GetBytes(text);
                //кодируем размер байтового массива сообщения приведенный к Int16(2 байта по условию) 
                byte[] messageSize = BitConverter.GetBytes((Int16)message.Length);
                //посылаем размер сообщения
                _socket.Send(messageSize, 2, SocketFlags.None);
                _socket.Send(message);
            }
        }

        public void Disconnect()
        {
            _cts.Cancel();
            _socket?.Close();
            _socket = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
