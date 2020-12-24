

using System;

namespace Assets.Scripts.Models
{
    public class MessageChat : IMessageChat
    {

        private string _time;

        public MessageChat()
        {
            Time = DateTime.Now.ToShortTimeString();
        }

        public string Caption { get; set; }
        public string Text { get; set; }
        public string Time { get; }
    }
}
