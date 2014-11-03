using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClientcardFB3
{
    class ServiceItem
    {
        DataRow drow;
        bool isSelected = false;
        
        //used as an index to the first FamilySizeMultiplyer in datarow
        const int fsmBase = 5; 

        public ServiceItem(DataRow drowIn)
        {
            drow = drowIn;
            isSelected = false;
        }

        #region Get/Set Accessors

        public string Description
        {
            get { return drow["ItemDesc"].ToString(); }
            set { drow["Description"] = value; }
        }
        public int Rule
        {
            get { return Convert.ToInt32(drow["ItemRule"]); }
            set { drow["Rule"] = value; }
        }
        public int ItemKey
        {
            get { return Convert.ToInt32(drow["ItemKey"]); }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public int LbsPerItem
        {
            get { return Convert.ToInt32(drow["LbsPerItem"]); }
            set { drow["LbsPerItem"] = value; }
        }
        public int ItemType
        {
            get { return Convert.ToInt32(drow["ItemType"]); }
            set { drow["ItemType"] = value; }
        }
        public bool NotAvailable
        {
            get { return (bool)drow["NotAvailable"]; }
            set { drow["NotAvailable"] = value; }
        }
        #endregion

        public int getFamSizeMultiplyer(int fSize)
        {
            if (fSize != 0)
                return Convert.ToInt32(drow[fSize + fsmBase]);
            else
                return 0;
        }

        public int getFamSizeLbs(int fSize)
        {
            if (fSize != 0)
                return  Convert.ToInt32(drow[fSize + fsmBase]) * LbsPerItem;
            else
                return 0;
        }
    }
}
