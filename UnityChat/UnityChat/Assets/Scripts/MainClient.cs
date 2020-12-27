using Assets.Scripts.Models;
using ClientLibrary;
using ClientLibrary.Abstractions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//для работы с чатом

namespace Assets.Scripts
{
    public class MainClient : MonoBehaviour
    {
       //тригер, для отрисовки префаба в методе Update, т.к не иснасит из подписанного метода (вызов не из главного потока)
        private bool _isNewMessageCame = false;
        private IMessage _newMessage;

        private ChatClient _client;
        private TMP_Text _sendingText;
        private GameObject _chatMainWindow;
        private GameObject _messageTemp;
        private MessageItem _message;
        private ScrollRect _scrollView;
        private AudioSource _audioListener;

        private void Start()
        {
            _client = new ChatClient();
            _client.NewMessage += _client_NewMessage;
            _chatMainWindow = GameObject.FindGameObjectWithTag("Chat");
            _sendingText = GameObject.FindGameObjectWithTag("InputMessageField").GetComponent<TMP_Text>();
            _scrollView = _chatMainWindow.GetComponentInParent<ScrollRect>();
            _messageTemp = (GameObject)Resources.Load("MessageItem");
            _audioListener = GetComponent<AudioSource>();
            
        }

        private void Update()
        {
            if (_isNewMessageCame) 
            {
                MessageChat msg = new MessageChat();
                msg.Caption = $"Сообщение от: {_newMessage.Login}";
                msg.Text = _newMessage.Text;
                msg.Color = new Color32(213, 236, 255, 253);
                PlaceMessageInChat(msg);
                _isNewMessageCame = false;
                _audioListener.Play();
            }
        }

        //если пришло новое сообщение (подписка на событие либы)
        private void _client_NewMessage(IMessage obj)
        {
            _newMessage = obj;
            _isNewMessageCame = true;
        }

        public void Disconnect()
        {
            _client.Disconnect();
        }

        //отсылает сообщение
        public void SendChatMessage()
        {
            //создаем модельку сообщения, чтоб поместить ее в MessageItem
            MessageChat message = new MessageChat();
            message.Caption = "Вы: ";
            message.Text = _sendingText.text;
            message.Color = new Color32(255, 213, 232, 253);
            PlaceMessageInChat(message);
          
            _client.SendMessage(_sendingText.text);

            _sendingText.SetText("");

        }

        internal void ConnectToServer(IConnection conn)
        {
            _client.ConnectToServer(conn);
        }

        //инстансит объект сообщения в окошке чата
        private void PlaceMessageInChat(IMessageChat message) 
        {
            //инстансим новый итем и помещаем в него модельку чтоб заполнить поля сообщения
            GameObject obj = Instantiate(_messageTemp, _chatMainWindow.transform);
            _message = obj.GetComponent<MessageItem>();
            VerticalLayoutGroup vlg = _chatMainWindow.GetComponent<VerticalLayoutGroup>();
            vlg.SetLayoutVertical();
            _message.FillFields(message);
            _scrollView.StartCoroutine(ForceScrollDown());
        }

        //карутина, чтоб всегда при новом сообщении в чате опускаться в конец списка сообщениий
        private IEnumerator ForceScrollDown()
        {
            // Wait for end of frame AND force update all canvases before setting to bottom.
            yield return new WaitForEndOfFrame();
            Canvas.ForceUpdateCanvases();
            _scrollView.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }
}
