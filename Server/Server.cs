using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        //хранит в себе список подключенных клиентов
        private List<ClientModel> _clients = new List<ClientModel>();

        //таймер, каждых пару сек проверяет, не ушел ли какой клиент с сервера.
        private Timer _timer;

        //основной слушающий сокет сервера
        private Socket _socket;

        //токен для отмены таски слушания
        private CancellationTokenSource _cts;

        //перечисление хранящее статус сервера, юзается в методах получения статуса сервера.
        private NetworkStatus _serverStatus = NetworkStatus.Offline;


        //если что-то интересное происходит в методах, уведомляет подписчиков
        public event Action<string> UsefulMessages;

        //метод таймера, проверяет клиентов которые онлайн
        public void CheckOnlineUsers(Object stateInfo)
        {
            //проверяем активных клиентов, дропаем неактивных
            for (int i = _clients.Count - 1; i >= 0; i--)
            {
                if (_clients[i].Status == NetworkStatus.Offline)
                {
                    ClientDisconnected(_clients[i]);
                }
            }
        }

        //метод запуска сервера
        public void Run()
        {
            //тянем настройки (валидации выполнены в модели SettingsModel)
            string ipAdress = SettingsManager.LoadSettings().IPadress;
            int port = SettingsManager.LoadSettings().Port;

            //конфигурируем таймер
            var autoEvent = new AutoResetEvent(false);
            _timer = new Timer(CheckOnlineUsers, autoEvent, 3000, 3000);

            ServerEventHappend($"Запускаем сервер {ipAdress}:{port}", MessageStatus.OK);
  
            if (_socket == null)
            {
                    _cts = new CancellationTokenSource();
                    Task.Run(() =>StartListen(ipAdress, port, _cts.Token));
            }

            else 
            {
                ServerEventHappend("Ошибка! Попытка повторного запуска сервера!", MessageStatus.Error);
            }
          
        }

        //метод остановки сервера
        public void Stop()
        {
                _socket.Close();
                _cts.Cancel();
        }

        //оповещает о состоянии сервера
        public void ShowStatus()
        {
            ServerEventHappend("Запрос статуса сервера....", MessageStatus.OK);

            if (_socket != null)
            {
                if (_socket.Available == 0)
                {
                    ServerEventHappend($"Сервер {_socket.LocalEndPoint} - {_serverStatus}. Количество подключенных клиентов: {_clients.Count}", MessageStatus.OK);
                }
            }

            else
            {
                ServerEventHappend($"Сервер {_serverStatus}", MessageStatus.OK);
            }
        }

        //метод таски, запускающейся из метода Run
        private void StartListen(string ipAdress, int port,CancellationToken cts)
        {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
                _socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.Bind(ipEndPoint);
                _socket.Listen(10);
                ServerEventHappend("Сервер запущен и начато прослушивание портов...", MessageStatus.OK);
                _serverStatus = NetworkStatus.Online;

            while (true)
            {
                _serverStatus = NetworkStatus.Online;

                if (cts.IsCancellationRequested)
                {
                    _serverStatus = NetworkStatus.Offline;
                    _socket = null;
                    ServerEventHappend("Сервер остановлен пользователем.", MessageStatus.Warning);
                    return;
                }

                ClientModel newClient;

                try
                {
                    newClient = new ClientModel(_socket.Accept());
                    ServerEventHappend($"Пользователь {newClient.Name} подключился к серверу!", MessageStatus.OK);
                    //когда достучался клиент, подписываемся на его события
                    //1 .Прием сообщений от клиента
                    newClient.MessageRecived += ClientMessageRecived;
                    _clients.Add(newClient);
                }

                catch (SocketException ex) 
                {
                    ServerEventHappend(ex.Message,MessageStatus.Warning);
                }   
            }
        }  

        private void ClientMessageRecived(ClientModel client,string message)
        {
            ServerEventHappend($"Новое сообщение от {client.Name}", MessageStatus.OK);
            ServerEventHappend(message,MessageStatus.OK);

            //рассылаем сообщение другим челам 
            foreach (ClientModel cl in _clients)
                if (client.Name != cl.Name)
                {
                    client.SendMessage(message);
                    return;
                }
        }

        private void ClientDisconnected(ClientModel client)
        {
            _clients.Remove(client);
            ServerEventHappend($"Пользователь {client.Name} отключился от сервера",MessageStatus.OK);
        }

        //сохраняет в лог и уведомляет подписчиков о событии
        private void ServerEventHappend(string text,MessageStatus status) 
        {
            LogManager.AddLog(text, MessageStatus.OK);
            UsefulMessages?.Invoke(text);
        }
    }
}
