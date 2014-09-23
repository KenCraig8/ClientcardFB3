using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    public class UserFieldItem
    {
        private int fldindex;
        private string editlabel;
        private string fldname;

        public UserFieldItem(int fldIndex, string editLabel, string fldName)
        {
            fldindex = fldIndex;
            editlabel = editLabel;
            fldname = fldName;
        }

        public int FldIndex
        {
            get { return fldindex; }
            set { fldindex = value; }
        }
        public string EditLabel
        {
            get { return editlabel; }
            set { editlabel = value; }
        }
        public string FldName
        {
            get { return fldname; }
            set { fldname = value; }
        }
        public string LongName
        {
            get { return editlabel; }
        }
        public string Index
        {
            get { return fldindex.ToString(); }
        }
    }
}
