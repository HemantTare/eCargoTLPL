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


//namespace Raj.FA.ReportsView
//{
    public interface IBankRecoView : IView
    {

        DataTable bind_dg_BankReco { set;}

        DataTable SetVariables { set;}

        DateTime End_Date { get;set;}
        DateTime Start_Date { get;set;}

        DataSet get_Ds_BankReco { get;}

        String Hierarchy_Code { get;set;}
        bool Is_Consol { get;}
        bool Is_Uncleared { get;}
        int Main_Id { get;set;}

    }
//}