using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class MessageChat
    {
        public MessageChat(string caption, string text, string time)
        {
            Caption = caption;
            Text = text;
            Time = time;
        }

        public string Caption { get; }
        public string Text { get; }
        public string Time { get;  }

    }
}
