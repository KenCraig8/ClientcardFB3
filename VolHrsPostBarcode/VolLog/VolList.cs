using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class VolList : Form
    {
        VolLogMain frmMain;

        public VolList(VolLogMain frmpParent)
        {
            frmMain = frmpParent;
            InitializeComponent();
        }

        public void refresh(string trxdate)
        {
            this.Text = "Volunteer Log for " + trxdate;
            lvwVols.Items.Clear();
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(CCFBGlobal.connectionString);
            conn.Open();
            SqlDataAdapter dadapt = new SqlDataAdapter("SELECT v.Name, vh.VolTimeIN, vh.VolTimeOUT, v.FBIDNbr, vh.NumVolHours FROM Volunteers v INNER JOIN VolunteerHours vh ON v.ID = vh.VolID AND vh.TrxDate = '" + trxdate + "' ORDER BY v.Name", conn);
            int iRowCnt = dadapt.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow drow = dt.Rows[i];
                ListViewItem lvi = new ListViewItem(drow.Field<string>("Name"));
                lvi.SubItems.Add(drow["VolTimeIN"].ToString());
                lvi.SubItems.Add(drow["VolTimeOUT"].ToString());
                lvi.SubItems.Add(drow["FBIDNbr"].ToString());
                lvi.SubItems.Add(drow["NumVolHours"].ToString());
                if (drow["VolTimeOUT"].ToString() != "")
                {
                    lvi.BackColor = Color.PowderBlue;
                }
                lvwVols.Items.Add(lvi);
            }
            conn.Close();
        }

        private void lvwVols_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked == true)
            {
                frmMain.setVol(e.Item.SubItems[3].Text);
                e.Item.Checked = false;
            }
        }
    }
}
