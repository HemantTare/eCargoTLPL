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
using Raj.EC.ControlsView;
/// <summary>
/// Summary description for BTHMultipleView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IBTHMultipleView : IView
    {
        int BrokerOwnerID { get; }
        int Total_No_Of_LHPO { get; set;}
        string BTHVoucherNo { get;set;}
        DateTime BTHVoucherDate { get;set; }
        DateTime LHPOFromDate { get;set; }
        DateTime LHPOToDate { get;set; }
        string ReferenceNo { get;set;}
        decimal TotalPayableAmount { get;set;}
        decimal TotalOtherAmount { get;set;}
        decimal TotalBalanceToBePaid { get;set;}
        string Remark { get;set;}
        decimal TotalTDSAmount { get;set; }

        decimal Tax_Rate{get;set;}
        decimal Surcharge_Rate{get;set;}
        decimal Add_Surcharge_Rate{get;set;}
        decimal Add_Edu_Cess_Rate { get;set;}

        IMRCashChequeDetailsView MRCashChequeDetailsView { get; }

        void SetOwnerID(string Name, string ID);
        DataTable SessionLHPODetailsGrid { get;set;}
        String LHPODetailsXML { get;}
        string Flag { get;}
        int Msg { get;set; }

        void ClearVariables();
    }

}

