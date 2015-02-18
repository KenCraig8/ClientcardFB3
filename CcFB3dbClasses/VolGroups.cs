using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClientcardFB3
{
    public class VolGroups : IDisposable
    {
        List<VolGroup> VolGrps = new List<VolGroup>();
        SqlConnection conn;
        //bool isValid = false;
        int volgrpindex = 0;
        private bool _disposed;
        
        public VolGroups(string connStringIn)
        {
            int iNbrRows = 0;
            conn = new SqlConnection();
            conn.ConnectionString = connStringIn;
            foreach (parmType item in CCFBGlobal.TypeCodesArray(CCFBGlobal.parmTbl_VolGroups))
            {
                VolGroup vgrp = new VolGroup(item.ID, item.TypeName);
                VolGrps.Add(vgrp);
                iNbrRows++;
            }
            //SqlCommand command = new SqlCommand("SELECT * FROM parm_VolunteerGroups ORDER BY [Type]", conn);
            //SqlDataAdapter dadAdpt = new SqlDataAdapter();
            //dadAdpt.SelectCommand = command;
            //DataSet dset = new DataSet();
            //iNbrRows = dadAdpt.Fill(dset);            
            //foreach (DataRow drow in dset.Tables[0].Rows)
            //{
            //    VolGroup vgrp = new VolGroup(drow.Field<int>("Id"), drow.Field<string>("Type"));
            //    VolGrps.Add(vgrp);
            //}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (conn != null)
                        conn.Dispose();
                }

                // Indicate that the instance has been disposed.
                conn = null;
                _disposed = true;
            }
        }

        #region Get/Set Accessors
        public int NbrGroups
        {
            get { return VolGrps.Count; }
        }
        public int GroupIndex
        {
            get { return volgrpindex; }
            set { volgrpindex = value; }
        }
        public VolGroup VolunGroup
        {
            get { return VolGrps[volgrpindex]; }
            set { VolGrps[volgrpindex] = value; }
        }

        #endregion

        public bool LoadAllMembers()
        {
            SqlDataAdapter dadAdpt = new SqlDataAdapter();
            DataSet dset = new DataSet();
            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            foreach (VolGroup vgrp in VolGrps)
            {
                vgrp.RemoveVolAll();

                command.CommandText = "SELECT *"
                                    + "  FROM VolunteerGroups "
                                    + " WHERE GroupId = " + vgrp.GroupID.ToString()
                                    + " ORDER BY VolId";
                dset.Clear();
                dadAdpt.SelectCommand = command;
                dadAdpt.Fill(dset);
                int grpid = vgrp.GroupID;
                foreach (DataRow drow in dset.Tables[0].Rows)
                {
                    if (grpid == drow.Field<int>("GroupId"))
                        vgrp.AddVolID(drow.Field<int>("VolId"));
                }
            }
            return true;
        }

        public void LoadAllGroupsForVolunteer(int VolId)
        {

        }
    }
}
