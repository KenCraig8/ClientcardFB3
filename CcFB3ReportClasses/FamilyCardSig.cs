using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    public class FamilyCardSig : IDisposable
    {
        bool haveSig = false;
        int sigUID;
        int sigHHId;
        DateTime sigDate;
        string docPath;
        Image sigImage;
        string sigStr;
        DateTime sigCreated;
        string sigCreatedBy = "";

        string sigconnString = "";
        string textInsert = "INSERT FamilyCardSig (HhID, SigDate, DocPath, SigImage, SigString, Created, CreatedBy)"
                          + "VALUES (@HhID, @SigDate, @DocPath, @SigImg, @SigStr, @Created, @CreatedBy)";
        string textLoad = "SELECT * FROM FamilyCardSig WHERE UID = @UID";
        string textUpdate = "UPDATE FamilyCardSig Set HhID = @HhID, SigDate = @SigDate, DocPath = @DocPath"
                          + ", SigImage = @SigImg, SigString = @SigStr, Created = GetDate(), CreatedBy = @CreatedBy "
                          + " WHERE UID = @UID";
        SqlCommand sqlInsertCmd;
        SqlCommand sqlLoadCmd;
        SqlConnection sqlConn;
        private bool _disposed;
        
        public FamilyCardSig(string connString)
        {
            sigconnString = connString;
            sigUID = 0;
            sigHHId = 0;
            sigImage = null;
            sqlConn = new SqlConnection(sigconnString);
            sqlInsertCmd = new SqlCommand(textInsert, sqlConn);
            //sqlInsertCmd.Parameters.Add("@UID", SqlDbType.Int);
            sqlInsertCmd.Parameters.Add("@HhID", SqlDbType.Int);
            sqlInsertCmd.Parameters.Add("@SigDate", SqlDbType.DateTime);
            sqlInsertCmd.Parameters.Add("@DocPath", SqlDbType.VarChar, 500);
            sqlInsertCmd.Parameters.Add("@SigImg", SqlDbType.VarBinary, -1);
            sqlInsertCmd.Parameters.Add("@SigStr", SqlDbType.VarChar, -1);
            sqlInsertCmd.Parameters.Add("@Created", SqlDbType.DateTime);
            sqlInsertCmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 50);

            sqlLoadCmd = new SqlCommand(textLoad, sqlConn);
            sqlLoadCmd.Parameters.Add("@UID", SqlDbType.Int);
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
                    if (sqlConn != null)
                        sqlConn.Dispose();
                    if (sigImage != null)
                        sigImage.Dispose();
                    if (sqlInsertCmd != null)
                        sqlInsertCmd.Dispose();
                    if (sqlLoadCmd != null)
                        sqlLoadCmd.Dispose();
                }

                // Indicate that the instance has been disposed.
                sqlConn = null;
                sigImage = null;
                sqlInsertCmd = null;
                sqlLoadCmd = null;
                _disposed = true;
            }
        }

        public bool HaveSignature
        {
            get { return haveSig; }
        }

        public int UID
        {
            get { return sigUID; }
            set { sigUID = value; }
        }
        public int HhID
        {
            get { return sigHHId; }
            set { sigHHId = value; }
        }
        public DateTime SigDate
        {
            get { return sigDate; }
            set { sigDate = value; }
        }
        public string DocPath
        {
            get { return docPath; }
            set { docPath = value; }
        }
        public Image SigImage
        {
            get { return sigImage; }
            set { sigImage = value; }
        }
        public string SigString
        {
            get { return sigStr; }
            set { sigStr = value; }
        }
        public DateTime Created
        {
            get { return sigCreated; }
            set { sigCreated = value; }
        }
        public string CreatedBy
        {
            get { return sigCreatedBy; }
            set { sigCreatedBy = value; }
        }
        public Boolean Insert()
        {
            //sqlInsertCmd.Parameters["@UID"].Value = sigUID;
            sqlInsertCmd.Parameters["@HhID"].Value = sigHHId;
            sqlInsertCmd.Parameters["@SigDate"].Value = sigDate.ToShortDateString();
            sqlInsertCmd.Parameters["@DocPath"].Value = docPath;
            sqlInsertCmd.Parameters["@SigImg"].Value = CCFBGlobal.imageToByteArray(sigImage);
            sqlInsertCmd.Parameters["@SigStr"].Value = sigStr;
            sqlInsertCmd.Parameters["@Created"].Value = DateTime.Now;
            sqlInsertCmd.Parameters["@CreatedBy"].Value = CCFBGlobal.dbUserName;
            sqlConn.Open();
            int retVal = sqlInsertCmd.ExecuteNonQuery();
            sqlConn.Close();
            return (retVal == 1);
        }

        public Boolean LoadImage(int newUID, int newHhId)
        {
            initSigFields(newUID, newHhId);
            sqlLoadCmd.Parameters["@UID"].Value = newUID;
            sqlConn.Open();
            SqlDataReader reader = sqlLoadCmd.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                sigUID = Convert.ToInt32(reader["UID"]);
                sigHHId = Convert.ToInt32(reader["HhID"]);
                sigDate = Convert.ToDateTime(reader["SigDate"]);
                docPath = reader["DocPath"].ToString();
                byte[] imgData = (byte[])reader["SigImage"];
                sigImage = CCFBGlobal.byteArrayToImage(imgData);
                sigStr = reader["SigString"].ToString();
                sigCreated = Convert.ToDateTime(reader["Created"]);
                sigCreatedBy = reader["CreatedBy"].ToString();
                count++;
            }
            haveSig = (count == 1);
            sqlConn.Close();
            return haveSig;
        }

        public void initSigFields(int newUID, int newHhId)
        {
            haveSig = false;
            UID = newUID;
            HhID = newHhId;
            SigImage = null;
            SigString = "";
            Created = CCFBGlobal.FBNullDateValue;
            CreatedBy = "";
        }

        public void Update()
        {
            SqlCommand sqlUpdateCmd = new SqlCommand(textUpdate, sqlConn);
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@HhID", sigHHId));
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@SigDate",sigDate));
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@DocPath",docPath));
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@SigImg", sigImage));
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@SigStr", sigStr));
            sqlUpdateCmd.Parameters.Add(new SqlParameter("@CreatedBy", CCFBGlobal.dbUserName));
            sqlUpdateCmd.ExecuteNonQuery();
            sqlUpdateCmd.Dispose();
        }
    }
}
