using System;

namespace Server
{
    public class LogModel
    {
        public LogModel(string text,MessageStatus status)
        {
            Date = DateTime.Now;
            UserName = Environment.UserName;
            WorkStation = Environment.MachineName;
            Text = text;
            Status = status;
        }

        public DateTime Date { get; }
        public string UserName { get;  }
        public string WorkStation { get; }

        public MessageStatus Status { get; }

        public string Text { get; }
    }
}
