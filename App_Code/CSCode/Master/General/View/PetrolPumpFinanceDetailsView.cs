using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for PetrolPumpFinanceDetailsView
/// </summary>
///  

namespace Raj.EC.MasterView
{
    public interface IPetrolPumpFinanceDetailsView : ClassLibraryMVP.General.IView
    {


        int LedgerGroupId { get;set;}
        int LedgerId { get;set;}
        int CreditDays { get;set;}     
        //int TDS_Deductee_Type_ID { get;set;}

        Decimal CreditLimit { get;set;}
        //Decimal TDS_Lower_Rate { get;set;}

        //DataSet Bind_ddl_Branch_Name { set;}
        DataTable BindLedgerGroup { set;}
        DataTable BindLedger { set;}
        //DataTable BindTDSDeducteeType { set;}
        DataTable BindDivision { set;}       
        
        String Notification_Detail { get;set;}
        //String Section_Number { get;set;}
        //String TDS_Deductee_Type_Name { get;}
        String Applicable_Divisions_Details_Xml { get;}

        Boolean Is_Service_Tax_Applicable { get;set;}       
        //Boolean Is_TDS_Applicable { get;set;}      
        Boolean Is_Exempted { get;set;}
        Boolean Is_Use_Existing_Ledger { get;set;}
        Boolean Use_Existing_Ledger { get;set;}
        //Boolean Is_Lower_No_Deduction_Applicable { get;set;}
        //Boolean Is_Ignore_Exemption_Limit { get;set;}
        // Boolean Is_Ignore_Surcharge_Exemption_Limit { get;set;}

        DataSet Session_Applicable_Divisions_Details { get;set;}

        ITDSAppView TDSAppView { get;}
         

      
    }
}
