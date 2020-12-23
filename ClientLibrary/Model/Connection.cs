using ClientLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientLibrary.Model
{
    public class Connection: IConnection
    {

        Regex _reg = new Regex(@"/ ^-?\d *\.?\d *$/");

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

                string[] splitValues = value.Split('.');

                if (!_reg.IsMatch(value)) 
                {
                    _ipAdress = _defaultIP;
                    throw new Exception("Эй?) IP адрес должен содержать только цифры и разделитель!");
                }

                if (splitValues.Length != 4)
                {
                    _ipAdress = _defaultIP;
                    return;
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
                if (value.Length > 3) 
                {
                    _port = _defaultPort;
                    throw new Exception("Как-то у вас длинный порт!");
                }


                if (!_reg.IsMatch(value))
                {
                    _port = _defaultPort;
                    throw new Exception("Эй?) порт должен содержать только цифры!");
                }

                _port = value;
            }

        }


    }
}

