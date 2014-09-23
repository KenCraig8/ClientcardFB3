using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class HDProcessorForm : Form
    {
        public HDProcessorForm()
        {
            InitializeComponent();
        }

        private void tsmiFileNew_Click(object sender, EventArgs e)
        {
            HDRouteDetailForm mdiRoute = new HDRouteDetailForm();
            mdiRoute.MdiParent = this;
            mdiRoute.Show();
        }
    }
}
