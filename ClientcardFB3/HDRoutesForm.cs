using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class HDRoutesForm : Form
    {
        DataSet dset;
        SqlDataAdapter dadAdpt;
        SqlCommand command;
        SqlConnection conn;
        
        HDRoutes clsHdRoutes = new HDRoutes(CCFBGlobal.connectionString);
        int rowCount = 0;
        int rowIndex = 0;
        bool loading = false;
        string orderBy = " ORDER BY h.Name";
        DateTime svcDate = DateTime.Today;
        string savePath = CCFBPrefs.ReportsSavePath;
        string sOriRouteText = "";

        public HDRoutesForm()
        {
            InitializeComponent();
            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            lbxRoutesRefresh();
        }

        private void lbxRoutesRefresh()
        {
            loading = true;
            clsHdRoutes.openWhere(" WHERE ID >0 ORDER BY RouteTitle");
            if (clsHdRoutes.RowCount > 0)
            {
                lbxRoutes.DataSource = clsHdRoutes.DTable;
                lbxRoutes.DisplayMember = "RouteTitle";
                lbxRoutes.ValueMember = "ID";
            }
            else
            {
                lbxRoutes.DataSource = null;
                lbxRoutes.Items.Add("No Routes Available");
            }
            loading = false;
        }

        private void HDRoutesForm_Load(object sender, EventArgs e)
        {
            pnlRouteInfo.BackColor = CCFBGlobal.bkColorAltEdit;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
//            changeGroupBoxVisibility(0);
        }

        
        private void btnSvcCancel_Click(object sender, EventArgs e)
        {
            
//            changeGroupBoxVisibility(0);
        }

        private void clearRouteInfo()
        {
            tbRouteID.Text = "";
            tbRouteTitle.Text = "";
            tbDriver.Text = "";
            tbPhone.Text = "";
            tbEstTime.Text = "";
            tbEstMiles.Text = "";
            tbRouteNotes.Text = "";
            tbDriverComments.Text = "";
        }

        private void displayRouteInfo(int idx)
        {
            clearRouteInfo();
            clsHdRoutes.find(idx, true);
            btnSaveRoute.Enabled = false;
            if (clsHdRoutes.ID == idx)
            {
                tbRouteID.Text = "ID: " + clsHdRoutes.ID.ToString();
                tbRouteTitle.Text = clsHdRoutes.RouteTitle;
                tbDriver.Text = clsHdRoutes.DriverName;
                tbPhone.Text = clsHdRoutes.DriverPhone;
                tbEstTime.Text = clsHdRoutes.EstHours.ToString("0.00");
                tbEstMiles.Text = clsHdRoutes.EstMiles.ToString("0.0");
                tbRouteNotes.Text = clsHdRoutes.Notes;
                tbDriverComments.Text = clsHdRoutes.DriverNotes;
            }
        }

        private void displaySelectedRoute()
        {
            if (clsHdRoutes.RowCount > 0 && loading == false)
            {
                displayRouteInfo(Convert.ToInt32(lbxRoutes.SelectedValue));
            }
            else if (loading == false)
            {
                clearRouteInfo();
            }
        }

        private void setRouteDataChanged()
        {
            btnSaveRoute.Enabled = true;
        }

        private void tbRoute_Enter(object sender, EventArgs e)
        {
            sOriRouteText = ((TextBox)sender).Text;
        }

        private void tbRoute_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != sOriRouteText)
            {
                clsHdRoutes.SetDataValue(tb.Tag.ToString(), tb.Text);
                setRouteDataChanged();
            }
        }

        private void btnSaveRoute_Click(object sender, EventArgs e)
        {
            clsHdRoutes.update();
            btnSaveRoute.Enabled = false;
        }

        private void lbxRoutes_SelectedValueChanged(object sender, EventArgs e)
        {
            displaySelectedRoute();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            loading = true;
            int lastID = clsHdRoutes.maxRouteId();
            int newRouteID = clsHdRoutes.Add();
            loading = false;
            lbxRoutesRefresh();
            setRouteDisplay(newRouteID);
        }

        private void setRouteDisplay(int newRouteID)
        {
            for (int i = 0; i < clsHdRoutes.RowCount; i++)
			{
                clsHdRoutes.setDataRow(i);
			    if (clsHdRoutes.ID == newRouteID)
                {
                    lbxRoutes.SelectedIndex = i;
                    break;
                }
			}
        }

        private void btnSelectDriver_Click(object sender, EventArgs e)
        {
            EditVolunteerForm frmVolunteers = new EditVolunteerForm(CCFBGlobal.connectionString, true);
            frmVolunteers.ShowDialog();
            int newVolId = frmVolunteers.SelectedId;
            if (newVolId > 0)
            {
                loading = true;
                clsHdRoutes.DefaultDriver = newVolId;
                clsHdRoutes.loadDriverInfo(newVolId);
                tbDriver.Text = clsHdRoutes.DriverName;
                tbPhone.Text = clsHdRoutes.DriverPhone;
                loading = false;
                btnSaveRoute.Enabled = true;
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (clsHdRoutes.RowCount > 0)
            {
                DialogResult dr = MessageBox.Show("Press OK to Delete this Route\r\nID = " + clsHdRoutes.ID.ToString() + "\r\nTitle = " + clsHdRoutes.RouteTitle, "Delete Home Delivery Route"
                                   , MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.OK)
                {
                    int idx = lbxRoutes.SelectedIndex;
                    loading = true;
                    clsHdRoutes.delete(clsHdRoutes.ID);
                    clsHdRoutes.refreshDataTable();
                    loading = false;
                    if (idx < lbxRoutes.Items.Count)
                    { lbxRoutes.SelectedIndex = idx; }
                    else if (lbxRoutes.Items.Count>0)
                    { lbxRoutes.SelectedIndex = 0; }
                    displaySelectedRoute();
                }
            }
        }
    }
}
