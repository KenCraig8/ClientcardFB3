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
    public partial class SelectDateRange : Form
    {
        private enum dtRangeType
        {
            CurFiscal = 0, CurCal = 1, PrevFiscal =2, PrevCal = 3, Custom = 4
        }
        public SelectDateRange()
        {
            InitializeComponent();
            setDateRange(0);
        }

        private void rdoRangeType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = (RadioButton)sender;
            if (rdo.Checked == true)
            {
                setDateRange((dtRangeType)Convert.ToInt32(rdo.Tag));
            }
        }

        private void setDateRange(dtRangeType rangeType)
        {
            dtpFirst.Enabled = false;
            dtpLast.Enabled = false;
            switch (rangeType)
            {
                case dtRangeType.CurFiscal:
                    dtpFirst.Value = CCFBGlobal.CurrentFiscalStartDate();
                    dtpLast.Value = DateTime.Today;
                    break;
                case dtRangeType.CurCal:
                    dtpFirst.Value = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());
                    dtpLast.Value = DateTime.Today;
                    break;
                case dtRangeType.PrevFiscal:
                    dtpFirst.Value = CCFBGlobal.PreviousFiscalStartDate();
                    dtpLast.Value = CCFBGlobal.PreviousFiscalEndDate();
                    break;
                case dtRangeType.PrevCal:
                    dtpFirst.Value = Convert.ToDateTime("01/01/" + DateTime.Now.AddYears(-1).Year.ToString());
                    dtpLast.Value = Convert.ToDateTime("12/31/" + DateTime.Now.AddYears(-1).Year.ToString());
                    break;
                default:
                    dtpFirst.Enabled = true;
                    dtpLast.Enabled = true;
                    break;
            }
        }

        public DateTime FirstDate()
        {
            return dtpFirst.Value;
        }

        public DateTime LastDate()
        {
            return dtpLast.Value;
        }
    }
}
