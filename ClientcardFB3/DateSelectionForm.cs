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
    public partial class DateSelectionForm : Form
    {
        DaysOpen clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
        
        bool setDateForAppts;
        bool bDateIsSelected = false;
        
        /// <summary>
        /// Coinstructor for the Default Date Picker Form
        /// </summary>
        /// <param name="setDfltForApptsIn">True = Default Appoinment Date.  
        /// False = Default Service Date</param>
        public DateSelectionForm(bool setDfltForApptsIn)
        {
            InitializeComponent();

            setDateForAppts = setDfltForApptsIn;
            if (setDateForAppts == true)
            {
                grpbxPeriod.Visible = false;
                this.Text = "Appointment Date Selection";
            }
            SetCalendarForCurrFiscalYear(); 
            LoadCalendarDays();
        }

        public bool DateIsSelected
        {
            get { return bDateIsSelected; }
        }

        public string DateSelected
        {
            get { return lblDateSelected.Text.ToString(); }
        }
        
        private void SetCalendarForCurrFiscalYear()
        {
            if (setDateForAppts == true)
            {
                pbCalendar.MinDate = DateTime.Today;
                pbCalendar.MaxDate = DateTime.Today.AddDays(15);
            }
            else
            {
                pbCalendar.MinDate = CCFBGlobal.CurrentFiscalStartDate();
                pbCalendar.MaxDate = DateTime.Today;
            }
            pbCalendar.ActiveMonth.Month = DateTime.Today.Month;
            pbCalendar.ActiveMonth.Year = DateTime.Today.Year;
        }

        private void SetCalendarForPrevFiscalYear()
        {
            pbCalendar.MinDate = CCFBGlobal.PreviousFiscalStartDate();
            pbCalendar.MaxDate = CCFBGlobal.PreviousFiscalEndDate();
            pbCalendar.ActiveMonth.Month = CCFBGlobal.PreviousFiscalStartDate().Month;
            pbCalendar.ActiveMonth.Year = CCFBGlobal.PreviousFiscalEndDate().Year;
        }

        /// <summary>
        /// Loads the Calendar Days where the foodbank is open
        /// </summary>
        private void LoadCalendarDays()
        {
            pbCalendar.Dates.Clear();
            string sWhereClause = "Date between '" + pbCalendar.MinDate.Date.ToShortDateString() + "' AND '";
            if (setDateForAppts == true)
                sWhereClause += pbCalendar.MaxDate.Date.ToShortDateString() + "'";
            else
                sWhereClause += DateTime.Today.ToShortDateString() + "'";
            clsDaysOpen.openWhere(sWhereClause);
            for (int i = 0; i < clsDaysOpen.RowCount; i++)
            {
                clsDaysOpen.CurrentRow = i;
                AddDateToCalendar(clsDaysOpen.date, clsDaysOpen.IsCommodity, clsDaysOpen.SpecialItems);
            }
        }

        /// <summary>
        /// Fomats the given date to show on calendar that it is an open day
        /// </summary>
        /// <param name="OpenDate">Date to format as open date</param>
        /// <param name="IsCommodity">Is This a Commodity Day</param>
        /// <param name="SpecialItemList">List of Special Service Items</param>
        private void AddDateToCalendar(DateTime OpenDate, Boolean IsCommodity, String SpecialItemList)
        {
            Pabo.Calendar.DateItem dItem = new Pabo.Calendar.DateItem();
            dItem.Date = OpenDate;
            dItem.BackColor1 = Color.PaleGreen;
            dItem.BoldedDate = true;
            if (IsCommodity == true)
                dItem.ImageListIndex = 0;
            else
                dItem.ImageListIndex = -1;
            if (SpecialItemList != null)
                dItem.Text = SpecialItemList;

            pbCalendar.AddDateInfo(dItem);
            dItem.Dispose();
        }

        private void pbCalendar_DayClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {
            if (pbCalendar.Dates.IndexOf(DateTime.Parse(e.Date)) != -1)
            {
                lblDateSelected.Text = e.Date;
                btnSelect.Enabled = true;
            }
        }

        private void pbCalendar_DayDoubleClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {
            if (pbCalendar.Dates.IndexOf(DateTime.Parse(e.Date)) != -1)
            {
                lblDateSelected.Text = e.Date;
                bDateIsSelected = true;
                this.Visible = false;
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTest = Convert.ToDateTime(lblDateSelected.Text);
                bDateIsSelected = true;
            }
            catch
            {
                MessageBox.Show("Invalid Date");
                bDateIsSelected = false;
            }
            this.Visible = false;
        }

        private void rdoCurrFiscalYr_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCurrFiscalYr.Checked == true)
            {
                SetCalendarForCurrFiscalYear();
            }
        }

        private void rdoPrevFiscalYr_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoPrevFiscalYr.Checked == true)
            {
                SetCalendarForPrevFiscalYear();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bDateIsSelected = false;
            this.Visible = false;
        }
    }
}
