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
    public partial class TypeCodeChangeForm : Form
    {
        int selectedID = 0;
        int idToIgnore = 0;
        int rowIndex = 0;
        string parmTableName = "";
        bool canceled = false;

        parmTypeCodes clsParmTypeCodes;
        System.Collections.ArrayList arrayList;

        public TypeCodeChangeForm(string tableName, int IdToIgnore)
        {
            InitializeComponent();
            idToIgnore = IdToIgnore;
            parmTableName = tableName;
            clsParmTypeCodes = new parmTypeCodes(tableName, CCFBGlobal.connectionString, "");
            arrayList = clsParmTypeCodes.TypeCodesArray;
            loadList();
        }

        public int RowIndex
        {
            get { return dgvTypeCodes.CurrentRow.Index; }
        }
        public int SelectedID
        {
            get{ return selectedID; }
        }
        public bool Canceled
        {
            get { return canceled; }
        }

        private void loadList()
        {
            int row = 0;
            for (int i = 0; i < arrayList.Count; i++)
            {
                if (idToIgnore != ((parmType)arrayList[i]).ID)
                {
                    dgvTypeCodes.Rows.Add();
                    dgvTypeCodes["clmID", row].Value = ((parmType)arrayList[i]).ID;
                    dgvTypeCodes["clmDescription", row].Value = ((parmType)arrayList[i]).LongName;
                    dgvTypeCodes["clmShortName", row].Value = ((parmType)arrayList[i]).ShortName;
                    dgvTypeCodes["clmSortOrder", row].Value = ((parmType)arrayList[i]).SortOrder;
                    row++;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectedID = Convert.ToInt32(dgvTypeCodes.CurrentRow.Cells["clmID"].Value);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }
    }
}
