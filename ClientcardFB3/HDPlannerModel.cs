using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientcardFB3
{
    HDRouteSheet clsHDRouteSheet = new HDRouteSheet(CCFBGlobal.connectionString);
        IEditVolunteerForm frmVolunteers;
        HDRSHist clsHDRSHist = new HDRSHist(CCFBGlobal.connectionString);
        parmTypeCodes parmHDRouteSheetStatus = new parmTypeCodes(CCFBGlobal.parmTbl_HDRouteSheetStatus, CCFBGlobal.connectionString, "");
        HDItems clsHDItems = new HDItems(CCFBGlobal.connectionString);
    class HDPlannerModel
    {
    }
}
