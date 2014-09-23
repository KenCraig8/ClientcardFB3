using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;

namespace ClientCardFB3
{
    public partial class MonthlyReportsForm : Form
    {
        MainForm frmMain;
        MonthlyReports clsMonthlyReports = new MonthlyReports(CCFBGlobal.connectionString);
        SqlDataAdapter dadAdpt;
        DataSet dset;
        SqlCommand command;
        SqlConnection conn;
        string tbName = "TrxLog";
        string file = "";
        string city = "";
        string savePath = CCFBPrefs.ReportsSavePath;

        // Create the Outlook application by using inline initialization.
        Microsoft.Office.Interop.Outlook.Application oApp;

        List<TextBox> tbPerList;
        List<TextBox> tbCumList;
        
        DataTable dTableMonthStats;

        string sqlCommandText = "Select Distinct DateName(yy, TrxDate) as 'Year' "
            + "From TrxLog where TrxDate is not null Order By 'Year' DESC";


        public MonthlyReportsForm(MainForm mainFormIn)
        {
            InitializeComponent();

            frmMain = mainFormIn;
            tbPerList = new List<TextBox>();
            tbCumList = new List<TextBox>();
            traverseAndAddControlsToCollections(this.Controls);
            clsMonthlyReports.openWhere(" Where GroupingBy=0");

            dadAdpt = new SqlDataAdapter();
            dset = new DataSet();
            conn = new SqlConnection(CCFBGlobal.connectionString);
            SqlCommand selectComm = new SqlCommand(sqlCommandText, conn);
            dadAdpt.SelectCommand = selectComm;
            getDistinctYears();
            fillYearsCombo();
            fillFoodBankData();
            setDefaultReports();
            loadReports();
            CCFBGlobal.verifyPath(savePath);
        }

        private void loadReports()
        {
            lvReports.Items.Clear();
            ListViewItem lvi = new ListViewItem("All");
            lvReports.Items.Add(lvi);
            for (int i = 0; i < clsMonthlyReports.RowCount; i++)
            {
                clsMonthlyReports.setDataRow(i);
                if (clsMonthlyReports.ReportActive == true)
                {
                    lvi = new ListViewItem(clsMonthlyReports.ReportName);
                    lvi.Tag = clsMonthlyReports.ID;
                    lvReports.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// Checks the Report Path and finds if the reports exist for the current period
        /// </summary>
        private void findIfReportsExist()
        {
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Tag != null && lvReports.Items[i].Tag.ToString() != "")
                {
                    //Get Report Path
                    file = CCFBPrefs.ReportsSavePath;
                    //Add Underschore
                    clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                    file += cboReportYear.SelectedItem.ToString() +
                        getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1).ToString() + "_";
                    //Add Period
                    file += clsMonthlyReports.ReportName + ".doc";

                    //Check if file exists
                    if (File.Exists(file) == true)
                    {
                        lvReports.Items[i].BackColor = Color.LightSteelBlue;
                        lvReports.Items[i].Checked = false;
                    }
                    else
                    {
                        lvReports.Items[i].BackColor = Color.Beige;
                    }
                }
            }

                        //For each report Checkbox
            foreach(CheckBox cb in pnlReports.Controls.OfType<CheckBox>())
            {
                //If that checkbox does not = ALL
                if (cb.Tag != null && cb.Tag.ToString() != "")
                {
                    //Get Report Path
                    file = CCFBPrefs.ReportsSavePath;
                    //Add Underschore
                    clsMonthlyReports.find(Convert.ToInt32(cb.Tag));
                    file += cboReportYear.SelectedItem.ToString() +
                        getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1).ToString() +"_";
                    //Add Period
                    file += clsMonthlyReports.ReportName + ".doc";

                    //Check if file exists
                    if (File.Exists(file) == true)
                    {
                        cb.BackColor = Color.Red;
                        cb.Checked = false;
                    }
                    else
                    {
                        cb.BackColor = Color.Cornsilk;
                    }
                }
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        private void setDefaultReports()
        {           
            foreach (CheckBox cb in pnlReports.Controls.OfType<CheckBox>())
            {
                for (int i = 0; i < clsMonthlyReports.RowCount; i++)
                {
                    if (cb.Tag != null &&
                        cb.Tag.ToString() == clsMonthlyReports.DSet.Tables[0].Rows[i]["ID"].ToString())
                    {
                        if (clsMonthlyReports.DSet.Tables[0].Rows[i]["ReportActive"].ToString() == "False")
                            cb.Visible = false;
                        else
                            cb.Visible = true;
                    }
                }
            }
        }

        private void fillFoodBankData()
        {
            tbFBName.Text = CCFBPrefs.FoodBankName;
            tbCounty.Text = CCFBPrefs.County;
            tbReportDate.Text = DateTime.Today.ToShortDateString();
            tbPreparedBy.Text = CCFBPrefs.PreparedBy;
            tbPhone.Text = CCFBPrefs.PhoneNumber;

            city = CCFBPrefs.DefaultCity;
        }

        private void getDistinctYears()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                dadAdpt.Fill(dset, tbName);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch (SqlException ex) 
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(), 
                    CCFBGlobal.serverName);
            }

        }

        private int getValueAsInt(int rowNum, string colName )
        {
            try
            {
                return Int32.Parse(dTableMonthStats.Rows[rowNum][colName].ToString());
            }
            catch (System.Exception ex) 
            {
                CCFBGlobal.appendErrorToErrorReport("rowNum=" + rowNum + ", colName=" + colName,
                    ex.GetBaseException().ToString());
                return 0; 
            }
        }

        private void calcTotals()
        {
            tbPerLbsRcvdTotal.Text = (getValueAsInt(0, "LbsEFAP")
                + getValueAsInt(0, "LbsTEFAP")
                + getValueAsInt(0, "LbsPurchased")
                + getValueAsInt(0, "LbsInkind")).ToString("N00");

            tbPerLbsServedTotal.Text = (getValueAsInt(0, "LbsStandard")
                + getValueAsInt(0, "LbsOther")
                + getValueAsInt(0, "LbsCommodity")
                + getValueAsInt(0, "LbsSupplemental")).ToString("N00");

            tbCumLbsRcvdTotal.Text = (getValueAsInt(1, "LbsEFAP")
                + getValueAsInt(1, "LbsTEFAP")
                + getValueAsInt(1, "LbsPurchased")
                + getValueAsInt(1, "LbsInkind")).ToString("N00");

            tbCumLbsServedTotal.Text = (getValueAsInt(1, "LbsStandard")
                + getValueAsInt(1, "LbsOther")
                + getValueAsInt(1, "LbsCommodity")
                + getValueAsInt(1, "LbsSupplemental")).ToString("N00");
        }

        private void fillYearsCombo()
        {
            cboReportYear.Items.Clear();
            for (int i = 0; i < dset.Tables[tbName].Rows.Count; i++)
            {
                cboReportYear.Items.Add(dset.Tables[tbName].Rows[i]["Year"]);

                if (dset.Tables[tbName].Rows[i]["Year"].ToString() == DateTime.Today.Year.ToString())
                    cboReportYear.SelectedIndex = i;

                if (DateTime.Today.Day > 10)
                    cboReportMonth.SelectedIndex = DateTime.Today.Month - 1;
                else if(DateTime.Today.Month  == 1)
                    cboReportMonth.SelectedIndex = 11;
                else
                    cboReportMonth.SelectedIndex = DateTime.Today.Month - 2;
            }
        }

        private void btnLoadPeriod_Click(object sender, EventArgs e)
        {
            
            command = new SqlCommand("MonthStatistics", conn);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dtable = new DataTable();
            try
            {

                SqlParameter parameter = new SqlParameter("@Period", SqlDbType.Char, 6,"Period");
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = cboReportYear.SelectedItem.ToString() + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1);

                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter);

                // Open the connection and execute the reader.
                if(conn.State == ConnectionState.Closed)
                    conn.Open();
                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    DataTable readerSchema = reader.GetSchemaTable().Copy();
                    dTableMonthStats = new DataTable();

                    for (int i = 0; i < readerSchema.Rows.Count; i++)
                    {
                        dTableMonthStats.Columns.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                    }

                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        reader.GetValues(values);
                        dTableMonthStats.Rows.Add(values);
                    }
                }
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                splitContainer1.Panel2.BackColor = Color.Beige;
                lvReports.Enabled = true;
                btnCreateReports.Enabled = false;
                btnEmailReports.Enabled = false;
                btnDisplayExisting.Enabled = false;
            }
            catch (SqlException ex) 
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString(),
                    CCFBGlobal.serverName);
            }

            initForm();
            fillForm();
            findIfReportsExist();
        }

        private void initForm()
        {
            foreach (TextBox tb in tbPerList.OfType<TextBox>())
            {
                tb.Text = "";
            }
            foreach (TextBox tb in tbCumList.OfType<TextBox>())
            {
                tb.Text = "";
            }
        }

        private void fillForm()
        {
            if (dTableMonthStats != null)
            {
                foreach (TextBox tb in tbPerList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                    {
                        if (dTableMonthStats.Columns.IndexOf(tb.Tag.ToString()) >= 0)
                        {
                            if (dTableMonthStats.Rows[0][tb.Tag.ToString()].ToString() == "")
                            {
                                tb.Text = "0";
                            }
                            else
                            {
                                tb.Text = Convert.ToDecimal(dTableMonthStats.Rows[0][tb.Tag.ToString()]).ToString("N00");
                            }
                        }
                    }
                }
                foreach (TextBox tb in tbCumList.OfType<TextBox>())
                {
                    if (tb.Tag.ToString() != "")
                    {
                        if (dTableMonthStats.Columns.IndexOf(tb.Tag.ToString()) >= 0)
                        {
                            if (dTableMonthStats.Rows[1][tb.Tag.ToString()].ToString() == "")
                            {
                                tb.Text = "0";
                            }
                            else
                            {
                                tb.Text = Convert.ToDecimal(dTableMonthStats.Rows[1][tb.Tag.ToString()]).ToString("N00");
                            }
                        }
                    }
                }
                calcTotals();
            }
            else
            {
                MessageBox.Show("ERROR: Your Data Has Caused An Error."
                + " Please Select A Different Period And Try Again");
            }
        }

        private string getFormatedMonthNumber(int month)
        {
            if (month < 10)
            {
                return "0" + month.ToString();
            }
            else
            {
                return month.ToString();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkAllReports_CheckedChanged(object sender, EventArgs e)
        {
            foreach (CheckBox cb in pnlReports.Controls.OfType<CheckBox>())
            {
                cb.Checked = true;
            }
        }

        private void btnCreateReports_Click(object sender, EventArgs e)
        {
            object saveAs = "";
            string templatePath = "";

            for (int i = 0; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Checked == true)
                {
                    switch (lvReports.Items[i].Tag.ToString())
                    {
                        case "3": //FB Coalition Report
                            {
                                //if (chkFBCoalitionReport.Checked == true)
                                //{
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_" +
                                    clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateFBCoalitionReport clsCreateFBCoalitionReport = new CreateFBCoalitionReport(dTableMonthStats);
                                clsCreateFBCoalitionReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    lblHHPerTot.Text, lblIndFiscalTot.Text, lblHHPerTot.Text, lblHHFicalTot.Text,
                                    tbCumLbsServedTotal.Text, tbPreparedBy.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                                //}
                            }
                        case "1":       //EFAP Report
                            //if (chkEFAP.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_" +
                                    clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateNewEFAPReport clsCreateEFAPReport = new CreateNewEFAPReport(dTableMonthStats);
                                clsCreateEFAPReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString() + "/" + cboReportYear.SelectedItem.ToString(),
                                    tbCounty.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                        case "4":       //NW Harvest Report
                            //if (chkNthWstHarvest.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateNWHarvestReport clsCreateNWHarReport = new CreateNWHarvestReport(dTableMonthStats);
                                clsCreateNWHarReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text,
                                    lblHHPerTot.Text, lblIndPerTot.Text, tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                        case "5":       //Food Life Line Report
                            //if (chkFoodLifeline.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateFoodLifeLineReport clsCreateFLLReport = new CreateFoodLifeLineReport(dTableMonthStats);
                                clsCreateFLLReport.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text, lblHHPerTot.Text, lblIndPerTot.Text,
                                    tbPreparedBy.Text, tbPhone.Text, tbCumLbsServedTotal.Text, saveAs, templatePath);
                                break;
                            }
                        case "6":
                            //if (chkScndHvstPantry.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                saveAs = savePath + "\\" + cboReportYear.SelectedItem.ToString()
                                    + getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) + "_"
                                    + clsMonthlyReports.ReportName + ".doc";

                                templatePath = clsMonthlyReports.ReportPath;

                                CreateSecHarvPantryMthlyReport clsCreateSecHarvMthRpt =
                                    new CreateSecHarvPantryMthlyReport(dTableMonthStats);
                                clsCreateSecHarvMthRpt.createReport(tbFBName.Text, cboReportMonth.SelectedItem.ToString(),
                                    cboReportYear.SelectedItem.ToString(), tbCounty.Text, city, tbPhone.Text, lblIndPerTot.Text,
                                    tbPreparedBy.Text, saveAs, templatePath);
                                break;
                            }
                    }
                }
            }

            findIfReportsExist();
        }

        private void btnEmailReports_Click(object sender, EventArgs e)
        {
            string fileName = "";
            string filePath = "";
            for (int i = 1; i < lvReports.Items.Count; i++)
            {
                if (lvReports.Items[i].Checked == true)
                {
                    switch (lvReports.Items[i].Tag.ToString())
                    {
                        case "3":
                            //if (chkFBCoalitionReport.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                        case "5":
                            //if (chkFoodLifeline.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    clsMonthlyReports.find(lvReports.Items[i].Tag.ToString());
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                        case "4":
                            //if (chkNthWstHarvest.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));

                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                        case "7":
                            //if (chkScndHvstMnthly.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));

                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                        case "6":
                            //if (chkScndHvstPantry.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                        case "1":
                            //if (chkEFAP.Checked == true)
                            {
                                clsMonthlyReports.find(Convert.ToInt32(lvReports.Items[i].Tag));
                                fileName = cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                filePath = savePath + "\\" + cboReportYear.SelectedItem.ToString() +
                                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1)
                                            + "_" + clsMonthlyReports.ReportName + ".doc";

                                if (File.Exists(filePath) == true)
                                {
                                    createAndSendEmail(filePath, fileName, clsMonthlyReports.EmailAddresses.Replace('|', ';'));
                                }
                                break;
                            }
                    }
                }
            }
        }


        private void createAndSendEmail(string filePath, string fileName, string emailList)
        {
            try
            {
                Microsoft.Office.Interop.Outlook.Application oApp;
                 Microsoft.Office.Interop.Outlook._NameSpace oNameSpace;
                 Microsoft.Office.Interop.Outlook.MAPIFolder oOutboxFolder; 
                oApp = new Microsoft.Office.Interop.Outlook.Application();
                oNameSpace = oApp.GetNamespace("MAPI");

                oNameSpace.Logon("", "", true, true);
                oOutboxFolder = oNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderOutbox);

                Microsoft.Office.Interop.Outlook._MailItem oMailItem = 
                    (Microsoft.Office.Interop.Outlook._MailItem)oApp.CreateItem(
                    Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

                oMailItem.To = emailList;
                oMailItem.Subject = fileName + " From " + tbFBName.Text; 
                oMailItem.Body = "TEST THIS";
                oMailItem.SaveSentMessageFolder = oOutboxFolder;
                String sSource = filePath;
                String sDisplayName = fileName;
                int iPosition = (int)oMailItem.Body.Length + 1;
                int iAttachType = (int)OlAttachmentType.olByValue;
                oMailItem.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);
                
                //uncomment this to also save this in your draft 
                //oMailItem.Save(); 
                //adds it to the outbox 
                oMailItem.Send(); 
                oMailItem = null;
                oNameSpace = null;
                ////Create the new message by using the simplest approach.
                //MailItem oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);
                //Inspector oAddSig = null;
                ////oAddSig = oMsg.GetInspector;

                ////Add a recipient.
                //// TODO: Change the following recipient where appropriate.
                //Recipient oRecip = (Recipient)oMsg.Recipients.Add(emailList);
                //oRecip.Resolve();

                ////Set the basic properties.
                //oMsg.Subject = fileName + " From " + tbFBName.Text;
                //oMsg.Body = "Put YOUR Message Here";

                ////Add an attachment.
                //// TODO: change file path where appropriate
                //String sSource = filePath;
                //String sDisplayName = fileName;
                //int iPosition = (int)oMsg.Body.Length + 1;
                //int iAttachType = (int)OlAttachmentType.olByValue;
                //Attachment oAttach = oMsg.Attachments.Add(sSource, iAttachType, iPosition, sDisplayName);

                //// If you want to, display the message.
                ////oMsg.Display(true);  //modal

                ////Send the message.
                ////oMsg.Save();
                //((Microsoft.Office.Interop.Outlook._MailItem)oMsg).Send();

                //Explicitly release objects.
                //oRecip = null;
                //oAttach = null;
                //oMsg = null;
                
                oApp = null;
            }

                // Simple error handler.
            catch (System.Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
        }

        private void btnDisplayExisting_Click(object sender, EventArgs e)
        {
            string fileName = "";
            foreach (ListViewItem lvi in lvReports.Items)
            {
                if (lvi.Checked == true && lvi.BackColor == Color.Red)
                {
                    try
                    {
                        clsMonthlyReports.find(Convert.ToInt32(lvi.Tag));
                        fileName = @"C:\ClientCardFB3\Reports\" + cboReportYear.SelectedItem.ToString() +
                            getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) +
                            "_" + clsMonthlyReports.ReportName + ".doc";

                        CCFBGlobal.openDocumentOutsideCCFB(fileName);
                    }
                    catch (System.Exception ex)
                    {
                        CCFBGlobal.appendErrorToErrorReport("FileName=" + fileName.ToString(), ex.GetBaseException().ToString());
                        MessageBox.Show("The File " + fileName.ToString() + " Does Not Exist");
                    }
                }
            }
            //foreach (CheckBox cb in pnlReports.Controls.OfType<CheckBox>())
            //{
            //    if (cb.Checked == true && cb.BackColor == Color.Red)
            //    {
            //        try
            //        {
            //            clsMonthlyReports.find(Convert.ToInt32(cb.Tag));
            //            fileName = @"C:\ClientCardFB3\Reports\" + cboReportYear.SelectedItem.ToString() +
            //                getFormatedMonthNumber(cboReportMonth.SelectedIndex + 1) +
            //                "_" + clsMonthlyReports.ReportName + ".doc";

            //            CCFBGlobal.openDocumentOutsideCCFB(fileName);
            //        }
            //        catch (System.Exception ex)
            //        {
            //            CCFBGlobal.appendErrorToErrorReport("FileName=" + fileName.ToString(), ex.GetBaseException().ToString());
            //            MessageBox.Show("The File " + fileName.ToString() + " Does Not Exist");
            //        }
            //    }
           // }
        }

        private void lvReports_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            bool bEnableCreate = false;
            bool bEnableDisplay = false;
            if (e.Item.Index == 0)
            {
                bEnableCreate = true;
                for (int i = 1; i < lvReports.Items.Count; i++)
                {
                    lvReports.Items[i].Checked = lvReports.Items[0].Checked;
                    bEnableDisplay = (lvReports.Items[i].BackColor == Color.LightSteelBlue);
                }
            }
            else
            {
                for (int i = 1; i < lvReports.Items.Count; i++)
                {
                    if (lvReports.Items[i].Checked == true)
                    {
                        bEnableCreate = true;
                        bEnableDisplay = (lvReports.Items[i].BackColor == Color.LightSteelBlue);
                    }
                }
            }
            btnCreateReports.Enabled = bEnableCreate;
            btnDisplayExisting.Enabled = bEnableDisplay;
            btnEmailReports.Enabled = bEnableDisplay;
        }

        private void cbo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            splitContainer1.Panel2.BackColor = Color.LightGray;
            lvReports.Enabled = false;
            btnCreateReports.Enabled = false;
            btnDisplayExisting.Enabled = false;
            btnEmailReports.Enabled = false;
        }
        /// <summary>
        /// Traverses all controls on the page using recursion and adds the proper ones
        /// to their proper collections and adds LostFocus event to Textboxes and Checkboxes
        /// </summary>
        /// <param name="controlList"></param>
        private void traverseAndAddControlsToCollections(Control.ControlCollection controlList)
        {
            foreach (Control ctrl in controlList.OfType<Control>())
            {
                if (ctrl.GetType().Name == "TextBox")
                {
                    if (ctrl.Tag != null)
                    {
                        if (ctrl.Name.ToString().StartsWith("tbPer") == true)
                            tbPerList.Add((TextBox)ctrl);
                        else if (ctrl.Name.ToString().StartsWith("tbCum") == true)
                            tbCumList.Add((TextBox)ctrl);
                    }
                }
                traverseAndAddControlsToCollections(ctrl.Controls);
            }
        }
    }
}
