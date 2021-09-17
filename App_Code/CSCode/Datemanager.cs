using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using ClassLibraryMVP;
using Raj.EC;

/// <summary>
/// Summary description for Datemanager
/// </summary>
/// 

public class Datemanager
{
    //Private Shared _dc As Hashtable = New Hashtable()

    private static string Query="";
    private static Common objcommon = new Common();
    private static Hashtable _dc = new Hashtable();
    private static string Code;

    private static DateTime MinDate
    {
        get
        {
            DateTime curDate, newDate;
            int processHours;
            DataSet ds = new DataSet();
            Query = "select Minhrs from EC_Com_Adm_DateSettings where Code = '" + Code.ToString() + "'";
            ds = objcommon.EC_Common_Pass_Query(Query);
            processHours = Convert.ToInt32(ds.Tables[0].Rows[0]["Minhrs"].ToString());

            processHours = 0 - processHours;

            curDate = DateTime.Now;
            newDate = curDate.AddHours(processHours);

            return newDate;
        }
    }

    private static DateTime MaxDate
    {
        get
        {
            DateTime curDate, newDate;
            int processHours;
            DataSet ds = new DataSet();
            Query = "select MaxHrs from EC_Com_Adm_DateSettings where Code ='" + Code.ToString() + "'";
            ds = objcommon.EC_Common_Pass_Query(Query);
            processHours = Convert.ToInt32(ds.Tables[0].Rows[0]["MaxHrs"].ToString());

            curDate = DateTime.Now;
            newDate = curDate.AddHours(processHours);

            return newDate;
        }
    }

    public static Boolean IsValidProcessDate(string _Code, DateTime CurProcessDate)
    {
        Code = _Code;
        DateTime Min_Date,Max_Date ;
        Min_Date = MinDate;
        Max_Date = MaxDate;

        if (CurProcessDate.Millisecond == 0)
            {
            Min_Date = Min_Date.Date;
            Max_Date = Max_Date.Date;
            }


        DateTime Start_Date, End_Date;
        int Year_Code = UserManager.getUserParam().YearCode;
        Start_Date = UserManager.getUserParam().StartDate;
        End_Date = UserManager.getUserParam().EndDate;

        Boolean Return_Value1, Return_Value2 ;

        if (CurProcessDate >= Start_Date && CurProcessDate <= End_Date)
            Return_Value1 = true;
        else
            Return_Value1 = false;



        if (CurProcessDate >= Min_Date && CurProcessDate <= Max_Date)
            Return_Value2 = true;
        else
            Return_Value2 = false;

        if (Return_Value1 == true && Return_Value2 == true)
            return true;
        else
            return false;

    }

    public static void Init()
    {
        int Index;
    }
}
