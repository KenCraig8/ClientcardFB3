using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientCardFB3
{
    public partial class DaysOpenForm : Form
    {
        List<ComboBox> cbList = new List<ComboBox>();

        public DaysOpenForm()
        {
            InitializeComponent();

            foreach (ComboBox cb in Controls.OfType<ComboBox>())
            {
                cbList.Add(cb);
            }
        }

        private void DaysOpen_Load(object sender, EventArgs e)
        {
            Defaults.openWhere("EditForm='OpenDOW'");
            foreach (ComboBox cb in this.Controls.OfType<ComboBox>())
            {
                cb.Items.Add("---Not Open---");
                cb.Items.Add("Every Week");
                cb.Items.Add("First and Thrid Week of the Month");
                cb.Items.Add("Second and Forth Week of the Month");
                cb.Items.Add("First Week of the Month");
                cb.Items.Add("Second Week of the Month");
                cb.Items.Add("Third Week of the Month");
                cb.Items.Add("Forth Week of the Month");
            }

            for (int i = 0; i < Defaults.RowCount; i++)
            {
                for (int j = 0; j < cbList.Count; j++ )
                {
                    if (cbList[j].Tag.ToString() == Defaults.DSet.Tables[0].Rows[i]["EditLabel"].ToString())
                    {
                        cbList[j].SelectedIndex = Int32.Parse(Defaults.DSet.Tables[0].Rows[i]["FldVal"].ToString());
                    }
                }
            }
        }

        private void cboOpenMon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenMon);
        }

        private void cboOpenTues_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenTues);
        }

        private void cboOpenWeds_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenWeds);
        }

        private void cboOpenThurs_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenThurs);
        }

        private void cboOpenFri_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenFri);
        }

        private void cboOpenSat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenSat);
        }

        private void cboOpenSun_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selctionCommited(cboOpenSun);
        }

        private void selctionCommited(ComboBox cb)
        {
            for (int i = 0; i < Defaults.RowCount; i++)
            {
                if (cb.Tag.ToString() == Defaults.DSet.Tables[0].Rows[i]["EditLabel"].ToString())
                {
                    Defaults.DSet.Tables[0].Rows[i]["FldVal"] = cb.SelectedIndex;
                    break;
                }
            }
            Defaults.update();
        }       
    }
}
