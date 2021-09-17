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
/// Summary description for ClientBillingView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface IClientBillingView : IView
    {
        bool Is_Paid { set;get;}
        bool Is_To_Pay { set;get;}
        bool Is_FOC { set;get;}
        bool Is_To_Be_Billed { set;get;}

        DataTable SessionBindBillingGrid { set;}
        DataTable BindPaymentMode { set;}
        string BillingDetailsXML { get;}
        int BillingCycle { set;get;}
        int BillingDays { set;get;}
        int BillingCycleDay1 { set;get;}
        int BillingCycleDay2 { set;get;}

        DataTable BindBillingCycle { set;}
        DataTable BindBillingDays { set;}
    }
}

