using ClientLibrary.Model;
using System;
using TMPro;
using UnityEngine;

//скрипт, содержащий логику для формы регистрации и подключения к серверу

namespace Assets.Scripts
{
    public class ClientConnector : MonoBehaviour
    {
        private MainClient _clientWorker;
        private MenuAnimator _animator;
        private ErrorMessage _error;

        private TMP_Text _ip;
        private TMP_Text _port;

        public void Start()
        {
            _clientWorker = GetComponentInChildren<MainClient>();
            _animator = GetComponent<MenuAnimator>();
            _ip = GameObject.Find("input_ip").GetComponent<TMP_Text>();
            _port = GameObject.Find("input_port").GetComponent<TMP_Text>();
            _error = GetComponentInChildren<ErrorMessage>(true);
        }

        public void EntryInChat()
        {
            //Чистим от мусора, который тянется в свойство Text из TextMeshPro

            string clean_port = _port.text.Replace("\u200B", "");
            string clean_ip = _ip.text.Replace("\u200B", "");

            try 
            {
                Connection conn = new Connection(clean_ip, clean_port);
                _clientWorker.ConnectToServer(conn);
                _animator.EntryChatAnimation();
            }

            catch(Exception ex)
            {
                _error.ShowMessage("Ой!", ex.Message);
                Debug.Log(ex.Message);
            }      
        }
    }
}
