using ClientLibrary.Enums;
using System;

namespace ClientLibrary.Abstractions
{
    public interface IClient
    {
        /// <summary>
        /// Возникает, когда приходит новое сообщение
        /// </summary>
        event Action<IMessage> NewMessage;

       /// <summary>
       /// Подключиться к серверу
       /// </summary>
       /// <param name="host">IP-aдрес и порт подключения</param>
       void ConnectToServer(IConnection host);

        /// <summary>
        /// Отсылает сообщение в чат
        /// </summary>
        void SendMessage(string text);

        /// <summary>
        /// Отключиться от сервера
        /// </summary>
        void Disconnect();

    }
}
