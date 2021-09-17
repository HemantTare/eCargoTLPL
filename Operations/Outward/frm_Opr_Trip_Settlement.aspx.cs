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
using Raj.EC;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;
using ClassLibraryMVP.General;

public partial class frm_Opr_Trip_Settlement : ClassLibraryMVP.UI.Page, ITrip_Settlement_2_View, IView
{
    #region ClassVariables
    Trip_Settlement_2Presenter objTrip_Settlement_2Presenter;
    Common objcomm = new Common();
    #endregion

    #region Trip Details

    #region Varaiables For Trip Details

    DropDownList ddl_FromBranch;
    DropDownList ddl_ToBranch;
    TextBox txt_THCNo;
    TextBox txt_StartKM;
    TextBox txt_EndKM;
    TextBox txt_HireAmount;
    TextBox txt_Advance;
    TextBox txt_Act_Wt;
    Label lbl_KmsRun;
    ComponentArt.Web.UI.Calendar dtp_THCDate;
    bool isValid = false;
    DataTable objDT;
    DataRow DR = null;
    int TableRow, _itemIndex = -1;
    decimal EndKM;

    #endregion

    #region Properties For Trip Details

    public int Total_KM_Run
    {
        get { return Util.String2Int(lbl_TotalKmsRun.Text); }
        set { lbl_TotalKmsRun.Text = Util.Int2String(value); }
    }

    public void BindTripHireChallanGrid(object o, EventArgs e)
    {

        decimal Total_Hire_Amount = Util.String2Decimal(SessionTripHireChallansGrid.Compute("Sum(Hire_Amount)", "").ToString());
        decimal Total_Advance = Util.String2Decimal(SessionTripHireChallansGrid.Compute("Sum(Advance_Amount)", "").ToString());
        int Total_Start_KM = Util.String2Int(SessionTripHireChallansGrid.Compute("MIN(Start_KM)", "").ToString());
        int Total_End_KM = Util.String2Int(SessionTripHireChallansGrid.Compute("MAX(End_KM)", "").ToString());
        int Total_KMs_Run = Total_End_KM - Total_Start_KM;

        if (Total_Hire_Amount == -1) Total_Hire_Amount = 0;
        if (Total_Advance == -1) Total_Advance = 0;
        if (Total_Start_KM == -1) Total_Start_KM = 0;
        if (Total_End_KM == -1) Total_End_KM = 0;
        if (Total_KMs_Run == -1) Total_KMs_Run = 0;

        updateTripSettlementDetails(this, e);

        lbl_Hire_Aount.Text = Total_Hire_Amount.ToString();
        lbl_Total_Advance.Text = Total_Advance.ToString();

        //lbl_StartKms.Text = Total_Start_KM.ToString();
        //lbl_EndKms.Text = Total_End_KM.ToString();
        lbl_TotalKmsRun.Text = Total_KMs_Run.ToString();


        dg_TripHireChallans.DataSource = SessionTripHireChallansGrid;
        dg_TripHireChallans.DataBind();
    }

    public DataTable BindPump
    {
        set
        {
            ddl_Pump.DataTextField = "Vendor_Name";
            ddl_Pump.DataValueField = "Vendor_ID";
            ddl_Pump.DataSource = value;
            ddl_Pump.DataBind();
            ddl_Pump.Items.Insert(0, new ListItem(" Select One ", "0"));
        }
    }

    public DataTable BindFromBranch
    {
        set
        {
            ddl_FromBranch.DataTextField = "Branch_Name";
            ddl_FromBranch.DataValueField = "Branch_ID";
            ddl_FromBranch.DataSource = value;
            ddl_FromBranch.DataBind();
            ddl_FromBranch.Items.Insert(0, new ListItem(" Select One ", "0"));
        }
    }

    public DataTable BindToBranch
    {
        set
        {
            ddl_ToBranch.DataSource = value;
            ddl_ToBranch.DataTextField = "Branch_Name";
            ddl_ToBranch.DataValueField = "Branch_ID";
            ddl_ToBranch.DataBind();
            ddl_ToBranch.Items.Insert(0, new ListItem(" Select One ", "0"));
        }
    }

    public DateTime Trip_Start_Date
    {
        set { dtp_TripStartDate.SelectedDate = value; }
        get { return dtp_TripStartDate.SelectedDate; }
    }

    public DateTime Trip_End_Date
    {
        set { dtp_TripEndDate.SelectedDate = value; }
        get { return dtp_TripEndDate.SelectedDate; }
    }

    public DataTable SessionPumpDropDown
    {
        get { return StateManager.GetState<DataTable>("PumpDropDown"); }
        set { StateManager.SaveState("PumpDropDown", value); }
    }

    public DataTable SessionFromBranchDropdown
    {
        get { return StateManager.GetState<DataTable>("FromBranchDropdown"); }
        set { StateManager.SaveState("FromBranchDropdown", value); }
    }

    public DataTable SessionToBranchDropdown
    {
        get { return StateManager.GetState<DataTable>("ToBranchDropdown"); }
        set { StateManager.SaveState("ToBranchDropdown", value); }
    }

     public DataTable SessionTripHireChallansGrid
    {
        get { return StateManager.GetState<DataTable>("TripHireChallansGrid"); }
        set { StateManager.SaveState("TripHireChallansGrid", value); }
    }

    public String TripHireChallansDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionTripHireChallansGrid.Copy());
            _objDs.Tables[0].TableName = "Trip_Hire_Challans_Details_2";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region Other Methods for Trip Details

    public bool ValidateUITripDetails()
    {
        bool IsValid = false;
        if (SessionTripHireChallansGrid.Rows.Count <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_TripHireCount").ToString();
        }
        else if (CheckTHCDate() == false) { }
        else
        {
            IsValid = true;
        }
        lbl_Errors.Visible = (!IsValid);

        return IsValid;
    }

    private bool CheckTHCDate()
    {
        bool ATS = false;
        foreach (DataRow dr in SessionTripHireChallansGrid.Rows)
        {
            if (Convert.ToDateTime(dr["Trip_Date"].ToString()) < Trip_Start_Date || Convert.ToDateTime(dr["Trip_Date"].ToString()) > Trip_End_Date)
            {
                errorMessage = GetLocalResourceObject("Msg_THCDate").ToString();
                ATS = false;
                break;
            }
            else
                ATS = true;
        }
        return ATS;
    }

    private void Insert_Update_Dataset_Challan_Details(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_FromBranch = (DropDownList)(e.Item.FindControl("ddl_FromBranch"));
        ddl_ToBranch = (DropDownList)(e.Item.FindControl("ddl_ToBranch"));
        lbl_KmsRun = (Label)(e.Item.FindControl("lbl_KmsRun"));
        txt_THCNo = (TextBox)(e.Item.FindControl("txt_THCNo"));
        txt_StartKM = (TextBox)(e.Item.FindControl("txt_StartKms"));
        txt_EndKM = (TextBox)(e.Item.FindControl("txt_EndKms"));
        txt_HireAmount = (TextBox)(e.Item.FindControl("txt_HireAmount"));
        txt_Advance = (TextBox)(e.Item.FindControl("txt_Advance"));
        txt_Act_Wt = (TextBox)(e.Item.FindControl("txt_ActWt"));

        dtp_THCDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_THCDate"));
        _itemIndex = e.Item.ItemIndex;
        objDT = SessionTripHireChallansGrid;
        if (objDT.Rows.Count > 0)
        {
            TableRow = objDT.Rows.Count - 1;
            EndKM = Util.String2Decimal(objDT.Rows[TableRow]["End_KM"].ToString());
        }


        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }
        lbl_KmsRun.Text = Util.Int2String(Util.String2Int(txt_EndKM.Text) - Util.String2Int(txt_StartKM.Text));
        //lbl_KmsRun.Text = hdn_KMsRun.Value;
        if (Allow_To_Add_Update_ChallanDetails() == true)
        {
            DR["From_Location_ID"] = ddl_FromBranch.SelectedValue;
            DR["From_Branch_Name"] = ddl_FromBranch.SelectedItem;
            DR["To_Location_ID"] = ddl_ToBranch.SelectedValue;
            DR["To_Branch_Name"] = ddl_ToBranch.SelectedItem;
            DR["Trip_Memo_No"] = txt_THCNo.Text;
            DR["Start_KM"] = txt_StartKM.Text.Trim() == string.Empty ? 0 : Util.String2Int(txt_StartKM.Text);
            DR["End_KM"] = txt_EndKM.Text.Trim() == string.Empty ? 0 : Util.String2Int(txt_EndKM.Text);
            DR["Hire_Amount"] = txt_HireAmount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_HireAmount.Text);
            DR["Advance_Amount"] = txt_Advance.Text == string.Empty ? 0 : Util.String2Decimal(txt_Advance.Text);
            //DR["KM_Run"] = Util.String2Int(hdn_KMsRun.Value);
            DR["KM_Run"] = Util.String2Int(txt_EndKM.Text) - Util.String2Int(txt_StartKM.Text);
            DR["Trip_Date"] = dtp_THCDate.SelectedDate;
            DR["Total_Act_Wt"] = txt_Act_Wt.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Act_Wt.Text);

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionTripHireChallansGrid = objDT;
        }
        else
        {
            lbl_KmsRun.Text = hdn_KMsRun.Value;
        }
    }

    private bool Allow_To_Add_Update_ChallanDetails()
    {

        errorMessage = "";

        if (dtp_THCDate.SelectedDate < Trip_Start_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_Trip_Memo_Start_Date").ToString() + " (" + Trip_Start_Date.ToShortDateString() + ")";

        }
        else if (dtp_THCDate.SelectedDate > Trip_End_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_Trip_Memo_End_Date").ToString() + " (" + Trip_End_Date.ToShortDateString() + ")";
        }
        else if (Util.String2Int(ddl_FromBranch.SelectedValue) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_FromBranch").ToString();
            SCM_TripSettlement.SetFocus(ddl_FromBranch);
        }
        else if (Util.String2Int(ddl_ToBranch.SelectedValue) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ToBranch").ToString();
            SCM_TripSettlement.SetFocus(ddl_ToBranch);
        }
        else if (Util.String2Int(ddl_FromBranch.SelectedValue) == Util.String2Int(ddl_ToBranch.SelectedValue))
        {
            errorMessage = "From branch (" + ddl_FromBranch.SelectedItem.Text.ToString() + ") and To branch (" + ddl_ToBranch.SelectedItem.Text.ToString() + ") can't be same";
            SCM_TripSettlement.SetFocus(ddl_ToBranch);
        }
        else if (Util.String2Int(txt_StartKM.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_StartKm").ToString();
            SCM_TripSettlement.SetFocus(txt_StartKM);
        }
        else if ((_itemIndex == -1) && (EndKM > Util.String2Decimal(txt_StartKM.Text)))
        {
            errorMessage = GetLocalResourceObject("Msg_StartKmValidation").ToString() + " " + EndKM;
            SCM_TripSettlement.SetFocus(txt_StartKM);
        }
        else if (Util.String2Int(txt_EndKM.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_EndKm").ToString();
            SCM_TripSettlement.SetFocus(txt_EndKM);
        }

        else if (Util.String2Int(txt_EndKM.Text) < Util.String2Int(txt_StartKM.Text))
        {
            lbl_Errors.Text = GetLocalResourceObject("Msg_EndKmValid").ToString();
            SCM_TripSettlement.SetFocus(txt_EndKM);
        }
        else if (Util.String2Int(lbl_KmsRun.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_RunKms").ToString();
        }
        else if (Util.String2Decimal(txt_HireAmount.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_HireAmount").ToString();
            SCM_TripSettlement.SetFocus(txt_HireAmount);
        }
        else if (Util.String2Decimal(txt_HireAmount.Text) < Util.String2Decimal(txt_Advance.Text))
        {
            lbl_Errors.Text = GetLocalResourceObject("Msg_AdvanceAmount").ToString();
            SCM_TripSettlement.SetFocus(txt_Advance);
        }
        else
        {

            isValid = true;
        }
        return isValid;
    }

    #endregion

    #region dg_TripHireChallans Events

    protected void dg_TripHireChallans_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_FromBranch = (DropDownList)(e.Item.FindControl("ddl_FromBranch"));
                ddl_ToBranch = (DropDownList)(e.Item.FindControl("ddl_ToBranch"));
                lbl_KmsRun = (Label)(e.Item.FindControl("lbl_KmsRun"));
                txt_THCNo = (TextBox)(e.Item.FindControl("txt_THCNo"));
                txt_StartKM = (TextBox)(e.Item.FindControl("txt_StartKms"));
                txt_EndKM = (TextBox)(e.Item.FindControl("txt_EndKms"));
                txt_HireAmount = (TextBox)(e.Item.FindControl("txt_HireAmount"));
                txt_Advance = (TextBox)(e.Item.FindControl("txt_Advance"));
                txt_Act_Wt = (TextBox)(e.Item.FindControl("txt_ActWt"));
                dtp_THCDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_THCDate"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindFromBranch = SessionFromBranchDropdown;
                BindToBranch = SessionToBranchDropdown;
                dtp_THCDate.SelectedDate = DateTime.Now.Date;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionTripHireChallansGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                txt_THCNo.Text = DR["Trip_Memo_No"].ToString();
                ddl_FromBranch.SelectedValue = DR["From_Location_ID"].ToString();
                ddl_ToBranch.SelectedValue = DR["To_Location_ID"].ToString();
                txt_StartKM.Text = DR["Start_KM"].ToString();
                txt_EndKM.Text = DR["End_KM"].ToString();
                txt_HireAmount.Text = DR["Hire_Amount"].ToString();
                txt_Advance.Text = DR["Advance_Amount"].ToString();
                txt_Act_Wt.Text = DR["Total_Act_Wt"].ToString();
                lbl_KmsRun.Text = DR["KM_Run"].ToString();
                dtp_THCDate.SelectedDate = Convert.ToDateTime(DR["Trip_Date"].ToString());
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                string calculate_Textbox = "Calculate_HireChallansKM(" + txt_StartKM.ClientID + "," + txt_EndKM.ClientID + "," + lbl_KmsRun.ClientID + "," + hdn_KMsRun.ClientID + ")";

                txt_StartKM.Attributes.Add("onblur", calculate_Textbox);
                txt_EndKM.Attributes.Add("onblur", calculate_Textbox);
            }
        }
    }

    protected void dg_TripHireChallans_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionTripHireChallansGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[2];
                _dtColumnPrimaryKey[0] = objDT.Columns["From_Location_ID"];
                _dtColumnPrimaryKey[1] = objDT.Columns["To_Location_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset_Challan_Details(source, e);
                if (isValid == true)
                {
                    BindTripHireChallanGrid(this, e);
                    dg_TripHireChallans.EditItemIndex = -1;
                    dg_TripHireChallans.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = GetLocalResourceObject("Msg_DuplicateBranch").ToString();
                SCM_TripSettlement.SetFocus(ddl_FromBranch);
            }
        }
    }

    protected void dg_TripHireChallans_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;

        dg_TripHireChallans.EditItemIndex = e.Item.ItemIndex;
        dg_TripHireChallans.ShowFooter = false;
        BindTripHireChallanGrid(this, e);
    }

    protected void dg_TripHireChallans_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TripHireChallans.EditItemIndex = -1;
        dg_TripHireChallans.ShowFooter = true;
        BindTripHireChallanGrid(this, e);
    }

    protected void dg_TripHireChallans_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionTripHireChallansGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionTripHireChallansGrid = objDT;
            dg_TripHireChallans.EditItemIndex = -1;
            dg_TripHireChallans.ShowFooter = true;
            BindTripHireChallanGrid(this, e);
        }
    }

    protected void dg_TripHireChallans_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionTripHireChallansGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[2];
            _dtColumnPrimaryKey[0] = objDT.Columns["From_Location_ID"];
            _dtColumnPrimaryKey[1] = objDT.Columns["To_Location_ID"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset_Challan_Details(source, e);

            if (isValid == true)
            {
                dg_TripHireChallans.EditItemIndex = -1;
                dg_TripHireChallans.ShowFooter = true;
                BindTripHireChallanGrid(this, e);
            }
        }
        catch (ConstraintException)
        {
            errorMessage = GetLocalResourceObject("Msg_DuplicateBranch").ToString();
            SCM_TripSettlement.SetFocus(ddl_FromBranch);
        }
    }

    #endregion

    #endregion

    #region Fuel Expenses Related

    #region Varaiable For Fule Expenses

    DropDownList ddl_Pump;
    TextBox txt_Place;
    TextBox txt_Pump;
    TextBox txt_MemoSlipNo;
    TextBox txt_Qty;
    TextBox txt_Rate;
    TextBox txt_FuelAmount;
    TextBox txt_OilAmount;
    Label lbl_Total;
    TextBox txt_OnKms;
    CheckBox chk_IsCash;
    ComponentArt.Web.UI.Calendar dtp_FuelDate;

    DateTime _Trip_End_Date, _Trip_Start_Date;

    #endregion

    #region Properties For Fuel Expenses

    public decimal TotalTripFuelDieselCash
    {
        get { return Util.String2Decimal((string)hdn_total_diesel_cash.Value); }
        set { hdn_total_diesel_cash.Value = value.ToString(); }
    }

    public decimal TotalTripFuelDieselCredit
    {
        get { return Util.String2Decimal((string)hdn_total_diesel_credit.Value); }
        set { hdn_total_diesel_credit.Value = value.ToString(); }
    }

    public DataTable SessionTripFuelDetailsGrid
    {
        get { return StateManager.GetState<DataTable>("TripFuelDetailsGrid"); }
        set { StateManager.SaveState("TripFuelDetailsGrid", value); }
    }

    public String TripFuelDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionTripFuelDetailsGrid.Copy());
            _objDs.Tables[0].TableName = "Trip_Fuel_Details_2";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region Other Methods for Fuel Expenses

    public void SetVendorID(string text, string value)
    {
        BindPump = SessionPumpDropDown;
        ddl_Pump.SelectedValue = value;
        
    }

    public bool ValidateUIFuelDetails()
    {
        bool _isValid = true;

        foreach (DataRow dr in SessionTripFuelDetailsGrid.Rows)
        {
            if (Convert.ToDateTime(dr["Fuel_Date"].ToString()) < Trip_Start_Date || Convert.ToDateTime(dr["Fuel_Date"].ToString()) > Trip_End_Date)
            {
                errorMessage = GetLocalResourceObject("Msg_FuelDateTSDTED").ToString();
                _isValid = false;
                break;
            }
            else
                _isValid = true;
        }
        return _isValid;
    }

    private void Insert_Update_Dataset_Fuel_Details(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        chk_IsCash = (CheckBox)(e.Item.FindControl("chk_IsCash"));
        ddl_Pump = (DropDownList)(e.Item.FindControl("ddl_Pump"));
        txt_Place = (TextBox)(e.Item.FindControl("txt_Place"));
        txt_Pump = (TextBox)(e.Item.FindControl("txt_Pump"));
        txt_MemoSlipNo = (TextBox)(e.Item.FindControl("txt_MemoSlipNo"));
        txt_Qty = (TextBox)(e.Item.FindControl("txt_Qty"));
        txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
        txt_FuelAmount = (TextBox)(e.Item.FindControl("txt_FuelAmount"));
        txt_OilAmount = (TextBox)(e.Item.FindControl("txt_OilAmount"));
        lbl_Total = (Label)(e.Item.FindControl("lbl_Total"));
        txt_OnKms = (TextBox)(e.Item.FindControl("txt_OnKms"));
        dtp_FuelDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_FuelDate"));

        objDT = SessionTripFuelDetailsGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update_Fuel_Details() == true)
        {
            DR["Is_Cash"] = chk_IsCash.Checked;
            DR["Fuel_Date"] = dtp_FuelDate.SelectedDate;
            if (chk_IsCash.Checked == false)
            {
                DR["Vendor_ID"] = ddl_Pump.SelectedValue;
                DR["Petrol_Pump_Name"] = ddl_Pump.SelectedItem;
            }
            else
            {
                DR["Vendor_ID"] = "0";
                DR["Petrol_Pump_Name"] = txt_Pump.Text;
            } 
            //DR["Vendor_ID"] = ddl_Pump.SelectedValue;
            //DR["Petrol_Pump_Name"] = ddl_Pump.SelectedItem;
            DR["Petrol_Pump_Place"] = txt_Place.Text;
            DR["Fuel_Slip_No"] = txt_MemoSlipNo.Text;
            DR["Quantity"] = txt_Qty.Text == string.Empty ? 0 : Util.String2Decimal(txt_Qty.Text);
            DR["Rate"] = txt_Rate.Text == string.Empty ? 0 : Util.String2Decimal(txt_Rate.Text);
            if (Util.String2Decimal(txt_Qty.Text.Trim()) > 0 && Util.String2Decimal(txt_Rate.Text.Trim()) > 0)
            {
                DR["Fuel_Amount"] = Util.Decimal2String(Math.Round(Util.String2Decimal(DR["Quantity"].ToString()) * Util.String2Decimal(DR["Rate"].ToString()), 2));
            }
            else
            {
                DR["Fuel_Amount"] = txt_FuelAmount.Text == string.Empty ? 0 : Util.String2Decimal(txt_FuelAmount.Text);
            }
            DR["Oil_Amount"] = txt_OilAmount.Text == string.Empty ? 0 : Util.String2Decimal(txt_OilAmount.Text);
            DR["Total_Amount"] = Math.Round(Util.String2Decimal(DR["Fuel_Amount"].ToString()) + Util.String2Decimal(DR["Oil_Amount"].ToString()), 2);
            DR["On_Kms"] = txt_OnKms.Text.Trim() == string.Empty ? 0 : Util.String2Int(txt_OnKms.Text);

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionTripFuelDetailsGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update_Fuel_Details()
    {

        errorMessage = "";

        if (dtp_FuelDate.SelectedDate < Trip_Start_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_FuelDateTSDValid").ToString();
        }
        else if (dtp_FuelDate.SelectedDate > Trip_End_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_FuelDateTEDValid").ToString();
        }
        else if (txt_Place.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Place").ToString();
            SCM_TripSettlement.SetFocus(txt_Place);
        }
        else if (chk_IsCash.Checked == false && Util.String2Int(ddl_Pump.SelectedValue) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_Pump").ToString();
        }
        else if (chk_IsCash.Checked == true && txt_Pump.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txtPump").ToString();
            SCM_TripSettlement.SetFocus(txt_Pump);
        }
        else if (chk_IsCash.Checked == false && txt_MemoSlipNo.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Memoslipno").ToString();
            SCM_TripSettlement.SetFocus(txt_MemoSlipNo);
        }
        //else if (Util.String2Decimal(txt_FuelAmount.Text) <= 0 && Util.String2Decimal(txt_OilAmount.Text) <= 0)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_FuelOil").ToString();
        //    SCM_TripSettlement.SetFocus(txt_FuelAmount);
        //}
        else if (Util.String2Decimal(txt_FuelAmount.Text) + Util.String2Decimal(txt_OilAmount.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_OilAmt").ToString();
            SCM_TripSettlement.SetFocus(txt_FuelAmount);
        }
        else if (Util.String2Int(txt_OnKms.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_OnKms").ToString();
            SCM_TripSettlement.SetFocus(txt_OnKms);
        }
        else
        {

            isValid = true;
        }
        return isValid;
    }

    public void BindTripFuelDetailsGrid(object o, EventArgs e)
    {
        decimal Total_Diesel_Cash;
        decimal Total_Diesel_Credit;

        Total_Diesel_Cash = Util.String2Decimal(SessionTripFuelDetailsGrid.Compute("Sum(Total_Amount)", "Is_Cash = 1").ToString());
        Total_Diesel_Credit = Util.String2Decimal(SessionTripFuelDetailsGrid.Compute("Sum(Total_Amount)", "Is_Cash = 0").ToString());

        //if (Total_Diesel_Cash == -1) Total_Diesel_Cash = 0;
        //if (Total_Diesel_Credit == -1) Total_Diesel_Credit = 0;

        hdn_total_diesel_cash.Value = Total_Diesel_Cash.ToString();
        hdn_total_diesel_credit.Value = Total_Diesel_Credit.ToString();

        updateTripSettlementDetails(this, e);

        dg_TripFuelDetails.DataSource = SessionTripFuelDetailsGrid;
        dg_TripFuelDetails.DataBind();
    }

    #endregion

    #region dg_TripFuelDetails Events

    protected void dg_TripFuelDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                chk_IsCash = (CheckBox)(e.Item.FindControl("chk_IsCash"));
                ddl_Pump = (DropDownList)(e.Item.FindControl("ddl_Pump"));
                txt_Place = (TextBox)(e.Item.FindControl("txt_Place"));
                txt_Pump = (TextBox)(e.Item.FindControl("txt_Pump"));
                txt_MemoSlipNo = (TextBox)(e.Item.FindControl("txt_MemoSlipNo"));
                txt_Qty = (TextBox)(e.Item.FindControl("txt_Qty"));
                txt_Rate = (TextBox)(e.Item.FindControl("txt_Rate"));
                txt_FuelAmount = (TextBox)(e.Item.FindControl("txt_FuelAmount"));
                txt_OilAmount = (TextBox)(e.Item.FindControl("txt_OilAmount"));
                lbl_Total = (Label)(e.Item.FindControl("lbl_Total"));
                txt_OnKms = (TextBox)(e.Item.FindControl("txt_OnKms"));
                dtp_FuelDate = (ComponentArt.Web.UI.Calendar)(e.Item.FindControl("dtp_FuelDate"));

            }


            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                dtp_FuelDate.SelectedDate = DateTime.Now.Date;
                BindPump = SessionPumpDropDown;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionTripFuelDetailsGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                chk_IsCash.Checked = Util.String2Bool(DR["Is_Cash"].ToString());
                chk_IsCash.Checked = Util.String2Bool(DR["Is_Cash"].ToString());
                if (chk_IsCash.Checked == false)
                {
                    ddl_Pump.Visible = true;
                    txt_Pump.Visible = false;

                    SetVendorID(DR["Petrol_Pump_Name"].ToString(), DR["Vendor_ID"].ToString());
                }
                else
                {
                    ddl_Pump.Visible = false;
                    txt_Pump.Visible = true;
                    txt_Pump.Text = DR["Petrol_Pump_Name"].ToString();
                } 
                //ddl_Pump.SelectedValue = DR["Vendor_ID"].ToString();
                txt_Place.Text = DR["Petrol_Pump_Place"].ToString();
                txt_MemoSlipNo.Text = DR["Fuel_Slip_No"].ToString();
                txt_Qty.Text = DR["Quantity"].ToString();
                txt_Rate.Text = DR["Rate"].ToString();
                txt_FuelAmount.Text = DR["Fuel_Amount"].ToString();
                txt_OilAmount.Text = DR["Oil_Amount"].ToString();
                lbl_Total.Text = DR["Total_Amount"].ToString();
                txt_OnKms.Text = DR["On_Kms"].ToString();
                dtp_FuelDate.SelectedDate = Convert.ToDateTime(DR["Fuel_Date"].ToString());
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                string calculate_Amount = "Calculate_FuelAmount(" + txt_Qty.ClientID + "," + txt_Rate.ClientID + "," + txt_FuelAmount.ClientID + "," + txt_OilAmount.ClientID + "," + lbl_Total.ClientID + "," + hdn_FuelAmt.ClientID + "," + hdn_TotalAmt.ClientID + ")";
                string calculate_Rate = "Calculate_FuelRate(" + txt_Qty.ClientID + "," + txt_Rate.ClientID + "," + txt_FuelAmount.ClientID + "," + txt_OilAmount.ClientID + "," + lbl_Total.ClientID + "," + hdn_FuelAmt.ClientID + "," + hdn_TotalAmt.ClientID + "," + hdn_FuelRate.ClientID + ")";

                txt_Qty.Attributes.Add("onblur", calculate_Amount);
                txt_Rate.Attributes.Add("onblur", calculate_Amount);
                txt_FuelAmount.Attributes.Add("onblur", calculate_Rate);
                txt_OilAmount.Attributes.Add("onblur", calculate_Rate);
            }
        }
    }

    protected void dg_TripFuelDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {

            Insert_Update_Dataset_Fuel_Details(source, e);
            if (isValid == true)
            {
                //BindTripFuelDetailsGrid = SessionTripFuelDetailsGrid;
                BindTripFuelDetailsGrid(this, e);
                dg_TripFuelDetails.EditItemIndex = -1;
                dg_TripFuelDetails.ShowFooter = true;
            }
            //}
            //catch (ConstraintException)
            //{
            //    lbl_Errors.Text = "Duplicate Pump";
            //    SCM_TripSettlement.SetFocus(ddl_Pump);
            //}
        }
    }
    protected void dg_TripFuelDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;

        dg_TripFuelDetails.EditItemIndex = e.Item.ItemIndex;
        dg_TripFuelDetails.ShowFooter = false;
        //BindTripFuelDetailsGrid = SessionTripFuelDetailsGrid;
        BindTripFuelDetailsGrid(this, e);
    }
    protected void dg_TripFuelDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TripFuelDetails.EditItemIndex = -1;
        dg_TripFuelDetails.ShowFooter = true;
        //BindTripFuelDetailsGrid = SessionTripFuelDetailsGrid;
        BindTripFuelDetailsGrid(this, e);
    }
    protected void dg_TripFuelDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {

        Insert_Update_Dataset_Fuel_Details(source, e);

        if (isValid == true)
        {
            dg_TripFuelDetails.EditItemIndex = -1;
            dg_TripFuelDetails.ShowFooter = true;

            BindTripFuelDetailsGrid(this, e);
        }
    }
    protected void dg_TripFuelDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionTripFuelDetailsGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionTripFuelDetailsGrid = objDT;
            dg_TripFuelDetails.EditItemIndex = -1;
            dg_TripFuelDetails.ShowFooter = true;
            //BindTripFuelDetailsGrid = SessionTripFuelDetailsGrid;
            BindTripFuelDetailsGrid(this, e);
        }
    }
    protected void chk_IsCash_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk_IsCash = (CheckBox)(sender);
        DataGridItem dgitm = (DataGridItem)(chk_IsCash.Parent.Parent);
        ddl_Pump = (DropDownList)(dgitm.FindControl("ddl_Pump"));
        txt_Pump = (TextBox)(dgitm.FindControl("txt_Pump"));

        if (chk_IsCash.Checked == false)
        {
            ddl_Pump.Visible = true;
            txt_Pump.Visible = false;
        }
        else
        {
            ddl_Pump.Visible = false;
            txt_Pump.Visible = true;
        }

    }

    #endregion

    #endregion

    #region Expenses Details Related

    #region Varaiable For Expenses Details

    DropDownList ddl_ExpenseHead;
    TextBox txt_Amount;
    TextBox txt_Description;

    #endregion

    #region Properties For Expense Details

    public decimal TotalTripExpenseAmount
    {
        get { return Util.String2Decimal((string)lbl_Total_expense.Text); }
    }

    public DataTable BindExpenseHead
    {
        set
        {
            ddl_ExpenseHead.DataTextField = "Expense_Head";
            ddl_ExpenseHead.DataValueField = "Expense_Head_ID";
            ddl_ExpenseHead.DataSource = value;
            ddl_ExpenseHead.DataBind();
            ddl_ExpenseHead.Items.Insert(0, new ListItem(" Select One ", "0"));
        }
    }

    public DataTable SessionExpenseHeadDropdown
    {
        get { return StateManager.GetState<DataTable>("ExpenseHeadDropdown"); }
        set { StateManager.SaveState("ExpenseHeadDropdown", value); }
    }

    public DataTable SessionTripExpenseGrid
    {
        get { return StateManager.GetState<DataTable>("TripExpenseGrid"); }
        set { StateManager.SaveState("TripExpenseGrid", value); }
    }

    public String TripExpenseDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionTripExpenseGrid.Copy());
            _objDs.Tables[0].TableName = "Trip_Expense_Details_2";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region Other Methods For Expense Details

    public bool ValidateUIExpensesDetails()
    {
        bool IsValid = true;

        return IsValid;
    }

    public void BindTripExpenseGrid(object o, EventArgs e)
    {

        updateTripSettlementDetails(this, e);

        dg_TripExpense.DataSource = SessionTripExpenseGrid;
        dg_TripExpense.DataBind();
    }

    private void Insert_Update_Dataset_Expense_Details(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        ddl_ExpenseHead = (DropDownList)(e.Item.FindControl("ddl_ExpenseHead"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        objDT = SessionTripExpenseGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update_Expense_Details() == true)
        {
            DR["Expense_Head_ID"] = ddl_ExpenseHead.SelectedValue;
            DR["Expense_Head"] = ddl_ExpenseHead.SelectedItem;
            DR["Expense_Amount"] = txt_Amount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text);
            DR["Description"] = txt_Description.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionTripExpenseGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update_Expense_Details()
    {

        lbl_Errors.Text = "";

        if (Util.String2Int(ddl_ExpenseHead.SelectedValue) == 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ExpenseHead").ToString();
            SCM_TripSettlement.SetFocus(ddl_ExpenseHead);
        }
        else if (Util.String2Decimal(txt_Amount.Text) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_Amount").ToString();
            SCM_TripSettlement.SetFocus(txt_Amount);
        }
        else if (txt_Description.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Description").ToString();
            SCM_TripSettlement.SetFocus(txt_Description);
        }
        else
        {

            isValid = true;
        }
        return isValid;
    }

    #endregion

    #region dg_TripExpense Events

    protected void dg_TripExpense_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_ExpenseHead = (DropDownList)(e.Item.FindControl("ddl_ExpenseHead"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
            }

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindExpenseHead = SessionExpenseHeadDropdown;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionTripExpenseGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                ddl_ExpenseHead.SelectedValue = DR["Expense_Head_ID"].ToString();
                txt_Amount.Text = DR["Expense_Amount"].ToString();
                txt_Description.Text = DR["Description"].ToString();
            }
        }
    }

    protected void dg_TripExpense_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionTripExpenseGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["Expense_Head_ID"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset_Expense_Details(source, e);
                if (isValid == true)
                {
                    //BindTripExpenseGrid = SessionTripExpenseGrid;
                    BindTripExpenseGrid(this, e);
                    dg_TripExpense.EditItemIndex = -1;
                    dg_TripExpense.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = GetLocalResourceObject("Msg_Duplicate").ToString();
                SCM_TripSettlement.SetFocus(ddl_ExpenseHead);
            }
        }
    }
    protected void dg_TripExpense_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionTripExpenseGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["Expense_Head_ID"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset_Expense_Details(source, e);

            if (isValid == true)
            {
                dg_TripExpense.EditItemIndex = -1;
                dg_TripExpense.ShowFooter = true;

                //BindTripExpenseGrid = SessionTripExpenseGrid;
                BindTripExpenseGrid(this, e);
            }
        }
        catch (ConstraintException)
        {
            errorMessage = GetLocalResourceObject("Msg_Duplicate").ToString();
            SCM_TripSettlement.SetFocus(ddl_ExpenseHead);
        }
    }
    protected void dg_TripExpense_EditCommand(object source, DataGridCommandEventArgs e)
    {
        LinkButton lbtn_Delete = (LinkButton)(e.Item.FindControl("lbtn_Delete"));
        lbtn_Delete.Enabled = false;

        dg_TripExpense.EditItemIndex = e.Item.ItemIndex;
        dg_TripExpense.ShowFooter = false;
        //BindTripExpenseGrid = SessionTripExpenseGrid;
        BindTripExpenseGrid(this, e);
    }
    protected void dg_TripExpense_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TripExpense.EditItemIndex = -1;
        dg_TripExpense.ShowFooter = true;
        //BindTripExpenseGrid = SessionTripExpenseGrid;
        BindTripExpenseGrid(this, e);
    }
    protected void dg_TripExpense_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionTripExpenseGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionTripExpenseGrid = objDT;
            dg_TripExpense.EditItemIndex = -1;
            dg_TripExpense.ShowFooter = true;
            //BindTripExpenseGrid = SessionTripExpenseGrid;
            BindTripExpenseGrid(this, e);
        }
    }


    #endregion

    #endregion

    #region  IView Implementation

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public string OBDrCr
    {
        set { ViewState["OBDrCr"] = value.ToString(); }
    }
    private string CBDrCr
    {
        set { lbl_CBcrdr.Text = value; }
    }

    public string Vehicle_Trip_No
    {
        get { return txt_TripSettlementNo.Text; }
        set { txt_TripSettlementNo.Text = value; }
    }

    public DateTime Vehicle_Trip_Date
    {
        get { return WucSettelemtDate.SelectedDate; }
        set { WucSettelemtDate.SelectedDate = value; }
    }

    public int Driver_ID
    {
        get { return Util.String2Int(ddl_Driver.SelectedValue); }
    }

    public int Vehicle_ID
    {
        set { WucVehicleNo.VehicleID = value; }
        get { return WucVehicleNo.VehicleID; }
    }

    public decimal DriverOpeningBalance
    {
        get { return 0; }
        set { ;}
    //    get { return Util.String2Decimal(lbl_DriverOpeningBalance.Text); }
    //    set
    //    {
    //        if (value <= 0)
    //        {
    //            lbl_DriverOpeningBalance.Text = "0.00";
    //        }
    //        else
    //            lbl_DriverOpeningBalance.Text = Util.Decimal2String(Math.Abs(value));
    //    }
    }

    public decimal Total_Hire_Amount
    {
        get { return Util.String2Decimal(lbl_Hire_Aount.Text ); }
        set { lbl_Hire_Aount.Text = value.ToString("0.00"); }
    }

    public decimal Total_Advance
    {
        get { return Util.String2Decimal(lbl_Total_Advance.Text ); }
        set { lbl_Total_Advance.Text = value.ToString("0.00"); }
    }

    public decimal Total_Trip_Expense
    {
        get { return Util.String2Decimal(lbl_Total_expense.Text ); }
        set { lbl_Total_expense.Text = value.ToString("0.00"); }
    }

    public decimal TotalDieselCash
    {
        get { return Util.String2Decimal(ViewState["TotalDieselCash"].ToString() ); }
        set { ViewState["TotalDieselCash"] = value.ToString("0.00"); }
    }

    public decimal TotalDieselCredit
    {
        get { return Util.String2Decimal(ViewState["TotalDieselCredit"].ToString()); }
        set { ViewState["TotalDieselCredit"] = value.ToString("0.00"); }
    }

    public decimal Driver_Closing_Balance
    {
        get { return Util.String2Decimal(lbl_DriverClosingBalance.Text); }
        set
        {
           lbl_DriverClosingBalance.Text = value.ToString("0.00");
        //    if (value <= 0)
        //    {
        //        lbl_DriverClosingBalance.Text = "0.00";
        //    }
        //    else
        //        lbl_DriverClosingBalance.Text = Util.Decimal2String(Math.Abs(value));
        }
    }

    public decimal Total_Actual_Wt
    {
        get { return Util.String2Decimal(lbl_Total_Actual_Wt.Text); }
        set { lbl_Total_Actual_Wt.Text = Util.Decimal2String(Math.Abs(value)); }
    }

    public decimal Total_Fuel_Qty
    {
        get { return Util.String2Decimal(lbl_Tot_Fuel_Qty.Text ); }
        set { lbl_Tot_Fuel_Qty.Text  = Util.Decimal2String(Math.Abs(value)); }
    }

    public decimal Total_Fuel_Amount
    {
        get { return Util.String2Decimal(lbl_Total_Fuel_Amount.Text ); }
        set { lbl_Total_Fuel_Amount.Text  = Util.Decimal2String(Math.Abs(value)); }
    }

    public decimal Total_Oil_Amount
    {
        get { return Util.String2Decimal(lbl_Total_Oil_Amount.Text); }
        set { lbl_Total_Oil_Amount.Text = Util.Decimal2String(Math.Abs(value)); }
    }

    public decimal Total_Fuel_Oil_Cost
    {
        get { return Util.String2Decimal(lbl_TotalFuel_Oil_Amount.Text); }
        set { lbl_TotalFuel_Oil_Amount.Text= Util.Decimal2String(Math.Abs(value)); }
    }

    public decimal Total_Trip_Cost
    {
        get { return Util.String2Decimal((string)lbl_Total_Trip_Cost.Text ); }
        set { lbl_Total_Trip_Cost.Text = Util.Decimal2String(Math.Abs(value)); }
    }

    public string Remarks
    {
        get { return txt_Remarks.Text; }
        set { txt_Remarks.Text = value; }
    }

    public bool validateUI()
    {
        bool IsValid = false;
        //DateTime VehicleOnRoadDate = objTrip_Settlement_2Presenter.GetVehicle_OnRoadDate();

        TextBox txt_Vehicle = (TextBox)WucVehicleNo.FindControl("txt_Vehicle_Last_4_Digits");
        TextBox txt_Driver = (TextBox)ddl_Driver.FindControl("txtBoxddl_Driver");

        errorMessage = "";

        //if (Vehicle_Trip_No == string.Empty)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_SettlementNo").ToString();
        //}
        //else 
        if (Datemanager.IsValidProcessDate("TP_SD", Vehicle_Trip_Date ) == false)
        {
            errorMessage = GetLocalResourceObject("Msg_SettlementDate").ToString();
        }
        else if (Trip_Start_Date > Vehicle_Trip_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_VehicleOnRoadDate").ToString(); //+ ": (" + objTrip_Settlement_2Presenter.GetVehicle_OnRoadDate().ToShortDateString() + ")";
        }
        else if (Trip_Start_Date > Trip_End_Date)
        {
            errorMessage = GetLocalResourceObject("Msg_Trip_StartDate").ToString();
        }
        else if ( Trip_End_Date > Vehicle_Trip_Date )
        {
            errorMessage = GetLocalResourceObject("Msg_Trip_End_Date").ToString();
        }
        else if (Vehicle_ID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_VehicleID").ToString();
            SCM_TripSettlement.SetFocus(txt_Vehicle);
        }
        else if (Driver_ID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_DriverId").ToString();
            SCM_TripSettlement.SetFocus(txt_Driver);
        }
        else if (Total_Trip_Cost <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_Total_Trip_Cost").ToString();
            SCM_TripSettlement.SetFocus(dg_TripFuelDetails);
        }
        else
        {
            if (ValidateUITripDetails())
            {
                if (ValidateUIFuelDetails())
                {
                    if (ValidateUIExpensesDetails())
                    {
                        IsValid = true;
                    }

                }
            }
        }
        lbl_Errors.Visible = (!IsValid);

        return IsValid;
    }

    #endregion

    #region Other Properties

    public int Mode
    {
        get { return Util.DecryptToInt(Request.QueryString["Mode"]); }
    }

    #endregion

    #region Page/Button Methods

    protected void Page_Load(object sender, EventArgs e)
    {

        WucVehicleNo.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ddl_Driver.DataValueField = "Driver_Id";
        ddl_Driver.DataTextField = "Driver_Name";

        objTrip_Settlement_2Presenter = new Trip_Settlement_2Presenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Get_Next_TripSettelmentNo();
            }
            hdf_ResourecString.Value = objcomm.GetResourceString("Operations/Outward/App_LocalResources/Frm_Opr_Trip_Settlement.aspx.resx");
            BindTripHireChallanGrid(this, e);
            BindTripFuelDetailsGrid(this, e);
            BindTripExpenseGrid(this, e);
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        CalcTotalCosts();
        objTrip_Settlement_2Presenter.Save();
    }
    #endregion

    #region OtherMethods

    private void Get_Next_TripSettelmentNo()
    {
        txt_TripSettlementNo.Text = objcomm.Get_Next_Number();
    }

    public void SetDriverID(string text, string value)
    {
        ddl_Driver.DataValueField = "Driver_Id";
        ddl_Driver.DataTextField = "Driver_Name";

        Common.SetValueToDDLSearch(text, value, ddl_Driver);
    }

    //protected void ddl_Driver_TxtChange(object sender, EventArgs e)
    //{
    //   getDriverBalance();
    //}

    private void updateTripSettlementDetails(object o, EventArgs e)
    {
        //decimal TripHireChallanTotalHireAmount;
        //decimal TripHireChallanTotalAdvance;
        //decimal TripExpenseTotalExpense;
        //decimal TripFuelDieselCash;
        //decimal TripFuelDieselCredit;
        //decimal TripFuelTotalTripCast;

        //decimal Opening_Balance;
        //decimal ClosingBalance;

        //TripHireChallanTotalHireAmount = Total_Hire_Amount;
        //TripHireChallanTotalAdvance = Total_Fuel_Amount;

        //TripExpenseTotalExpense = TotalTripExpenseAmount;

        //TripFuelDieselCash = TotalTripFuelDieselCash;
        //TripFuelDieselCredit = TotalTripFuelDieselCredit;

        //TripFuelTotalTripCast = TripExpenseTotalExpense + TripFuelDieselCash + TripFuelDieselCredit;
        //Total_Hire_Amount = TripHireChallanTotalHireAmount;
        //Total_Advance = TripHireChallanTotalAdvance;
        //Total_Trip_Expense = TripExpenseTotalExpense;
        //TotalDieselCash = TripFuelDieselCash <= 0 ? 0 : TripFuelDieselCash;
        //TotalDieselCredit = TripFuelDieselCredit <= 0 ? 0 : TripFuelDieselCredit;
        //Total_Trip_Cost = TripFuelTotalTripCast <= 0 ? 0 : TripFuelTotalTripCast;
        ////Total_Trip_Cost += Total_Trip_Expense <= 0 ? 0 : Total_Trip_Expense;
        ////Opening_Balance = objTrip_Settlement_2Presenter.GetDriverOpeningBalance();

        ////if (Opening_Balance == -1) 
        //Opening_Balance = 0;

        //ClosingBalance = (Opening_Balance - TripHireChallanTotalAdvance) + (TripExpenseTotalExpense + TripFuelDieselCash);

        //if (ClosingBalance == -1) ClosingBalance = 0;

        //lbl_DriverClosingBalance.Text = Math.Abs(ClosingBalance).ToString();
        //if (ClosingBalance <= 0)
        //{ CBDrCr = "Dr"; }
        //else
        //{ CBDrCr = "Cr"; }
        CalcTotalCosts(); 
    }
    private void CalcTotalCosts()
    {
        Total_Actual_Wt = 0;
        Total_Fuel_Qty = 0;
        Total_Fuel_Amount = 0;
        Total_Oil_Amount = 0;
        Total_Fuel_Oil_Cost = 0;
        Total_Trip_Cost = 0;
        Total_Advance = 0;
        Total_Trip_Expense = 0;
        TotalTripFuelDieselCash = 0;
        TotalTripFuelDieselCredit = 0;

        foreach (DataRow DR in SessionTripFuelDetailsGrid.Select("", "", DataViewRowState.CurrentRows))
        {
            Total_Fuel_Qty += (decimal)DR["Quantity"]; ;
            Total_Fuel_Amount += (decimal)DR["Fuel_Amount"];
            Total_Oil_Amount += (decimal)DR["Oil_Amount"];
            Total_Fuel_Oil_Cost += ((decimal)DR["Fuel_Amount"] + (decimal)DR["Oil_Amount"]);
            Total_Trip_Cost += ((decimal)DR["Fuel_Amount"] + (decimal)DR["Oil_Amount"]);
            if ((bool)DR["Is_Cash"] == true)
            {
                TotalTripFuelDieselCash+=((decimal)DR["Fuel_Amount"] + (decimal)DR["Oil_Amount"]);
            }
            else
            {
                TotalTripFuelDieselCredit += ((decimal)DR["Fuel_Amount"] + (decimal)DR["Oil_Amount"]);
            }
        }

        foreach (DataRow DR in SessionTripHireChallansGrid.Select("", "", DataViewRowState.CurrentRows))
        {
            Total_Actual_Wt += (decimal)DR["Total_Act_Wt"];
            Total_Advance += (decimal)DR["Advance_Amount"];
        }

        foreach (DataRow DR in SessionTripExpenseGrid.Select("", "", DataViewRowState.CurrentRows))
        {
            Total_Trip_Cost += (decimal)DR["expense_amount"];
            Total_Trip_Expense += (decimal)DR["expense_amount"];
        }
        Driver_Closing_Balance = Total_Advance - (Total_Trip_Expense + TotalTripFuelDieselCash);
        //if (Driver_Closing_Balance == -1) Driver_Closing_Balance = 0;

        //lbl_DriverClosingBalance.Text = Math.Abs(ClosingBalance).ToString();
        if (Driver_Closing_Balance <= 0)
        { CBDrCr = "Dr"; }
        else
        { CBDrCr = "Cr"; }
    }
    //private void getDriverBalance()
    //{
    //    //objTrip_Settlement_2Presenter.GetDriverOpeningBalance();
    //    UpdatePanel1.Update();
    //}

    private void VehicleIndexChange(object sender, EventArgs e)
    {
    }

    #endregion

}
