using System;
using System.Collections.Generic;
using System.Text;

namespace ClientLibrary.Abstractions
{
    public interface IMessage
    {
       short Size { get; set; }
       int Login { get; set; }
       string Text { get; set; }
    }
}
