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
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;


namespace Raj.EC.FinanceView
{
    public interface ISupplementaryBillView : IView
    {
        int Client_ID { get;}
        string ReferenceNo { get;set;}
        string Remarks { get;set;}
        string BillNo { get;set;}
        String BillingName { get;set;}
        String ContactPerson { get;set;}
        String BillingAddress { get;set;}
        String ContactNo { get;set;}
        String Email { get;set;}
        DataTable SessionBillGrid{get;set;}
        DataTable SessionBillOtherChargeGrid { get;set; }
        string GetGCNoXML { get;set; }
        DateTime BillDate { get;set;}
        DataTable BindGrid { set;}
        DataTable BindServiceType { set;}
        string GetDetailGridXML{get;}
        string GetOtherChargeGridXML { get; }
        int Next_No { get;set;}
        int Document_Allocation_ID { get;set; }
        int Service_Type_ID { get;set; } 
        void ClearVariables();
        void SetClientId(string text, string value);
        string Flag { get; }
        int Msg { get;set; }
        decimal GrandTotal{get;set;}
        decimal TotalOtherCharge{get;set;}
        decimal TotalServiceTax { get;set;}

    }
}



