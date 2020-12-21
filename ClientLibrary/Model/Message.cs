using ClientLibrary.Abstractions;

namespace ClientLibrary.Model
{
    internal class Message : IMessage
    {
        public short Size { get; set; }
        public int Login { get; set; }
        public string Text { get; set; }
    }
}
