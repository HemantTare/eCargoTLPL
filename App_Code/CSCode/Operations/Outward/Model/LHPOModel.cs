using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;



/// <summary>
/// Summary description for LHPOModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOModel : IModel
    {
        private ILHPOView objILHPOView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _yearCODE = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private int _menuitemID = Raj.EC.Common.GetMenuItemId();
        private int _branchID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _keyID;

        public LHPOModel(ILHPOView lHPOView)
        {
            objILHPOView = lHPOView;
            if (objILHPOView.LHPOHireDetailsView.LHPOTypeID == 2 && objILHPOView.keyID <= 0)
            {
                _keyID = objILHPOView.LHPOHireDetailsView.LHPONo;                
            }

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_SeriesGeneration_FillValues", ref objDS);
            return objDS;

        }

        public DataSet ReadValues()
        {            
            return objDS;

        }
        public Message SaveLHPORectification()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCODE),
                                            objDAL.MakeInParams("@LHPOId",SqlDbType.Int,0,objILHPOView.keyID),
                                             objDAL.MakeInParams("@LHPO_Freight_Basis_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.FreightTypeID),
                                            objDAL.MakeInParams("@Min_Wt_Guarantee",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.WtGuarantee),
                                            objDAL.MakeInParams("@Rate",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.RateKg),
                                            objDAL.MakeInParams("@Actual_Kms",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.ActualKms),
                                            objDAL.MakeInParams("@Wt_Kms_Articles_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.ActualWtPayableValue),
                                            objDAL.MakeInParams("@Truck_Hire_Charge",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TruckHireCharge),
                                            objDAL.MakeInParams("@OtherCharges",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.OtherCharges),
                                            objDAL.MakeInParams("@Loading_Charges",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.LoadingCharge),
                                            objDAL.MakeInParams("@TDS_Percent",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TDSPercentage),
                                            objDAL.MakeInParams("@TDS_Amount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalTDSAmount),
                                            objDAL.MakeInParams("@Total_Truck_Hire_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalTruckHireCharge),
                                            objDAL.MakeInParams("@Total_Advance_To_Be_Paid",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalAdvancePaid),
                                            objDAL.MakeInParams("@Balance_Payble_Amount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.BalanceAmount),
                                            objDAL.MakeInParams("@Balance_Payable_Hierarchy_Code",SqlDbType.VarChar,2,objILHPOView.LHPOHireDetailsView.HierarchyCode),
                                            objDAL.MakeInParams("@Balance_Payable_Main_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.MainID),
                                             objDAL.MakeInParams("@CharityLedgerId",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.CharityLedgerId),
                                            objDAL.MakeInParams("@CharityAmount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.CharityAmount),
                                             objDAL.MakeInParams("@ATHDetailsXML",SqlDbType.Xml,0,objILHPOView.LHPOAlertsBranchesView.ATHDetailsXML)
             };
            objDAL.RunProc("EC_Opr_LHPORectification_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
               
                 if (objILHPOView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }
            return objMessage;
                                            
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
                                            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_divisionID),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCODE),
                                            objDAL.MakeInParams("@LHPO_Type_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.LHPOTypeID),
                                            objDAL.MakeInParams("@LHPO_Branch_ID",SqlDbType.Int,0,_branchID),
                                            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,_menuitemID),                                            
                                            objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0, objILHPOView.keyID),
                                            objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,0,_hierarchyCode),                                            
                                            objDAL.MakeInParams("@LHPO_Date",SqlDbType.DateTime,0,objILHPOView.LHPOHireDetailsView.LHPODate),
                                            objDAL.MakeInParams("@Main_LHPO_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.LHPONo),
                                            objDAL.MakeInParams("@LHPO_No",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.Next_No),
                                            objDAL.MakeInParams("@LHPO_No_For_Print",SqlDbType.VarChar,20,objILHPOView.LHPOHireDetailsView.LHPO_No),
                                            objDAL.MakeInParams("@Manual_Ref_No",SqlDbType.VarChar,20,objILHPOView.LHPOHireDetailsView.ManualRefNo),
                                            objDAL.MakeInParams("@Vehicle_Category_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.VehicleCategoryID),
                                            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.VehicleID),
                                            objDAL.MakeInParams("@From_Location_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.FromLocationID),
                                            objDAL.MakeInParams("@To_Location_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.ToLocationID),
                                            objDAL.MakeInParams("@Broker_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.BrokerID),
                                            objDAL.MakeInParams("@Is_TDS_Certificate_Broker",SqlDbType.Bit,0,objILHPOView.LHPOHireDetailsView.TDSCertificateToID),
                                            objDAL.MakeInParams("@Total_No_Of_Memo",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.TotalMemos),
                                            objDAL.MakeInParams("@Total_Articles",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.TotalArticle),
                                            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalArticleWT),
                                            objDAL.MakeInParams("@Total_No_Of_GCs",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.TotalGC),
                                            objDAL.MakeInParams("@Driver1_Id",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.Driver1ID),
                                            objDAL.MakeInParams("@Driver2_Id",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.Driver2ID),
                                            objDAL.MakeInParams("@Cleaner_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.CleanerID),
                                            objDAL.MakeInParams("@LHPO_Freight_Basis_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.FreightTypeID),
                                            objDAL.MakeInParams("@Min_Wt_Guarantee",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.WtGuarantee),
                                            objDAL.MakeInParams("@Rate",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.RateKg),
                                            objDAL.MakeInParams("@Actual_Kms",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.ActualKms),
                                            objDAL.MakeInParams("@Wt_Kms_Articles_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.ActualWtPayableValue),
                                            objDAL.MakeInParams("@Truck_Hire_Charge",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TruckHireCharge),
                                            objDAL.MakeInParams("@OtherCharges",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.OtherCharges),
                                            objDAL.MakeInParams("@Loading_Charges",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.LoadingCharge),
                                            objDAL.MakeInParams("@TDS_Percent",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TDSPercentage),
                                            objDAL.MakeInParams("@TDS_Amount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalTDSAmount),
                                            objDAL.MakeInParams("@Total_Truck_Hire_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalTruckHireCharge),
                                            objDAL.MakeInParams("@Total_Advance_To_Be_Paid",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalAdvancePaid),
                                            objDAL.MakeInParams("@Balance_Payble_Amount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.BalanceAmount),
                                            objDAL.MakeInParams("@Balance_Payable_Hierarchy_Code",SqlDbType.VarChar,2,objILHPOView.LHPOHireDetailsView.HierarchyCode),
                                            objDAL.MakeInParams("@Balance_Payable_Main_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.MainID),
                                            objDAL.MakeInParams("@Crossing_Cost_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.CrossingCostPayble),
                                            objDAL.MakeInParams("@To_Pay_Collection",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.ToPayCollection),                
                                            objDAL.MakeInParams("@Delivery_Commission_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.DeliveryCommission),
                                            objDAL.MakeInParams("@Other_Payable",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.OthersPayble),
                                            objDAL.MakeInParams("@Net_Amount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.NetAmount),
                                            objDAL.MakeInParams("@Vehicle_Departure_Time",SqlDbType.VarChar,0,objILHPOView.LHPOHireDetailsView.VehicleDepartureTime),
                                            objDAL.MakeInParams("@Transit_Days",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.TransitDays),
                                            objDAL.MakeInParams("@Commited_Delivery_Date",SqlDbType.DateTime,0,objILHPOView.LHPOHireDetailsView.CommitedDelDate),
                                            objDAL.MakeInParams("@Loading_Supervisor_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.LoadingSupervisorID),
                                            objDAL.MakeInParams("@Remark",SqlDbType.NVarChar,0,objILHPOView.LHPOHireDetailsView.Remark),                            
                                            objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,0),
                                            objDAL.MakeInParams("@MemoGridXML",SqlDbType.Xml,0,objILHPOView.LHPOHireDetailsView.MemoGridXML),                                            
                                            objDAL.MakeInParams("@AlertBranchesXML",SqlDbType.Xml,0,objILHPOView.LHPOAlertsBranchesView.AlertBranchesXML),
                                            objDAL.MakeInParams("@AttachedLHPOBranchesXML",SqlDbType.Xml,0,objILHPOView.LHPOAttachedBranchView.AttachedLHPOBranchesXML),
                                            objDAL.MakeInParams("@ATHDetailsXML",SqlDbType.Xml,0,objILHPOView.LHPOAlertsBranchesView.ATHDetailsXML),
                                            objDAL.MakeInParams("@PenaltyDetailsXML",SqlDbType.Xml,0,objILHPOView.LHPOIncentivesPenaltiesView.PenaltyDetailsXML),
                                            objDAL.MakeInParams("@IncentiveDetailsXML",SqlDbType.Xml,0,objILHPOView.LHPOIncentivesPenaltiesView.IncentiveDetailsXML),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID),
                                            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.Document_Series_Allocation_ID),
                                            objDAL.MakeInParams("@OtherChargesDetailsXML",SqlDbType.Xml,0,objILHPOView.LHPOHireDetailsView.OtherChargesDetailsXML),
                                            objDAL.MakeInParams("@IsTerminatedLHCReceivedCash" ,SqlDbType.Bit,1,objILHPOView.LHPOHireDetailsView.IsLHCTerminatedByReceivedCash),
                                            objDAL.MakeInParams("@TerminatedLHCReceivedCash",SqlDbType.Money,0,objILHPOView.LHPOHireDetailsView.TerminatedLHCReceivedCash),
                                            objDAL.MakeInParams("@IsTerminatedLHCDebitTo",SqlDbType.Bit,1,objILHPOView.LHPOHireDetailsView.IsLHCTerminatedByDebitToLedger),
                                            objDAL.MakeInParams("@TermiantedLHCDebitedLedger",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.TerminatedLHCDebitToLedgerId),
                                            objDAL.MakeInParams("@CharityLedgerId",SqlDbType.Int,0,objILHPOView.LHPOHireDetailsView.CharityLedgerId),
                                            objDAL.MakeInParams("@CharityAmount",SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.CharityAmount),
                                            objDAL.MakeInParams("@DVLP_ID",SqlDbType.Int,0,Util.String2Int(objILHPOView.LHPOHireDetailsView.DVLPID)),
                                            objDAL.MakeInParams("@TotalAfterTDSDeduction", SqlDbType.Decimal,0,objILHPOView.LHPOHireDetailsView.TotalAfterTDSDeduction)
                                            };


            objDAL.RunProc("EC_Opr_LHPO_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                objILHPOView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objILHPOView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Outward/FrmLHPO.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objILHPOView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objILHPOView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        }

    }
}