
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace ClientCardFB3
{
public class FBJobsActuals
{
string connString;
SqlDataAdapter dadAdpt;
DataRow drow;
DataSet dset;
SqlCommand command;
System.Data.SqlClient.SqlConnection conn;
static string tbName = "FBJobsActuals";
public FBJobsActuals()
{
conn = new System.Data.SqlClient.SqlConnection();
connString =@"Data Source=localhost\SQLEXPRESS;initial catalog=ClientCardFB3; Integrated Security=SSPI";
conn.ConnectionString = connString;
dset = new DataSet();
}

#region Get/Set Accessors
public  int ID
{
get { return Convert.ToInt32(drow["ID"]); }
set { drow["ID"] = value; }
}
public  DateTime JobDate
{
get { return (DateTime)drow["JobDate"]; }
set { drow["JobDate"] = value; }
}
public  int JobPlanId
{
get { return Convert.ToInt32(drow["JobPlanId"]); }
set { drow["JobPlanId"] = value; }
}
public  int VolId
{
get { return Convert.ToInt32(drow["VolId"]); }
set { drow["VolId"] = value; }
}
public  string ShiftStart
{
get { return drow["ShiftStart"].ToString(); }
set { drow["ShiftStart"] = value; }
}
public  string ShiftEnd
{
get { return drow["ShiftEnd"].ToString(); }
set { drow["ShiftEnd"] = value; }
}
public float VolHours
{
get { return (float)drow["VolHours"]; }
set { drow["VolHours"] = value; }
}
public  int Status
{
get { return Convert.ToInt16(drow["Status"]); }
set { drow["Status"] = value; }
}
public  DateTime Created
{
get { return (DateTime)drow["Created"]; }
set { drow["Created"] = value; }
}
public  string CreatedBy
{
get { return drow["CreatedBy"].ToString(); }
set { drow["CreatedBy"] = value; }
}
public  DateTime Modified
{
get { return (DateTime)drow["Modified"]; }
set { drow["Modified"] = value; }
}
public  string ModifiedBy
{
get { return drow["ModifiedBy"].ToString(); }
set { drow["ModifiedBy"] = value; }
}
public  DateTime TimePosted
{
get { return (DateTime)drow["TimePosted"]; }
set { drow["TimePosted"] = value; }
}
public  string TimePostedBy
{
get { return drow["TimePostedBy"].ToString(); }
set { drow["TimePostedBy"] = value; }
}
#endregion Get/Set Accessors


public void open (System.Int32 key)
{
try
{
if(conn.State == ConnectionState.Closed)
{
conn.Open();
}
command = new SqlCommand("SELECT * FROM " + tbName + " WHERE ID=" + key.ToString(), conn);
dadAdpt = new SqlDataAdapter(command);
dadAdpt.SelectCommand = command;
dset.Clear();
dadAdpt.Fill(dset, tbName);
if (conn.State == ConnectionState.Open)
{
conn.Close();
}
}
catch (SqlException ex) { } 
}


public void delete(System.Int32 key)
{
SqlCommand commDelete = new SqlCommand(" DELETE FROM FBJobsActuals WHERE ID=" + key.ToString(), conn);
command = new SqlCommand("SELECT * FROM FBJobsActuals WHERE ID=" + key.ToString(), conn);
dadAdpt = new SqlDataAdapter(command);
dadAdpt.DeleteCommand = commDelete;
dadAdpt.SelectCommand = command;
 dset.Clear();
dadAdpt.Fill(dset, tbName);
dset.Tables[tbName].Rows[0].Delete();
dadAdpt.Update(dset, tbName);
}


public void update()
{
if(dset.HasChanges() == true)
{
try
{
conn.Open();
SqlCommand commUpdate = new SqlCommand("UPDATE FBJobsActuals SET JobDate=@JobDate, JobPlanId=@JobPlanId, VolId=@VolId, ShiftStart=@ShiftStart, ShiftEnd=@ShiftEnd, VolHours=@VolHours, Status=@Status, Created=@Created, CreatedBy=@CreatedBy, Modified=@Modified, ModifiedBy=@ModifiedBy, TimePosted=@TimePosted, TimePostedBy=@TimePostedBy WHERE ID=@ID", conn);
dadAdpt.UpdateCommand = commUpdate;
commUpdate.Parameters.Add("@ID", SqlDbType.Int,2, "ID");
commUpdate.Parameters.Add("@JobDate", SqlDbType.DateTime, 8, "JobDate");
commUpdate.Parameters.Add("@JobPlanId", SqlDbType.Int,4, "JobPlanId");
commUpdate.Parameters.Add("@VolId", SqlDbType.Int,4, "VolId");
commUpdate.Parameters.Add("@ShiftStart", SqlDbType.NVarChar, 10, "ShiftStart");
commUpdate.Parameters.Add("@ShiftEnd", SqlDbType.NVarChar, 10, "ShiftEnd");
commUpdate.Parameters.Add("@VolHours", SqlDbType.Real, 4, "VolHours");
commUpdate.Parameters.Add("@Status", SqlDbType.SmallInt, 2, "Status");
commUpdate.Parameters.Add("@Created", SqlDbType.DateTime, 8, "Created");
commUpdate.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 50, "CreatedBy");
commUpdate.Parameters.Add("@Modified", SqlDbType.DateTime, 8, "Modified");
commUpdate.Parameters.Add("@ModifiedBy", SqlDbType.NVarChar, 50, "ModifiedBy");
commUpdate.Parameters.Add("@TimePosted", SqlDbType.DateTime, 8, "TimePosted");
commUpdate.Parameters.Add("@TimePostedBy", SqlDbType.NVarChar, 50, "TimePostedBy");
dadAdpt.Update(dset, "FBJobsActuals");
conn.Close();
}
catch(SqlException ex){}
}
}
}
}

