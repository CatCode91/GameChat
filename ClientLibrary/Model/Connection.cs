using ClientLibrary.Abstractions;
using System;
using System.Text.RegularExpressions;

namespace ClientLibrary.Model
{
    public class Connection: IConnection
    {

        Regex _reg = new Regex(@"^[0-9]{1,2}([.][0-9]{1,2})?$");

        private string _defaultIP = "127.0.0.1";
        private string _defaultPort = "8050";

        private string _ipAdress;
        private string _port;

        public Connection(string iPadress, string port)
        {
            IPadress = iPadress;
            Port = port;
        }

        public string IPadress
        {
            get
            {
                return _ipAdress;
            }

            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _ipAdress = _defaultIP;
                    return;
                }

                if (_reg.IsMatch(value)) 
                {
                    _ipAdress = _defaultIP;
                    throw new Exception("IP адрес должен содержать только цифры и точки.");
                }

                string[] splitValues = value.Split('.');
                if (splitValues.Length != 4)
                {
                    //если меньше чем 4 точки в IP адресе
                    _ipAdress = _defaultIP;
                    throw new Exception("Неправильный IP адрес");
                }

                _ipAdress = value;
            }

        }
        public string Port
        {
            get
            {
                return _port;
            }

            private set
            {
                if (value.Length > 4) 
                {
                    _port = _defaultPort;
                    throw new Exception("Какой-то у вас длинный порт!");
                }


                if (_reg.IsMatch(value))
                {
                    _port = _defaultPort;
                    throw new Exception("Эй?) порт должен содержать только цифры!");
                }

                _port = value;
            }

        }
    }
}

