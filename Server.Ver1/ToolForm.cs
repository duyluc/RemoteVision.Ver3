﻿using System;
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
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
            this.CogToolBlockEditer.Subject = ServerForm.CogToolBlock;
        }
    }
}
