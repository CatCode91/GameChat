using ClientLibrary.Abstractions;
using ClientLibrary.Enums;
using ClientLibrary.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientLibrary
{
    public class ChatClient
    {
        //сокет клиента
        private Socket _socket;
        //поток для прослушивания входящих сообщений
        private NetworkStream _stream;
        //токен для отмены таски прослушивания входящих сообщений
        private CancellationTokenSource _cts;

        public event Action<IMessage> NewMessage;

        public void SendMessage(string text)
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

                NewMessage?.Invoke(message);
            }
        }

        public void ConnectToServer(IConnection conn)
        {
            int port = int.Parse(conn.Port);

            IPEndPoint localIPEndPoint = new IPEndPoint(IPAddress.Parse(conn.IPadress),port);
            _cts = new CancellationTokenSource();

            if (_socket == null)
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
               
                try
                {
                    _socket.Connect(localIPEndPoint);
                }

                catch(Exception ex)
                {
                    _socket = null;
                    throw ex;
                }

                _stream = new NetworkStream(_socket);
                Task.Run(() => ReceiveMessage(_cts.Token));
            }
        }

    }
}
