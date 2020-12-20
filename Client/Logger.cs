using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public static class Logger<T>
    {
        public static void ShowLog(T element, string text) 
        {
            if (element is TextBox textbox)
            {
                textbox.Text += String.Format("{0} - {1} {2}", DateTime.Now.ToShortTimeString(),text,Environment.NewLine);
            }
        }
    }
}
