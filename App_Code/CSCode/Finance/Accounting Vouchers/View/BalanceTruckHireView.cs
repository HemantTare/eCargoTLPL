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


namespace Raj.EC.FinanceView
{
    public interface IBalanceTruckHireView : ClassLibraryMVP.General.IView
    {
        string BTHVoucherNo { get;set;}
        DateTime BTHVoucherDate { get;set; }
        string ReferenceNo { get;set;}
        decimal TotalPayableAmount { get;set;}
        string Remark { get;set;}
        IMRCashChequeDetailsView MRCashChequeDetailsView { get; }
        IVehicleSearchView VehicleSearchView { get;}
        IOtherChargeDetailsView OtherChargeDetailsView { get;}
        DataSet Bind_ddlLHPONo { set;}
        int LHPONo_ID { get;set;}
        decimal Balance_To_Be_Paid { get;set;}

        void LHPO_DDl_Fill();
        void Fill_LHPO_Detail(DataSet ds);
        void SetOwnerID(string Name, string ID);
        int BrokerOwnerID { get; }
        DateTime LHPO_Date { get;set;}


        void ClearVariables();
    }

}
