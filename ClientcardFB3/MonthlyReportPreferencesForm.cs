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
    public partial class MonthlyReportPreferencesForm : Form
    {
        //Class that interacts with the EmailRecipients table
        EmailRecipients clsEmailAddr = new EmailRecipients(CCFBGlobal.connectionString);

        //Class that interacts with the MonthlyReports table
        MonthlyReports clsMonthlyReports = new MonthlyReports(CCFBGlobal.connectionString);

        //Array to hold the email address after being read from dataset and split on '|'
        string[] emailAddresses;

        //Used to stop firing events when loading
        bool loadingValues = true;

        public MonthlyReportPreferencesForm()
        {
            InitializeComponent();

            //Opens all email address and reports
            clsEmailAddr.openAll();
            clsMonthlyReports.openWhere("");

            //Add the tags to the recipients column
            dataGridViewReicpitents.Columns[0].Tag = "ActiveReportRecipeint";
            dataGridViewReicpitents.Columns[2].Tag = "RecipientName";
            dataGridViewReicpitents.Columns[3].Tag = "EmailAddress";

            //Fills reports
            fillReports();
            //Fills Recipients
            fillRecipients(true);
            //Done loading values
            loadingValues = false;
        }

        /// <summary>
        /// Fills the Reports Grid
        /// </summary>
        private void fillReports()
        {
            //Traverse all reports
            for (int i = 0; i < clsMonthlyReports.RowCount; i++)
            {
                //Add row
                dataGridViewReports.Rows.Add();
                
                //Insert report name
                dataGridViewReports.Rows[i].Cells["clmMonthlyReports"].Value =
                    clsMonthlyReports.DSet.Tables[0].Rows[i]["ReportName"].ToString();
                
                //insert ID
                dataGridViewReports.Rows[i].Cells["clmReportId"].Value =
                    clsMonthlyReports.DSet.Tables[0].Rows[i].Field<int>("ID");
                
                //Set if report is active
                dataGridViewReports.Rows[i].Cells["clmReportActive"].Value =
                    clsMonthlyReports.DSet.Tables[0].Rows[i].Field<bool>("ReportActive");

                try
                {
                    //Set the selected row to the first row
                    dataGridViewReports.Rows[0].Selected = true;
                }
                catch { }
            }
        }

        /// <summary>
        /// Fills the dataGridViewRecipients 
        /// </summary>
        /// <param name="rowIndex">Row to set checks to</param>
        /// <param name="setChecks">Tells if should set checks or not</param>
        public void fillRecipients(bool setChecks)
        {
            //Clear list
            dataGridViewReicpitents.Rows.Clear();

            //Travers all recipients
            for (int i = 0; i < clsEmailAddr.RowCount; i++)
            {
                //Add row
                dataGridViewReicpitents.Rows.Add();

                //Insert ID
                dataGridViewReicpitents.Rows[i].Cells["clmID"].Value =
                    clsEmailAddr.GetDataValue("ID", i).ToString();
                //Insert Name
                dataGridViewReicpitents.Rows[i].Cells["clmName"].Value =
                    clsEmailAddr.GetDataValue("RecipientName", i).ToString();
                //Insert Email Adress
                dataGridViewReicpitents.Rows[i].Cells["clmEmailAddress"].Value =
                    clsEmailAddr.GetDataValue("EmailAddress", i).ToString();
            }

            //If there is recipients and setChecks is true
            if (setChecks == true && clsEmailAddr.RowCount > 0)
            {
                try
                {
                    //Set the checks for the currently selcted report
                    setRecipientChecksForReport(dataGridViewReports.CurrentRow.Index);
                }
                catch(NullReferenceException ex)
                { 
                    //Set row for the first report in grid
                    setRecipientChecksForReport(0); 
                }
            }
        }

        /// <summary>
        /// Sets the checks for each recipient for the current report
        /// </summary>
        /// <param name="rowIndex">The index of the current row of report</param>
        private void setRecipientChecksForReport(int rowIndex)
        {
            //Gets and splits the concatenated list of email addresses
            emailAddresses = clsMonthlyReports.GetDataValue("EmailAddresses").ToString().Split('|');

            //Traverse all recipients
            for (int j = 0; j < dataGridViewReicpitents.Rows.Count; j++)
            {
                //Travers the split email addresses
                for (int k = 0; k < emailAddresses.Length; k++)
                {
                    //If recipient is in report list
                    if (dataGridViewReicpitents.Rows[j].Cells["clmEmailAddress"].Value.ToString()
                        == emailAddresses[k])
                    {
                        //Set the check to true
                        dataGridViewReicpitents.Rows[j].Cells["clmAddRecpt"].Value = true;
                        break;
                    }
                    else
                    {
                        //Set the check to false
                        dataGridViewReicpitents.Rows[j].Cells["clmAddRecpt"].Value = false;
                    }
                }
            }
        }

        /// <summary>
        /// Traverses all report emaill recipients and deletes the 
        /// selected recipient from each 
        /// </summary>
        /// <param name="emailAddress">Email Recipient To Delete</param>
        private void deleteEmailRecptientsFromReports(string emailAddress)
        {
            //Array of Email Addresses used when concatenated list is split
            string[] emails;

            //THe new concatenated list of email addresses
            string concatEmails;

            //Traverse all reports and look for the recpient to delete
            for(int i= 0; i < dataGridViewReports.Rows.Count; i++)
            {
                clsMonthlyReports.find(Convert.ToInt32(dataGridViewReports.Rows[i].Cells["clmReportID"].Value));

                //Split the concatenated list of email addresses that is in database
                 emails = clsMonthlyReports.GetDataValue
                    ("EmailAddresses").ToString().Split('|');
                
                //Reset new concatenated list to empty
                 concatEmails = "";

                //Travers the split email addresses and check for one to delete
                for (int j = 0; j < emails.Length; j++)
                {
                    //Not empty and split adderess is not one to delete
                    if (emails[j] != emailAddress && emails[j].Length >0)
                    {
                        //Add email address to new concatenated list
                        concatEmails += emails[j] + "|";
                    }
                }

                //Insert list into the dataset
                clsMonthlyReports.SetDataValue("EmailAddresses", concatEmails);
            }
            //Update the database
            clsMonthlyReports.update();
        }

        /// <summary>
        /// Deletes a user from the databse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRecipients_Click(object sender, EventArgs e)
        {
            //Confirm that the User wants to delete recipient
            if (MessageBox.Show("Are You Sure You Would Like To Permenantly Delete "
                + dataGridViewReicpitents.CurrentRow.Cells["clmName"].Value.ToString()
                + "?", "Delete Recipient", MessageBoxButtons.YesNo)
                == System.Windows.Forms.DialogResult.Yes)
            {
                //Delete user
                clsEmailAddr.delete(Int32.Parse(
                    dataGridViewReicpitents.CurrentRow.Cells["clmID"].Value.ToString()));

                //Takes the user out of all concatenated list of emails for each report
                deleteEmailRecptientsFromReports(dataGridViewReicpitents.CurrentRow.
                    Cells["clmEmailAddress"].Value.ToString());

                //Refill the email recipients
                clsEmailAddr.openAll();

                //Refill the form
                fillRecipients(true);
            }

        }

        /// <summary>
        /// Add an email recipient
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRecipients_Click(object sender, EventArgs e)
        {
            //Create new form to add an email recipient
            AddNewEmailRecipientForm frmAddNewEmailRecpt
                = new AddNewEmailRecipientForm(clsEmailAddr, this);
            
            frmAddNewEmailRecpt.ShowDialog();
        }

        /// <summary>
        /// Triggers when user leaves the email address textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTemplatePath_Leave(object sender, EventArgs e)
        {
            //Sets the template path from the textbox
            clsMonthlyReports.SetDataValue(tbTemplatePath.Tag.ToString(), tbTemplatePath.Text);
            
            //Update the database
            clsMonthlyReports.update();
        }

        private void MonthlyReportPrefrences_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewReports_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            clsMonthlyReports.find(Convert.ToInt32(dataGridViewReports.Rows[e.RowIndex].Cells["clmReportId"].Value));

            //Set the checks for the report email recipeints
            setRecipientChecksForReport(e.RowIndex);

            //Set the template path
            tbTemplatePath.Text = clsMonthlyReports.GetDataValue("ReportPath").ToString();

            bool valid = (bool)dataGridViewReports.Rows[e.RowIndex].Cells["clmReportActive"].Value;;

            //Sets visibility of the recipients dataGridView 
            dataGridViewReicpitents.Visible = valid;
            btnAddRecipients.Enabled = valid;
            btnDeleteRecipients.Enabled = valid;

        }

        /// <summary>
        /// Updates the database when a cell in the reipients dataGridView is edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReicpitents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //If cell is not the checkbox cell
            if (dataGridViewReicpitents.Columns[e.ColumnIndex].Tag.ToString() != "ActiveReportRecipeint")
            {
                //Set value
                clsEmailAddr.SetDataValue(dataGridViewReicpitents.Columns[e.ColumnIndex].Tag.ToString(),
                    dataGridViewReicpitents.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                //Update database
                clsEmailAddr.update();
            }
            else
            {
                //Create new holder for the concatenated email addresses
                string concatEmails = "";

                //Traverse the Recipients dataGridView
                for (int i = 0; i < dataGridViewReicpitents.Rows.Count; i++)
                {
                    //If checkbox is checked
                    if (dataGridViewReicpitents.Rows[i].Cells["clmAddRecpt"].Value.ToString()
                        == "True")
                    {
                        //Add the email address to the concatenated list
                        concatEmails += dataGridViewReicpitents.Rows[i].Cells["clmEmailAddress"].Value.ToString()
                            + "|";
                    }
                }

                //Set new concatenated list in dataset
                clsMonthlyReports.SetDataValue("EmailAddresses", concatEmails);

                //Update database
                clsMonthlyReports.update();
            }
        }

        /// <summary>
        /// Triggers when the Value of a cell is changed AND the end edit is called or triggered
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReports_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //If not loading the values into the dataGridView
            if (loadingValues == false)
            {
                //Sets visibility level of recipients dataGridView off of if report is active or not
                dataGridViewReicpitents.Visible =
                        (bool)dataGridViewReports.Rows[dataGridViewReports.CurrentRow.Index].Cells
                        ["clmReportActive"].GetEditedFormattedValue(dataGridViewReports.CurrentRow.Index,
                        DataGridViewDataErrorContexts.Formatting);

                //Set value in dataset
                clsMonthlyReports.SetDataValue("ReportActive",
                    dataGridViewReicpitents.Visible.ToString());

                //Update the database
                clsMonthlyReports.update();
            }
        }

        /// <summary>
        /// Triggered when the mouse click is released inside a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewReports_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //If the column is the checkbox cell
            if (e.ColumnIndex == 0)
            {
                //End the edit to trigger cell value change event
                dataGridViewReports.EndEdit();
            }
        }

        private void dataGridViewReicpitents_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex != dataGridViewReicpitents.CurrentRow.Index)
            //{
            //    //End the edit to trigger cell value change event
            //    dataGridViewReicpitents.EndEdit();
            //}
        }

        private void dataGridViewReicpitents_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            clsEmailAddr.find(Convert.ToInt32(
                dataGridViewReicpitents.Rows[e.RowIndex].Cells["clmID"].Value));
        }

        private void dataGridViewReicpitents_Leave(object sender, EventArgs e)
        {
            //End the edit to trigger cell value change event
            dataGridViewReicpitents.EndEdit();
        }

        private void MonthlyReportPrefrences_FormClosing(object sender, FormClosingEventArgs e)
        {
            dataGridViewReicpitents.EndEdit();         
        }
    }
}
