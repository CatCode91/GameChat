using Assets.Scripts.Models;
using ClientLibrary;
using ClientLibrary.Abstractions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ChatWorker : MonoBehaviour
    {
        private bool _isConnected = false;
        private ChatClient _client = new ChatClient();
        private TMP_Text _sendingText;
        private GameObject _chatMainWindow;
        private GameObject _messageObject;
        private MessageItem _message;

        private void Start()
        {
            _client.NewMessage += _client_NewMessage;
            _chatMainWindow = GameObject.FindGameObjectWithTag("Chat");
            _sendingText = GameObject.FindGameObjectWithTag("InputMessageField").GetComponent<TMP_Text>();
        }

        private void _client_NewMessage(IMessage obj)
        {
            IMessageChat msg = new MessageChat();
            msg.Caption = $"Сообщение от:{obj.Login}";
            msg.Text = obj.Text;

            PlaceMessageInChat(msg);
        }

        public void Disconnect()
        {
            _client.Disconnect();
            _isConnected = false;
        }

        public void SendChatMessage()
        {
            //создаем модельку сообщения, чтоб поместить ее в MessageItem
            MessageChat message = new MessageChat();
            message.Caption = "Вы:";
            message.Text = _sendingText.text;

            PlaceMessageInChat(message);
            _client.SendMessage(message.Text);

            _sendingText.SetText(String.Empty);
        }

        internal void ConnectToServer(IConnection conn)
        {
            if (_isConnected) 
            {
                throw new Exception("Клиент уже подключен к серверу!");
            }

            _client.ConnectToServer(conn);
        }

        private void PlaceMessageInChat(IMessageChat message) 
        {
            //инстансим новый итем и помещаем в него модельку чтоб заполнить поля сообщения
            _messageObject = (GameObject)Instantiate(Resources.Load("MessageItem"), _chatMainWindow.transform);
            _message = _messageObject.GetComponent<MessageItem>();
            _message.FillFields(message);

        }
    }
}
