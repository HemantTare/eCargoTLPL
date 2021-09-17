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
    public interface IBankRecoExcelImportView : IBankRecoView
    {
        DataTable bind_dg_BankReco1 { set;}
        DataTable bind_dg_BankReco2 { set;}
        DataTable bind_dg_BankReco3 { set;}
    }
}