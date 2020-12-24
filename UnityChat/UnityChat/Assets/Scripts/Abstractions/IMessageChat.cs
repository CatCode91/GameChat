using UnityEngine;

namespace Assets.Scripts
{
    public interface IMessageChat
    {
        string Caption { get; set; }
        string Text { get; set; }
        string Time { get; }

        Color32 Color { get; }
    }
}
