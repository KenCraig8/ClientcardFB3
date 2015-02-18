//*************************************************************************************************
//
// This class provides basic database operations using the .NET DataSet object. Functionality
// provided includes:
//
//		*	Basic SQL database operations (open, close, etc.)
//		*	(future) Create/Delete SQL database.
//		*	(future) Create/Delete database table.
//		*	(future) Database import/export.
//
// This class requires SQL connection details. The application must supply either a SQL Connection
// string or the Connection fields.
//
//		1.	Instantiate the object either by itself or use this as a base class.
//		2.  Supply the SQL Connection details using ConnectionString or ConnectionStringCreate.
//		3.	Open the SQL database and create a dataset using DbOpen(tableName,{sqlQuery}).
//
//=================================================================================================
//
// Date			Version		Author				Description
// ----------	-------		------------------	--------------------------------------------------
// 2010-11-09	00.01.00	T. Cataldo			Moved code from SqlGridDataSet to here.
//
//*************************************************************************************************

using System;
using System.Data;											// For DataSet, etc.
using System.Data.SqlClient;								// For SqlConnection, SqlDataAdapter, etc.
using System.Windows.Forms;


namespace ClientcardFB3
{
	public class SqlBaseDataSet : Form
	{
		#region ----------Structures----------

		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// This structure defines the columns in the data record and the input field and grid
		/// column used to display the database column field data.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public struct TYPE_FIELD_MAP
		{
			public string [] m_dbColumn;					// List of the column names in the DB.
			public string [] m_gridColumn;					// Display data in this grid column.
			public string [] m_uiFieldName;					// The edit field to display/edit data.
			public Control[] m_uiControl;					// The UI control object on the form.


			//-------------------------------------------------------------------------------------
			/// <summary>
			/// Add a field/grid mapping for the database column.
			/// </summary>
			/// <param name="?"></param>
			/// <returns></returns>
			//-------------------------------------------------------------------------------------
			public bool AddGridColumn (string a_dbColumn, string a_gridColumn)
			{
				if (m_dbColumn != null)
				{
					for (int i=0;  i<m_dbColumn.Length;  i++)
						if (m_dbColumn[i] == a_dbColumn)
						{
							m_gridColumn[i] = a_gridColumn;
							return (true);
						}
				}
				return (false);
			}		// end of AddGridColumn


			//-------------------------------------------------------------------------------------
			/// <summary>
			/// Add a field/grid mapping for the database column.
			/// </summary>
			/// <param name="?"></param>
			/// <returns></returns>
			//-------------------------------------------------------------------------------------
			public bool AddControl (string a_dbColumn, string a_uiFieldName, Control a_uiControl)
			{
				if (m_dbColumn != null)
				{
					for (int i=0;  i<m_dbColumn.Length;  i++)
						if (m_dbColumn[i] == a_dbColumn)
						{
							m_uiFieldName[i]= a_uiFieldName;
							m_uiControl[i]	= new Control ();
							m_uiControl[i]	= a_uiControl;
							return (true);
						}
				}
				return (false);
			}		// end of AddControl


			//-------------------------------------------------------------------------------------
			/// <summary>
			/// Return the number of DB columns defined in this database table row.
			/// </summary>
			//-------------------------------------------------------------------------------------
			public int Count
			{
				get
				{
					if (m_dbColumn == null)
						return (-1);
					return (m_dbColumn.Length);
				}
			}		// end of property Count


			//-------------------------------------------------------------------------------------
			/// <summary>
			/// Add a field/grid mapping for the database column.
			/// </summary>
			/// <param name="?"></param>
			/// <returns></returns>
			//-------------------------------------------------------------------------------------
			public void New (int a_noColumns)
			{
				m_dbColumn	 = new string [a_noColumns];
				m_gridColumn = new string [a_noColumns];
				m_uiControl	 = new Control[a_noColumns];
				m_uiFieldName= new string [a_noColumns];
				for (int i=0;  i<a_noColumns;  i++)			// Allocate an empty string for each
				{											//   element in the mapping tables.
//					m_dbColumn   [i] = "";
//					m_gridColumn [i] = "";
//					m_uiFieldName[i] = "";
				}
			}		// end of Add
		}		// end of TYPE_FIELD_MAP

		#endregion


		#region ----------Variables----------
		/// <summary>
		/// The dataset that acts as the buffer between the database and the user interface code.
		/// Technically, the DataSet is the buffer to the DataAdapter and the DataAdapter provides
		/// the interface to the database.
		/// </summary>
		public DataSet m_dataSet;

		/// <summary>
		/// The connection string to access the SQL database. This string is in the format:
		/// 
		///		Server={server};Database={dbName};User ID={logon};Password={password};Trusted_Connection=True;
		/// 
		/// Set the connection string using the ConnectionString property or method.
		/// </summary>
		private string m_dbConnectionString = "";

		/// <summary>
		/// The name of the SQL table that this class will operate upon. When exporting the data
		/// to XML this value is also used as the XML record header. This value MUST be set prior
		/// to performing any database operations. Use the SqlTable property to get/set this
		/// value. It is initialized to an obviously wrong value to aid in debugging.
		/// </summary>
		private string m_dbTableName = "{BadDbName in SqlBase.cs}";

		/// <summary>
		/// This structure contains a mapping from the database columns to the edit fields and
		/// DataGridView columns. This field/column mapping allows the class to move data between
		/// the grid, display fields, and the columns in the DataSet.
		/// </summary>
		public TYPE_FIELD_MAP m_map = new TYPE_FIELD_MAP ();

		/// <summary>
		/// The text of the last error generated by one of the higher-level methods. For example,
		/// if the .Open method is called and fails then this value contains the text of the error
		/// encountered by the .Open method.
		/// </summary>
		private string m_lastError = "";

		/// <summary>
		/// The database connection object. All database access is done through this connection.
		/// </summary>
		private SqlConnection m_sqlConnection = new SqlConnection ();

		/// <summary>
		/// The adapter that connects the SQL database to the DataSet. If another type of database
		/// were used then you should change the adapter and all of the code above would remain
		/// unchanged.
		/// </summary>
		private SqlDataAdapter m_sqlAdapter;
        private bool _disposed;

		#endregion


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// ********** Constructor **********
		/// </summary>
		/// <param name="a_connectionString"></param>
		//-----------------------------------------------------------------------------------------
		public SqlBaseDataSet ()
		{
		}		// end of constructor

		public SqlBaseDataSet (string a_connectionString)
		{
			ConnectionString = a_connectionString;
		}		// end of constructor

        ////////////-----------------------------------------------------------------------------------------
        ///////////// <summary>
        ///////////// ********** Destructor **********
        ///////////// </summary>
        ///////////// <param name="a_connectionString"></param>
        ////////////-----------------------------------------------------------------------------------------
        //////////~SqlBaseDataSet ()
        //////////{
        //////////    DbClose ();										// Close DataSet, connections, etc.
        //////////}		// end of destructor


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get/Set the SQL connection string used to access the database.
		/// </summary>
		/// <param name="a_dbConnectionString">The SQL database connection string.<param>
		//-----------------------------------------------------------------------------------------
		public string ConnectionString
		{
			get	{	return (m_dbConnectionString);	}
            set { m_dbConnectionString = value; m_sqlConnection.ConnectionString = m_dbConnectionString; }
		}		// end of property ConnectionString


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Create and store a database connection string using the specified parameters.
		/// </summary>
		/// <param name="a_dbServer">Name of the database server.</param>
		/// <param name="a_dbName">Name of the database.</param>
		/// <param name="a_dbLogon">Login ID to the database.</param>
		/// <param name="a_dbPassword">Database login password.</param>
		/// <returns>The formatted connection string (also stored in m_dbConnectionString).</returns>
		//-----------------------------------------------------------------------------------------
		public string ConnectionStringCreate (string a_dbServer, string a_dbName, string a_dbLogon, string a_dbPassword)
		{
			ConnectionString =
				"Server="				+ a_dbServer	+ ";"	+
				"Database="				+ a_dbName		+ ";"	+
				"User ID="				+ a_dbLogon		+ ";"	+
				"Password="				+ a_dbPassword	+ ";"	+
				"Trusted_Connection="	+ "True;";
			return (m_dbConnectionString);
		}		// end of ConnectionStringCreate


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the number of rows in the DataSet.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public int DataSetCount
		{
			get	{	return (m_dataSet.Tables[TableName].Rows.Count);	}
		}		// end of property DataSetCount


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the change status of the DataSet (TRUE if changes were made; FALSE otherwise).
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public bool DataSetHasChanges
		{
			get	{	return (m_dataSet.HasChanges());		}
		}		// end of property DataSetHasChanges


        //////////-----------------------------------------------------------------------------------------
        /////////// <summary>
        /////////// Close the DataSet and connection and free all resources used by the class.
        /////////// </summary>
        /////////// <param name="a_save">TRUE = save DB updates, FALSE=close without saving.</param>
        //////////-----------------------------------------------------------------------------------------
        ////////public void DbClose ()
        ////////{
        ////////    if (m_sqlAdapter != null)
        ////////        m_sqlAdapter.Dispose ();
        ////////    m_sqlConnection.Close ();
        ////////}		// end of DbClose


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// SQL command to insert a new record into the database.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public void DbInsert ()
		{
//			m_sqlAdapter.InsertCommand = "";
//			m_sqlAdapter.DeleteCommand = "";
//			m_sqlAdapter.UpdateCommand = "UPDATE " + TableName + " SET " + "";
//					"INSERT INTO " + TableName + "(" + rowColumnNames + ") VALUES (" + rowColumnValues + ")"));
//				"UPDATE " + TableName + " SET " + rowUpdate + " WHERE ID=" + m_rowData.Value(0))

		}		// end of DbInsert


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Open the database table and read all rows from the database.
		/// </summary>
		/// <param name="a_tableName">Read the data from this database table.</param>
		/// <param name="a_sqlQueryString">Query string used to exact data from database.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool DbOpen (string a_tableName)
		{
			TableName = a_tableName;
			return (DbOpen (TableName, "SELECT * FROM " + TableName));
		}		// end of DbOpen


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Open the database table using the given query string.
		/// </summary>
		/// <param name="a_tableName">Read the data from this database table.</param>
		/// <param name="a_sqlQueryString">Query string used to exact data from database.</param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------
		public bool DbOpen (string a_tableName, string a_sqlQueryString)
		{
			TableName = a_tableName;
			try
			{
				// Create a connection to the database. The application must have supplied a
				// connection string prior to trying to open the database.
                if (String.IsNullOrEmpty(ConnectionString) == true)
				{
					// Developer message -- should never occur in a production application.
					MessageBox.Show (
						"Must supply a connection string prior to opening the database",
						"Connection String Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error);
					throw new Exception ("Empty connection string");
				}
				m_sqlConnection.ConnectionString = ConnectionString;
				m_sqlConnection.Open  ();					// Open the connection to the SQL DB.

				// The DataSet acts as a buffer between the UI and the DataAdapter (and to the
				// database by extension).
				m_dataSet = new DataSet ();
				m_dataSet.Clear ();

				// Create a data adapter object and select the database records specified in the
				// SQL query string (SELECT * FROM... is the default).
				m_sqlAdapter = new SqlDataAdapter (a_sqlQueryString, ConnectionString);

				// The connection does not seem to be needed after the DataAdapter is instantiated.
				m_sqlConnection.Close ();
				m_sqlConnection.Dispose ();

				// Buffer the database data into the DataSet object.
				m_sqlAdapter.Fill (m_dataSet, TableName);

				// Store the datacase column names in the m_map table. This mapping will be used
				// to move data between the DataSet, the input fields, and the grid columns.
				// Create an entry for each column in the dataset and store the column names.
				int noColumnsInDataSet = m_dataSet.Tables[TableName].Columns.Count;
				m_map.New (noColumnsInDataSet);
				for (int i=0;  i<noColumnsInDataSet;  i++)	// Store DB column names in map table.
					m_map.m_dbColumn[i] = m_dataSet.Tables[TableName].Columns[i].ColumnName.ToLower();
				return (true);
			}
			catch (Exception ex)
			{
				LastError = ex.Message + "(SqlGridDataSet:DbOpen)";
				return (false);
			}
		}		// end of DbOpen


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the number of rows in the current dataset.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public int DbRowCount
		{
			get	{	return (m_dataSet.Tables[TableName].Rows.Count);		}
		}		// end of property DbRowCount


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Save the DataSet data to the database.
		/// </summary>
		/// <param name="a_save">TRUE = save DB updates, FALSE=close without saving.</param>
		//-----------------------------------------------------------------------------------------
		public void DbSave ()
		{
            try
            {
                if (m_sqlAdapter.UpdateCommand == null)
                {
                    // When the DataSet is working with a single table then the SqlCommandBuilder
                    // will create the INSERT, DELETE, and UPDATE commands to update the DataSet
                    // to the SQL database.
                    SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(m_sqlAdapter);
                    sqlCommandBuilder.GetUpdateCommand();
                }
                m_sqlAdapter.Update(m_dataSet, TableName);	// Update DataSet data to SQL database.
            }
            catch (Exception ex)
            {
                CCFBGlobal.appendErrorToErrorReport("", ex.GetBaseException().ToString());
            }
		}		// end of DbSave


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Return the last error string generated. This is useful when a higher-level method fails
		/// and you need to view the specific cause of the error.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public string LastError
		{
			get	{	return (m_lastError);	}
			set	{	m_lastError = value;	}
		}		// end of property LastError


		//-----------------------------------------------------------------------------------------
		/// <summary>
		/// Get or set the name of the SQL table used by this class.
		/// </summary>
		//-----------------------------------------------------------------------------------------
		public string TableName
		{
			get	{	return (m_dbTableName);		}
			set	{	m_dbTableName = value;		}
		}		// end of property TableName

        public int ExecuteQuery(string queryText)
        {
            if (String.IsNullOrEmpty(m_sqlConnection.ConnectionString) == true)
                m_sqlConnection.ConnectionString = ConnectionString;
            SqlCommand sqlCmd = new SqlCommand(queryText, m_sqlConnection);
            m_sqlConnection.Open();
            int retValue = (int)sqlCmd.ExecuteScalar();
            m_sqlConnection.Close();
            return retValue;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_sqlAdapter != null)
                    m_sqlAdapter.Dispose();
                if (m_sqlConnection != null)
                    m_sqlConnection.Dispose();
            }

            // Indicate that the instance has been disposed.
            m_sqlAdapter = null;
            m_sqlConnection = null;
            _disposed = true;
        }

	}		// end of class

}		// end of namespace