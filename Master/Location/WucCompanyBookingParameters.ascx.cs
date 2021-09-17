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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;

public partial class Master_Location_WucCompanyBookingParameters : System.Web.UI.UserControl,IBookingParametersView
{
    #region ClassVariable
    BookingParametersPresenter objBookingParametersPresenter;
    private ScriptManager scm_CompanyParameter;
    ClassLibrary.UIControl.DDLSearch ddl_AdvanceBookingLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_BookingIncomeLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_ServiceTaxLedgerName;
    ClassLibrary.UIControl.DDLSearch ddl_OtherChargeLedger;

    private int _Index;
    DropDownList ddl_BookingType;
    DropDownList ddl_PaymentType;
    DataSet objDS;
    DataTable objDT;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    Label lbl_SrNo;
    int NextSrNo = 0;
    bool isValid = false;
    LinkButton lbtn_Delete;    


    #endregion

    #region ControlsValues

    public bool IsTreatBookingIncomeAdvIncome
    {
        set { Chk_IsTreatBookingIncomeAdvIncome.Checked = value; }
        get { return Chk_IsTreatBookingIncomeAdvIncome.Checked; }

    }
    public bool IsToBilledAccountingGCWise
    {
        set { Chk_IsToBilledAccountingGCWise.Checked = value; }
        get { return Chk_IsToBilledAccountingGCWise.Checked; }

    }
    public bool IsBookingMoneyReceiptRequired
    {
        set { Chk_IsBookingMoneyReceiptRequired.Checked = value; }
        get { return Chk_IsBookingMoneyReceiptRequired.Checked; }

    }
    public bool IsDebitTodelivery
    {
        set { chk_Debittodelivery.Checked = value; }
        get { return chk_Debittodelivery.Checked; }

    }
    
    public int DivisionId
    {
        set { ddl_Division.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Division.SelectedValue); }
    }

    public DataTable SessionBookingParametersGrid
    {
        get { return StateManager.GetState<DataTable>("BookingParametersGrid"); }
        set {

            if (value!=null)
            {
                for (int i = 0; i <= value.Rows.Count - 1; i++)
                {
                    value.Rows[i]["SrNo"] = i;
                }

                StateManager.SaveState("BookingParametersGrid", value);
            }
        }
    }
    public int AdvanceBookingIncomeLedgerId
    {
        get { return ddl_AdvanceBookingLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_AdvanceBookingLedgerName.SelectedValue); }
    }
    public int BookingIncomeLedgerId
    {
        get { return ddl_BookingIncomeLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_BookingIncomeLedgerName.SelectedValue); }
    }
    public int ServiceTaxLedgerNameId
    {
        get { return ddl_ServiceTaxLedgerName.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_ServiceTaxLedgerName.SelectedValue); }
    }
    public int OtherChargeLedgerId
    {
        get { return ddl_OtherChargeLedger.SelectedValue.Trim() == "" ? 0 : Util.String2Int(ddl_OtherChargeLedger.SelectedValue); }
    }
    
    public string BookingParametersDetails
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBookingParametersGrid.Copy());
            _objDs.Tables[0].TableName = "BookingParametersGrid";
            return _objDs.GetXml().ToLower();
        }
    }
    public int CompanyId
    {
        set { hdn_CompanyId.Value = Convert.ToString(value); }
        get { return Util.String2Int(hdn_CompanyId.Value); }
    }

    public int ShortTermBillLedgerID
    {
        get 
        {
            if (ddl_StbLedger.SelectedValue == "")
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_StbLedger.SelectedValue);
            }
        }
    }
    public int PayforBookigBranchID
    {
        get 
        {
            if (ddl_toPayforBookigBranch.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_toPayforBookigBranch.SelectedValue);
            }
        }
        set
        {
            ddl_toPayforBookigBranch.SelectedValue = "0";
        }
    }
    public int PayforCrossingBranchID
    {
        get 
        {
            if (ddl_toPayforCrossingBranch.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_toPayforCrossingBranch.SelectedValue);
            }
        }
        set
        {
            ddl_toPayforCrossingBranch.SelectedValue = "0";
        }
    }
    public int PayforDeliveryBranchID
    {
        get 
        {
            if (ddl_toPayforDeliveryBranch.SelectedValue == "" || IsDebitTodelivery==true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_toPayforDeliveryBranch.SelectedValue);
            }
        }
        set
        {           
           ddl_toPayforDeliveryBranch.SelectedValue = "0";           
            
        }
    }

    public int DeliveryCommisionIncomeID
    {
        get
        {
            if (ddl_Delivery_Commision_Income_AC.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_Delivery_Commision_Income_AC.SelectedValue);
            }
        }
        
    }
    public int DeliveryCommisionExpenseID
    {
        get
        {
            if (ddl_Delivery_Commision_Expense_AC.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_Delivery_Commision_Expense_AC.SelectedValue);
            }
        }

    }
    public int LHPOOtherChargesExpenseID
    {
        get
        {
            if (ddl_LHPO_Other_Charges_Expense_AC.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_LHPO_Other_Charges_Expense_AC.SelectedValue);
            }
        }

    }

    public int LHPOOtherChargesPaybleID
    {
        get
        {
            if (ddl_LHPO_Other_Charges_Payble_AC.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_LHPO_Other_Charges_Payble_AC.SelectedValue);
            }
        }

    }

    public int LorryPayble_ATH_BTH_ID
    {
        get
        {
            if (ddl_Lorry_Payble_ATH_BTH_AC.SelectedValue == "" || IsDebitTodelivery == true)
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_Lorry_Payble_ATH_BTH_AC.SelectedValue);
            }
        }

    }
    public int UpcountryCostAC_ID
    {
        get
        {
            if (ddl_UpcountryCostAC.SelectedValue == "")
            {
                return 0;
            }
            else
            {
                return Util.String2Int(ddl_UpcountryCostAC.SelectedValue);
            }
        }

    }
    public int SrNo
    {
        set
        {
            lbl_SrNo.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_SrNo.Text);
        }

    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = true;
        if (IsDebitTodelivery == false)
        {
            if (PayforBookigBranchID <= 0)
            {
                errorMessage = "Please Enter To Pay Recovery Ledger For Booking Branch";
                _isValid = false;
            }
            else if (PayforCrossingBranchID <= 0)
            {
                errorMessage = "Please Enter To Pay Recovery Ledger For Crossing  Branch";
                _isValid = false;
            }
            else if (PayforDeliveryBranchID <= 0)
            {
                errorMessage = "Please Enter To Pay Recovery Ledger For Delivery  Branch";
                _isValid = false;
            }
            else if (DeliveryCommisionIncomeID <= 0)
            {
                errorMessage = "Please Enter Delivery Commision Income A/C";
                _isValid = false;
            }
            else if (DeliveryCommisionExpenseID <= 0)
            {
                errorMessage = "Please Enter Delivery Commision Expense A/C";
                _isValid = false;
            }
            else if (LHPOOtherChargesExpenseID <= 0)
            {
                errorMessage = "Please Enter LHPO Other Charges Expense A/C";
                _isValid = false;
            }
            else if (LHPOOtherChargesPaybleID <= 0)
            {
                errorMessage = "Please Enter LHPO Other Charges Payble A/C";
                _isValid = false;
            }
            else if (LorryPayble_ATH_BTH_ID <= 0)
            {
                errorMessage = "Please Enter Lorry Payble (ATH/BTH) A/C";
                _isValid = false;
            }
            else if (UpcountryCostAC_ID  <= 0)
            {
                errorMessage = "Please Enter Upcountry Cost A/C";
                _isValid = false;
            }
        }    
        if (_isValid == true)
        {
            return true;
        }
        else
        {
            return false;
        }
            

        
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
    #region ControlsBind

    public DataTable BindAdvanceBookingIncomeLedger
    {
        set
        {
            DropDownList ddl_AdvanceBookingLedgerName = (DropDownList)(dg_BookingParameters.Controls[2].Controls[_Index].FindControl("ddl_AdvanceBookingLedgerName"));
            ddl_AdvanceBookingLedgerName.DataTextField = "Advance_Booking_Income_Ledger_Name";
            ddl_AdvanceBookingLedgerName.DataValueField = "Advance_Booking_Income_Ledger_Id";
            SessionAdvanceBookingIncomeDropDown = value;
            ddl_AdvanceBookingLedgerName.DataSource = value;
            ddl_AdvanceBookingLedgerName.DataBind();





        }

    }

    public DataTable SessionAdvanceBookingIncomeDropDown
    {
        get { return StateManager.GetState<DataTable>("AdvanceBookingIncomeDropDown"); }
        set { StateManager.SaveState("AdvanceBookingIncomeDropDown", value); }
    }
    public DataTable BindBookingIncomeLedger
    {
        set
        {
            DropDownList ddl_BookingIncomeLedgerName = (DropDownList)(dg_BookingParameters.Controls[3].Controls[_Index].FindControl("ddl_BookingIncomeLedgerName"));
            ddl_BookingIncomeLedgerName.DataTextField = "Booking_Income_Ledger_Name";
            ddl_BookingIncomeLedgerName.DataValueField = "Booking_Income_Ledger_Id";
            SessionBookingIncomeDropDown = value;
            ddl_BookingIncomeLedgerName.DataSource = value;
            ddl_BookingIncomeLedgerName.DataBind();

        }

    }

    public DataTable SessionBookingIncomeDropDown
    {
        get { return StateManager.GetState<DataTable>("BookingIncomeDropDown"); }
        set { StateManager.SaveState("BookingIncomeDropDown", value); }
    }

    public DataTable BindSessionTaxLedger
    {
        set
        {
            DropDownList ddl_ServiceTaxLedgerName = (DropDownList)(dg_BookingParameters.Controls[4].Controls[_Index].FindControl("ddl_ServiceTaxLedgerName"));

            ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
            ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_Id";
            SessionServiceTaxLedgerDropDown = value;
            ddl_ServiceTaxLedgerName.DataSource = value;
            ddl_ServiceTaxLedgerName.DataBind();




        }

    }
    public DataTable BindSessionOtherChargeLedger
    {
        set
        {
            DropDownList ddl_OtherChargeLedger = (DropDownList)(dg_BookingParameters.Controls[5].Controls[_Index].FindControl("ddl_OtherChargeLedger"));

            ddl_OtherChargeLedger.DataTextField = "OtherChargeLedger_Name";
            ddl_OtherChargeLedger.DataValueField = "OtherChargeLedger_Id";
            SessionOtherChargeLedgerDropDown = value;
            ddl_OtherChargeLedger.DataSource = value;
            ddl_OtherChargeLedger.DataBind();

        }

    }

    public DataTable SessionServiceTaxLedgerDropDown
    {
        get { return StateManager.GetState<DataTable>("SessionTaxLedgerDropDown"); }
        set { StateManager.SaveState("SessionTaxLedgerDropDown", value); }
    }
    public DataTable SessionOtherChargeLedgerDropDown
    {
        get { return StateManager.GetState<DataTable>("SessionOtherChargeLedgerDropDown"); }
        set { StateManager.SaveState("SessionOtherChargeLedgerDropDown", value); }
    }

    public DataTable BindDivision
    {
        set
        {
            ddl_Division.DataSource = value;
            ddl_Division.DataTextField = "Division_Name";
            ddl_Division.DataValueField = "Division_ID";
            SessionDivision = value;

            ddl_Division.DataBind();
            ddl_Division.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }

    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("DivisionDropDown"); }
        set { StateManager.SaveState("DivisionDropDown", value); }
    }

    public DataTable BindBookingType
    {
        set
        {
            ddl_BookingType.DataSource = value;
            ddl_BookingType.DataTextField = "Booking_Type";
            ddl_BookingType.DataValueField = "Booking_Type_ID";
            SessionBookingType = value;

            ddl_BookingType.DataBind();
           ddl_BookingType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionBookingType
    {
        get { return StateManager.GetState<DataTable>("BookingTypeDropDown"); }
        set
        {
           
                 StateManager.SaveState("BookingTypeDropDown", value);
           
        }
    }
    public DataTable BindPaymentType
    {
        set
        {
            ddl_PaymentType.DataSource = value;
            ddl_PaymentType.DataTextField = "Payment_Type";
            ddl_PaymentType.DataValueField = "Payment_Type_ID";
            SessionPaymentType = value;

            ddl_PaymentType.DataBind();
            ddl_PaymentType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable SessionPaymentType
    {
        get { return StateManager.GetState<DataTable>("PaymentTypeDropDown"); }
        set { StateManager.SaveState("PaymentTypeDropDown", value); }
    }
    public DataTable BindBookingParametersGrid
    {
        set
        {

            SessionBookingParametersGrid = value;
            dg_BookingParameters.DataSource = value;
            dg_BookingParameters.DataBind();

        }
    }

    #endregion

    #region OtherMethods
    public void BindGrid()
    {
       

        DataSet ds_Temp = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();

        dv = ObjCommon.Get_View_Table(SessionBookingParametersGrid, "Division_ID= '" + DivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds_Temp.Tables.Add(dt);
        dg_BookingParameters.DataSource = ds_Temp;
        dg_BookingParameters.DataBind();
      
      
    }

    
    private void insertUpdateDataset(DataGridCommandEventArgs e)
    {
        DataTable objDT = SessionBookingParametersGrid;
        DataRow objDR = null;
        ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));
        ddl_PaymentType = (DropDownList)(e.Item.FindControl("ddl_paymentType"));
        ddl_AdvanceBookingLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_AdvanceBookingLedgerName"));
        ddl_BookingIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BookingIncomeLedgerName"));
        ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ServiceTaxLedgerName"));
        ddl_OtherChargeLedger = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_OtherChargeLedger"));
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");



      //  ddl_AdvanceBookingLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_AdvanceBookingLedgerName"));
        ddl_AdvanceBookingLedgerName.DataValueField = "Advance_Booking_Income_Ledger_ID";
        ddl_AdvanceBookingLedgerName.DataTextField = "Advance_Booking_Income_Ledger_Name";
        ddl_AdvanceBookingLedgerName.OtherColumns = "1";

      //  ddl_BookingIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BookingIncomeLedgerName"));
        ddl_BookingIncomeLedgerName.DataValueField = "Booking_Income_Ledger_ID";
        ddl_BookingIncomeLedgerName.DataTextField = "Booking_Income_Ledger_Name";
        ddl_BookingIncomeLedgerName.OtherColumns = "2";

       // ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ServiceTaxLedgerName"));
        ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_ID";
        ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
        ddl_ServiceTaxLedgerName.OtherColumns = "3";


      //  ddl_OtherChargeLedger = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_OtherChargeLedger"));
        ddl_OtherChargeLedger.DataValueField = "OtherChargeLedger_ID";
        ddl_OtherChargeLedger.DataTextField = "OtherChargeLedger_Name";
        ddl_OtherChargeLedger.OtherColumns = "4";


        DataRow [] DRArray;

        if (e.CommandName == "Add")
        {
            objDR = objDT.NewRow();
            NextSrNo = AssignSrNo(0);
           // lbl_SrNo.Text = Util.Int2String(NextSrNo);
        }
        else if (e.CommandName == "Update")
        {
            //objDR=GetDataRow(e.Item.ItemIndex, objDS);      
            //objDR = objDT.Rows.Find(NextSrNo); 
            // NextSrNo = Util.String2Int(objDR["SrNo"].ToString());
            NextSrNo = Util.String2Int(lbl_SrNo.Text);
            DRArray=  objDT.Select("SrNo='"+NextSrNo+"'");
            if (DRArray.Length > 0)
            {
                objDR = DRArray[0];
            }
            //objDR = objDT.Rows.Find(NextSrNo);
        }


        if (validateGrid() == true)
        {

            objDR["SrNo"] = NextSrNo;
            objDR["Booking_Type_ID"] = ddl_BookingType.SelectedValue;
            objDR["Division_ID"] = Util.String2Int(ddl_Division.SelectedValue);
            objDR["Booking_Type"] = ddl_BookingType.SelectedItem.Text;
            objDR["Payment_Type_ID"] = ddl_PaymentType.SelectedValue;
            objDR["Payment_Type"] = ddl_PaymentType.SelectedItem.Text;
            objDR["Advance_Booking_Income_Ledger_ID"] = Util.String2Int(ddl_AdvanceBookingLedgerName.SelectedValue);
            objDR["Advance_Booking_Income_Ledger_Name"] = ddl_AdvanceBookingLedgerName.SelectedText;
            objDR["Booking_Income_Ledger_ID"] = Util.String2Int(ddl_BookingIncomeLedgerName.SelectedValue);
            objDR["Booking_Income_Ledger_Name"] = ddl_BookingIncomeLedgerName.SelectedText;
            objDR["Service_Tax_Ledger_ID"] = Util.String2Int(ddl_ServiceTaxLedgerName.SelectedValue);
            objDR["Service_Tax_Ledger_Name"] = ddl_ServiceTaxLedgerName.SelectedText;
            objDR["OtherChargeLedger_ID"] = Util.String2Int(ddl_OtherChargeLedger.SelectedValue);
            objDR["OtherChargeLedger_Name"] = ddl_OtherChargeLedger.SelectedText;
            

            

            if (e.CommandName == "Add")
            {

                objDT.Rows.Add(objDR);
            }

            else if (e.CommandName == "Update")
            {
                dg_BookingParameters.EditItemIndex = -1;
                dg_BookingParameters.ShowFooter = true;

            }
            BindBookingParametersGrid = objDT;

        }

    }
    public bool validateGrid()
    {
        if (Util.String2Int(ddl_Division.SelectedValue) <= 0)
        {
            errorMessage = "Please Select  Division";
            ddl_Division.Focus();
        }
        else if (Util.String2Int(ddl_BookingType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Booking Type";
            ddl_BookingType.Focus();
        }
        else if(Util.String2Int(ddl_PaymentType.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Payment Type";
            ddl_PaymentType.Focus();
        }
        else if (AdvanceBookingIncomeLedgerId <= 0)
        {
            errorMessage = "Please Select Advance Booking Income Leger";
            ddl_AdvanceBookingLedgerName.Focus();
        }
        else if (BookingIncomeLedgerId <= 0)
        {
            errorMessage = "Please Select Booking Income Ledger";
            ddl_BookingIncomeLedgerName.Focus();
        }
        else if (ServiceTaxLedgerNameId <= 0)
        {
            errorMessage = "Please Select Service Tax Ledger";
            ddl_ServiceTaxLedgerName.Focus();
        }
        else if (OtherChargeLedgerId <= 0 && Util.String2Int(ddl_PaymentType.SelectedValue) == 3)
        {
            errorMessage = "Please Select Other Charge Ledger";
            ddl_OtherChargeLedger.Focus();
        }
        else
            isValid = true;

        return isValid;
    }

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyParameter = value; }
    }

    //private DataRow GetDataRow(int ItemIndex, DataSet objDS)
    //{
    //    int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataView dv = new DataView();
    //    dv = ObjCommon.Get_View_Table(SessionBookingParametersGrid, "Division_ID= '" + DivisionId.ToString() + " '");
    //    dt = dv.ToTable();
    //    ds.Tables.Add(dt);
    //    DataRow dr = null;
    //    DataRow drNew = null;
    //    int BookingTypeId, DivisionTypeId,PaymentTypeId;
    //    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        if (i == ItemIndex)
    //        {
    //            SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
    //            BookingTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Booking_Type_ID"].ToString());
    //            DivisionTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Division_ID"].ToString());
    //            PaymentTypeId = Util.String2Int(ds.Tables[0].Rows[i]["Payment_Type_ID"].ToString());
    //            object[] o = new object[] { BookingTypeId, DivisionId,PaymentTypeId };
    //            drNew = objDS.Tables[0].Rows.Find(o);
    //        }
    //    }
    //    return drNew;
    //}
    private int AssignSrNo(int SrNo)
    {
        int NextSrNo = 0;
        if (SrNo == 0 &&  SessionBookingParametersGrid.Rows.Count > 0)
        {
            NextSrNo = (int)SessionBookingParametersGrid.Compute("max(SrNo)", "");
            NextSrNo = NextSrNo + 1;
        }
        else if (SessionBookingParametersGrid.Rows.Count <= 0)
        {
            NextSrNo = 1;
        }
        else if (SrNo != 0)
        {
            NextSrNo = (int)SessionBookingParametersGrid.Compute("max(SrNo)", "");
        }
        return NextSrNo;
    }
    private int GetSrNo(int ItemIndex)
    {
        int i, SrNo = 0, SrNoForEditDeleteCancel = 0;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        dv = ObjCommon.Get_View_Table(SessionBookingParametersGrid, "Division_ID= '" + DivisionId.ToString() + " '");
        dt = dv.ToTable();
        ds.Tables.Add(dt);

        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (i == ItemIndex)
            {
                SrNo = Util.String2Int(ds.Tables[0].Rows[i]["SrNo"].ToString());
                SrNoForEditDeleteCancel = ds.Tables[0].Rows.IndexOf(ds.Tables[0].Rows[i]);
            }
        }
        return SrNoForEditDeleteCancel;
    }
    #endregion


  

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.CompanyCallBackFunction.CallBack));
        
        objBookingParametersPresenter = new BookingParametersPresenter(this, IsPostBack);
       
        if (!IsPostBack)
        {
            BindGrid();
        }
        Upd_pnl_BookingParameters.Update();
    }
    protected void ddl_Division_SelectedIndexChanged(object sender, EventArgs e)
    {       
        BindGrid();
    }
    #endregion

    #region GridEvents
    protected void dg_BookingParameters_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {

                ddl_BookingType = (DropDownList)(e.Item.FindControl("ddl_BookingType"));
                ddl_PaymentType = (DropDownList)(e.Item.FindControl("ddl_paymentType"));
                
                ddl_AdvanceBookingLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_AdvanceBookingLedgerName"));
                ddl_AdvanceBookingLedgerName.DataValueField = "Advance_Booking_Income_Ledger_ID";
                ddl_AdvanceBookingLedgerName.DataTextField = "Advance_Booking_Income_Ledger_Name";
                ddl_AdvanceBookingLedgerName.OtherColumns = "1";

                ddl_BookingIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_BookingIncomeLedgerName"));
                ddl_BookingIncomeLedgerName.DataValueField = "Booking_Income_Ledger_ID";
                ddl_BookingIncomeLedgerName.DataTextField = "Booking_Income_Ledger_Name";
                ddl_BookingIncomeLedgerName.OtherColumns = "2";

                ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ServiceTaxLedgerName"));
                ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_ID";
                ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
                ddl_ServiceTaxLedgerName.OtherColumns = "3";


                ddl_OtherChargeLedger = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_OtherChargeLedger"));
                ddl_OtherChargeLedger.DataValueField = "OtherChargeLedger_ID";
                ddl_OtherChargeLedger.DataTextField = "OtherChargeLedger_Name";
                ddl_OtherChargeLedger.OtherColumns = "4";

                lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");

              
                //ddl_TyreNo.OtherColumns = KeyId.ToString();

                BindBookingType = SessionBookingType;
                BindPaymentType = SessionPaymentType;
               // BindDivision = SessionDivision;
                ddl_OtherChargeLedger.Enabled = false;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                //ddl_BookingType.SelectedValue = SessionBookingType.Rows[e.Item.ItemIndex]["Booking_Type_ID"].ToString();
                //ddl_PaymentType.SelectedValue = SessionPaymentType.Rows[e.Item.ItemIndex]["Payment_Type_ID"].ToString();
                ddl_AdvanceBookingLedgerName.OtherColumns = "1";
                ddl_BookingIncomeLedgerName.OtherColumns = "2";
                ddl_ServiceTaxLedgerName.OtherColumns = "3";
                ddl_OtherChargeLedger.OtherColumns = "4";
               DataTable objDT = SessionBookingParametersGrid;
                DataTable dt = new DataTable();
                DataView dv = new DataView();

                dv = ObjCommon.Get_View_Table(SessionBookingParametersGrid, "SrNo= '" + lbl_SrNo.Text.Trim() + "'");
                dt = dv.ToTable();

                if (dt.Rows.Count > 0)
                {
                    DataRow objDR = dt.Rows[0];
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Advance_Booking_Income_Ledger_Name"].ToString(), objDR["Advance_Booking_Income_Ledger_ID"].ToString(), ddl_AdvanceBookingLedgerName);
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Booking_Income_Ledger_Name"].ToString(), objDR["Booking_Income_Ledger_ID"].ToString(), ddl_BookingIncomeLedgerName);
                    Raj.EC.Common.SetValueToDDLSearch(objDR["Service_Tax_Ledger_Name"].ToString(), objDR["Service_Tax_Ledger_ID"].ToString(), ddl_ServiceTaxLedgerName);
                    Raj.EC.Common.SetValueToDDLSearch(objDR["OtherChargeLedger_Name"].ToString(), objDR["OtherChargeLedger_ID"].ToString(), ddl_OtherChargeLedger);

                    ddl_BookingType.SelectedValue = objDR["Booking_Type_ID"].ToString();
                    ddl_PaymentType.SelectedValue = objDR["Payment_Type_ID"].ToString();
                    if (Util.String2Int(ddl_PaymentType.SelectedValue) == 3)
                    {
                        ddl_OtherChargeLedger.Enabled = true;
                    }
                }

               

            }


        }
    }
    protected void dg_BookingParameters_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
                errorMessage = "";
                 objDT = SessionBookingParametersGrid;
                 try
                 {

                     insertUpdateDataset(e);
                     if (isValid == true)
                     {

                         BindGrid();
                         dg_BookingParameters.EditItemIndex = -1;
                         dg_BookingParameters.ShowFooter = true;

                     }
                 }
                 catch (ConstraintException)
                 {
                     errorMessage = "Duplicate Booking Type";
                     return;
                 }
        }
    }
    protected void dg_BookingParameters_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        ddl_PaymentType = (DropDownList)e.Item.FindControl("ddl_PaymentType");
        SrNo = GetSrNo(e.Item.ItemIndex);
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_BookingParameters.EditItemIndex = SrNo;
        dg_BookingParameters.ShowFooter = false;
        BindGrid();
       
        

    }
    protected void dg_BookingParameters_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_BookingParameters.EditItemIndex = -1;
        dg_BookingParameters.ShowFooter = true;
         BindGrid();
    }

    protected void dg_BookingParameters_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbl_SrNo = (Label)e.Item.FindControl("lbl_SrNo");
        objDT = SessionBookingParametersGrid;
        DataRow dr = null;
        //dr = GetDataRow(e.Item.ItemIndex, objDS);
        SrNo = Util.String2Int(lbl_SrNo.Text);
        dr = objDT.Rows[Util.String2Int(lbl_SrNo.Text)];
        if (e.Item.ItemIndex != -1)
        {
            objDT= SessionBookingParametersGrid;
                //objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDT.Rows.Remove(dr);
            objDT.AcceptChanges();
            SessionBookingParametersGrid = objDT;
            dg_BookingParameters.EditItemIndex = -1;
            dg_BookingParameters.ShowFooter = true;
            BindGrid();

        }
    }

    public void SetShortTermBillLedgerID(string text, string value)
    {
        ddl_StbLedger.DataTextField = "Ledger_Name";
        ddl_StbLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_StbLedger);
    }
    public void SetPayforBookigBranchID(string text, string value)
    {
        ddl_toPayforBookigBranch.DataTextField = "Ledger_Name";
        ddl_toPayforBookigBranch.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_toPayforBookigBranch);
    }
    public void SetPayforCrossingBranchID(string text, string value)
    {
        ddl_toPayforCrossingBranch.DataTextField = "Ledger_Name";
        ddl_toPayforCrossingBranch.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_toPayforCrossingBranch);
    }
    public void SetPayforDeliveryBranchID(string text, string value)
    {
        ddl_toPayforDeliveryBranch.DataTextField = "Ledger_Name";
        ddl_toPayforDeliveryBranch.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_toPayforDeliveryBranch);
    }

    public void SetDeliveryCommisionIncomeID(string text, string value)
    {
        ddl_Delivery_Commision_Income_AC.DataTextField = "Ledger_Name";
        ddl_Delivery_Commision_Income_AC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Delivery_Commision_Income_AC);
    }
    public void SetDeliveryCommisionExpenseID(string text, string value)
    {
        ddl_Delivery_Commision_Expense_AC.DataTextField = "Ledger_Name";
        ddl_Delivery_Commision_Expense_AC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Delivery_Commision_Expense_AC);
    }
    public void SetLHPOOtherChargesExpenseID(string text, string value)
    {
        ddl_LHPO_Other_Charges_Expense_AC.DataTextField = "Ledger_Name";
        ddl_LHPO_Other_Charges_Expense_AC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_LHPO_Other_Charges_Expense_AC);
    }
    public void SetLHPOOtherChargesPaybleID(string text, string value)
    {
        ddl_LHPO_Other_Charges_Payble_AC.DataTextField = "Ledger_Name";
        ddl_LHPO_Other_Charges_Payble_AC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_LHPO_Other_Charges_Payble_AC);
    }
    public void SetLorryPayble_ATH_BTH_ID(string text, string value)
    {
        ddl_Lorry_Payble_ATH_BTH_AC.DataTextField = "Ledger_Name";
        ddl_Lorry_Payble_ATH_BTH_AC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Lorry_Payble_ATH_BTH_AC);
    }
    public void SetUpcountryCostAC_ID(string text, string value)
    {
        ddl_UpcountryCostAC.DataTextField = "Ledger_Name";
        ddl_UpcountryCostAC.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_UpcountryCostAC);
        
    }    
    protected void ddl_PaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_PaymentType = (DropDownList)sender;
        DataGridItem _Item = (DataGridItem)ddl_PaymentType.Parent.Parent;       
        //ddl_OtherChargeLedger = (ClassLibrary.UIControl.DDLSearch)_Item.FindControl("ddl_OtherChargeLedger");


        ddl_AdvanceBookingLedgerName = (ClassLibrary.UIControl.DDLSearch)(_Item.FindControl("ddl_AdvanceBookingLedgerName"));
        ddl_AdvanceBookingLedgerName.DataValueField = "Advance_Booking_Income_Ledger_ID";
        ddl_AdvanceBookingLedgerName.DataTextField = "Advance_Booking_Income_Ledger_Name";
        ddl_AdvanceBookingLedgerName.OtherColumns = "1";

        ddl_BookingIncomeLedgerName = (ClassLibrary.UIControl.DDLSearch)(_Item.FindControl("ddl_BookingIncomeLedgerName"));
        ddl_BookingIncomeLedgerName.DataValueField = "Booking_Income_Ledger_ID";
        ddl_BookingIncomeLedgerName.DataTextField = "Booking_Income_Ledger_Name";
        ddl_BookingIncomeLedgerName.OtherColumns = "2";

        ddl_ServiceTaxLedgerName = (ClassLibrary.UIControl.DDLSearch)(_Item.FindControl("ddl_ServiceTaxLedgerName"));
        ddl_ServiceTaxLedgerName.DataValueField = "Service_Tax_Ledger_ID";
        ddl_ServiceTaxLedgerName.DataTextField = "Service_Tax_Ledger_Name";
        ddl_ServiceTaxLedgerName.OtherColumns = "3";


        ddl_OtherChargeLedger = (ClassLibrary.UIControl.DDLSearch)(_Item.FindControl("ddl_OtherChargeLedger"));
        ddl_OtherChargeLedger.DataValueField = "OtherChargeLedger_ID";
        ddl_OtherChargeLedger.DataTextField = "OtherChargeLedger_Name";
        ddl_OtherChargeLedger.OtherColumns = "4";



        ddl_OtherChargeLedger.Enabled = false;
        if (Util.String2Int(ddl_PaymentType.SelectedValue) == 3)
        {
            ddl_OtherChargeLedger.Enabled = true;
        }
    }
    #endregion

}

