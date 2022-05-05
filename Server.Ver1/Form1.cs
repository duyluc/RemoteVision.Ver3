using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpSupport;

namespace Server.Ver1
{
    public partial class Form1 : Form
    {
        public TcpServer Server { get; set; }
        public Form1()
        {
            InitializeComponent();
            this.Server = new TcpServer();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string ip = this.tbIp.Text;
            int port = int.Parse(this.tbPort.Text);
            try
            {
                this.Server.Start(ip, port);
            }
            catch(Exception t)
            {
                this.tbxMessage.Text += t.Message;
            }
        }
    }
}
