using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.ControlsView; 

/// <summary>
//Created : Ankit champaneriya
//Date : 2/12/08
//Desc : Pod Receipt DD Details
/// Summary description for PODReceiptDD
/// </summary>
/// 

namespace Raj.EC.OperationView
{
    public interface IPODReceiptDDView : IView
    {
        int GCNo { get;}

        string BookingDate { set;}
        string BookingType { set;}
        string BookingBranch { set;}
        string PaymentType { set;}
        string DeliveredDate { set;}
        string DeliveredBranch { set;}
        string DeliveredRemark { set;}
        DateTime PODReceiptDate { get;set;}

        string Remarks { get;set;}

        void SetGCNo(string text, string value);

        IPODSentByView PODSentByView { get;}
        string Flag { get;}

        
        //void ClearVariables();
    }
}