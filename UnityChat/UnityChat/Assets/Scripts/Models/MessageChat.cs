using System;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public class MessageChat : IMessageChat
    {
        public MessageChat()
        {
            Time = DateTime.Now.ToShortTimeString();
        }

        public string Caption { get; set; }
        public string Text { get; set; }
        public string Time { get; }

        public Color32 Color { get; set; }
    }
}
