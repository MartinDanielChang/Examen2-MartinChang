﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen2
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TicketForm TicketForm = new TicketForm();
            TicketForm.MdiParent = this;
            TicketForm.Show();
        }
    }
}