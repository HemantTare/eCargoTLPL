using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using System.Data.SqlClient;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 24th October 2008
/// Description   : This is the Page For LHPO Forms First Tab takes LHPO Hire Details
/// </summary>
public partial class Operations_Outward_WucLHPOHireDetails : System.Web.UI.UserControl, ILHPOHireDetailsView
{
    #region ClassVariables
    LHPOHireDetailsPresenter objLHPOHireDetailsPresenter;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    private ScriptManager scm_LHPOHireDetails;
    public EventHandler ddlAttachedLHPONoChanged;
    public EventHandler ddlFromLocationSelectionChanged;
    public EventHandler txtTotalAdvancePaid;
    PageControls pc = new PageControls();
    #endregion

    #region ControlsValue

    public string DVLPID
    {
        set { hdn_DVLPID.Value = value; }
        get { return hdn_DVLPID.Value; }
    }
    public string DVLPFromBranchID
    {
        set { hdn_DVLPFromBranchID.Value = value; }
        get { return hdn_DVLPFromBranchID.Value; }
    }

    public int VehicleID
    {
        set { WucVehicleSearch1.VehicleID = value; }
        get { return WucVehicleSearch1.VehicleID; }
    }
    public int VehicleCategoryID
    {
        set { ddl_VehicleCategory.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_VehicleCategory.SelectedValue); }
    }
    public int LHPOTypeID
    {
        set { ddl_LHPOType.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_LHPOType.SelectedValue); }
    }
    public int LHPONo
    {
        set
        {
            ddl_LHPONo.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(ddl_LHPONo.SelectedValue);
        }
    }
    public int BrokerID
    {
        set { ddl_BrokerName.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_BrokerName.SelectedValue); }
    }
    public bool TDSCertificateToID
    {
        set
        {   //ddl_TDSCertificateTo.SelectedValue = Convert.ToString(value); 
            if (value == true)
            {
                ddl_TDSCertificateTo.SelectedValue = "2";
            }
            else
            {
                ddl_TDSCertificateTo.SelectedValue = "1";
            }
        }
        get
        {
            if (ddl_TDSCertificateTo.SelectedValue == "2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public int FreightTypeID
    {
        set { ddl_FreightType.SelectedValue = Util.Int2String(value);
    }
        get { return Util.String2Int(ddl_FreightType.SelectedValue); }
    }
    public int FreightTypeID_Edit
    {
        set { hdn_FreightType.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_FreightType.Value); }
    }
    public int LoadingSupervisorID
    {
        get { return Util.String2Int(ddl_LoadingSupervisor.SelectedValue); }
    }
    public int FromLocationID
    {
        get { return Util.String2Int(ddl_FromLocation.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_FromLocation.SelectedValue); }
    }
    public int ToLocationID
    {
        get { return Util.String2Int(ddl_ToLocation.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_ToLocation.SelectedValue); }
    }
    public int Driver1ID
    {
        get { return Util.String2Int(ddl_Driver1.SelectedValue); }
    }
    public int Driver2ID
    {
        get { return Util.String2Int(ddl_Driver2.SelectedValue); }
    }
    public int CleanerID
    {
        get { return Util.String2Int(ddl_Cleaner.SelectedValue); }
    }
    public int CharityLedgerId
    {
        get { return Util.String2Int(ddl_Charity.SelectedValue); }
    }
    public string VehicleOwner
    {
        set { lbl_OwnerValue.Text = value; }
        get { return lbl_OwnerValue.Text; }
    }
    public int VehicleCapacity
    {
        set { lbl_VehicleCapacityValue.Text = Util.Int2String(value) + "  Kg"; }
        get { return Util.String2Int(lbl_VehicleCapacityValue.Text); }
    }
    public int TotalMemos
    {
        set { hdn_Total_No_of_GC.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Total_No_of_GC.Value); }
    }
    public int TotalArticle
    {
        set
        {
            txt_TotalArticle.Text = Util.Int2String(value);
            hdn_TotalArticle.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_TotalArticle.Value); }
    }
    public int TotalGC
    {
        set
        {
            txt_TotalGC.Text = Util.Int2String(value);
            hdn_TotalGC.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_TotalGC.Value); }
    }
    public int TransitDays
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            txt_TransitDays.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_TransitDays.Text); }
    }
    public int MainID
    {
        set { WucHierarchyWithID1.MainId = value; }
        get { return WucHierarchyWithID1.MainId; }
    }

    public int ToLocationBranchId
    {
        set { hdn_ToLocationBranchId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ToLocationBranchId.Value); }
    }
    // -- Decimal Property

    public decimal TruckHireCharge
    {
        set
        {
            lbl_TruckHireChargeValue.Text = Util.Decimal2String(value);
            hdn_TruckHireCharge.Value = Util.Decimal2String(value);
            txt_TruckHireCharge.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TruckHireCharge.Value); }
    }
    public decimal TotalTruckHireCharge
    {
        set
        {
            lbl_TotalTruckHireValue.Text = Util.Decimal2String(value);
            hdn_TotalTruckHire.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalTruckHire.Value); }
    }
    public decimal BalanceAmount
    {
        set
        {
            lbl_BalanceAmountValue.Text = Util.Decimal2String(value);
            hdn_BalanceAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_BalanceAmount.Value); }
    }
    public decimal TotalAdvancePaid
    {
        set { txt_TotalAdvance.Text = Util.Decimal2String(value); }
        get { return txt_TotalAdvance.Text == string.Empty ? 0 : Util.String2Decimal(txt_TotalAdvance.Text); }
    }
    public decimal CrossingCostPayble
    {
        set
        {
            lbl_CrossingCostValue.Text = Util.Decimal2String(value);
            hdn_TotalCrossingCost.Value = Util.Decimal2String(value);
        }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(hdn_TotalCrossingCost.Value);
            }
        }
    }
    public decimal ToPayCollection
    {
        set
        {
            lbl_ToPayCollectionValue.Text = Util.Decimal2String(value);
            hdn_TotalToPayCollection.Value = Util.Decimal2String(value);
        }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(hdn_TotalToPayCollection.Value);
            }
        }
    }

    public decimal DeliveryCommission
    {
        set
        {
            lbl_DeliveryCommissionValue.Text = Util.Decimal2String(value);
            hdn_TotalDeliveryCommision.Value = Util.Decimal2String(value);
        }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(hdn_TotalDeliveryCommision.Value);
            }
        }
    }
    public decimal OthersPayble
    {
        set { txt_Others.Text = Util.Decimal2String(value); }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(txt_Others.Text);
            }
        }
    }
    public decimal TotalPayable
    {
        set
        {
            lbl_TotalPayableValue.Text = Util.Decimal2String(value);
            hdn_TotalPayable.Value = Util.Decimal2String(value);
        }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(hdn_TotalPayable.Value);
            }
        }
    }

    public decimal NetAmount
    {
        set
        {
            lbl_NetAmountValue.Text = Util.Decimal2String(value);
            hdn_NetAmount.Value = Util.Decimal2String(value);
        }
        get
        {
            if (pc.Control_Is_Mandatory(Pnl_OtherPayable) == false)
            {
                return 0;
            }
            else
            {
                return Util.String2Decimal(hdn_NetAmount.Value);
            }
        }
    }
    public decimal TotalArticleWT
    {
        set
        {
            txt_TotalArticleWT.Text = Util.Decimal2String(value);
            hdn_TotalArticleWT.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalArticleWT.Value); }
    }
    public decimal WtGuarantee
    {
        set { txt_WtGuarantee.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_WtGuarantee.Text); }
    }
    public decimal WtGuarantee_Edit
    {
        set { hdn_WtGuarantee.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_WtGuarantee.Value); }
    }
    public decimal RateKg
    {
        set { txt_RateKg.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_RateKg.Text); }
    }
    public decimal RateKg_Edit
    {
        set { hdn_RateKg.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_RateKg.Value); }
    }
    public decimal ActualWtPayableValue
    {
        set
        {
            lbl_ActualWtPayableValue.Text = Util.Decimal2String(value);
            hdn_ActualWtPayable.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ActualWtPayable.Value); }
    }

    //public decimal ActualWtPayableValue_Edit
    //{
    //    set { hdn_ActualWtPayable.Value = Util.Decimal2String(value); }
    //    get { return Util.String2Decimal(hdn_ActualWtPayable.Value); }
    //}
    public decimal LoadingCharge
    {
        set { txt_LoadingCharge.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_LoadingCharge.Text); }
    }
    public decimal ActualKms
    {
        set
        {
            if (value <= 0)
            {
                value = 0;
            }
            lbl_ActualKmsValue.Text = Util.Decimal2String(value);
            hdn_ActualKms.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ActualKms.Value); }
    }
    public decimal TDSPercentage
    {
        set
        {
            lbl_TDSPerValue.Text = Util.Decimal2String(value);
            hdn_TDSPer.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSPer.Value); }
    }
    public decimal TDSAmount
    {
        set
        {
            lbl_TDSPerValue1.Text = Util.Decimal2String(value);
            hdn_TDSAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSAmount.Value); }
    }

    public decimal TotalTDSAmount
    {
        set
        {
            lbl_TDSAmountValue.Text = Util.Decimal2String(value);
            hdn_TDSAmountValue.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TDSAmountValue.Value); }
    }
    public decimal ExemptionLimit
    {
        set
        {
            lbl_ExemptionLimitPer.Text = Util.Decimal2String(value);
            hdn_ExemptionLimit.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ExemptionLimit.Value); }
    }

    public decimal ExemptionLimitAmount
    {
        set
        {
            lbl_ExemptionLimitAmountValue.Text = Util.Decimal2String(value);
            hdn_ExemptionLimitAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ExemptionLimitAmount.Value); }
    }
    public decimal Surcharge
    {
        set
        {
            lbl_SurchargePer.Text = Util.Decimal2String(value);
            hdn_Surcharge.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Surcharge.Value); }
    }

    public decimal SurchargeAmount
    {
        set
        {
            lbl_SurchargeAmountValue.Text = Util.Decimal2String(value);
            hdn_SurchargeAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_SurchargeAmount.Value); }

    }
    public decimal AddlSurchargeCess
    {
        set
        {
            lbl_Addl_Surcharges_CessPer.Text = Util.Decimal2String(value);
            hdn_Addl_Surcharges_CessPer.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Addl_Surcharges_CessPer.Value); }
    }

    public decimal AddlSurchargeCessAmount
    {
        set
        {
            lbl_AddlSurchargeCessAmountValue.Text = Util.Decimal2String(value);
            hdn_AddlSurchargeCessAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_AddlSurchargeCessAmount.Value); }

    }
    public decimal AddlEducationCess
    {
        set
        {
            lbl_Addl_Education_CessPer.Text = Util.Decimal2String(value);
            hdn_Addl_Education_Cess.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Addl_Education_Cess.Value); }
    }
    public decimal AddlEducationCessAmount
    {
        set
        {
            lbl_AddlEducationCessAmountValue.Text = Util.Decimal2String(value);
            hdn_AddlEducationCessAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_AddlEducationCessAmount.Value); }
    }
    public decimal OtherCharges
    {
        set
        {
            txt_OtherCharges.Text = Util.Decimal2String(value);
            hdn_OtherCharges.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_OtherCharges.Value); }
    }

    public string OtherChargesDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            if (StateManager.IsValidSession("SessionOtherChargeGrid"))
            {
                //SessionOtherChargesGrid = StateManager.GetState<DataSet>("SessionOtherChargeGrid");
                _objDs = StateManager.GetState<DataSet>("SessionOtherChargeGrid");
                return _objDs.GetXml();
            }
            else
            {
                return "<doc></doc>";
            }
        }
    }
    public string ManualRefNo
    {
        set { txt_ManualRefNo.Text = value; }
        get { return txt_ManualRefNo.Text; }
    }
    public string Remark
    {
        set { txt_Remark.Text = value; }
        get { return txt_Remark.Text; }
    }
    public string HierarchyCode
    {
        set { WucHierarchyWithID1.HierarchyCode = value; }
        get
        {
            if (Util.String2Decimal(hdn_BalanceAmount.Value) <= 0)
            {
                WucHierarchyWithID1.HierarchyCode = "0";
            }
            return WucHierarchyWithID1.HierarchyCode;
        }
    }
    public string VehicleDepartureTime
    {
        set { Wuc_VehicleDepartureTime.setTime(value); }
        get { return Wuc_VehicleDepartureTime.getTime(); }
    }
    public string LHPONOForPrint
    {
        set { lbl_LHPONoValue.Text = value; }
    }
    public string MemoGridXML
    {
        get
        {
            DataSet ds = new DataSet();
            ds = CheckDataSet();
            return ds.GetXml().ToLower();
        }
    }
    public DateTime LHPODate
    {
        set { WucLHPODate.SelectedDate = value; }
        get { return WucLHPODate.SelectedDate; }
    }

    public DateTime CommitedDelDate
    {
        set
        {
            lbl_CommitedDelDateValue.Text = String.Format("{0:MMMM dd, yyyy}", value);
            hdn_CommitedDelDate.Value = String.Format("{0:MMMM dd, yyyy}", value);
        }
        get { return Convert.ToDateTime(hdn_CommitedDelDate.Value); }
    }
    public DateTime AttachedLHPODate
    {
        set { hdn_AttachedLHPODate.Value = String.Format("{0:MMM/dd/yyyy}", value); }
        get
        {
            if (hdn_AttachedLHPODate.Value != "")
            {
                return Convert.ToDateTime(hdn_AttachedLHPODate.Value);
            }
            else
            {
                return DateTime.Now;
            }
        }
    }

    // Integer Property

    public int Next_No
    {
        set { hdn_Next_No.Value = value.ToString(); }
        get
        {
            if (UserManager.getUserParam().IsLHPOSeriesReq == true)
            {
                return Util.String2Int(LHPO_No);
            }
            else
            {
                return Util.String2Int(hdn_Next_No.Value);
            }
        }
    }
    public int Start_No
    {
        set { hdn_Start_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_Start_No.Value); }
    }
    public int End_No
    {
        set { hdn_End_No.Value = value.ToString(); }
        get { return Util.String2Int(hdn_End_No.Value); }
    }
    private string Start_End_No
    {
        get { return lbl_Start_End_No.Text; }
        set { lbl_Start_End_No.Text = value; }
    }
    public string LHPO_No
    {
        set
        {
            //hdn_Padded_Next_No.Value = value;
            lbl_LHPONoValue.Text = value;
        }
        get { return lbl_LHPONoValue.Text.Trim(); }
    }

    public int Document_Series_Allocation_ID
    {
        get { return Util.String2Int(hdn_Document_Allocation_ID.Value); }
        set { hdn_Document_Allocation_ID.Value = value.ToString(); }
    }

    public int MenuItemId
    {
        get { return Raj.EC.Common.GetMenuItemId(); }
    }

    public int Mode
    {
        get { return Util.DecryptToInt(Request.QueryString["Mode"]); }
        set { hdn_Mode.Value = value.ToString(); }
    }
    public bool IsLHCTerminatedByReceivedCash
    {
        set { Chk_LHCTerminatedByReceivingCash.Checked = value; }
        get { return Chk_LHCTerminatedByReceivingCash.Checked; }
    }

    public decimal TerminatedLHCReceivedCash
    {
        set { txt_ReceivedAmtTerminatedLHC.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_ReceivedAmtTerminatedLHC.Text); }
    }
    public bool IsLHCTerminatedByDebitToLedger
    {
        set { Chk_LHCTerminatedByDebit.Checked = value; }
        get { return Chk_LHCTerminatedByDebit.Checked; }
    }
    public int TerminatedLHCDebitToLedgerId
    {
        get { return Util.String2Int(ddl_LHCTermiantedByDebitToLedger.SelectedValue); }
    }

    public decimal CharityAmount
    {
        set
        {
            txt_CharityAmount.Text = Util.Decimal2String(value);
            hdn_CharityAmount.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_CharityAmount.Value); }
    }
    public decimal TotalAfterTDSDeduction
    {
        set
        {

            lbl_TotalAfterTDSDedValue.Text = Util.Decimal2String(value);
            hdn_TotalAfterTDSDedValue.Value = Util.Decimal2String(value);

        }
        get { return Util.String2Decimal(hdn_TotalAfterTDSDedValue.Value); }
    }

    #endregion

    #region LHPOParametersValues
    private int FromLocationParameterId
    {
        set { hdn_FromLocation_Parameter.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_FromLocation_Parameter.Value); }
    }
    private int ToLocationParameterId
    {
        set { hdn_ToLocation_Parameter.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ToLocation_Parameter.Value); }
    }
    private string BalancePayAtParameter
    {
        set { hdn_Balance_Pay_At_Parameter.Value = value; }
        get { return hdn_Balance_Pay_At_Parameter.Value; }
    }
    private bool IsOtherChargeEnabledParameter
    {
        set { chk_Is_ATH_Enabled.Checked = value; }
        get { return chk_Is_ATH_Enabled.Checked; }
    }

    private bool IsPostBackRequiredOnAdvanceAmt
    {
        set { chk_Is_PostBack_On_Advance_Amt.Checked = value; }
        get { return chk_Is_PostBack_On_Advance_Amt.Checked; }
    }
    #endregion

    #region ControlsBind
    public DataTable Bind_ddl_VehicleCategory
    {
        set
        {
            ddl_VehicleCategory.DataSource = value;
            ddl_VehicleCategory.DataTextField = "Vehicle_Category";
            ddl_VehicleCategory.DataValueField = "Vehicle_Category_ID";
            ddl_VehicleCategory.DataBind();
            ddl_VehicleCategory.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    public DataTable Bind_ddl_LHPOType
    {
        set
        {
            ddl_LHPOType.DataSource = value;
            ddl_LHPOType.DataTextField = "LHPO_Type";
            ddl_LHPOType.DataValueField = "LHPO_Type_ID";
            ddl_LHPOType.DataBind();
            ddl_LHPOType.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    public DataTable Bind_ddl_LHPONo
    {
        set
        {
            ddl_LHPONo.DataSource = value;
            ddl_LHPONo.DataTextField = "LHPO_No_For_Print";
            ddl_LHPONo.DataValueField = "LHPO_ID";
            ddl_LHPONo.DataBind();
            ddl_LHPONo.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    public DataTable Bind_ddl_BrokerName
    {
        set
        {
            ddl_BrokerName.DataSource = value;
            ddl_BrokerName.DataTextField = "Vendor_Name";
            ddl_BrokerName.DataValueField = "Vendor_ID";
            ddl_BrokerName.DataBind();
            ddl_BrokerName.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }

    public DataTable Bind_ddl_FreightType
    {
        set
        {
            ddl_FreightType.DataSource = value;
            ddl_FreightType.DataTextField = "LHPO_Freight_Basis";
            ddl_FreightType.DataValueField = "LHPO_Freight_Basis_ID";
            ddl_FreightType.DataBind();
            ddl_FreightType.Items.Insert(0, new ListItem("----Select One----", "0"));
        }
    }
    public DataSet Bind_dg_LHPOHireDetails
    {
        set
        {
            // SessionContractTermsGrid = value;
            dg_LHPOHireDetails.DataSource = value;
            dg_LHPOHireDetails.DataBind();
        }
    }
    public DataSet SessionLHPOHireDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("LHPOHireDetails"); }
        set { StateManager.SaveState("LHPOHireDetails", value); }
    }

    public DataSet SessionATHDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("ATHDetails"); }
        set { StateManager.SaveState("ATHDetails", value); }
    }

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (VehicleCategoryID == 0)
        {
            errorMessage = "Please Select Vehicle Category";//GetLocalResourceObject("Msg_VehicleCategoryResource").ToString();
            ddl_VehicleCategory.Focus();
        }
        if (Datemanager.IsValidProcessDate("OPR_LHPO", LHPODate) == false)
        {
            errorMessage = "Please Select Valid LHPO Date";//GetLocalResourceObject("Msg_LHPODate").ToString();
        }
        else if (LHPOTypeID == 1 && keyID <= 0 && UserManager.getUserParam().IsLHPOSeriesReq == true && (Util.String2Int(LHPO_No) < Start_No || Util.String2Int(LHPO_No) > End_No))
        {
            errorMessage = CompanyManager.getCompanyParam().LHPOCaption + " No. Should be Between " + Start_No + " and " + End_No;
            lbl_LHPONoValue.Focus();
        }
        else if (VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle No";//GetLocalResourceObject("Msg_VehicleNoResource").ToString();
            WucVehicleSearch1.Focus();
        }
        else if (LHPOTypeID == 0)
        {
            errorMessage = "Please Select LHPO Type";// GetLocalResourceObject("Msg_LHPOTypeResource").ToString();
            ddl_LHPOType.Focus();
        }
        else if (CheckLHPONo() == false)
        {
            _isValid = false;
        }
        else if (CheckBrokerNameWithTDS() == false)
        {
            _isValid = false;
        }
        else if (VehicleCategoryID != 1 && ValidateAdvanceAmount() == false)
        {
            _isValid = false;
        }
        else if (FromLocationID <= 0)
        {
            errorMessage = "Please Select From Location";//GetLocalResourceObject("Msg_FromLocationResource").ToString();         
        }
        else if (ToLocationID <= 0)
        {
            errorMessage = "Please Select To Location"; //GetLocalResourceObject("Msg_ToLocationResource").ToString();
        }
        else if (ddl_Driver1.SelectedValue == "")
        {
            errorMessage = "Please Enter Driver 1";//GetLocalResourceObject("Msg_Driver1Resource").ToString();
        }
        else if (ddl_Driver1.SelectedValue == ddl_Driver2.SelectedValue)
        {
            errorMessage = "Driver 1 and Driver 2 Should Not be Same";//GetLocalResourceObject("Msg_Driver").ToString();            
        }
        else if (CheckTotalNoofMemos() == false)
        {
            errorMessage = "Please Select atleast One Manifest"; //GetLocalResourceObject("Msg_Grid").ToString();
            _isValid = false;
            dg_LHPOHireDetails.Focus();
        }
        else if (FreightTypeID == 0)
        {
            errorMessage = "Please Select Freight Type"; //GetLocalResourceObject("Msg_FreightTypeResource").ToString();
            ddl_FreightType.Focus();
        }
        else if (VehicleCategoryID != 1 && Util.String2Decimal(hdn_BalanceAmount.Value) > 0 && WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {
            WucHierarchyWithID1.Focus();
            _isValid = false;
        }
        else if (LoadingSupervisorID <= 0)
        {
            errorMessage = "Please Select Loading Supervisor";//GetLocalResourceObject("Msg_LoadingSupervisorResource").ToString();           
            _isValid = false;
        }
        else if (CharityAmount > TotalAfterTDSDeduction)
        {
            errorMessage = "Charity Amount Cannot Be Greater than  TotalafterTDSDeduction";
            txt_CharityAmount.Text = "";
            txt_CharityAmount.Focus();
            _isValid = false;
        }
        else if (VehicleCategoryID != 1 && CharityLedgerId > 0 && CharityAmount <= 0)
        {
            errorMessage = "Please Enter Charity Amount";
            _isValid = false;
        }
        else if (VehicleCategoryID != 1 && CharityAmount > 0 && CharityLedgerId < 0)
        {
            errorMessage = "Please Select Charity Ledger";
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }


    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {

            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }
    }

    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_LHPOHireDetails = value; }
    }

    #endregion

    #region OtherMethods

    private bool CheckBrokerNameWithTDS()
    {
        if (VehicleCategoryID == 5)
        {
            if (BrokerID == 0 && pc.Control_Is_Mandatory(ddl_BrokerName) == true)
            {
                errorMessage = "Please Select Broker Name";
                ddl_BrokerName.Focus();
                return false;
            }
            else if (ddl_TDSCertificateTo.SelectedValue == "0" && pc.Control_Is_Mandatory(ddl_TDSCertificateTo) == true)
            {
                errorMessage = "Please Select TDS Certificate To";
                ddl_TDSCertificateTo.Focus();
                return false;
            }
        }
        return true;
    }
    private bool ValidateAdvanceAmount()
    {

        bool return_Value;
        decimal MaxAdvanceAmt = 0;
        MaxAdvanceAmt = TotalTruckHireCharge * Util.String2Decimal(hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge.Value);
        MaxAdvanceAmt = MaxAdvanceAmt / 100;
        if (TotalAdvancePaid > MaxAdvanceAmt)
        {
            lbl_Errors.Text = "Advance Amount Should not be Greater than " + " " + hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge.Value + "%" + " " + "i.e" + " " + MaxAdvanceAmt + " " + "Of Total Truck Hire Charge";
            return_Value = false;
        }
        else
        {
            return_Value = true;
        }
        return return_Value;
    }
    private bool CheckLHPONo()
    {

        if (LHPOTypeID == 2)
        {
            if (LHPONo == 0)
            {
                errorMessage = "Please Select LHPO No";//GetLocalResourceObject("Msg_DDLLHPONoResource").ToString();
                ddl_LHPONo.Focus();
                return false;
            }
        }
        return true;
    }
    public void SetFromLocationID(string FromLocationName, string FromLocationID)
    {
        ddl_FromLocation.DataTextField = "Service_Location_Name";
        ddl_FromLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(FromLocationName, FromLocationID, ddl_FromLocation);
    }
    public void SetToLocationID(string ToLocationName, string ToLocationID)
    {
        ddl_ToLocation.DataTextField = "Service_Location_Name";
        ddl_ToLocation.DataValueField = "Service_Location_ID";
        Raj.EC.Common.SetValueToDDLSearch(ToLocationName, ToLocationID, ddl_ToLocation);
    }
    public void SetDriver1ID(string Driver1Name, string Driver1ID)
    {
        ddl_Driver1.DataTextField = "Driver_Name";
        ddl_Driver1.DataValueField = "Driver_ID";
        Raj.EC.Common.SetValueToDDLSearch(Driver1Name, Driver1ID, ddl_Driver1);
    }
    public void SetDriver2ID(string Driver2Name, string Driver2ID)
    {
        ddl_Driver2.DataTextField = "Driver_Name";
        ddl_Driver2.DataValueField = "Driver_ID";
        Raj.EC.Common.SetValueToDDLSearch(Driver2Name, Driver2ID, ddl_Driver2);
    }
    public void SetCleanerID(string CleanerName, string CleanerID)
    {
        ddl_Cleaner.DataTextField = "Cleaner_Name";
        ddl_Cleaner.DataValueField = "Cleaner_ID";
        Raj.EC.Common.SetValueToDDLSearch(CleanerName, CleanerID, ddl_Cleaner);
    }
    public void SetLoadingSupervisorID(string LoadingSupervisorName, string LoadingSupervisorID)
    {
        ddl_LoadingSupervisor.DataTextField = "Supervisor";
        ddl_LoadingSupervisor.DataValueField = "Supervisor_ID";
        Raj.EC.Common.SetValueToDDLSearch(LoadingSupervisorName, LoadingSupervisorID, ddl_LoadingSupervisor);
    }
    public void SetLedgerForTerminatedLHC(string LedgerName, string LedgerId)
    {
        ddl_LHCTermiantedByDebitToLedger.DataTextField = "Ledger_Name";
        ddl_LHCTermiantedByDebitToLedger.DataValueField = "Ledger_ID";
        Raj.EC.Common.SetValueToDDLSearch(LedgerName, LedgerId, ddl_LHCTermiantedByDebitToLedger);
    }
    public void SetCharityLedger(string LedgerName, string LedgerId)
    {
        ddl_Charity.DataTextField = "CharityLedger_Name";
        ddl_Charity.DataValueField = "CharityLedger_Id";
        Raj.EC.Common.SetValueToDDLSearch(LedgerName, LedgerId, ddl_Charity);
    }
    private void SetAllDDLSearch()
    {
        ddl_FromLocation.DataTextField = "Service_Location_Name";
        ddl_FromLocation.DataValueField = "Service_Location_ID";

        ddl_ToLocation.DataTextField = "Service_Location_Name";
        ddl_ToLocation.DataValueField = "Service_Location_ID";

        ddl_Driver1.DataTextField = "Driver_Name";
        ddl_Driver1.DataValueField = "Driver_ID";

        ddl_Driver2.DataTextField = "Driver_Name";
        ddl_Driver2.DataValueField = "Driver_ID";

        ddl_Cleaner.DataTextField = "Cleaner_Name";
        ddl_Cleaner.DataValueField = "Cleaner_ID";

        ddl_LoadingSupervisor.DataTextField = "Supervisor";
        ddl_LoadingSupervisor.DataValueField = "Supervisor_ID";

        ddl_LHCTermiantedByDebitToLedger.DataTextField = "Ledger_Name";
        ddl_LHCTermiantedByDebitToLedger.DataValueField = "Ledger_Id";

        ddl_Charity.DataTextField = "CharityLedger_Name";
        ddl_Charity.DataValueField = "CharityLedger_Id";

        //string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }
    private void EnabledDisabledBroker()
    {

        if (VehicleCategoryID == 5)
        {
            ddl_BrokerName.Visible = true;
            lbl_BrokerName.Visible = true;
            ddl_TDSCertificateTo.Visible = true;
            lbl_TDSCertificateTo.Visible = true;
            lbl_Man1.Visible = true;
            lbl_Man2.Visible = true;
            td_Broker.Visible = true;
        }
        else
        {
            ddl_BrokerName.Visible = true;
            lbl_BrokerName.Visible = true;
            ddl_TDSCertificateTo.Visible = true;
            lbl_TDSCertificateTo.Visible = true;
            lbl_Man1.Visible = true;
            lbl_Man2.Visible = true;
            td_Broker.Visible = true;
        }
    }
    private void EnabledDisableOwner()
    {
        if (VehicleCategoryID == 1)
        {
            //tr_Owner.Visible = false;
            lbl_Owner.Visible = false;
            lbl_OwnerValue.Visible = false;
        }
        else
        {
            //tr_Owner.Visible = true;
            lbl_Owner.Visible = true;
            lbl_OwnerValue.Visible = true;
        }
        UpdatePanel2.Update();
    }
    private DataSet CheckDataSet()
    {
        DataSet _objDs = new DataSet();
        int i;
        CheckBox Chk_Attach;
        DataRow _dr;
        _objDs.Tables.Add(SessionLHPOHireDetailsGrid.Tables[0].Clone());
        for (i = 0; i < dg_LHPOHireDetails.Items.Count; i++)
        {
            Chk_Attach = (CheckBox)dg_LHPOHireDetails.Items[i].FindControl("Chk_Attach");
            if (Chk_Attach.Checked == true)
            {
                _dr = _objDs.Tables[0].NewRow();
                _dr = SessionLHPOHireDetailsGrid.Tables[0].Rows[i];
                _objDs.Tables[0].ImportRow(_dr);
            }
        }
        return _objDs;
    }
    private void SetLabels()
    {
        //txt_TotalGC.Text = hdn_TotalGC.Value;
        txt_TotalArticle.Text = hdn_TotalArticle.Value;
        txt_TotalArticleWT.Text = hdn_TotalArticleWT.Value;
        lbl_ActualKmsValue.Text = hdn_ActualKms.Value;
        lbl_ActualWtPayableValue.Text = hdn_ActualWtPayable.Value;
        lbl_TruckHireChargeValue.Text = hdn_TruckHireCharge.Value;
        txt_TruckHireCharge.Text = hdn_TruckHireCharge.Value;
        txt_OtherCharges.Text = hdn_OtherCharges.Value;
        lbl_BalanceAmountValue.Text = hdn_BalanceAmount.Value;
        lbl_TDSPerValue1.Text = hdn_TDSAmount.Value;
        lbl_TDSAmountValue.Text = hdn_TDSAmountValue.Value;
        lbl_SurchargeAmountValue.Text = hdn_SurchargeAmount.Value;
        lbl_AddlSurchargeCessAmountValue.Text = hdn_AddlSurchargeCessAmount.Value;
        lbl_AddlEducationCessAmountValue.Text = hdn_AddlEducationCessAmount.Value;
        lbl_TotalTruckHireValue.Text = hdn_TotalTruckHire.Value;
        lbl_CommitedDelDateValue.Text = hdn_CommitedDelDate.Value;
        lbl_TotalAfterTDSDedValue.Text = hdn_TotalAfterTDSDedValue.Value;
        txt_CharityAmount.Text = hdn_CharityAmount.Value;
    }
    private void SetUpdatePanelUpdate()
    {
        Upd_Pnl_lbl_VehicleCapacityValue.Update();
        Upd_Pnl_ddl_LHPONo.Update();
        Upd_Pnl_lbl_OwnerValue.Update();
        Upd_Pnl_BrokerVisible.Update();
        Upd_Pnl_ddl_Driver1.Update();
        Upd_Pnl_ddl_Driver2.Update();
        Upd_Pnl_ddl_Cleaner.Update();
        Upd_Pnl_dg_LHPOHireDetails.Update();
        Upd_Pnl_TotalofGrid.Update();
        Upd_Pnl_lbl_ActualKmsValue.Update();
        // Upd_Pnl_lbl_TDSPerValue.Update();
        Upd_Pnl_lbl_VehicleCapacityValue.Update();
        Upd_Pnl_txt_ManualRefNo.Update();
        Upd_Pnl_ddl_FromLocation.Update();
        Upd_Pnl_ddl_ToLocation.Update();
        Upd_Pnl_pnl_VehicleHireDetails.Update();
        Upd_Pnl_pnl_TotalTDSCalculation.Update();
        Upd_Pnl_OtherPayable.Update();
        //Upd_Pnl_pnl_BalanceCalculation.Update();
        Upd_Pnl_Wuc_VehicleDepartureTime.Update();
        Upd_Pnl_ddl_LoadingSupervisor.Update();
        Upd_Pnl_txt_TransitDays.Update();
        Upd_Pnl_lbl_CommitedDelDateValue.Update();
        //Upd_Pnl_txt_Remark.Update();
        Upd_Pnl_WucVehicleSearch1.Update();
        Upd_Pnl_ddl_LHPOType.Update();
        //Upd_Pnl_HIDDENFields.Update();
        Upd_Pnl_hdn_AttachedLHPODate.Update();
        Upd_Pnl_hdn_ToLocationBranchId.Update();
        Upd_Pnl_hdn_LHPOId.Update();
        UpdatePanel1.Update();
        UpdatePanel2.Update();
    }
    private void EnabledDisabledPageControl()
    {
        TextBox Txt_Driver1 = (TextBox)ddl_Driver1.FindControl("txtBoxddl_Driver1");
        TextBox Txt_Driver2 = (TextBox)ddl_Driver2.FindControl("txtBoxddl_Driver2");
        TextBox Txt_Cleaner = (TextBox)ddl_Cleaner.FindControl("txtBoxddl_Cleaner");
        TextBox Txt_FromLocation = (TextBox)ddl_FromLocation.FindControl("txtBoxddl_FromLocation");
        TextBox Txt_ToLocation = (TextBox)ddl_ToLocation.FindControl("txtBoxddl_ToLocation");
        TextBox Txt_LoadingSupervisor = (TextBox)ddl_LoadingSupervisor.FindControl("txtBoxddl_LoadingSupervisor");
        TextBox Txt_Charity = (TextBox)ddl_Charity.FindControl("txtBoxddl_Charity");

        if (LHPOTypeID == 2)
        {
            //txt_ManualRefNo.Enabled = false;
            ddl_FromLocation.Enabled = false;
            ddl_ToLocation.Enabled = false;
            ddl_BrokerName.Enabled = false;
            ddl_TDSCertificateTo.Enabled = false;
            ddl_Driver1.Enabled = false;
            ddl_Driver2.Enabled = false;
            ddl_Cleaner.Enabled = false;
            ddl_FreightType.Enabled = false;
            txt_WtGuarantee.Enabled = false;
            txt_RateKg.Enabled = false;
            txt_TruckHireCharge.Enabled = false;
            txt_TotalAdvance.Enabled = false;
            txt_LoadingCharge.Enabled = false;
            txt_Others.Enabled = false;
            //ddl_LoadingSupervisor.Enabled = false;
            //txt_TransitDays.Enabled = false;
            txt_Remark.Enabled = false;
            Wuc_VehicleDepartureTime.SetEnabled = false;
            WucHierarchyWithID1.Set_Location_Visible = false;
            WucHierarchyWithID1.SetEnabled = false;
            Upd_Pnl_pnl_TotalTDSCalculation.Update();
            lbtn_AddDriver.Enabled = false;
            ddl_Charity.Enabled = false;
            txt_CharityAmount.Enabled = false;


            if (WucHierarchyWithID1.HierarchyCode == "HO")
            {
                WucHierarchyWithID1.Set_Location_Visible = false;
            }

        }
        else
        {
            lbtn_AddDriver.Enabled = true;
            txt_ManualRefNo.Enabled = true;
            txt_ManualRefNo.Text = "";
            ddl_FromLocation.Enabled = true;
            //Txt_FromLocation.Text = "";
            ddl_ToLocation.Enabled = true;
            //Txt_ToLocation.Text = "";
            ddl_BrokerName.Enabled = true;
            ddl_BrokerName.SelectedValue = "0";
            ddl_TDSCertificateTo.Enabled = true;
            ddl_TDSCertificateTo.SelectedValue = "0";
            ddl_Driver1.Enabled = true;
            if (LHPOTypeID != 1)
            {
                Txt_Driver1.Text = "";
                Txt_Driver2.Text = "";
                Txt_Cleaner.Text = "";
            }
            ddl_Driver2.Enabled = true;
            ddl_Cleaner.Enabled = true;
            ddl_FreightType.Enabled = true;
            ddl_FreightType.SelectedValue = "0";
            txt_WtGuarantee.Enabled = true;
            WtGuarantee = 0;
            txt_RateKg.Enabled = true;
            RateKg = 0;
            txt_TruckHireCharge.Enabled = true;
            TruckHireCharge = 0;
            txt_TotalAdvance.Enabled = true;
            TotalAdvancePaid = 0;
            txt_LoadingCharge.Enabled = true;
            LoadingCharge = 0;
            txt_Others.Enabled = true;
            Wuc_VehicleDepartureTime.SetEnabled = true;
            if (Mode == 4)
            {
                WucHierarchyWithID1.SetEnabled = false;
            }
            else
            {
                WucHierarchyWithID1.SetEnabled = true;
            }
            OthersPayble = 0;
            ddl_LoadingSupervisor.Enabled = true;
            Txt_LoadingSupervisor.Text = "";
            txt_TransitDays.Enabled = true;
            TransitDays = 0;
            txt_Remark.Enabled = true;
            txt_Remark.Text = "";
            ActualKms = 0;
            ActualWtPayableValue = 0;
            BalanceAmount = 0;
            lbl_CommitedDelDateValue.Text = "";
            hdn_CommitedDelDate.Value = "";
            TotalTDSAmount = 0;
            TDSAmount = 0;
            lbl_TDSPerValue.Text = "0";
            AddlEducationCess = 0;
            AddlEducationCessAmount = 0;
            AddlSurchargeCess = 0;
            AddlSurchargeCessAmount = 0;
            Surcharge = 0;
            SurchargeAmount = 0;
            TotalPayable = 0;
            TotalGC = 0;
            TotalTruckHireCharge = 0;
            Wuc_VehicleDepartureTime.setTime("24");
            VehicleDepartureTime = DateTime.Now.ToShortTimeString();
            CommitedDelDate = DateTime.Now;
            TotalArticle = 0;
            TotalArticleWT = 0;
            CrossingCostPayble = 0;
            DeliveryCommission = 0;
            ToPayCollection = 0;
            lbl_CrossingCostValue.Text = "0";
            hdn_CrossingCost.Value = "0";
            lbl_DeliveryCommissionValue.Text = "0";
            hdn_DeliveryCommission.Value = "0";
            txt_Others.Text = "0";
            TotalPayable = 0;
            NetAmount = 0;
            CharityAmount = 0;
            txt_CharityAmount.Enabled = true;
            ddl_Charity.Enabled = true;
            objLHPOHireDetailsPresenter.FillGrid();
        }
        if (VehicleCategoryID == 1)
        {
            WucHierarchyWithID1.SetEnabled = false;
            ddl_Charity.Enabled = false;
            txt_CharityAmount.Enabled = false;
        }
    }
    private void DisableControlForRectification()
    {
        txt_ManualRefNo.Enabled = false;
        ddl_FromLocation.Enabled = false;
        ddl_ToLocation.Enabled = false;
        ddl_BrokerName.Enabled = false;
        ddl_TDSCertificateTo.Enabled = false;
        ddl_Driver1.Enabled = false;
        ddl_Driver2.Enabled = false;
        ddl_Cleaner.Enabled = false;
        dg_LHPOHireDetails.Enabled = false;
        ddl_LoadingSupervisor.Enabled = false;
        WucLHPODate.Enabled = false;
        TD_Calender.Visible = false;
    }

    private void EnabledDisabledLHPONo()
    {
        ddl_LHPONo.Visible = true;
        lbl_LHPONoValue.Visible = true;

        if (LHPOTypeID == 0 || LHPOTypeID == 1)
        {
            ddl_LHPONo.Visible = false;

            if (UserManager.getUserParam().IsLHPOSeriesReq == true)
            {
                Get_Next_No();
                lbl_Start_End_No.Visible = true;
            }
            else
            {
                lbl_LHPONoValue.Text = ObjCommon.Get_Next_Number();
                lbl_Start_End_No.Visible = false;
            }

            //lbl_LHPONoValue.Text = ObjCommon.Get_Next_Number();
        }
        else
        {
            lbl_LHPONoValue.Visible = false;
            lbl_Start_End_No.Visible = false;
            objLHPOHireDetailsPresenter.FillAttachedLHPONo();
        }

        //Upd_Pnl_ddl_LHPONo.Update();
    }

    private bool CheckTotalNoofMemos()
    {
        int i;
        CheckBox Chk_Attach;
        int TotalMemos = 0;
        for (i = 0; i < dg_LHPOHireDetails.Items.Count; i++)
        {
            Chk_Attach = (CheckBox)dg_LHPOHireDetails.Items[i].FindControl("Chk_Attach");
            if (Chk_Attach.Checked == true)
            {
                TotalMemos = TotalMemos + 1;
            }
        }
        if (TotalMemos > 0)
            return true;
        else
            return false;
    }
    private void CalculateTruckHireCharge()
    {
        //1	Per Kg
        if (FreightTypeID == 1)
        {
            ActualWtPayableValue = WtGuarantee;
            if ((TotalArticleWT > WtGuarantee) && LHPOTypeID == 1)
            {
                ActualWtPayableValue = TotalArticleWT;
            }
            TruckHireCharge = (RateKg * ActualWtPayableValue);
        }

        //2	Fixed
        if (FreightTypeID == 2)
        {
            hdn_TruckHireCharge.Value = txt_TruckHireCharge.Text;
        }
        //3	Per Km
        if (FreightTypeID == 3)
        {
            ActualWtPayableValue = WtGuarantee;
            TruckHireCharge = (RateKg * ActualWtPayableValue);
            if ((ActualKms > WtGuarantee) && LHPOTypeID == 1)
            {
                ActualWtPayableValue = ActualKms;
                TruckHireCharge = (RateKg * ActualKms);
            }
            lbl_TruckHireChargeValue.Text = hdn_TruckHireCharge.Value;
        }

        //4	Per Article
        if (FreightTypeID == 4)
        {
            ActualWtPayableValue = WtGuarantee;
            if ((TotalArticle > WtGuarantee) && LHPOTypeID == 1)
            {
                ActualWtPayableValue = TotalArticle;
            }
            TruckHireCharge = (RateKg * ActualWtPayableValue);

        }
        CalculateTotalTruckHireCharge();
    }

    private void CalculateTotalTruckHireCharge()
    {
        //Calculate TDS Amount on the Basis of TDS Percentage
        if (txt_OtherCharges.Text == string.Empty)
        {
            OtherCharges = 0;
        }

        if (Util.String2Int(ddl_VehicleCategory.SelectedValue) == 1)
        {
            TDSAmount = 0;
            SurchargeAmount = 0;
            AddlSurchargeCessAmount = 0;
            AddlEducationCessAmount = 0;
            TotalTruckHireCharge = 0;
            CharityAmount = 0;
            TotalAfterTDSDeduction = 0;
        }
        else
        {
            TDSAmount = Math.Round((TruckHireCharge + OtherCharges) * TDSPercentage / 100, 2);
            SurchargeAmount = Math.Round(TDSAmount * Surcharge / 100, 2);
            AddlSurchargeCessAmount = Math.Round((TDSAmount + SurchargeAmount) * AddlSurchargeCess / 100, 2);
            AddlEducationCessAmount = Math.Round((TDSAmount + SurchargeAmount) * AddlEducationCess / 100, 2);
            TotalTDSAmount = Math.Round(TDSAmount + SurchargeAmount + AddlSurchargeCessAmount + AddlEducationCessAmount);
            TotalAfterTDSDeduction = Math.Round(TruckHireCharge + OtherCharges - TotalTDSAmount);
            TotalTruckHireCharge = Math.Round(TruckHireCharge + OtherCharges - TotalTDSAmount - CharityAmount);
        }
        CalculateBalanceAmount();
    }
    private void CalculateBalanceAmount()
    {
        if (Util.String2Int(ddl_VehicleCategory.SelectedValue) == 1)
        {
            BalanceAmount = (TotalTruckHireCharge - TotalAdvancePaid);
            if (BalanceAmount <= 0)
            {
                BalanceAmount = 0;
            }
        }
        else
        {
            if (TotalAdvancePaid > TotalTruckHireCharge)
            {
                TotalAdvancePaid = TotalTruckHireCharge;
            }
            BalanceAmount = (TotalTruckHireCharge - TotalAdvancePaid);
        }

        lbl_BalanceAmountValue.Text = hdn_BalanceAmount.Value;
        //BalanceAmount = hdn_BalanceAmount.Value;
    }
    private void SetPageControls(object sender, EventArgs e)
    {
        EnabledDisabledPageControl();
        ddlAttachedLHPONoChanged(sender, e);
        //  objLHPOHireDetailsPresenter.initValues();
        this.txtTotalAdvancePaid(TotalAdvancePaid, e);
    }
    public void Get_Next_No()
    {
        int _Document_Allocation_ID = 0;
        int _Start_No = 0;
        int _End_No = 0;
        int _Next_No = 0;
        string _Padded_Next_No = "";

        ObjCommon.Get_Document_Allocation_Details(ref _Document_Allocation_ID, ref _Start_No, ref _End_No, ref _Next_No, 0, UserManager.getUserParam().MainId, 5);
        Document_Series_Allocation_ID = _Document_Allocation_ID;
        Next_No = _Next_No;
        Start_No = _Start_No;
        End_No = _End_No;

        if (_Next_No <= 0)
        {
            Raj.EC.Common.DisplayErrors(1013);
        }

        _Padded_Next_No = _Next_No.ToString();
        LHPO_No = _Padded_Next_No;

        Start_End_No = "(" + Start_No + " - " + End_No + ")";
    }
    private void SetStandardCaption()
    {
        const int GCCaption = 5;
        lbl_LHPODate.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Date:";
        lbl_LHPOType.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Type:";
        lbl_LHPONo.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No:";
        Page.Title = CompanyManager.getCompanyParam().LHPOCaption;
        dg_LHPOHireDetails.Columns[GCCaption].HeaderText = "Total  " + CompanyManager.getCompanyParam().GcCaption;
    }
    private void SetLinks()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        fRights = uObj.getForm_Rights(107);
        bool can_add = fRights.canAdd();
        lbtn_AddDriver.Visible = false;
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdn_Driver_path.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(107).AddUrl + "&Call_From=LHPO";

            lbtn_AddDriver.Visible = true;
        }
        else
        {
            hdn_Driver_path.Value = "";
        }
    }

    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Session["SessionLHPOTypeID"] = null;
        Session["SessionLHPOID"] = null;
        hdn_Max_Advance_Percent_Of_Vehicle_hire_Charge.Value = CompanyManager.getCompanyParam().MaxAdvancePercentOfVehiclehireCharge.ToString();
        hdn_Mode.Value = Mode.ToString();
        SetStandardCaption();
        SetAllDDLSearch();

        if (MenuItemId == 198)
        {
            DisableControlForRectification();
        }
        else
        {
            ddl_FreightType.Enabled = false;
            ddl_BrokerName.Enabled = false;
            ddl_TDSCertificateTo.Enabled = false;
            txt_TruckHireCharge.Enabled = false;
            txt_TotalAdvance.Enabled = false;
            WucHierarchyWithID1.SetEnabled = false;
            ddl_Driver1.Enabled = false;
        }

        if (!IsPostBack)
        {
            FreightTypeID = 2;
            WucHierarchyWithID1.Allow_All_Hierarchy = true;
            WucHierarchyWithID1.Set_hierarchy_Width();
            Wuc_VehicleDepartureTime.setFormat("24");
            VehicleDepartureTime = DateTime.Now.ToShortTimeString();
            TransitDays = 0;
            CommitedDelDate = DateTime.Now;

            if (keyID > 0)
            {
                string script = "<script language='javascript'> " + "CalculateTruckHireCharge(0);EnabledDisabledBalancePayableOnBalanceAmountChange();EnabledDisabledControlOnFreightType(0)" + "</script>";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
            }
        }

        objLHPOHireDetailsPresenter = new LHPOHireDetailsPresenter(this, IsPostBack);
        WucVehicleSearch1.SetAutoPostBack = true;
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

        WucHierarchyWithID1.setDDLLocationAutoPostBack = false;
        WucHierarchyWithID1.DDLHierarchyChange += new EventHandler(WUChierarchyIndexChange);

        if (tr_CharityLedger.Visible == false && tr_CharityAmount.Visible == false)
        {
            CharityAmount = 0;

        }
        if (VehicleCategoryID == 1)
        {
            WucHierarchyWithID1.SetEnabled = false;
            WucHierarchyWithID1.Set_Location_Visible = false;
        }
        if (!IsPostBack)
        {
            Session["SessionOtherChargeGrid"] = null;
            //if (StateManager.Exist("SessionOtherChargeGrid"))
            //{
            //    Session.Remove("SessionOtherChargeGrid");
            //}
            Fill_LHPO_Parameters();

            //hdf_ResourceString.Value = ObjCommon.GetResourceString("Operations/Outward/App_LocalResources/WucLHPOHireDetails.ascx.resx");
            EnabledDisabledBroker();
            EnabledDisableOwner();
            WucHierarchyWithID1.Set_Hierarchy_Caption = "Balance Payable At";
            WucHierarchyWithID1.Set_Location_Caption = "Balance Payable Location";
            //WucHierarchyWithID1.Set_TD_Data_Width = "30%";            
            ddl_LHPONo.Visible = false;
            lbl_LHPONoValue.Visible = true;
            SetLinks();

            if (keyID > 0)
            {
                lbl_LHPONoValue.ReadOnly = true;
                lbl_LHPONoValue.CssClass = "TEXTBOXASLABEL";

                if (LHPOTypeID == 2)//Attached LHPO
                {
                    Session["SessionLHPOTypeID"] = 2;
                    ddl_VehicleCategory.Enabled = false;
                    WucVehicleSearch1.SetEnabled = false;
                    ddl_LHPOType.Enabled = false;
                    EnabledDisabledPageControl();
                }
                else //New LHPO
                {
                    Session["SessionLHPOTypeID"] = 1;
                    ddl_VehicleCategory.Enabled = false;
                    WucVehicleSearch1.SetEnabled = false;
                    ddl_LHPOType.Enabled = false;

                }
                if (VehicleCategoryID == 1)
                {
                    WucHierarchyWithID1.SetEnabled = false;
                    WucHierarchyWithID1.Set_Location_Visible = false;
                    ddl_Charity.Enabled = false;
                    txt_CharityAmount.Enabled = false;

                }
                Upd_Pnl_pnl_TotalTDSCalculation.Update();
                ddl_BrokerName_SelectedIndexChanged(sender, e);
            }
            else
            {
                if (UserManager.getUserParam().IsLHPOSeriesReq == true)
                {
                    Get_Next_No();
                    lbl_LHPONoValue.ReadOnly = false;
                    lbl_Start_End_No.Visible = true;
                    lbl_LHPONoValue.Attributes.Add("onkeypress", "return Only_Integers(" + lbl_LHPONoValue.ClientID + ",event)");
                    lbl_LHPONoValue.MaxLength = Util.String2Int(ObjCommon.Get_Values_Where("ec_master_company_gc_parameter", "GC_No_Length", "", "GC_No_Length", false).Tables[0].Rows[0]["GC_No_Length"].ToString());
                }
                else
                {
                    lbl_LHPONoValue.Text = ObjCommon.Get_Next_Number();
                    lbl_LHPONoValue.ReadOnly = true;
                    lbl_Start_End_No.Visible = true;
                    lbl_LHPONoValue.CssClass = "TEXTBOXASLABEL";
                }
            }
            //OtherCharges = 0;

            if (IsPostBackRequiredOnAdvanceAmt == true)
                txt_TotalAdvance.AutoPostBack = true;
            else
                txt_TotalAdvance.AutoPostBack = false;

        }
        if (Mode == 4)
        {
            lnkbtn_OtherCharges.Enabled = true;
            lbtn_AddDriver.Enabled = false;
            WucHierarchyWithID1.SetEnabled = false;
            WucLHPODate.Enabled = false;
            TD_Calender.Visible = false;
        }
        hdn_LHPOId.Value = keyID.ToString();
        txt_OtherCharges.Text = hdn_OtherCharges.Value;


        SetLabels();
        SetUpdatePanelUpdate();

        Wuc_VehicleDepartureTime.Visible = false;
        lnkbtn_OtherCharges.Attributes.Add("onclick", "return OpenPopup('" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "','" + ClassLibraryMVP.Util.EncryptInteger(keyID) + "')");
    }

    private void Fill_LHPO_Parameters()
    {
        DataSet DsLHPOParameters = new DataSet();
        DsLHPOParameters = objLHPOHireDetailsPresenter.FillLHPOParameters();

        FromLocationParameterId = Util.String2Int(DsLHPOParameters.Tables[0].Rows[0]["From_Loc_To_Be_Filled_ID"].ToString());
        ToLocationParameterId = Util.String2Int(DsLHPOParameters.Tables[0].Rows[0]["To_Loc_To_Be_Filled_ID"].ToString());
        BalancePayAtParameter = DsLHPOParameters.Tables[0].Rows[0]["Balance_Payable_At"].ToString();
        IsOtherChargeEnabledParameter = Convert.ToBoolean(DsLHPOParameters.Tables[0].Rows[0]["Is_Other_Charge_Enabled"].ToString());
        IsPostBackRequiredOnAdvanceAmt = Convert.ToBoolean(DsLHPOParameters.Tables[0].Rows[0]["Is_PostBack_Required_On_Advance_Amount"].ToString());

        lbl_FromLocation.Text = DsLHPOParameters.Tables[0].Rows[0]["From_Location_Text"].ToString();
        lbl_ToLocation.Text = DsLHPOParameters.Tables[0].Rows[0]["To_Location_Text"].ToString();

        if (ToLocationParameterId == 1)
        {
            WucHierarchyWithID1.SetEnabled = false;
        }

        if (IsOtherChargeEnabledParameter == true)
        {
            txt_OtherCharges.Enabled = true;
            tr_OtherCharges.Disabled = true;
            lnkbtn_OtherCharges.Enabled = false;
        }
        else
        {
            txt_OtherCharges.Enabled = false;
            tr_OtherCharges.Disabled = false;
            lnkbtn_OtherCharges.Enabled = true;
        }
    }

    private void VehicleIndexChange(object sender, EventArgs e)
    {
        if (LHPOTypeID > 0)
        {
            LHPOTypeID = 0;
            EnabledDisabledLHPONo();
            SetPageControls(sender, e);
            EnabledDisabledPageControl();
        }

        objLHPOHireDetailsPresenter.SetVehicleInfoOnVehicleChanged();
        objLHPOHireDetailsPresenter.GetTDSPercent();
        objLHPOHireDetailsPresenter.FillGrid();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

        ddl_LHPOType.SelectedValue = "2";
        //if (UserManager.getUserParam().MainId == DVLPFromBranchID)
        //{
        //    ddl_LHPOType.SelectedValue = "1";
        //    ddl_LHPOType_SelectedIndexChanged(sender, e);
        //}

    }

    private void WUChierarchyIndexChange(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
        }
    }
    protected void ddl_VehicleCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleCategoryIds = ddl_VehicleCategory.SelectedValue;
        WucVehicleSearch1.VehicleID = 0;
        LHPOTypeID = 0;
        SetLinks();
        EnabledDisabledLHPONo();
        EnabledDisabledBroker();
        SetPageControls(sender, e);
        EnabledDisableOwner();
        if (VehicleCategoryID == 1)
        {
            WucHierarchyWithID1.SetEnabled = false;
            ddl_Charity.Enabled = false;
            txt_CharityAmount.Enabled = false;

        }

        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void ddl_FromLocation_TxtChange(object sender, EventArgs e)
    {
        string FromBranchID = "0";
        DataSet ds = null;
        objLHPOHireDetailsPresenter.GetKMS();

        if (FromLocationParameterId == 1)
        {
            if (FromLocationID > 0)
            {
                ds = objLHPOHireDetailsPresenter.GetFromLocationBranchId();
                FromBranchID = ds.Tables[0].Rows[0]["Branch_Id"].ToString();

                if (SessionATHDetailsGrid.Tables[0].Rows.Count == 1)
                {
                    SessionATHDetailsGrid.Tables[0].Rows[0]["ATH_Payable_Main_ID"] = FromBranchID.ToString();
                    SessionATHDetailsGrid.Tables[0].Rows[0]["Main_Name"] = ds.Tables[0].Rows[0]["Branch_Name"].ToString();
                }
            }

            ddlFromLocationSelectionChanged(FromBranchID, e);
        }

        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void ddl_ToLocation_TxtChange(object sender, EventArgs e)
    {
        DataSet ds = null;

        objLHPOHireDetailsPresenter.GetKMS();

        if (ToLocationParameterId == 1 && VehicleCategoryID != 1)
        {
            WucHierarchyWithID1.Set_hierarchy_Width();

            if (ToLocationID > 0)
            {
                ds = objLHPOHireDetailsPresenter.GetToLocationBranchId();

                HierarchyCode = BalancePayAtParameter;
                MainID = Util.String2Int(ds.Tables[0].Rows[0]["Branch_Id"].ToString());
                WucHierarchyWithID1.SetEnabled = false;
            }
            else
            {
                WucHierarchyWithID1.Set_Default_Values(sender, e);
            }
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }
    protected void ddl_LHPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        EnabledDisabledLHPONo();
        //SetPageControls(sender, e);
        //EnabledDisableOwner();
        //objLHPOHireDetailsPresenter.GetTDSPercent();
        //if (VehicleCategoryID == 1)
        //{
        //    WucHierarchyWithID1.SetEnabled = false;
        //    WucHierarchyWithID1.Set_Location_Visible = false;
        //    ddl_Charity.Enabled = false;
        //    txt_CharityAmount.Enabled = false;
        //    Upd_Pnl_pnl_TotalTDSCalculation.Update();

        //}
        //scm_LHPOHireDetails.SetFocus(ddl_LHPOType);
        //string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void ddl_BrokerName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objLHPOHireDetailsPresenter.GetTDSPercentage();
        objLHPOHireDetailsPresenter.GetTDSPercent();

        if (keyID < 0)
        {
            scm_LHPOHireDetails.SetFocus(ddl_BrokerName);
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(1);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }
    protected void WucLHPODate_SelectionChanged(object sender, EventArgs e)
    {
        objLHPOHireDetailsPresenter.FillGrid();
        CommitedDelDate = LHPODate.AddDays(Convert.ToDouble(TransitDays));
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void ddl_LHPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SessionLHPOTypeID"] = LHPOTypeID;
        Session["SessionLHPOID"] = LHPONo;
        ddlAttachedLHPONoChanged(sender, e);
        objLHPOHireDetailsPresenter.initValues();
        this.txtTotalAdvancePaid(TotalAdvancePaid, e);
        EnabledDisabledPageControl();
        if (LHPOTypeID == 2 && keyID <= 0)
        {
            hdn_LHPOId.Value = LHPONo.ToString();
        }
        scm_LHPOHireDetails.SetFocus(ddl_LHPONo);
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }

    protected void txt_TotalAdvance_TextChanged(object sender, EventArgs e)
    {
        if (IsPostBackRequiredOnAdvanceAmt == true)
        {
            string Branch_Id = "0";
            DataSet ds = null;
            if (FromLocationID > 0)
            {
                ds = objLHPOHireDetailsPresenter.GetFromLocationBranchId();
                Branch_Id = ds.Tables[0].Rows[0]["Branch_Id"].ToString();
            }

            if (TotalAdvancePaid > 0)
            {
                if (SessionATHDetailsGrid.Tables[0].Rows.Count == 1)
                {
                    SessionATHDetailsGrid.Tables[0].Rows[0]["Advance_Amount"] = TotalAdvancePaid.ToString();
                }
            }

            ddlFromLocationSelectionChanged(Branch_Id, e);
        }

        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);

    }

    #endregion


}
