using ClientLibrary;
using ClientLibrary.Abstractions;
using ClientLibrary.Enums;
using ClientLibrary.Model;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Client : MonoBehaviour
    {
        private bool _isBusy = false;

        private ChatClient _client = new ChatClient();
        private MenuAnimator _animator;

        private IConnection _host;

        public event Action<ServerStatus> ServerStatusChanged;
        public event Action<IMessage> NewMessageCame;

        private TMP_Text _ip;
        private TMP_Text _port;

        public void Start()
        {
            _animator = GetComponent<MenuAnimator>();
            _ip = GameObject.Find("input_ip").GetComponent<TMP_Text>();
            _port = GameObject.Find("input_port").GetComponent<TMP_Text>();
        }

        public void EntryInChat()
        {
            IConnection conn = new Connection(_ip.text, _port.text);

            try 
            {
                if (!_isBusy) 
                {
                    _client.ConnectToServer(_host);
                    _animator.EntryChatAnimation();
                }
            }

            catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }
          
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
