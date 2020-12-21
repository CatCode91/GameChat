using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class Message
    {
        public Int16 Size { get; set; }
        public Int32 Login { get; set; }
        public string Text { get; set; }
    }
}
