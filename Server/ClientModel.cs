using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class ClientModel
    {
        private Socket _socket;
        private NetworkStream _stream;

        public event Action<ClientModel,string> MessageRecived;

        public int Name { get; private set; }

        public NetworkStatus Status 
        { 
            get 
            {
                if (_socket == null) 
                {
                    return NetworkStatus.Offline;
                }

                if ((_socket.Poll(1000, SelectMode.SelectRead) && (_socket.Available == 0)))
                {
                    return NetworkStatus.Offline;
                }

                else
                {
                   return NetworkStatus.Online;
                }
            }
        }

        public ClientModel(Socket socket)
        {
            _socket = socket;
            _stream = new NetworkStream(_socket);
            Name = socket.GetHashCode();
            //StartReceive();
        }

        private void StartReceive()
        {
            while (_socket.Connected & _stream != null)
            {
                byte[] buffer = new byte[2];
                int readBytes = _stream.Read(buffer, 0, 2);
                if (readBytes == 0)
                    break;

                int messageSize = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[messageSize];
                readBytes = _stream.Read(buffer, 0, messageSize);
                if (readBytes == 0)
                    break;

                string mesageText = Encoding.UTF8.GetString(buffer);
                MessageRecived?.Invoke(this, mesageText);
            }
        }

        public void SendMessage(string text) { }
    }
}
