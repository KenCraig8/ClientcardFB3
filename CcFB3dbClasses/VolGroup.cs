using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClientcardFB3
{
    public class VolGroup
    {
        int groupid;
        string groupname;
        List<int> volidList;
        Single totHrs;

        public VolGroup(int newGroupId, string newGroupName)
        {
            groupid = newGroupId;
            groupname = newGroupName;
            volidList = new List<int>();
            totHrs = 0;
        }

        #region Get/Set Accessors
        public int GroupID
        {
            get { return groupid; }
            set { groupid = value; }
        }
        public string GroupName
        {
            get { return groupname; }
            set { groupname = value; }
        }
        public int NbrVols
        {
            get { return volidList.Count; }
        }
        public int GetVolId(int index)
        {
            if (index >= 0 && index < volidList.Count)
                return volidList[index];
            else
                return -1;
        }
        public string GetVolIdString
        {
            get
            {
                string vList = "";
                foreach (int item in volidList)
                {
                    if (vList.Length >0)
                        vList += ",";
                    vList += item.ToString();
                }
                return vList;
            }
        }
        public Single VolHrs
        {
            get { return totHrs; }
            set { totHrs = value; }
        }

        #endregion Get/Set Accessors
        
        public int AddVolID(int newVolId)
        {
            for (int i = 0; i < volidList.Count; i++)
            {
                if (volidList[i] == newVolId)
                {
                    return i;
                }
            }
            volidList.Add(newVolId);
            return volidList.Count - 1;
        }

        public bool RemoveVolID(int tstVolId)
        {
            for (int i = 0; i < volidList.Count; i++)
            {
                if (volidList[i] == tstVolId)
                {
                    volidList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveVolAll()
        {
            volidList = new List<int>();
        }
    }
}
