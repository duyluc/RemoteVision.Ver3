using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        public Dictionary<string, Bitmap> InputImageList;

        string TestImagPath = ""; //->TEST
        int SerializeTimeout = 1000;
        public enum Status
        {
            Free,
            Busy
        }

        public Status TransferStatus { get; set; }

        //INPUT OUTPUT
        private Dictionary<string, Terminal> Output { get; set; }
        private Dictionary<string, Terminal> Input { get; set; }

        public ClientForm()
        {
            InitializeComponent();
            TcpClient = new TcpClient();
            TcpClient.Connected += TcpClient_Connected;
            TcpClient.Disconnected += TcpClient_Disconnected;
            TcpClient.Sended += TcpClient_Sended;
            TcpClient.Received += TcpClient_Received;
            TcpClient.SendTimeout += TcpClient_SendTimeout;
            TcpClient.ReceivedTimeout += TcpClient_ReceivedTimeout;
            Output = new Dictionary<string, Terminal>();
            Input = new Dictionary<string, Terminal>();
            InputImageList = new Dictionary<string, Bitmap>();
            this.cbxInputImageList.DropDownStyle = ComboBoxStyle.DropDownList;
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

                    //update cbxInputImageList
                    long deserializetime = 0;
                    byte[] receivedData = ((TcpArgs)e).Data;
                    ShowMessage($"<<------------------>>");
                    ShowMessage($"Receive: {receivedData.Length} byte");
                    Input = Serialize.ByteArrayToTerminal(receivedData, out deserializetime);
                    ShowMessage($"Deserialization Time: {deserializetime} ms");
                    string _lastsellectedvalue = null;
                    int _lastsellectedindex = -1;
                    this.cbxInputImageList.Invoke(new Action(() => 
                    {
                        if(this.cbxInputImageList.SelectedItem != null)
                            _lastsellectedvalue =  this.cbxInputImageList.SelectedItem.ToString();
                        _lastsellectedindex = this.cbxInputImageList.SelectedIndex;
                    }));
                    this.InputImageList.Clear();
                    this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.Items.Clear(); }));
                    
                    foreach (KeyValuePair<string, Terminal> t in Input)
                    {
                        Terminal ter = t.Value;
                        if(ter.Type == typeof(Bitmap))
                        {
                            this.InputImageList.Add(ter.Name,(Bitmap)ter.Value);
                            this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.Items.Add(ter.Name); }));
                        }
                        else
                        {
                            ShowMessage($"{ter.Name}: {ter.Value.ToString()}");
                        }
                    }
                    if (_lastsellectedvalue != null)
                    {
                        if (this.InputImageList.ContainsKey(_lastsellectedvalue)) 
                            this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.SelectedIndex = _lastsellectedindex; }));
                        else
                        {
                            this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.SelectedIndex = -1; }));
                        }
                    }
                    else if(this.InputImageList.Count >0)
                    {
                        this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.SelectedIndex = 0; }));
                    }
                    else
                    {
                        this.cbxInputImageList.Invoke(new Action(() => { this.cbxInputImageList.SelectedIndex = -1; }));
                    }
                    
                    
                    ShowMessage($"<<------------------>>\n\n\n");
                }
                catch (Exception t)
                {
                    ShowMessage($">>> Desiralization data: {t.Message}");
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
            tbxMessage.Invoke(new Action(() => { tbxMessage.Text += t + Environment.NewLine; }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (TransferStatus == Status.Busy) return;

            Bitmap image = null;
            Bitmap simage = null;
            try
            {
                using (Bitmap _image = new Bitmap(TestImagPath)) //->TEST
                {
                    image = new Bitmap(_image);
                    simage = new Bitmap(_image);
                }
                if (image == null || simage == null) throw new Exception("Image is NUll");
            }
            catch (Exception t)
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
                        string ip = tbxIp.Text;
                        int port = int.Parse(tbxPort.Text);
                        isconnected = TcpClient.Connect(ip, port, out ConnectTime);
                    }
                    catch (Exception t)
                    {
                        ShowMessage($">>> {t.Message}");
                    }
                });
                Task _serialize = new Task(() =>
                {
                    try
                    {
                        Terminal outputImage = new Terminal("Image", subject, typeof(Bitmap));
                        if (!Output.ContainsKey(outputImage.Name))
                            Output.Add(outputImage.Name, outputImage);
                        else
                            Output[outputImage.Name] = outputImage;
                        _sendData = Serialize.TerminalToByteArray(Output, out SerializeTime);
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
                //await Task.WhenAll(_connect, _serialize);
                int count = 0;
                while (count < SerializeTimeout / 10 && (!isconnected||!isserialize))
                {
                    await Task.Delay(10);
                    count++;
                }
                if (!isconnected) throw new Exception("Can not connect to Server!");
                if (!isserialize) throw new Exception("Can not Serialize!");
                try
                {

                    Runtime = await TcpClient.Run(_sendData, false);
                    
                }
                catch (Exception t)
                {
                    ShowMessage($">>> {t.Message}");
                    throw t;
                }
                ShowMessage("<<-------------------->>");
                ShowMessage($"Connect Time: {ConnectTime} ms");
                ShowMessage($"Serialize Time: {SerializeTime} ms");
                ShowMessage($"Run Time: {Runtime} ms");
                ShowMessage($"TactTime: {sw.ElapsedMilliseconds} ms");
                ShowMessage("<<-------------------->>");
            }
            catch (Exception t)
            {
                ShowMessage($">>> {t.Message}");
            }
            finally
            {
                sw.Stop();
                sw.Reset();
                TransferStatus = Status.Free;
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = @"C:\\";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TestImagPath = ofd.FileName;
                    ShowMessage($">>> Selected {TestImagPath}");
                }
            }
        }

        private void cbxInputImageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            if (index == -1) return;
            try
            {
                this.Display.Image = this.InputImageList[((ComboBox)sender).SelectedItem.ToString()];
            }
            catch(Exception t)
            {
                ShowMessage(t.Message);
            }
        }

        private void cbxInputImageList_SelectedIndexChanged(int index)
        {
            if (index == -1) return;
            try
            {
                this.Display.Invoke(new Action(() => { this.Display.Image = this.InputImageList[this.cbxInputImageList.Items[index].ToString()]; }));
            }
            catch (Exception t)
            {
                ShowMessage(t.Message);
            }
        }
    }
}
