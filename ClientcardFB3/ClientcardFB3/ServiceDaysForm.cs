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
    public partial class ServiceDaysForm : Form
    {

        ServiceItems clsSvcItems;
        ServiceDays clsSvcDays;

        string[] dateFormats;

        public ServiceDaysForm()
        {
            InitializeComponent();

            clsSvcItems = new ServiceItems(GlobalVariables.connectionString);
            clsSvcDays = new ServiceDays(GlobalVariables.connectionString);

            panel1.BackColor = Color.LightGreen;
            setTodayLable(DateTime.Today);
            getDateFormat(DateTime.Today);

            clsSvcItems.openAll();
            loadList();

            //calSvcDates.DayRender += new DayRenderEventHandler(calSvcDates_DayRender);
        }

        private void setTodayLable(DateTime date)
        {
            lblDayOfWeek.Text = date.DayOfWeek.ToString();
        }

        private void loadList()
        {
            lvSvcItms.Items.Clear();
            lvSvcItms.ListViewItemSorter = null;

            for (int i = 0; i < clsSvcItems.RowCount; i++)
            {
                lvSvcItms.Items.Add(clsSvcItems.DSet.Tables[0].Rows[i]["ItemDesc"].ToString());
                lvSvcItms.Items[i].SubItems.Add(clsSvcItems.DSet.Tables[0].Rows[i]["LbsPerItem"].ToString());
                lvSvcItms.Items[i].SubItems.Add(clsSvcItems.DSet.Tables[0].Rows[i]["ItemType"].ToString());
                lvSvcItms.Items[i].SubItems.Add(clsSvcItems.DSet.Tables[0].Rows[i]["ItemKey"].ToString());
            }
        }

        private void calSvcDates_DateSelected(object sender, DateRangeEventArgs e)
        {
            setTodayLable(calSvcDates.SelectionStart);
            getDateFormat(calSvcDates.SelectionStart);
        }

        //private void Calendar1_DayRender(Object source, System. DayRenderEventArgs e)
        //{
        //    // Check for May 5 in any year, and format it.
        //    if (e.Day.Date.Day == 5 && e.Day.Date.Month == 5)
        //    {
        //        e.Cell.BackColor = System.Drawing.Color.Yellow;

        //        // Add some static text to the cell.
        //        Label lbl = new Label();
        //        lbl.Text = "<br>My Birthday!";
        //        e.Cell.Controls.Add(lbl);
        //    }
        //}

        private void getDateFormat(DateTime date)
        {
            dateFormats = date.GetDateTimeFormats();
            tbCurDate.Text = dateFormats[2].ToString();
        }

        private void btnAddSvcDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Parse(tbCurDate.Text);
                calSvcDates.SetDate(date);
                calSvcDates.AddBoldedDate(date);
                calSvcDates.ForeColor = Color.LightGreen;
            }
            catch { }
        }
    }
}
