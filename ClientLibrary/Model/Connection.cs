using ClientLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Model
{
    public class Connection: IConnection
    {

        private string _defaultIP = "127.0.0.1";
        private int _defaultPort = 8050;

        private string _ipAdress;
        private int _port;

        public Connection(string iPadress, int port)
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

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _ipAdress = _defaultIP;
                    return;
                }

                string[] splitValues = value.Split('.');

                if (splitValues.Length != 4)
                {
                    _ipAdress = _defaultIP;
                    return;
                }

                _ipAdress = value;
            }

        }
        public int Port
        {
            get
            {
                return _port;
            }

            set
            {
                if (value <= 0)
                {
                    _port = _defaultPort;
                    return;
                }
                _port = value;
            }

        }

    }
}

