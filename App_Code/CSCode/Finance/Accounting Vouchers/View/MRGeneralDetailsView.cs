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


namespace Raj.EC.FinanceView
{
    public interface IMRGeneralDetailsView : IView
    {
        int GC_ID { get;set;}
        string GCNo { get;set;}
        string MRNo { get;set;}
        string BookingBranch { get;set;}
        string DeliveryBranch { get;set;}
        string Consignor { get;set;}
        string Consignee { get;set;}
        string BookingType { get;set;}
        string PaymentType { get;set;}

        string GCAmount { get;set;}
        string ServiceTax { get;set;}
        string ServiceTaxBy { get;set;}
        DateTime MRDate { get;set; }
        string BookingDate { get;set;}
        decimal Total_Receivables { get;set;}
        //int RoundOff { get;set;}
        int Document_Allocation_ID { set;get; }
        int Next_No { set;get; }
        int MR_Type_ID { get;set;}
        int Document_ID { get;set;}

        bool Is_MR_FirstTime { get;set; }
        bool Is_CreditMemoOctroi_FirstTime { get;set; }



    }
}