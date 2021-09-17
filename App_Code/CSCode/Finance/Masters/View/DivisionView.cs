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
    public interface IDivisionView
    {
        string getDivisonXML {get;}
        DataTable SetDivisions { set;}
        DataTable bind_chkl_Division { set;}
    }
}
