﻿using SKOffice.domain.order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SKOffice
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OrderParser OP = OrderParser.Instance;
            OrderConfirmation oc = OP.readOrder("C:\\School\\SKøkken\\w0000520.e02");
            Console.WriteLine("Done..");
        }
    }
}