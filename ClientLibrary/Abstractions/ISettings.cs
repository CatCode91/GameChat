using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Abstractions
{
    public interface ISettings
    {
        IConnection Connection { get; set; }

        IConnection LoadSettings();

        void SaveSettings();
    }
}
