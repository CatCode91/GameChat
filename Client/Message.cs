namespace Client
{
    class Message : IMessage
    {
        public short Size { get; set; }
        public int Login { get; set; }
        public string Text { get; set; }
    }
}
