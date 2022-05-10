using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpSupport;
using VisionSupport;

namespace Client.Ver2
{
    public partial class ClientForm : Form
    {
        public TcpClient TcpClient { get; set; }
        Stopwatch sw = new Stopwatch();

        string TestImagPath = ""; //->TEST

        public enum Status
        {
            Free,
            Busy
        }

        public Status TransferStatus { get; set; }

        //INPUT OUTPUT
        private Dictionary<string,Terminal> Output { get; set; }
        private Dictionary<string,Terminal> Input { get; set; }

        public ClientForm()
        {
            InitializeComponent();
            this.TcpClient = new TcpClient();
            this.TcpClient.Connected += TcpClient_Connected;
            this.TcpClient.Disconnected += TcpClient_Disconnected;
            this.TcpClient.Sended += TcpClient_Sended;
            this.TcpClient.Received += TcpClient_Received;
            this.TcpClient.SendTimeout += TcpClient_SendTimeout;
            this.TcpClient.ReceivedTimeout += TcpClient_ReceivedTimeout;
            this.Output = new Dictionary<string, Terminal>();
            this.Input = new Dictionary<string, Terminal>();
            TransferStatus = Status.Free;
        }

        private void TcpClient_ReceivedTimeout(object sender, EventArgs e)
        {
            ShowMessage(">>> Receive Timeout!");
        }

        private void TcpClient_SendTimeout(object sender, EventArgs e)
        {
            ShowMessage(">>> Send Timeout!");
        }

        private void TcpClient_Received(object sender, EventArgs e)
        {
            Thread _ = new Thread(() =>
            {
                try
                {
                    long deserializetime = 0;
                    byte[] receivedData = ((TcpArgs)e).Data;
                    this.ShowMessage($"<<------------------>>");
                    this.ShowMessage($"Receive: {receivedData.Length} byte");
                    Input = Serialize.ByteArrayToTerminal(receivedData, out deserializetime);
                    this.ShowMessage($"Deserialization Time: {deserializetime} ms");
                    foreach (KeyValuePair<string, Terminal> t in Input)
                    {
                        Terminal ter = t.Value;
                        switch (t.Value.Name)
                        {
                            case "Count":
                                this.ShowMessage($"Finded Pattern: {(int)ter.Value}");
                                break;
                            case "OutputImage1":
                                if (ter.Value == null) break;
                                this.Display1.Invoke(new Action(() =>
                                {
                                    this.Display1.BackgroundImage = ter.Value as Bitmap;
                                }));
                                break;
                            case "OutputImage2":
                                if (ter.Value == null) break;
                                this.Display2.Invoke(new Action(() =>
                                {
                                    this.Display2.BackgroundImage = ter.Value as Bitmap;
                                }));
                                break;
                            case "OutputImage3":
                                if (ter.Value == null) break;
                                this.Display3.Invoke(new Action(() =>
                                {
                                    this.Display3.BackgroundImage = ter.Value as Bitmap;
                                }));
                                break;
                            case "OutputImage4":
                                if (ter.Value == null) break;
                                this.Display4.Invoke(new Action(() =>
                                {
                                    this.Display4.BackgroundImage = ter.Value as Bitmap;
                                }));
                                break;
                            default: break;
                        }
                    }
                    this.ShowMessage($"<<------------------>>\n\n\n");
                }
                catch (Exception t)
                {
                    this.ShowMessage($">>> Desiralization data: {t.Message}");
                }
            });
            _.Start();
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
            if (TransferStatus == Status.Busy) return;
            
            Bitmap image = null;
            Bitmap simage = null;
            try
            {
                using (Bitmap _image = new Bitmap(this.TestImagPath)) //->TEST
                {
                    image = new Bitmap(_image);
                    simage = new Bitmap(_image);
                }
                if (image == null || simage == null) throw new Exception("Image is NUll");
            }
            catch(Exception t)
            {
                ShowMessage($">>> Load Image: {t.Message}");
                return;
            }
            TransferStatus = Status.Busy;
            Task _ = Transfer(image);
        }
        
        public async Task Transfer(Bitmap subject)
        {
            try
            {
                sw.Start();
                byte[] _sendData = null;
                bool isconnected = false;
                bool isserialize = false;
                long SerializeTime = 0;
                long ConnectTime = 0;
                long Runtime = 0;
                Task _connect = new Task(() =>
                {
                    try
                    {
                        string ip = this.tbxIp.Text;
                        int port = int.Parse(this.tbxPort.Text);
                        isconnected = this.TcpClient.Connect(ip, port, out ConnectTime);
                    }
                    catch (Exception t)
                    {
                        this.ShowMessage($">>> {t.Message}");
                    }
                });
                Task _serialize = new Task(() =>
                {
                    try
                    {
                        Terminal outputImage = new Terminal("Image", subject, typeof(Bitmap));
                        if (!this.Output.ContainsKey(outputImage.Name))
                            this.Output.Add(outputImage.Name, outputImage);
                        else
                            this.Output[outputImage.Name] = outputImage;
                        _sendData = Serialize.TerminalToByteArray(this.Output, out SerializeTime);
                        isserialize = true;
                    }
                    catch (Exception t)
                    {
                        ShowMessage($">>> Serialization processing: {t.Message}");
                        throw t;
                    }
                });
                _connect.Start();
                _serialize.Start();
                await Task.WhenAll(_connect, _serialize);
                if (!isconnected) throw new Exception("Can not connect to Server!");
                if (!isserialize) throw new Exception("Can not Serialize!");
                try
                {

                    Runtime = await this.TcpClient.Run(_sendData, false);
                    sw.Stop();
                    ShowMessage("<<-------------------->>");
                    ShowMessage($"Connect Time: {ConnectTime} ms");
                    ShowMessage($"Serialize Time: {SerializeTime} ms");
                    ShowMessage($"Run Time: {Runtime} ms");
                    ShowMessage($"TactTime: {sw.ElapsedMilliseconds} ms");
                    ShowMessage("<<-------------------->>");
                    sw.Reset();
                }
                catch (Exception t)
                {
                    this.ShowMessage($">>> {t.Message}");
                    throw t;
                }
            }
            catch(Exception t)
            {

            }
            finally
            {
                this.TransferStatus = Status.Free;
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = @"C:\\";
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    this.TestImagPath = ofd.FileName;
                    this.ShowMessage($">>> Selected {this.TestImagPath}");
                }
            }
        }
    }
}
