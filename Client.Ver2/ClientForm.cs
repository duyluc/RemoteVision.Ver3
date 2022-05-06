using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpSupport;

namespace Client.Ver2
{
    public partial class ClientForm : Form
    {
        public TcpClient TcpClient { get; set; }
        public ClientForm()
        {
            InitializeComponent();
            this.TcpClient = new TcpClient();
            this.TcpClient.Connected += TcpClient_Connected;
            this.TcpClient.Disconnected += TcpClient_Disconnected;
            this.TcpClient.Sended += TcpClient_Sended;
            this.TcpClient.Received += TcpClient_Received;

        }

        private void TcpClient_Received(object sender, EventArgs e)
        {
            byte[] receivedData = ((TcpArgs)e).Data;
            this.ShowMessage($"Receive: {receivedData.Length} byte");
        }

        private void TcpClient_Sended(object sender, EventArgs e)
        {
        }

        private void TcpClient_Disconnected(object sender, EventArgs e)
        {
        }

        private void TcpClient_Connected(object sender, EventArgs e)
        {
            
        }

        public void ShowMessage(string t)
        {
            this.tbxMessage.Invoke(new Action(() => { this.tbxMessage.Text += t + Environment.NewLine; }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Bitmap image;
            using (Bitmap _image = new Bitmap(@"C:\Users\duong\Desktop\Image.jpg"))
            {
                image = new Bitmap(_image);
            }            
            Task _ = Transfer(image);
        }
        
        public async Task Transfer(object subject)
        {
            Task _t = new Task(() =>
            {
                try
                {
                    byte[] _sendData = Serialize.ObjectToByteArray(subject);
                    string ip = this.tbxIp.Text;
                    int port = int.Parse(this.tbxPort.Text);
                    bool isconnected = this.TcpClient.Connect(ip, port);
                    if (!isconnected) throw new Exception("Can not connect to Server!");
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    Task _ = this.TcpClient.Run(_sendData);
                    _.Wait();
                    sw.Stop();
                    ShowMessage($"TactTime: {sw.ElapsedMilliseconds} ms");
                }
                catch(Exception t)
                {
                    this.ShowMessage(t.Message);
                }
            });
            _t.Start();
            await _t;
        }
    }
}
