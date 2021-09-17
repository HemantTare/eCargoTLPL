using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for DateCommon
/// </summary>
public class DateCommon
{
    DateTime F_Start_Date, F_End_Date;
    public DateCommon()
    {
        F_Start_Date = (DateTime)UserManager.getUserParam().StartDate;
        F_End_Date = (DateTime)UserManager.getUserParam().EndDate;
    }

    public bool Vaildate_Date(DateTime Start_date, ref string msg)
    {
        bool ATS = false;

        if (Start_date < F_Start_Date || Start_date > F_End_Date)
        {
            msg = msg + "</br> Selected Date Must Be In Current Finanical Year";
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }

    public bool Vaildate_Date(DateTime Start_date, DateTime End_Date, ref string msg)
    {
        bool ATS = false;

        if (Start_date > End_Date)
        {
            msg = "</br> From Date Should Not Greater than To Date";
        }
        else if (Start_date < F_Start_Date || Start_date > F_End_Date)
        {
            msg = msg + "</br> From Date Must Be In Current Finanical Year";
        }
        else if (End_Date < F_Start_Date || End_Date > F_End_Date)
        {
            msg = msg + "</br> To DateMust Be In Current Finanical Year";
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }
}
