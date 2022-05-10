using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Ver2
{
    public partial class FrmStartup : Form
    {
        public FrmStartup()
        {
            InitializeComponent();
            Task _ = WaitingMainForm();
        }

        public async Task WaitingMainForm()
        {
            Task _t = new Task(() =>
            {
                Thread _ = new Thread(() =>
                {
                    ServerForm serverform = new ServerForm();
                    serverform.ShowDialog();
                });
                _.SetApartmentState(ApartmentState.STA);
                _.Start();
            });
            _t.Start();
            while (!ServerForm.IsStartuped)
            {
                await Task.Delay(10);
            }
            this.Invoke(new Action(() => { this.Hide(); }));
            await _t;
            this.Close();
        }
    }
}
