using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Abstractions
{
    public interface IConnection
    {
         string IPadress { get; set; }
         int Port { get; set; }
    }
}
