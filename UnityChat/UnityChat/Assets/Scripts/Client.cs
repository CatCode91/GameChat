using ClientLibrary;
using ClientLibrary.Abstractions;
using ClientLibrary.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Client : MonoBehaviour
    {
        private ChatClient _client = new ChatClient();
        private IConnection _host;

        public event Action<ServerStatus> ServerStatusChanged;
        public event Action<IMessage> NewMessageCame;

        public void Start()
        {


            //ChatClient chat = new ChatClient();
           // chat.ConnectToServer(_host);




        }

        public void EntryInChat()
        {
            _client.ConnectToServer(_host);


        }



        public void Disconnect()
        {
            _client.Disconnect();
        }

        public new void SendMessage(string text)
        {
            _client.SendMessage(text);   
        }

       
    }
}
