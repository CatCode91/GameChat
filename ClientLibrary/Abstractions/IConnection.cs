using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Abstractions
{
    public interface IConnection
    {
         string IPadress { get;  }
         string Port { get;  }
    }
}
