using ClientLibrary;
using ClientLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWinForms
{
    public partial class Form1 : Form
    {
        private ChatClient _chat = new ChatClient();

        public Form1()
        {
            InitializeComponent();
            _chat.NewMessage += _chat_NewMessage;
        }

        private void _chat_NewMessage(ClientLibrary.Abstractions.IMessage obj)
        {
            txt_console.Text += obj.Text + Environment.NewLine;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                _chat.ConnectToServer(new Connection("127.0.0.1", "8050"));
            }

            catch (Exception ex) 
            {
                txt_console.Text += ex.Message + Environment.NewLine;     
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                _chat.Disconnect();
            }

            catch (Exception ex)
            {
                txt_console.Text += ex.Message + Environment.NewLine;
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            try
            {
                _chat.SendMessage(txt_input.Text);
            }

            catch (Exception ex)
            {
                txt_console.Text += ex.Message + Environment.NewLine;
            }
        }
    }
}
