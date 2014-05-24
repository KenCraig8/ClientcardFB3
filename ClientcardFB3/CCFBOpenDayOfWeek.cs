
using System;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;


namespace ClientcardFB3
{
    public static class CCFBOpenDayOfWeek
    {
        private static SqlCommand command2;
        private static SqlConnection conn;
        private static String tbName = "Defaults";
        private static Boolean bisValid = false;
        private static SqlDataAdapter dadAdptDOW = new SqlDataAdapter();
        private static DataSet dsetDOW = new DataSet();
        
    #region Get/Set Accessors
        
        public static Boolean isValid
        {
            get { return bisValid; } 
            set { bisValid = value; }
        }
        public static DataSet DSetDOW
        {
            get { return dsetDOW; }
            set { dsetDOW = value; }
        }
        public static Int64 DSetDOWRowsCount
        {
            get { return dsetDOW.Tables[tbName].Rows.Count; }
        }
    #endregion

        //public static void update()
        //{
        //    if (dset.HasChanges() == true)
        //    {
        //        if (dadAdpt.UpdateCommand == null)
        //        {
        //            command = new SqlCommand("UPDATE Defaults SET FldVal=@FldVal WHERE FldName=@FldName", conn);
        //            command.Parameters.Add("@FldName", SqlDbType.NVarChar, 100, "FldName");
        //            command.Parameters.Add("@FldVal", SqlDbType.NVarChar, 510, "FldVal");
        //            dadAdpt.UpdateCommand = command;
        //        }
        //        try
        //        {
        //            dadAdpt.Update(dset, tbName);
        //        }
        //        catch (SqlException ex) { }
        //    }
        //}

        public static Int64 OpenDOW()
        {
            try
            {
                if (conn == null)
                    conn = new SqlConnection(CCFBGlobal.connectionString);
                if (dadAdptDOW.SelectCommand == null)
                { dadAdptDOW = new SqlDataAdapter("SELECT * FROM " + tbName + " WHERE EditForm = 'OpenDOW' ORDER BY FldName", conn); }
                dsetDOW.Clear();
                return dadAdptDOW.Fill (dsetDOW, tbName);
            }
            catch (SqlException ex) { return 0; }
        }

        public static void updateDOW()
        {
            if (dsetDOW.HasChanges() == true)
            {
                try
                {
                    if (dadAdptDOW.UpdateCommand == null)
                    {
                        dadAdptDOW = new SqlDataAdapter("SELECT * FROM " + tbName + " WHERE EditForm = 'OpenDOW'", conn);
                        command2 = new SqlCommand("UPDATE Defaults SET FldVal=@FldVal WHERE FldName=@FldName", conn);
                        command2.Parameters.Add("@FldName", SqlDbType.NVarChar, 100, "FldName");
                        command2.Parameters.Add("@FldVal", SqlDbType.NVarChar, 510, "FldVal");
                        dadAdptDOW.UpdateCommand = command2;
                    }
                    dadAdptDOW.Update(dsetDOW, tbName);
                }
                catch (SqlException ex) { }
            }
        }

        public static int findRow(string dow)
        {
            for (int i = 0; i < dsetDOW.Tables[0].Rows.Count; i++)
            {
                if (dsetDOW.Tables[0].Rows[i].Field<string>("FldName").Trim().ToLower() == ("Cycle" + dow.Trim()).ToLower())
                {
                    return i;
                }
            }
            return 0;
        }
    }
}

