using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms; 

namespace ClientcardFB3
{
    class ListViewColumnSorter : IComparer
    {
        /// <summary>
	/// Specifies the column to be sorted
	/// </summary>
	private int ColumnToSort;
	/// <summary>
	/// Specifies the order in which to sort (i.e. 'Ascending').
	/// </summary>
	private SortOrder OrderOfSort;
	/// <summary>
	/// Case insensitive comparer object
	/// </summary>
	private CaseInsensitiveComparer ObjectCompare;

	/// <summary>
	/// Class constructor.  Initializes various elements
	/// </summary>
	public ListViewColumnSorter()
	{
		// Initialize the column to '0'
		ColumnToSort = 0;

		// Initialize the sort order to 'none'
		OrderOfSort = SortOrder.None;

		// Initialize the CaseInsensitiveComparer object
		ObjectCompare = new CaseInsensitiveComparer();
	}

	/// <summary>
	/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
	/// </summary>
	/// <param name="x">First object to be compared</param>
	/// <param name="y">Second object to be compared</param>
	/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
	public int Compare(object x, object y)
	{
        if (!(x is ListViewItem))
            return (0);
        if (!(y is ListViewItem))
            return (0);
        if (OrderOfSort == SortOrder.None)
            return (0);

        ListViewItem l1 = (ListViewItem)x;
        ListViewItem l2 = (ListViewItem)y;

        float fl1 = 0;
        float fl2 = 0;


        if (l1.ListView.Columns[ColumnToSort].Tag == null)
        {
            l1.ListView.Columns[ColumnToSort].Tag = "Text";
        }

        if (l1.ListView.Columns[ColumnToSort].Tag.ToString() == "Numeric")
        {
            if (String.IsNullOrEmpty(l1.SubItems[ColumnToSort].Text)  == true && String.IsNullOrEmpty(l2.SubItems[ColumnToSort].Text)  == true)
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(l1.SubItems[ColumnToSort].Text)  == true)
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(l2.SubItems[ColumnToSort].Text) == true)
            {
                return 1;
            }
            else
            {
                fl1 = float.Parse(l1.SubItems[ColumnToSort].Text);
                fl2 = float.Parse(l2.SubItems[ColumnToSort].Text);
            }

            if (Order == SortOrder.Ascending)
            {
                return fl1.CompareTo(fl2);
            }
            else
            {
                return fl2.CompareTo(fl1);
            }
        }
        else
        {
            //try
            //{
                string str1 = l1.SubItems[ColumnToSort].Text;
                string str2 = l2.SubItems[ColumnToSort].Text;

                if (Order == SortOrder.Ascending)
                {
                    return str1.CompareTo(str2);
                }
                else
                {
                    return str2.CompareTo(str1);
                }
            //}
            //catch
            //{
            //    return 0;
            //}
        }
	}
    
	/// <summary>
	/// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
	/// </summary>
	public int SortColumn
	{
		set
		{
			ColumnToSort = value;
		}
		get
		{
			return ColumnToSort;
		}
	}

	/// <summary>
	/// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
	/// </summary>
	public SortOrder Order
	{
		set
		{
			OrderOfSort = value;
		}
		get
		{
			return OrderOfSort;
		}
	}
    }
}
