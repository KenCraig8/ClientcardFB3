using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MapData2
{
    public partial class CustSqlWhere : Form
    {
        ArrayList sqlWhereProperties;
        ArrayList sqlSelectProperties;
        string connectionString;
        string[] selectedColumns;
        string databaseName;
        string selectedTableName;

        string sqlFromString;

        string[] kNumTypes;
        string[] kDateTypes;
        const int kMaxValsToSelect = 50;

        //string createdSqlQuery = "";

        //BoundString saveFilePath;
        //DataTable loadedData;

        public CustSqlWhere(string connectionString, string[] selectedColumns, string databaseName, string selectedTableName)
        {
            this.connectionString = connectionString;
            this.selectedColumns = selectedColumns;
            this.databaseName = databaseName;
            this.selectedTableName = selectedTableName;

            kNumTypes = new string[]{"int", "smallint", "float"};
            kDateTypes = new string[] { "datetime" };

            sqlFromString = " FROM [" + databaseName + "].[dbo].[" + selectedTableName + "]";

            InitializeComponent();
        }

        private void CustSqlWhere_Load(object sender, EventArgs e)
        {
            ArrayList sqlRangeTypes = new ArrayList(kNumTypes);
            sqlRangeTypes.AddRange(kDateTypes);

            string rangeTypesSqlPart = DataHelper.enumToSqlIn(sqlRangeTypes);

            string rangeTypeSpecs = "(DATA_TYPE IN " + rangeTypesSqlPart + ")";
            string numTypeSpecs = "(DATA_TYPE IN " + DataHelper.enumToSqlIn(kNumTypes) + ")";

            setupRangeSelect(getColsOfType(rangeTypeSpecs), getColsOfType(numTypeSpecs));

            string stringTypeSpecs = "(DATA_TYPE NOT IN " + rangeTypesSqlPart + ")";
            setupStringSelect(getColsOfType(stringTypeSpecs));

            lstOrder.Items.AddRange((SqlSelectProperty[])sqlSelectProperties.ToArray(typeof(SqlSelectProperty)));
            lstOrder.DisplayMember = "columnName";
        }

        //querys the database to get the entries colums matching a certain type
        private string[] getColsOfType(string typeSpecs)
        {
            //SELECT COLUMN_NAME FROM [ClientCardFB3F].information_schema.columns WHERE TABLE_NAME = 'HouseholdMembers'
            string sqlColumnConditions = String.Join("' OR COLUMN_NAME = '", selectedColumns);
            string partTypeQuery = "SELECT COLUMN_NAME FROM [" + databaseName + "].information_schema.columns WHERE TABLE_NAME = '" 
                + selectedTableName + "' AND {0} AND (COLUMN_NAME = '" + sqlColumnConditions + "')";

            string typeQuery = String.Format(partTypeQuery, typeSpecs);

            DataTable columnNamesTable = DataHelper.sqlQuery(connectionString, typeQuery);

            return DataHelper.dataTableToArray(columnNamesTable);
        }

        //Querys the database to get all of the distinct entries in a column
        private string[] getDistinctValsInCol(string colName)
        {
            string sqlQuery = "SELECT DISTINCT " + colName + sqlFromString;

            DataTable distinctElementsTable = DataHelper.sqlQuery(connectionString, sqlQuery);
            return DataHelper.dataTableToArray(distinctElementsTable);
        }

        //adds the controls to select the displayed values for the string columns
        private void setupStringSelect(string[] columnNames)
        {
            for (int columnNum = 0; columnNum < columnNames.Length; columnNum++)
            {
                string colName = columnNames[columnNum];
                string[] values = getDistinctValsInCol(colName);

                FlowLayoutPanel flpSelection = new FlowLayoutPanel();

                flpSelection.Controls.Add(new Label() { Text = colName });

                SqlSelectProperty sqlSelect = new SqlSelectProperty() { columnName = colName };

                CheckBox chkDisplay = new CheckBox(){ Text = "Display", Checked = true};
                chkDisplay.DataBindings.Add("Checked", sqlSelect, "IsEnabled");
                flpSelection.Controls.Add(chkDisplay);

                if (values.Length < kMaxValsToSelect)
                {
                    ListBox lstSelections = new ListBox();
                    lstSelections.DataSource = values;
                    lstSelections.SelectionMode = SelectionMode.MultiExtended;

                    SqlStringWhereProperty sqlStringWhere = new SqlStringWhereProperty(lstSelections, colName);

                    CheckBox chkEnabled = new CheckBox() { Text = "Apply filter" };
                    chkEnabled.DataBindings.Add("Checked", sqlStringWhere, "IsEnabled");

                    flpSelection.Controls.Add(chkEnabled);
                    flpSelection.Controls.Add(lstSelections);

                    sqlWhereProperties.Add(sqlStringWhere);
                }
                sqlSelectProperties.Add(sqlSelect);
                //flpSelection.AutoScroll = true;
                flpSelection.AutoSize = true;
                flpSelection.FlowDirection = FlowDirection.TopDown;
                flpStringSelect.Controls.Add(flpSelection);
            }
            //It's best not to turn on auto scroll untill all the eliments are added or it will try to calcualte the scroll for each one
            flpStringSelect.AutoScroll = true;
        }

        //adds the controls to select the range of the number columns to the form with the apropriate binding
        private void setupRangeSelect(string[] columnNames, string[] numColNames){
            sqlWhereProperties = new ArrayList();
            sqlSelectProperties = new ArrayList();

            for (int entryNum = 0; entryNum < columnNames.Length; entryNum++)
            {
                SqlRangeWhereProperty sqlWhere;
                SqlSelectProperty sqlSelect = new SqlSelectProperty();
                TextBoxBase txtLower;
                TextBoxBase txtUpper;

                const string kDateMaskString = "00/00/0000";

                if (numColNames.Contains(columnNames[entryNum]))
                {//Num
                    sqlWhere = new SqlNumWhereProperty();
                    txtLower = new TextBox();
                    txtUpper = new TextBox();
                }
                else
                {//Date
                    sqlWhere = new SqlDateWhereProperty();
                    txtLower = new MaskedTextBox();
                    txtUpper = new MaskedTextBox();
                    ((MaskedTextBox)txtLower).Mask = kDateMaskString;
                    ((MaskedTextBox)txtUpper).Mask = kDateMaskString;
                }
              
                sqlWhere.columnName = columnNames[entryNum];
                sqlSelect.columnName = columnNames[entryNum];

                txtLower.DataBindings.Add("Text", sqlWhere, "LowerLimit");
                txtUpper.DataBindings.Add("Text", sqlWhere, "UpperLimit");

                CheckBox chkDisplay = new CheckBox() { Text = "Display", Checked = true };
                chkDisplay.DataBindings.Add("Checked", sqlSelect, "IsEnabled");

                CheckBox chkEnabled = new CheckBox(){ Text = "Enabled" };
                chkEnabled.DataBindings.Add("Checked", sqlWhere, "IsEnabled");

                sqlWhereProperties.Add(sqlWhere);
                sqlSelectProperties.Add(sqlSelect);

                tblWhereSelection.RowCount++;

                tblWhereSelection.Controls.Add(new Label() { Text = columnNames[entryNum] }, 0, entryNum);
                tblWhereSelection.Controls.Add(chkDisplay, 1, entryNum);
                tblWhereSelection.Controls.Add(chkEnabled, 2, entryNum);
                tblWhereSelection.Controls.Add(txtLower, 3, entryNum);
                tblWhereSelection.Controls.Add(new Label() { Text = "<= "+sqlWhere.columnName+" <=" }, 4, entryNum);
                tblWhereSelection.Controls.Add(txtUpper, 5, entryNum);
            }
            tblWhereSelection.AutoScroll = true;
        }

        //Creates the sql query with the information the user selected
        private string makeSqlQuery()
        {
            //create query with appropriate columns selected
            string selectedColumnsSql = sqlPropertiesToString(lstOrder.Items, ", ");
            string whereCaluse = sqlPropertiesToString(sqlWhereProperties, " AND ");
            if (whereCaluse.Length > 0)
                whereCaluse = " WHERE " + whereCaluse;
            return "SELECT " + selectedColumnsSql + sqlFromString + whereCaluse;
        }

        //Works much like string.join but only includes elements where "IsEnabled" is true
        private static string sqlPropertiesToString(IEnumerable sqlProperties, string joiner)
        {
            string outString = "";
            //add where clauses
            bool isFirst = true;
            //Can't use String.Join for this casue I don't want to include it if it's not enabled
            foreach (SqlProperty sqlProp in sqlProperties)
            {
                if (sqlProp.IsEnabled)
                {
                    if (isFirst)
                        isFirst = false;
                    outString += sqlProp + joiner;
                }
            }
            if (!isFirst)
                outString = outString.Substring(0, outString.Length - joiner.Length);

            return outString;
        }

        //Loads the data into the preview
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            string sqlQuery = makeSqlQuery();

            DataTable loadedData = DataHelper.sqlQuery(connectionString, sqlQuery);

            gvPreview.DataSource = new DataTable();
            gvPreview.DataSource = loadedData;
        }
          /*      
        //write the data in the table to an excel file
        private void btnExcel_Click(object sender, EventArgs e)
        {
          * sfdSaveExcel.Filter = "excel files|*.csv";
          * 
            const string seperator = ",";

            var lines = new List<string>();

            //for some reason excel doesn't like .csv files to have the capital ID in them
            string[] columnNames = loadedData.Columns.Cast<DataColumn>().Select(column => column.ColumnName.Replace("ID", "id")).ToArray();

            var header = string.Join(seperator, columnNames);
            lines.Add(header + seperator);

            var valueLines = loadedData.AsEnumerable().Select(row => string.Join(seperator, row.ItemArray));
            lines.AddRange(valueLines);

            sfdSaveExcel.ShowDialog();

            File.WriteAllLines(sfdSaveExcel.FileName, lines);
        }*/

        //Saves the sql query in a text file.
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlQuery = makeSqlQuery();

            sfdSaveQuery.ShowDialog();
            File.WriteAllText(sfdSaveQuery.FileName, sqlQuery);
        }

        private void lstOrder_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.lstOrder.SelectedItem == null) return;
            this.lstOrder.DoDragDrop(this.lstOrder.SelectedItem, DragDropEffects.Move);
        }

        private void lstOrder_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lstOrder_DragDrop(object sender, DragEventArgs e)
        {
            Point point = lstOrder.PointToClient(new Point(e.X, e.Y));
            int index = this.lstOrder.IndexFromPoint(point);
            if (index < 0) index = this.lstOrder.Items.Count - 1;
            object data = e.Data.GetData(typeof(SqlSelectProperty));
            this.lstOrder.Items.Remove(data);
            this.lstOrder.Items.Insert(index, data);
        }
    }



    //Inheritance is used so much here so that the same code can be used for different sql properties if they are similar enough
    //The setupSqlRange function can use do the same thing for a number range or a date range this way
    //And all of the properties can be converted into part of a SQL query the same way

    //For the all of the where properties to be selected and used
    public abstract class SqlProperty{
        protected bool isEnabled;

        public string columnName;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        abstract public string toString();

        public static implicit operator string(SqlProperty val)
        {
            return val.toString();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public class SqlSelectProperty : SqlProperty
    {
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public override string toString()
        {
            return columnName;
        }
    }

    // For classes that involve ranges with sql queries
    public abstract class SqlRangeWhereProperty : SqlProperty
    {
        public string LowerLimit;
        public string UpperLimit;
    }

    //Stores information about a range of dates and can be converted into an SQL query
    //Designed to be used in a binding
    public class SqlDateWhereProperty : SqlRangeWhereProperty, INotifyPropertyChanged
    {
        // lowerLimit and LowerLimit have different types since lowerLimit is a "behind the scenes" variable to store the actuall value
        //and LowerLimit is interacts with the user input
        private static DateTime kMinDate = new DateTime(1800, 1, 1);
        
        private DateTime lowerLimit = kMinDate;
        private DateTime upperLimit = DateTime.MaxValue;

        new public string LowerLimit
        {
            get{return dateToString(lowerLimit);}

            set
            {
                //this will validate the user input. If they entered a invalid date nothing will happen
                try {
                    lowerLimit = stringToDate(value);
                    if (lowerLimit < kMinDate)
                        lowerLimit = kMinDate;
                    OnPropertyChanged("LowerLimit");
                }
                catch(Exception e){}
            }
        }

        new public string UpperLimit
        {
            get { return dateToString(upperLimit); }
            set
            {
                try
                {
                    upperLimit = stringToDate(value);
                    if (upperLimit < kMinDate)
                        upperLimit = kMinDate;
                    OnPropertyChanged("UpperLimit");
                }
                catch (Exception e) { }
            }
        }

        //I use funcitons for these so if I want to change how I convert strings and dates I can just do it in one place.
        private static string dateToString(DateTime inDate){
            return Convert.ToString(inDate);
        }

        private static DateTime stringToDate(string inStr){
            return Convert.ToDateTime(inStr);
        }

        private static string dateToSqlString(DateTime inDate){
            return "'" + inDate.ToString("s") + "'";
        }

        public override string toString()
        {
            return "(" + columnName + " >= " + dateToSqlString(lowerLimit) + " AND " + columnName + " <= " + dateToSqlString(upperLimit) + ")";
        }
    }

    //Class for storing the column name and the upper and lower bounds for a where clause
    //Made for being used with a binding to controls
    public class SqlNumWhereProperty : SqlRangeWhereProperty, INotifyPropertyChanged
    {
        private float lowerLimit;
        private float upperLimit;
        
        new public String LowerLimit
        {
            get { return lowerLimit.ToString(); }
            set
            {
                try
                {
                    lowerLimit = Convert.ToSingle(value);
                    OnPropertyChanged("LowerLimit");
                }
                catch (Exception e) { }
            }
        }

        new public String UpperLimit
        {
            get { return upperLimit.ToString(); }
            set
            {
                try
                {
                    upperLimit = Convert.ToSingle(value);
                    OnPropertyChanged("UpperLimit");
                }
                catch (Exception e) { }
            }
        }

        override public string toString()
        {
            return "(" + columnName + " >= " + LowerLimit + " AND " + columnName + " <= " + UpperLimit + ")";
        }
    }

    //For using a list box to select sql IN clause
    //I wanted to do this with a binding, but I couldn't figure out how
    //The Items property should reflect the selected items in the list box
    public class SqlStringWhereProperty : SqlProperty
    {
        private ListBox lstItems;

        public SqlStringWhereProperty(ListBox lstItems, string columnName)
        {
            this.lstItems = lstItems;
            this.columnName = columnName;
        }

        public string[] Items
        {
            get
            {
                string[] vals = new string[lstItems.Items.Count];
                lstItems.SelectedItems.CopyTo(vals, 0);
                return vals;
            }
            set { }
        }

        //Converts the values from the list into part of the SQL query
        override public string toString()
        {
            return "(" + columnName + " IN " + DataHelper.enumToSqlIn(Items) + ")";
        }
    }


}
