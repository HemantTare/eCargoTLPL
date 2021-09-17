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
using Raj.EC.OperationView; 

/// <summary>
/// Summary description for LHPOHireDetailsView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface ILHPOHireDetailsView:IView 
    {
        //Interger Property Declaration
        string DVLPID { get;set;}
        string DVLPFromBranchID { get;set;}
        int VehicleID { get;set;}
        int VehicleCategoryID { get;set;}
        int LHPOTypeID { get;set;}
        int LHPONo { get;set;}
        int FromLocationID { get;}
        int ToLocationID { get;}
        int BrokerID { set;get;}
        int Driver1ID { get;}
        int Driver2ID { get;}
        int CleanerID { get;}
        bool TDSCertificateToID { set; get;}        
        int FreightTypeID { get;set;}      
        int LoadingSupervisorID { get;}
        int VehicleCapacity { get;set;}
        int TotalMemos { get;set;}
        int TotalArticle { get;set;}
        int TotalGC { get;set;}
        int TransitDays { get;set;}
        int MainID { get;set;}
        int ToLocationBranchId { get;set;}


        int FreightTypeID_Edit { get;set;}
        //Decimal Property Declaration

        decimal WtGuarantee{ get;set;}
        decimal RateKg{ get;set;}
        decimal ActualWtPayableValue{ get;set;}
        decimal LoadingCharge{ get;set;}
        decimal ActualKms { get;set;}
        decimal TDSPercentage { get;set;}
        decimal TruckHireCharge { get;set;}
        decimal TotalTruckHireCharge { get;set;}
        decimal BalanceAmount { get;set;}
        decimal TotalAdvancePaid { get;set;}
        decimal CrossingCostPayble { get;set;}
        decimal DeliveryCommission { get;set;}
        decimal OthersPayble { get;set;}
        decimal NetAmount { get;set;}
        decimal TotalArticleWT { get;set;}
        decimal TDSAmount { get;set;}
        decimal TotalTDSAmount { get;set;}
        decimal TotalPayable   { get;set;}
        decimal ToPayCollection { get;set;}
        decimal AddlSurchargeCess { get;set;}
        decimal AddlEducationCess { get;set;}
        decimal OtherCharges { set;get;}
        decimal ExemptionLimit { set;get;}
        decimal Surcharge { set;get;}
        decimal ExemptionLimitAmount{set;get;}
        decimal SurchargeAmount{set;get;}        
        decimal AddlSurchargeCessAmount{set;get;}
        decimal AddlEducationCessAmount { set;get;}

        decimal RateKg_Edit { set;get;}
        decimal WtGuarantee_Edit { set;get;}
        //decimal ActualWtPayableValue_Edit { set;get;}
        //
        DateTime LHPODate { get;set;}
        DateTime CommitedDelDate { get;set;}
        DateTime AttachedLHPODate { get;set;}
        
        //String Property Declaration
        string ManualRefNo{ get;set;}
        string Remark{ get;set;}
        string VehicleOwner{ get;set;}
        string HierarchyCode { get;set;}
        string MemoGridXML { get;}
        string VehicleDepartureTime { get;set;}
        string LHPONOForPrint { set;}
        //string Flag { get;}

        //Function Declaration to Set DDLSearch Value 
        void SetFromLocationID(string FromLocationName, string FromLocationID);
        void SetToLocationID(string ToLocationName, string ToLocationID);
        void SetDriver1ID(string Driver1Name, string Driver1ID);
        void SetDriver2ID(string Driver2Name, string Driver2ID);
        void SetCleanerID(string CleanerName, string CleanerID);
        void SetLoadingSupervisorID(string LoadingSupervisorName, string LoadingSupervisorID);    

        //Bind Property Declaration
        DataTable Bind_ddl_VehicleCategory { set;}
        DataTable Bind_ddl_LHPOType { set;}
        DataTable Bind_ddl_LHPONo { set;}
        DataTable Bind_ddl_BrokerName { set;}       
        DataTable Bind_ddl_FreightType { set;}
       // DataTable Bind_ddl_BalancePayableAt { set;}
       // DataTable Bind_ddl_BalancePayableLocation { set;}
       // DataTable Bind_ddl_LoadingSupervisor { set;}

        DataSet Bind_dg_LHPOHireDetails{ set;}
        DataSet SessionLHPOHireDetailsGrid { set;get;}
        string OtherChargesDetailsXML { get;}

        int Next_No {get;set;}
        string LHPO_No { get;set;}
        int Document_Series_Allocation_ID { get;set;}

        bool IsLHCTerminatedByReceivedCash { set;get;}
        decimal TerminatedLHCReceivedCash { set;get;}
        bool IsLHCTerminatedByDebitToLedger { set;get;}
        void SetLedgerForTerminatedLHC(string LedgerName, string LedgerId);
        int TerminatedLHCDebitToLedgerId { get;}
        void SetCharityLedger(string LedgerName, string LedgerId);
        decimal CharityAmount { set;get;}
        decimal TotalAfterTDSDeduction { set;get;}
        int CharityLedgerId { get;}
       
    }

}