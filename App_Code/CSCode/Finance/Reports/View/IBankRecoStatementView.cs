using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
/// <summary>
/// Summary description for Class1
/// </summary>
/// 


namespace Raj.EC.FinanceView
{
    public interface IBankRecoStatementView : IView
    {

        DataTable bind_dg_AddBankRecoStatement { set;}
        DataTable bind_dg_LessBankRecoStatement { set;}

        DataTable SetLables { set;}
        DateTime End_Date { get;set;}
        DateTime Start_Date { get;set;}

        String Hierarchy_Code { get;set;}

        int Main_Id { get;set;}
        bool Is_Consol { get;}
        bool Is_Uncleared { get;}
        DataTable Session_Dt_AddBankRecoStatement{ get;set;}
        DataTable Session_Dt_LessBankRecoStatement { get;set;}

    }
}