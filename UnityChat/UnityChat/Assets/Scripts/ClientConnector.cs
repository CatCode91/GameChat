using ClientLibrary;
using ClientLibrary.Abstractions;
using ClientLibrary.Enums;
using ClientLibrary.Model;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class ClientConnector : MonoBehaviour
    {
        private MainClient _clientWorker;
        private MenuAnimator _animator;

        private TMP_Text _ip;
        private TMP_Text _port;

        public void Start()
        {
            _clientWorker = GetComponentInChildren<MainClient>();
            _animator = GetComponent<MenuAnimator>();
            _ip = GameObject.Find("input_ip").GetComponent<TMP_Text>();
            _port = GameObject.Find("input_port").GetComponent<TMP_Text>();
        }

        public void EntryInChat()
        {
            //Чистим от мусора, который тянется из TextMeshPro

            string clean_port = _port.text.Replace("\u200B", "");
            string clean_ip = _ip.text.Replace("\u200B", "");


            Connection conn = new Connection(clean_ip, clean_port);

            try 
            {
                _clientWorker.ConnectToServer(conn);
                _animator.EntryChatAnimation();
            }

            catch(Exception ex)
            {
                Debug.Log(ex.Message);
            }      
        }
    }
}
