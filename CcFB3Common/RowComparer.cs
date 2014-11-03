using System;
using System.Windows.Forms;

namespace ClientcardFB3
{
    class RowComparer : System.Collections.IComparer
    {

        private static int sortOrderModifier = 1;
        private string columnToSort;
        private string objType;
        SortOrder sortOrder;

        public RowComparer(SortOrder sortOrderIn, string column, string objTypeIn)
        {
            columnToSort = column;
            objType = objTypeIn;
            sortOrder = sortOrderIn;
        }

        public int Compare(object x, object y)
        {
            DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
            DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

            if (objType == "Int")
            {
                float fl1 = 0;
                float fl2 = 0;

                if (DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim() == ""
                    && DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 0;
                }
                else if (DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 0;
                }
                else if (DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 1;
                }
                else
                {
                    fl1 = float.Parse(DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim());
                    fl2 = float.Parse(DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim());
                }

                if (sortOrder == SortOrder.Ascending)
                {
                    return fl1.CompareTo(fl2);
                }
                else
                {
                    return fl2.CompareTo(fl1);
                }
            }
            else if (objType == "DateTime")
            {
                DateTime date1;
                DateTime date2;

                if (DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim() == ""
                    && DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 0;
                }
                else if (DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 0;
                }
                else if (DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim() == "")
                {
                    return 1;
                }
                else
                {
                    date1 = DateTime.Parse(DataGridViewRow1.Cells[columnToSort].Value.ToString().Trim());
                    date2 = DateTime.Parse(DataGridViewRow2.Cells[columnToSort].Value.ToString().Trim());
                }

                int compare = date1.CompareTo(date2);

                if (sortOrder == SortOrder.Ascending)
                {
                    return compare;
                    //return date1.CompareTo(date2);
                }
                else
                {
                    return compare * -1;
                    //return date2.CompareTo(date1);
                }
            }
            else
            {
                // Try to sort based on the Last Name column.
                int CompareResult = System.String.Compare(
                        DataGridViewRow1.Cells[columnToSort].Value.ToString(),
                        DataGridViewRow2.Cells[columnToSort].Value.ToString());

                if (sortOrder == SortOrder.Ascending)
                {
                    return CompareResult;
                }
                else
                {
                    return CompareResult * -1;
                }
            }
        }
    }
}
