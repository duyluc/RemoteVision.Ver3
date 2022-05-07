using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.Ver1
{
    public partial class FrmStartup : Form
    {
        public FrmStartup()
        {
            InitializeComponent();
            this.Show();
            this.Load += FrmStartup_Load;
        }

        private void FrmStartup_Load(object sender, EventArgs e)
        {
            
        }
    }
}
