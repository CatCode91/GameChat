using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IMessageChat
    {
        string Caption { get; set; }
        string Text { get; set; }
        string Time { get; }
    }
}
