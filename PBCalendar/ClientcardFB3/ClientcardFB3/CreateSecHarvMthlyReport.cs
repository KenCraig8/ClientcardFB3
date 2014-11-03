using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Office.Interop.Word;

namespace ClientcardFB3
{
    class CreateSecHarvMthlyReport
    {
        System.Data.DataTable monthStatsTable;

        public CreateSecHarvMthlyReport(System.Data.DataTable dataTableIn)
        {
            monthStatsTable = dataTableIn;
        }

        public void createReport(string FBName, string month, string year, string county,
             string city, string phoneNumber, string totIndServed, string preparedBy, object saveAs,
            string templatePath)
        {
            Object oMissing = System.Reflection.Missing.Value;

            object missing = System.Reflection.Missing.Value;
            Object oTrue = true;
            Object oFalse = false;
            Application oWord = new Application();
            Document oWordDoc = new Document();
            oWord.Visible = false;
            
            Object oTemplatePath = templatePath;

            oWordDoc = oWord.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

            //Save so that the template is free to be used by the next user
            oWordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);



        }
    }
}
