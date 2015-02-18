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
    public partial class DeleteHousehold : Form
    {
        Client clsClientToDelete;
        Client clsClientToTransfer;
        int idMoveTo = 0;
        List<PictureBox> pbxList = new List<PictureBox>();
        List<CheckBox> chkListDelClient = new List<CheckBox>();
        List<CheckBox> chkListMoveToClient = new List<CheckBox>();
        bool bClientDeleted = false;
        bool bClientMarkedInactive = false;

        public DeleteHousehold(int idToDelete)
        {
            InitializeComponent();
            CCFBGlobal.InitCombo(cboClientType, CCFBGlobal.parmTbl_Client);
            CCFBGlobal.InitCombo(cboIDType, CCFBGlobal.parmTbl_AddressID);
            CCFBGlobal.InitCombo(cboPhoneType, CCFBGlobal.parmTbl_Phone);

            CCFBGlobal.InitCombo(cboClientMoveTo, CCFBGlobal.parmTbl_Client);
            CCFBGlobal.InitCombo(cboIDTypeMoveTo, CCFBGlobal.parmTbl_AddressID);
            CCFBGlobal.InitCombo(cboPhoneTypeMoveTo, CCFBGlobal.parmTbl_Phone);

            clsClientToDelete = new Client();
            clsClientToDelete.connectionString = CCFBGlobal.connectionString;
            clsClientToDelete.open(idToDelete, true, false);
            clsClientToDelete.clsHHSvcTrans.IncludeAppointments = false;
            clsClientToDelete.clsHHSvcTrans.openForHH(clsClientToDelete.clsHH.ID);
            grpbxClientToMoveTo.Enabled = false;
            btnLoadClient.Enabled = false;
            foreach (PictureBox pbx in grpbxClientToDelete.Controls.OfType<PictureBox>())
            {
                pbx.Image = imageList1.Images[1];
                pbx.Image.Tag = "";
                pbxList.Add(pbx);
            }
            foreach (CheckBox chk in grpbxClientToDelete.Controls.OfType<CheckBox>())
            {
                chkListDelClient.Add(chk);
            }
            foreach (CheckBox chk in grpbxClientToMoveTo.Controls.OfType<CheckBox>())
            {
                chkListMoveToClient.Add(chk);
            }

            ClearMoveToClient();
            ShowClientData(clsClientToDelete, grpbxClientToDelete);
            if (clsClientToDelete.clsHHSvcTrans.RowCount > 0)
            {
                btnDeleteClient.Enabled = false;
                tbClientId.Enabled = true;
            }
            else
            {
                btnDeleteClient.Enabled = true;
                tbClientId.Enabled = true;
                //tbClientId.Visible = false;
                //btnLoadClient.Visible = false;
                //lblClientId.Visible = false;
                //grpbxClientToMoveTo.Visible = false;
            }
        }

        public bool ClientDeleted()
        {
            return bClientDeleted;
        }

        public bool ClientMarkedInactive()
        {
            return bClientMarkedInactive;
        }

        private void ShowClientData(Client clsClient, GroupBox grpbx)
        {
            foreach (Object obj in grpbx.Controls)
            {
                if (obj.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    TextBox tb = (TextBox)obj;
                    if (tb.Tag != null && tb.Tag.ToString().Length >0)
                    {
                        tb.Text = clsClient.clsHH.GetDataString(tb.Tag.ToString()).ToString();
                        tb.BackColor = Color.White;
                    }
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.CheckBox")
                {
                    CheckBox chk = (CheckBox)obj;
                    if (chk.Tag != null && chk.Tag.ToString().Length >0)
                    {
                        chk.Checked = Convert.ToBoolean(clsClient.clsHH.GetDataString(chk.Tag.ToString()));
                    }
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.ListView")
                {
                    ListView lvw = (ListView)obj;
                    lvw.Items.Clear();
                    if (lvw.Name.Substring(0,6) == "lvwHHM")
                    {
                        for (int i = 0; i < clsClient.clsHHmem.RowCount; i++)
                        {
                            ListViewItem lvItm = new ListViewItem();
                            clsClient.clsHHmem.SetRecord(i);
                            lvItm.Text = clsClient.clsHHmem.FirstName + " " + clsClient.clsHHmem.LastName;
                            lvItm.Tag = clsClient.clsHHmem.ID;
                            lvw.Items.Add(lvItm);
                        }
                    }
                    else if (lvw.Name.Substring(0,6) == "lvwTrx")
                    {
                        for (int i = 0; i < clsClient.clsHHSvcTrans.RowCount; i++)
                        {
                            ListViewItem lvItm = new ListViewItem();
                            clsClient.clsHHSvcTrans.setDataRow(i);
                            lvItm.Text = clsClient.clsHHSvcTrans.TrxDate.ToShortDateString(); 
                            lvItm.Tag = clsClient.clsHHSvcTrans.TrxId;
                            lvw.Items.Add(lvItm);
                        }
                    }
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    ComboBox cb = (ComboBox)obj;
                    cb.SelectedValue = clsClient.clsHH.GetDataValue(cb.Tag.ToString()).ToString();
                    cb.Enabled = false;
                }
                grpbx.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bClientMarkedInactive = false;
            bClientDeleted = false;
            idMoveTo = 0;
            this.Hide();
        }

        private void tbClientId_TextChanged(object sender, EventArgs e)
        {
            btnLoadClient.Enabled = (tbClientId.Text.Length >0);
        }

        private void btnLoadClient_Click(object sender, EventArgs e)
        {
            if (tbClientId.Text.Length >0)
            {
                idMoveTo = Convert.ToInt32(tbClientId.Text);
                if (idMoveTo == 0)
                {
                    MessageBox.Show("Client Id MUST be greater than Zero");
                    ClearMoveToClient();
                }
                else if (idMoveTo == clsClientToDelete.clsHH.ID)
                {
                    MessageBox.Show("Client Id [" + idMoveTo.ToString() + "] MUST be different than the Client Id that is being Deleted");
                    ClearMoveToClient();
                }
                else
                {
                    clsClientToTransfer = new Client(CCFBGlobal.connectionString);
                    clsClientToTransfer.clsHHSvcTrans.IncludeAppointments = false;
                    clsClientToTransfer.open(idMoveTo, true, true);
                    grpbxClientToMoveTo.BackColor = Color.DarkSeaGreen;
                    ShowClientData(clsClientToTransfer, grpbxClientToMoveTo);
                    foreach (PictureBox pbx in pbxList)
                    {
                        pbx.Visible = true;
                    }
                    btnDeleteClient.Enabled = true;
                    btnClearClient.Enabled = true; 
                }
            }
        }

        private void tbClientId_KeyDown(object sender, KeyEventArgs e)
        {
            CCFBGlobal.checkForIntOnKeyPress(e);
        }

        private void pbxList_Click(object sender, EventArgs e)
        {
            PictureBox pbx = (PictureBox)sender;
            if (pbx.Image.Tag != null && pbx.Image.Tag.ToString() != "set")
            {
                pbx.Image = imageList1.Images[0];
                pbx.Image.Tag = "set";
            }
            else
            {
                pbx.Image = imageList1.Images[1];
                pbx.Image.Tag = "";
            }
        }

        private void btnClearClient_Click(object sender, EventArgs e)
        {
            ClearMoveToClient();
        }
        private void ClearMoveToClient()
        {
            tbClientId.Text = ""; 
            foreach (TextBox tb in grpbxClientToMoveTo.Controls.OfType<TextBox>())
            {
                tb.Text = "";
                tb.BackColor = Color.LightGray; 
            }
            foreach (CheckBox tb in grpbxClientToMoveTo.Controls.OfType<CheckBox>())
            {
                chkBabyServices.Checked = false;
            }
            lvwHHMDest.Items.Clear();
            lvwTrxLogDest.Items.Clear();
            grpbxClientToMoveTo.BackColor = Color.LightGray;
            foreach (PictureBox pbx in pbxList)
            {
                pbx.Visible = false;
            }
            btnClearClient.Enabled = false;
        }

        private void chkBox_MouseEnter(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Enabled = false;
        }

        private void chkBox_MouseLeave(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            chk.Enabled = true;
        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            bool bNeedToUpdate = false;
            if (idMoveTo != 0)  // Move Data to Alternate Client Id
            {
                if (clsClientToTransfer.clsHH.ID == idMoveTo)
                {
                    foreach (PictureBox pbx in pbxList)
                    {
                        if (pbx.Image.Tag != null && pbx.Image.Tag.ToString() == "set" && pbx.Tag.ToString() !="")
                        {
                            clsClientToTransfer.clsHH.SetDataValue(pbx.Tag.ToString(), clsClientToDelete.clsHH.GetDataValue(pbx.Tag.ToString()).ToString());
                            bNeedToUpdate = true;
                        }
                    }
                    if (bNeedToUpdate == true)
                        clsClientToTransfer.clsHH.update(true);

                    foreach (ListViewItem item in lvwHHMSrc.Items)
                    {
                        if (item.Checked == true)
                        {
                            int idHHM = Convert.ToInt32(item.Tag);
                            clsClientToDelete.clsHHmem.MoveToHH(idHHM, idMoveTo);
                        }
                    }
                    foreach (ListViewItem item in lvwTrxLogSrc.Items)
                    {
                        int idTrxLog = Convert.ToInt32(item.Tag);
                        clsClientToDelete.clsHHSvcTrans.MoveToHH(idTrxLog, idMoveTo);
                    }
                    bClientDeleted = true;
                }
                else
                {
                    MessageBox.Show("Serious Problem");
                }
            }
            else
            {
                bClientDeleted = true;
            }
            if (bClientDeleted == true)
            {
                clsClientToDelete.clsHHSvcTrans.deleteAllHHSvcTrans(clsClientToDelete.clsHH.ID);
                clsClientToDelete.clsHHmem.deleteAllForHousehold(clsClientToDelete.clsHH.ID);
                clsClientToDelete.clsHH.delete(clsClientToDelete.clsHH.ID);
                this.Hide();
            }
        }

        public int ClientIdMovedTo()
        {
            return idMoveTo;
        }

        private void btnMarkInactive_Click(object sender, EventArgs e)
        {
            bClientDeleted = false;
            bClientMarkedInactive = true;
            idMoveTo = 0;
            clsClientToDelete.clsHH.Inactive = true;
            clsClientToDelete.clsHH.update(true);
            this.Hide();
        }
    }
}
