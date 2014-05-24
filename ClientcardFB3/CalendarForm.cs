﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ClientcardFB3
{
    public partial class YearlyForm : Form
    {
        Int32 mClickMode = 0;
        String[] ClickModeName =  new String[] 
            {"Display Only","Add Days Open","Mark Commodity Days","Mark Special Food Days"};
        List<ComboBox> cbList = new List<ComboBox>();
        DateTime CurrentFiscalStart;
        DateTime CurrentFiscalEnd;
        DaysOpen clsDaysOpen = new DaysOpen(CCFBGlobal.connectionString);
        Boolean AlreadyHere = false; 

        public YearlyForm()
        {
            InitializeComponent();
            foreach (ComboBox cb in tabPage2.Controls.OfType<ComboBox>())
            {
                cbList.Add(cb);
            }
            ServiceItems clsServiceItems = new ServiceItems(CCFBGlobal.connectionString);
            clsServiceItems.openWhere("ItemRule = " + CCFBGlobal.itemRule_SpecialService.ToString() );
            lvwSpclFood.Items.Clear();
            ListViewItem lvwItm;
            for (int i = 0; i < clsServiceItems.DSet.Tables[0].Rows.Count; i++)
            {
                lvwItm = new ListViewItem();
                lvwItm.Text = clsServiceItems.DSet.Tables[0].Rows[i]["ItemDesc"].ToString();
                lvwItm.SubItems.Add(clsServiceItems.DSet.Tables[0].Rows[i]["ItemKey"].ToString());
                lvwSpclFood.Items.Add(lvwItm);
            }
            CurrentFiscalStart = CCFBGlobal.CurrentFiscalStartDate();
            CurrentFiscalEnd = CCFBGlobal.CurrentFiscalEndDate();
            SetCalendarRange(CurrentFiscalStart, CurrentFiscalEnd);
        }


        private void YearlyForm_Load(object sender, EventArgs e)
        {
            DateTime dateTest;
            foreach (ComboBox cb in tabPage2.Controls.OfType<ComboBox>())
            {
                cb.Items.Add("---Not Open---");
                cb.Items.Add("Every Week");
                cb.Items.Add("First and Third Week of the Month");
                cb.Items.Add("Second and Forth Week of the Month");
                cb.Items.Add("First Week of the Month");
                cb.Items.Add("Second Week of the Month");
                cb.Items.Add("Third Week of the Month");
                cb.Items.Add("Forth Week of the Month");
            }
            CCFBOpenDayOfWeek.OpenDOW();
            for (int i = 0; i < CCFBOpenDayOfWeek.DSetDOWRowsCount; i++)
            {
                for (int j = 0; j < cbList.Count; j++ )
                {
                    if (cbList[j].Tag.ToString() == CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows[i]["EditLabel"].ToString())
                    {
                        cbList[j].SelectedIndex = Int32.Parse(CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows[i]["FldVal"].ToString());
                        break;
                    }
                }
            }
            clsDaysOpen.openWhere("date = (SELECT MAX(date) From DaysOpen)");
            if (clsDaysOpen.RowCount > 0)
            {
                dateTest = clsDaysOpen.date;
            }
            else
            {
                dateTest = DateTime.Today;
            }
            dateTest = dateTest.AddMonths(1);
            string tmp = dateTest.Month.ToString() + "/01/" + dateTest.Year.ToString();
            dtpPeriodStart.Value = DateTime.Parse(tmp);
            dateTest = dtpPeriodStart.Value.AddYears(1).AddDays(-1);
            dtpPeriodEnd.Value = dateTest;
            ChangeMode(1);
        }

        private void cboOpenDay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            selctionCommited(cb);
        }

        private void selctionCommited(ComboBox cb)
        {
            for (int i = 0; i < CCFBOpenDayOfWeek.DSetDOWRowsCount; i++)
            {
                if (cb.Tag.ToString() == CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows[i]["EditLabel"].ToString())
                {
                    CCFBOpenDayOfWeek.DSetDOW.Tables[0].Rows[i]["FldVal"] = cb.SelectedIndex;
                    break;
                }
            }
            CCFBOpenDayOfWeek.updateDOW();
        }       

        private void ChangeMode(Int32 NewMode = 0)
        {
            mClickMode = NewMode;

            pbCalendar1.Footer.Text = ClickModeName[mClickMode];
            pbCalendar2.Footer.Text = ClickModeName[mClickMode];
            pbCalendar3.Footer.Text = ClickModeName[mClickMode];
        }

        private void rbAddDaysOpen_CheckedChanged(object sender, EventArgs e)
        {
            ChangeMode(1);
            lvwSpclFood.Visible = false;
        }

        private void rbMarkCommodity_CheckedChanged(object sender, EventArgs e)
        {
            ChangeMode(2);
            lvwSpclFood.Visible = false;
        }

        private void rbMarkSpclFood_CheckedChanged(object sender, EventArgs e)
        {
            ChangeMode(3);
            lvwSpclFood.Visible = true;
        }

        private void pbCalendar_DayDoubleClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {
            if (AlreadyHere == false)
            {
                AlreadyHere = true;
                CalendarDoubleClick(pbCalendar1, e);
                CalendarDoubleClick(pbCalendar2, e);
                if (CalendarDoubleClick(pbCalendar3, e) == true) 
                {
                    clsDaysOpen.deleteDate(e.Date);
                }
                AlreadyHere = false;
            }
            else
            {
            }
            clsDaysOpen.update();
        }

        private void pbCalendar_DayClick(object sender, Pabo.Calendar.DayClickEventArgs e)
        {
            CalendarDayClick(pbCalendar1, e);
            //CalendarDayClick(pbCalendar2, e);
            //CalendarDayClick(pbCalendar3, e);
            //CalendarDayClick(pbCalendar4, e);
        }
        private void CalendarDayClick(Pabo.Calendar.PBCalendar  sender, Pabo.Calendar.DayClickEventArgs e)
        {
            Pabo.Calendar.DateItem[] dItems;
            Int32 dtIndex = -1;
            DateTime dateWork = Convert.ToDateTime(e.Date);
            int newImageListIndex = 0;
            String newDateText = "";
            dtIndex = pbCalendar1.Dates.IndexOf(dateWork);
            if (dtIndex == -1)
            { 
                AddDateToCalendar( dateWork,false,"" );
                clsDaysOpen.AddDate(dateWork, false, "");
            }
            else if (mClickMode == 2)
            {
                dItems = pbCalendar1.Dates.DateInfo(dateWork);
                if (dItems[0].ImageListIndex == 0)
                    { newImageListIndex = -1; }
                else
                    { newImageListIndex = 0; }
                dItems[0].ImageListIndex = newImageListIndex;
                dItems = pbCalendar2.Dates.DateInfo(dateWork);
                dItems[0].ImageListIndex = newImageListIndex;
                dItems = pbCalendar3.Dates.DateInfo(dateWork);
                dItems[0].ImageListIndex = newImageListIndex;
                clsDaysOpen.FindDate(dateWork);
                clsDaysOpen.IsCommodity = (newImageListIndex == 0);
            }
            else if (mClickMode == 3)
            {
                dItems = pbCalendar1.Dates.DateInfo(dateWork);
                if (dItems[0].Text  == "")
                    { newDateText = "Spcl"; }
                else
                    { newDateText = ""; }
                dItems[0].Text = newDateText;
                dItems = pbCalendar2.Dates.DateInfo(dateWork);
                dItems[0].Text = newDateText;
                dItems = pbCalendar3.Dates.DateInfo(dateWork);
                dItems[0].Text = newDateText;
                clsDaysOpen.FindDate(dateWork);
                if (newDateText =="" )
                    { clsDaysOpen.SpecialItems = ""; }
                else
                    { clsDaysOpen.SpecialItems = SpecialFoodList(); }
            }
            clsDaysOpen.update(); 
        }

        private bool CalendarDoubleClick(Pabo.Calendar.PBCalendar sender, Pabo.Calendar.DayClickEventArgs e)
        {
            DateTime dt = Convert.ToDateTime(e.Date);
            Int32 dtIndex = -1;
            dtIndex = sender.Dates.IndexOf(dt.Date);
            if (dtIndex != -1)
            {
                sender.Dates.RemoveAt(dtIndex);
            }
            return (dtIndex != -1);
        }

        private void YearlyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void butMarkOpenDays_Click(object sender, EventArgs e)
        {
            int InsertMode = 2;
            if (rbClearBeforeWrite.Checked )
                InsertMode = 0;
            else if (rbOverWrite.Checked)
                InsertMode =1 ;
            else
                InsertMode = 2;

            clsDaysOpen.BatchProcess(dtpPeriodStart.Value, dtpPeriodEnd.Value, InsertMode, chkMarkCommodity.Checked);
            LoadCalendarDays();
        }

        private void SetCalendarRange(DateTime FirstDate, DateTime LastDate)
        {
            int FirstDisplayMonth = 1;
            int FirstDisplayYear = 2010;
            int TestMonth = 1;
            int ViewMonth = 1;
            int FiscalMonthStart = CCFBPrefs.FiscalYearStartMonth;
            //pbCalendar1.Dates.Clear();
            //pbCalendar2.Dates.Clear();
            //pbCalendar3.Dates.Clear(); 
            //pbCalendar4.Dates.Clear();
            SetDisplayMinMax();
            FirstDisplayYear = FirstDate.Year;
            if (DateTime.Today.Month >= FirstDate.Month)
            {
                if (FirstDisplayYear == DateTime.Today.Year)
                    { 
                    ViewMonth = DateTime.Today.Month; 
                }
                else if (FirstDate.Year <= DateTime.Today.Year)
                {
                    FirstDisplayYear = LastDate.Year;
                    ViewMonth = LastDate.Month;
                }
                else
                {   ViewMonth = FirstDate.Month; }
            }
            else
            {
                FirstDisplayYear = LastDate.Year;
                ViewMonth = LastDate.Month ;
            }
            TestMonth = ViewMonth - FiscalMonthStart + 1;
            if (TestMonth <= 3 )
                { FirstDisplayMonth = FiscalMonthStart; }
            else if (TestMonth <= 6)
                { FirstDisplayMonth = FiscalMonthStart + 3; }
            else
                { FirstDisplayMonth = FiscalMonthStart + 6; }
            if (FirstDisplayMonth > 12)
                { FirstDisplayMonth -= 12; }
            SetCalendarDisplayPeriod(FirstDisplayYear, FirstDisplayMonth);
        }

        private void SetCalendarDisplayPeriod(int FirstDisplayYear, int FirstDisplayMonth)
        {
            DateTime DisplayDate = new DateTime(FirstDisplayYear, FirstDisplayMonth, 1);
            SetCalendarActiveMonth(pbCalendar1, DisplayDate);
            SetCalendarActiveMonth(pbCalendar2, DisplayDate.AddMonths(1));
            SetCalendarActiveMonth(pbCalendar3, DisplayDate.AddMonths(2));
            butPrevQuarter.Enabled = (pbCalendar1.MinDate < DisplayDate);
            butNextQuarter.Enabled = (pbCalendar1.MaxDate > DisplayDate.AddMonths(4));
        }
        private void SetCalendarActiveMonth(Pabo.Calendar.PBCalendar myCalendar, DateTime DisplayMonth)
        {
            myCalendar.ActiveMonth.Year = DisplayMonth.Year;
            myCalendar.ActiveMonth.Month = DisplayMonth.Month;
        }

        private void butNextQuarter_Click(object sender, EventArgs e)
        {
            DateTime TestDate = new DateTime(pbCalendar3.ActiveMonth.Year, pbCalendar3.ActiveMonth.Month, 1).AddMonths(3);
            if (pbCalendar3.MaxDate.AddDays(1) > TestDate)
            {
                TestDate = new DateTime(pbCalendar1.ActiveMonth.Year, pbCalendar1.ActiveMonth.Month,1).AddMonths(3);
                SetCalendarDisplayPeriod(TestDate.Year, TestDate.Month);
            }
        }

        private void butPrevQuarter_Click(object sender, EventArgs e)
        {
            DateTime TestDate = new DateTime(pbCalendar1.ActiveMonth.Year, pbCalendar1.ActiveMonth.Month, 1).AddMonths(-3);
            if (pbCalendar1.MinDate <= TestDate)
            {
                SetCalendarDisplayPeriod(TestDate.Year, TestDate.Month);
            }
        }

        private void chkFiscalYr_CheckedChanged(object sender, EventArgs e)
        {
            SetDisplayMinMax();
        }

        private void SetDisplayMinMax()
        {
            DateTime FirstDate = CurrentFiscalStart;
            DateTime LastDate = CurrentFiscalEnd;
            if (chkPrevFiscalYr.Checked)
            {
                FirstDate = CurrentFiscalStart.AddYears(-1);
            }
            if (chkNextFiscalYr.Checked)
            { 
                LastDate = CurrentFiscalEnd.AddYears(1); 
            }
            pbCalendar1.MinDate = FirstDate;
            pbCalendar1.MaxDate = LastDate;
            pbCalendar2.MinDate = FirstDate;
            pbCalendar2.MaxDate = LastDate;
            pbCalendar3.MinDate = FirstDate;
            pbCalendar3.MaxDate = LastDate;
            DateTime DisplayDate = new DateTime(pbCalendar1.ActiveMonth.Year, pbCalendar1.ActiveMonth.Month, 1);
            butPrevQuarter.Enabled = (pbCalendar1.MinDate < DisplayDate);
            butNextQuarter.Enabled = (pbCalendar1.MaxDate > DisplayDate.AddMonths(3));
            LoadCalendarDays();
        }
        private void LoadCalendarDays()
        {
            DateTime OpenDate;
            String SpecialItems = "";
            Boolean IsCommodity = false;
            pbCalendar1.Dates.Clear();
            pbCalendar2.Dates.Clear();
            pbCalendar3.Dates.Clear();
            clsDaysOpen.openWhere("date between '" + pbCalendar1.MinDate.Date + "' AND '" + pbCalendar1.MaxDate.Date + "'");
            for (int i = 0; i < clsDaysOpen.RowCount; i++)
            {
                OpenDate = clsDaysOpen.DSet.Tables[0].Rows[i].Field<DateTime>("Date");
                SpecialItems = clsDaysOpen.DSet.Tables[0].Rows[i].Field<String>("SpecialItems");
                if (SpecialItems == null)
                    { SpecialItems = ""; }
                else if (SpecialItems != "" )
                    { SpecialItems = "Spcl"; }
                IsCommodity = clsDaysOpen.DSet.Tables[0].Rows[i].Field<Boolean>("IsCommodity");
                AddDateToCalendar(OpenDate, IsCommodity, SpecialItems);
            }
        }

        private void AddDateToCalendar(DateTime OpenDate, Boolean IsCommodity, String SpecialItemList)
        {
            Pabo.Calendar.DateItem dItem = new Pabo.Calendar.DateItem();
            dItem.Date = OpenDate;
            dItem.BackColor1 = Color.PaleGreen;
            dItem.BoldedDate = true;

            if (IsCommodity)
                { dItem.ImageListIndex = 0; }
            else
                { dItem.ImageListIndex = -1; }
            if (SpecialItemList != null)
                { dItem.Text = SpecialItemList; }
            pbCalendar1.AddDateInfo(dItem);
            pbCalendar2.AddDateInfo(dItem);
            pbCalendar3.AddDateInfo(dItem);
        }

        private string SpecialFoodList()
        {
            String myList = "";
            foreach (ListViewItem lvItm in lvwSpclFood.Items)
            {
                if (lvItm.Checked)
                {
                    if (myList != "")
                    { myList += "|"; }
                    myList += lvItm.SubItems[1].Text;
                }
            }
            return myList;
        }

        private void SaveCal1_Click(object sender, EventArgs e)
            { PrintCalendar(pbCalendar1); }

        private void SaveCal2_Click(object sender, EventArgs e)
            { PrintCalendar(pbCalendar2); }

        private void SaveCal3_Click(object sender, EventArgs e)
            { PrintCalendar(pbCalendar3); }

        private void PrintCalendar(Pabo.Calendar.PBCalendar myCalendar)
        {
            string Filename = System.IO.Path.Combine(CCFBGlobal.pathScreenshots,"Calendar" 
                + myCalendar.ActiveMonth.Year.ToString() + "-" 
                + myCalendar.ActiveMonth.Month.ToString() + ".jpg");

            int oriHeight = myCalendar.Size.Height;
            int oriWidth = myCalendar.Size.Width;
            int oriX = myCalendar.Location.X;
            int oriY = myCalendar.Location.Y;
            myCalendar.SetBounds(5, 10, 600, 600);
            myCalendar.SaveAsImage(Filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            myCalendar.SetBounds(oriX, oriY, oriWidth, oriHeight);
        }

     }
}
