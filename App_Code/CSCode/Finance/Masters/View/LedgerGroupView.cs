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
 
namespace Raj.EC.FinanceView
{
    public interface ILedgerGroupView : IView         
    {
        string LedgerGroupName { set; get;}
        string Alias { set; get;}

        int UnderId { get;set;}
        string NatureName { get;set;}
        int IndexNo { get;set;}
        bool IsAffectGrossProfit { get;set;}
        DataTable bind_ddl_Under { set;}
        bool EnableControls { set;} 
    }
}
