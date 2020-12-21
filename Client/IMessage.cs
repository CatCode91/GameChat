using System;

namespace Client
{
    public interface IMessage
    {
        Int16 Size { get; set; }
        Int32 Login { get; set; }
        string Text { get; set; }
    }
}
