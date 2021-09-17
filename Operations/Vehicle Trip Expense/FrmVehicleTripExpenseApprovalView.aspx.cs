using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using Raj.EC;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

public partial class Operations_VehicleTripExpense_FrmVehicleTripExpenseApprovalView : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList ddl_Branch;
    string Mode = "0", IsApprove;
    DataRow dr;
    bool Allow_To_Save;

    TextBox txt_Amount;
    Label lbl_ExpenseHead, lbl_ClosingCash;
    DataTable objDT;

    #region properties

    private int TripExpenseApprovalID
    {
        set { hdnKeyID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnKeyID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnKeyID.Value);
        }
    }

    private int TripExpenseID
    {
        set { hdnTripExpenseID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnTripExpenseID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnTripExpenseID.Value);
        }
    }

    private Boolean CanCloseTrip
    {
        set { hdnCanCloseTrip.Value = value.ToString(); }
        get
        {
            return Util.String2Bool(hdnCanCloseTrip.Value);
        }
    }

    private int Driver_ID
    {
        set { hdnDriver_ID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnDriver_ID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnDriver_ID.Value);
        }
    }

    private DateTime TripExpenseDate
    {
        set { dtpTripExpenseDate.SelectedDate = value; }
        get { return dtpTripExpenseDate.SelectedDate; }
    }
    private decimal TotalTripExpense
    {
        set
        {
            lblTotalTripExpense.Text = Util.Decimal2String(value);
            hdnTotalTripExpense.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdnTotalTripExpense.Value); }
    }

    private decimal TotalExpenseApproved
    {
        set
        {
            lblTotalExpenseApproved.Text = Util.Decimal2String(value);
            hdnTotalExpenseApproved.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdnTotalExpenseApproved.Value); }
    }

    private decimal OpeningBalance
    {
        set
        {
            lbl_OpeningBalance.Text = Util.Decimal2String(value);
            hdn_OpeningBalance.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_OpeningBalance.Value); }
    }

    private decimal ClosingBalance
    {
        set
        {
            lbl_ClosingBalance.Text = Util.Decimal2String(value);
            hdn_ClosingBalance.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ClosingBalance.Value); }
    }

    private decimal ExpectedAdvance
    {
        set
        {
            lbl_ExpectedAdvance.Text = Util.Decimal2String(value);
            hdn_ExpectedAdvance.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ExpectedAdvance.Value); }
    }

    public decimal AdditionalAdvance
    {
        set { txt_AdditionalAdvance.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_AdditionalAdvance.Text); }

    }

    private decimal TotalAdvanceToBeGiven
    {
        set
        {
            lbl_TotalAdvanceToBeGiven.Text = Util.Decimal2String(value);
            hdn_TotalAdvanceToBeGiven.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalAdvanceToBeGiven.Value); }
    }

    private decimal TotalAdvance
    {
        set
        {
            lbl_TotalAdvance.Text = Util.Decimal2String(value);
            hdn_TotalAdvance.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalAdvance.Value); }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public bool IsLastTrip
    {
        set { chk_LastTrip.Checked = value; }
        get { return chk_LastTrip.Checked; }
    }

    public bool IsVehicleChange
    {
        set { chk_VehicleChange.Checked = value; }
        get { return chk_VehicleChange.Checked; }
    }

    public bool IsAdjustInSalary
    {
        set { chk_AdjustInSalary.Checked = value; }
        get { return chk_AdjustInSalary.Checked; }
    }

    public decimal DriverBalanceDeposit
    {
        set { txt_DriverBalance.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_DriverBalance.Text); }

    }
    public int BalanceDepositBranchID
    {
        get { return Util.String2Int(DDLBalanceDepositBranch.SelectedValue); }
    }

    public void SetBalanceDepositBranch(string text, string value)
    {
        DDLBalanceDepositBranch.DataTextField = "BalanceDepositedBranch";
        DDLBalanceDepositBranch.DataValueField = "BalanceDepositedBranchID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDLBalanceDepositBranch);
    }

    public DataTable Session_TripExpense
    {
        get { return StateManager.GetState<DataTable>("TripExpenseGrid"); }
        set { StateManager.SaveState("TripExpenseGrid", value); }
    }
    public String TripExpenseXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_TripExpense.Copy());
            _objDs.Tables[0].TableName = "TripExpenseDetails";
            return _objDs.GetXml().ToLower();
        }
    }
    public DataTable Session_BranchDdl
    {
        get { return StateManager.GetState<DataTable>("BranchDdl"); }
        set { StateManager.SaveState("BranchDdl", value); }
    }
    public DataTable BindBranch
    {
        set { Set_Common_DDL(ddl_Branch, "Branch_Name", "Branch_ID", value, true); }
    }

    public DataTable Session_AdvanceDetails
    {
        get { return StateManager.GetState<DataTable>("AdvanceGrid"); }
        set { StateManager.SaveState("AdvanceGrid", value); }
    }

    public String AdvanceXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_AdvanceDetails.Copy());
            _objDs.Tables[0].TableName = "AdvanceDetails";
            return _objDs.GetXml().ToLower();
        }
    }

    private string ErrorMsg
    {
        set { lbl_Errors.Text = value; }
    }

    #endregion

    #region events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Util.DecryptToString(Request.QueryString["Mode"].ToString()) == "4")
        {

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));

        lnk_btnBranchClosingBalance.Text = "Branch Wise Cash Balance";
        lnk_btnBranchClosingBalance.Attributes.Add("onclick", "return BranchwiseCashBalance()");

        string Crypt = "";

        Crypt = System.Web.HttpContext.Current.Request.QueryString["IsApprove"];

        if (Crypt == null)
        {
            IsApprove = "1";
        }
        else
        {
            IsApprove = Util.DecryptToString(Request.QueryString["IsApprove"].ToString());

        }

        if (IsApprove == "0")
        {
            dg_GridTripExpense.Enabled = false;
        }

        if (!IsPostBack)
        {
            clearsessions();
            fillBranchList();

            Crypt = System.Web.HttpContext.Current.Request.QueryString["FromVB"];

            if (Crypt == null)
            {
                TripExpenseID = Util.DecryptToInt(Request.QueryString["Id"]);
            }
            else
            {
                TripExpenseID = Util.String2Int(Request.QueryString["Id"]);
            }

            ReadValues();
        }
        Assign_Hidden_Values_To_TextBox();
        if (Mode == "4")
        {
            btn_Save_Exit.Visible = false;
            btn_Close.Visible = false;
        }


        StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());

        PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4.Append("/Operations/Vehicle Trip Expense/Frm_Trip_Expense_Driver_Trip_History.aspx?Driver_ID=" + ClassLibraryMVP.Util.EncryptInteger(Driver_ID)
        + "&TripExpenseID=" + ClassLibraryMVP.Util.EncryptInteger(TripExpenseID));

        btn_DriverHistory.Attributes.Add("onclick", "return viewwindow_DriverHistory('" + PathF4 + "')");


        //Start History Listing
        String FromHistory;
        Crypt = System.Web.HttpContext.Current.Request.QueryString["FromHistory"];

        if (Crypt == null)
        {
            FromHistory = "0";
        }
        else
        {
            FromHistory = Util.DecryptToString(Request.QueryString["FromHistory"].ToString());
        }

        if (FromHistory == "1")
        {
            btn_DriverHistory.Visible = false;
            dg_AdvanceDetails.Enabled = false;
            dg_GridTripExpense.Enabled = false;
            txt_AdditionalAdvance.Enabled = false;
        }
        //End History Listing

    }

    private void Assign_Hidden_Values_To_TextBox()
    {
        lblTotalTripExpense.Text = hdnTotalTripExpense.Value;
        lblTotalExpenseApproved.Text = hdnTotalExpenseApproved.Value;
        lbl_ClosingBalance.Text = hdn_ClosingBalance.Value;
        lbl_ExpectedAdvance.Text = hdn_ExpectedAdvance.Value;
        lbl_OpeningBalance.Text = hdn_OpeningBalance.Value;
        lbl_TotalAdvanceToBeGiven.Text = hdn_TotalAdvanceToBeGiven.Value;

    }

    private void clearsessions()
    {
        Session_AdvanceDetails = null;
        Session_TripExpense = null;
    }

    private void fillBranchList()
    {
        objDAL.RunProc("EC_Fill_Branch_List", ref objDS);
        Session_BranchDdl = objDS.Tables[0];
    }

    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    protected void dg_GridTripExpense_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int TripExpenseHeadID;
            lbl_ExpenseHead = (Label)e.Item.FindControl("lbl_ExpenseHead");
            TextBox txtApproved = (TextBox)e.Item.FindControl("txtApproved");
            TextBox txt_Expense = (TextBox)e.Item.FindControl("txt_Expense");
            TripExpenseHeadID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseHeadID").ToString());
            if (e.Item.ItemIndex != -1)
            {
                txtApproved.Attributes.Add("onblur", "Calculate_ApprovedAmount('" + txtApproved.ClientID + "','" + dg_GridTripExpense.ClientID + "');");
            }
        }
    }

    private void FillTripExpenseGrid()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        objDAL.RunProc("dbo.EF_Opr_Trip_Expense_Fill_Expense_Head", ref ds);
        Session_TripExpense = ds.Tables[0];
        Bind_dg_TripExpense();
    }

    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0, TripExpenseID) };
        objDAL.RunProc("EF_Opr_Trip_Expense_Sheet_Details_For_Approval", objSqlParam, ref objDS);

        decimal AdvancePaid;
        AdvancePaid = 0;

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            TripExpenseApprovalID = Convert.ToInt32(objDR["TripExpenseAprovalID"].ToString());
            TripExpenseDate = Convert.ToDateTime(objDR["TripExpenseDate"].ToString());
            lbl_VehicleNo.Text = objDR["Vehicle_No"].ToString();

            if (Convert.ToBoolean(objDR["IsOpeningTrip"].ToString()) == false)
            {
                lbl_PreviousTripWeight.Text = objDR["PreviousTripWeight"].ToString() + " Kg Dated : " + objDR["PreviousTripWeightDate"].ToString();
            }
            else
            {
                lbl_PreviousTripWeight.Text = "Opening Trip";
                lbl_PreviousTripWeightH.Visible = false;
            }

            lbl_FromDate.Text = objDR["FromDate"].ToString();
            lbl_ToDate.Text = objDR["ToDate"].ToString();
            lbl_DriverName.Text = objDR["Driver_Name"].ToString();
            lbl_Cleaner.Text = objDR["Cleaner"].ToString();
            lbl_TripNo.Text = objDR["TripNo"].ToString();
            lbl_PreviousRoute.Text = objDR["PreviousRoute"].ToString();
            lbl_ReturnRoute.Text = objDR["ReturnRoute"].ToString();
            lbl_CurrentRoute.Text = objDR["CurrentRoute"].ToString();
            OpeningBalance = Util.String2Decimal(objDR["OpeningBalance"].ToString());
            lbl_CalculatedOpeningBalance.Text = objDR["CalculatedOpeningBalance"].ToString();
            hdn_CalculatedOpeningBalance.Value = objDR["CalculatedOpeningBalance"].ToString();
            TotalTripExpense = Util.String2Decimal(objDR["TotalTripExpense"].ToString());

            TotalExpenseApproved = Util.String2Decimal(objDR["TotalApprovedAmount"].ToString());
            lblCalculatedTotalExpenseApproved.Text = objDR["TotalApprovedAmount"].ToString();
            AdditionalAdvance = Util.String2Decimal(objDR["AdditionalAdvance"].ToString());
            TotalAdvanceToBeGiven = Util.String2Decimal(objDR["TotalAdvanceToBeGiven"].ToString());
            ClosingBalance = Util.String2Decimal(objDR["ClosingBalance"].ToString());
            lbl_CalculatedClosingBalance.Text = Convert.ToString(Util.String2Decimal(hdn_CalculatedOpeningBalance.Value) - Util.String2Decimal(lblCalculatedTotalExpenseApproved.Text));
            Remarks = objDR["Remark"].ToString();
            AdvancePaid = Util.String2Decimal(objDR["AdvancePaid"].ToString());
            Driver_ID = Convert.ToInt32(objDR["Driver_ID"].ToString());
            IsLastTrip = Convert.ToBoolean(objDR["IsLastTrip"].ToString());
            IsVehicleChange = Convert.ToBoolean(objDR["IsVehicleChange"].ToString());
            IsAdjustInSalary = Convert.ToBoolean(objDR["IsAdjustInSalary"].ToString());

            ClosingBalance = OpeningBalance - TotalExpenseApproved;
            CanCloseTrip = Convert.ToBoolean(objDR["CanCloseTrip"].ToString());

            lbl_IsEmptyReturn.Text = objDR["EmptyReturn"].ToString();
            lbl_IsEmptyTrip.Text = objDR["EmptyTrip"].ToString();

            if (CanCloseTrip == false)
            {
                chk_LastTrip.Enabled = false;
                chk_VehicleChange.Enabled = false;
            }
            if (TripExpenseApprovalID == 0 && (IsLastTrip || IsVehicleChange))
            {
                chk_LastTrip.Enabled = false;
                chk_VehicleChange.Enabled = false;
            }

            if (IsLastTrip == false)
            {
                ExpectedAdvance = 2000 - ClosingBalance;
                lbl_CalculatedExpectedAdvance.Text = Convert.ToString(2000 - Util.String2Decimal(lbl_CalculatedClosingBalance.Text));
                tr_DriverBalanceDeposit.Attributes.Add("style", "display:none");
            }
            else
            {
                ExpectedAdvance = ClosingBalance * -1;
                lbl_CalculatedExpectedAdvance.Text = Convert.ToString(Util.String2Decimal(lbl_CalculatedClosingBalance.Text) * -1);
                if (ExpectedAdvance < 0)
                {
                    tr_DriverBalanceDeposit.Attributes.Add("style", "display:block");
                    DriverBalanceDeposit = Util.String2Decimal(objDR["BalanceDepositedAmount"].ToString());

                    if (IsAdjustInSalary == true)
                    {
                        td_DepositeBranch.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        td_DepositeBranch.Attributes.Add("style", "display:block");
                        SetBalanceDepositBranch(objDR["BalanceDepositedBranch"].ToString(), objDR["BalanceDepositedBranchID"].ToString());
                    }
                }
            }




            if (ExpectedAdvance > 0)
            {
                TotalAdvanceToBeGiven = ExpectedAdvance + AdditionalAdvance;
                lbl_CalculatedTotalAdvanceToBeGiven.Text = Convert.ToString(Util.String2Decimal(lbl_CalculatedExpectedAdvance.Text) + AdditionalAdvance);

            }
        }

        if (TripExpenseID > 0)
        {
            Session_TripExpense = objDS.Tables[1];
            Bind_dg_TripExpense();
            dtpTripExpenseDate.Disable = true;

            Session_AdvanceDetails = objDS.Tables[2];
            Bind_dg_GridAdvanceDetails();

            if (AdvancePaid > 0)
            {
                dg_AdvanceDetails.Enabled = false;
            }

        }

        DataRow dr = objDS.Tables[3].Rows[0];
        TotalAdvance = Util.String2Decimal(dr["TotalAdvance"].ToString());

    }

    private void CalculateTotalExpenseApproved()
    {
        decimal TotalApproved;

        TotalApproved = 0;
        decimal amount = 0;
        int i;

        TextBox txtApproved, txtRemarks, txt_Expense;
        Label lbl_tripexpId;

        if (dg_GridTripExpense.Items.Count > 0 && StateManager.IsValidSession("TripExpenseGrid"))
        {
            for (i = 0; i <= dg_GridTripExpense.Items.Count - 1; i++)
            {
                txt_Expense = (TextBox)dg_GridTripExpense.Items[i].FindControl("txt_Expense");
                txtApproved = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtApproved");
                lbl_ExpenseHead = (Label)dg_GridTripExpense.Items[i].FindControl("lbl_ExpenseHead");
                txtRemarks = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtRemark");
                lbl_tripexpId = (Label)dg_GridTripExpense.Items[i].FindControl("lblTripExpenseHeadID");

                if (string.IsNullOrEmpty(txtApproved.Text))
                {
                    amount = 0;
                }
                else
                {
                    amount = Util.String2Decimal(txtApproved.Text);
                }

                if (Convert.ToDecimal(txt_Expense.Text) < amount)
                {
                    txtApproved.Text = txt_Expense.Text;
                    amount = Util.String2Decimal(txtApproved.Text);
                }

                txtApproved.Text = amount.ToString("###0.00");

                TotalApproved = TotalApproved + amount; //Convert.ToInt32(txtAmount.Text);
                Session_TripExpense.Rows[i]["Approved"] = Util.String2Decimal(txtApproved.Text);

                if (Util.String2Int(lbl_tripexpId.Text) == 9998 || Util.String2Int(lbl_tripexpId.Text) == 9999 || Util.String2Int(lbl_tripexpId.Text) == 10000 || Util.String2Int(lbl_tripexpId.Text) == 10001 || Util.String2Int(lbl_tripexpId.Text) == 10002)
                    Session_TripExpense.Rows[i]["TripExpenseHead"] = lbl_ExpenseHead.Text;

                Session_TripExpense.Rows[i]["Remark"] = txtRemarks.Text;
            }

            TotalExpenseApproved = TotalApproved;

            ClosingBalance = OpeningBalance - TotalExpenseApproved;

            if (chk_LastTrip.Checked == false)
            {
                ExpectedAdvance = 2000 - ClosingBalance;
            }
            else
            {
                ExpectedAdvance = ClosingBalance * -1;
            }
        }
        else
        {
            TotalExpenseApproved = 0;
        }
    }

    private void Bind_dg_TripExpense()
    {
        dg_GridTripExpense.DataSource = Session_TripExpense;
        dg_GridTripExpense.DataBind();
    }
    private void Bind_dg_GridAdvanceDetails()
    {
        dg_AdvanceDetails.DataSource = Session_AdvanceDetails;
        dg_AdvanceDetails.DataBind();
    }
    protected void dg_AdvanceDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_AdvanceBranch"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                lbl_ClosingCash = (Label)(e.Item.FindControl("lbl_ClosingCash"));

                BindBranch = Session_BranchDdl;

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataRow DR = null;
                DataTable dt = Session_AdvanceDetails;
                DR = dt.Rows[e.Item.ItemIndex];

                ddl_Branch.SelectedValue = DR["Branch_ID"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();
                lbl_ClosingCash.Text = DR["ClosingCash"].ToString();
            }

        }
    }
    protected void ddl_AdvanceBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Branch = (DropDownList)sender;
        DataGridItem dg_AdvanceDetails = (DataGridItem)ddl_Branch.Parent.Parent;
        ddl_Branch = (DropDownList)(dg_AdvanceDetails.FindControl("ddl_AdvanceBranch"));
        txt_Amount = (TextBox)(dg_AdvanceDetails.FindControl("txt_Amount"));
        lbl_ClosingCash = (Label)(dg_AdvanceDetails.FindControl("lbl_ClosingCash"));

        if (Util.String2Int(ddl_Branch.SelectedValue) > 0)
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeOutParams("@ClosingCash", SqlDbType.Int, 0),
                objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, ddl_Branch.SelectedValue)};
            objDAL.RunProc("EF_Opr_Trip_Expense_Approval_Get_Branch_Closing_Amount", objSqlParam);
            long closingBalance = Convert.ToInt64(objSqlParam[0].Value);
            lbl_ClosingCash.Text = closingBalance.ToString();
        }
        else
            lbl_ClosingCash.Text = "0";
        ScriptManager.SetFocus(txt_Amount);
    }
    protected void dg_AdvanceDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_AdvanceDetails.EditItemIndex = e.Item.ItemIndex;
        dg_AdvanceDetails.ShowFooter = false;
        Bind_dg_GridAdvanceDetails();
        ErrorMsg = "";
    }
    protected void dg_AdvanceDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_AdvanceDetails.EditItemIndex = -1;
        dg_AdvanceDetails.ShowFooter = true;

        Bind_dg_GridAdvanceDetails();
        ErrorMsg = "";
    }
    protected void dg_AdvanceDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_AdvanceDetails.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_AdvanceDetails.AcceptChanges();
            dg_AdvanceDetails.EditItemIndex = -1;
            dg_AdvanceDetails.ShowFooter = true;
            Bind_dg_GridAdvanceDetails();

            if (Session_AdvanceDetails.Compute("Sum(Amount)", "").ToString() != "")
            {
                TotalAdvance = Util.String2Decimal(Session_AdvanceDetails.Compute("Sum(Amount)", "").ToString());
            }
            else
            {
                TotalAdvance = 0;
            }
        }
    }
    private void Insert_Update_dg_AdvanceDetails_Dataset(object source, DataGridCommandEventArgs e)
    {
        ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_AdvanceBranch"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        lbl_ClosingCash = (Label)(e.Item.FindControl("lbl_ClosingCash"));
        if (Allow_To_Add_Update_AdvanceDetails())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_AdvanceDetails.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_AdvanceDetails.Rows[e.Item.ItemIndex];
            }

            dr["Branch_ID"] = ddl_Branch.SelectedValue;
            dr["Branch_Name"] = Util.String2Int(ddl_Branch.SelectedValue) == 0 ? "" : ddl_Branch.SelectedItem.Text;
            dr["Amount"] = txt_Amount.Text.Trim() == string.Empty ? "0" : txt_Amount.Text.Trim();
            dr["ClosingCash"] = lbl_ClosingCash.Text.Trim() == string.Empty ? "0" : lbl_ClosingCash.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_AdvanceDetails.Rows.Add(dr);
            }

            TotalAdvance = Util.String2Decimal(Session_AdvanceDetails.Compute("Sum(Amount)", "").ToString());
        }
    }
    public bool Allow_To_Add_Update_AdvanceDetails()
    {
        Allow_To_Save = false;
        ErrorMsg = "";

        if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Branch";
            ScriptManager.SetFocus(ddl_Branch);
        }
        else if (Util.String2Int(txt_Amount.Text) == 0 || txt_Amount.Text.Trim() == "")
        {
            ErrorMsg = "Please Enter Amount";
            ScriptManager.SetFocus(txt_Amount);
        }

        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }
    protected void dg_AdvanceDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_dg_AdvanceDetails_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_AdvanceDetails.EditItemIndex = -1;
                dg_AdvanceDetails.ShowFooter = true;

                Bind_dg_GridAdvanceDetails();
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate Branch Name ";
        }
    }
    protected void dg_AdvanceDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_AdvanceDetails;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["Branch_Name"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_dg_AdvanceDetails_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_GridAdvanceDetails();
                    dg_AdvanceDetails.EditItemIndex = -1;
                    dg_AdvanceDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate Branch Name";
            }
        }
    }

    #endregion

    #region save

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close();}</script>");
    }
    private bool AllowToSave()
    {
        bool ATS = true;
        lbl_Errors.Text = "Fields with * mark are mandatory";

        CalculateTotalExpenseApproved();

        if (chk_LastTrip.Checked == true & TotalAdvanceToBeGiven > 0 & TotalAdvance != TotalAdvanceToBeGiven)
        {
            lbl_Errors.Text = "Total Advance To Be Given Is Not Equal To Total Advance Allocated";
            ATS = false;
        }
        else if (chk_LastTrip.Checked == true & ExpectedAdvance < 0 & DriverBalanceDeposit != (ExpectedAdvance * -1))
        {
            lbl_Errors.Text = "Driver Is Having Excess Balance. Must Be Deposited In Branch. Enter Amount in Balance Amount";
            ScriptManager.SetFocus(txt_DriverBalance);
            ATS = false;
        }
        else if (chk_LastTrip.Checked == true & ExpectedAdvance < 0 & BalanceDepositBranchID <= 0 && IsAdjustInSalary == false)
        {
            lbl_Errors.Text = "Driver Is Having Excess Balance. Must Be Deposited In Branch Or Adjust Against Salary. Select Branch To Deposite Balance.";
            ScriptManager.SetFocus(DDLBalanceDepositBranch);
            ATS = false;
        }

        else if (chk_LastTrip.Checked == true & ExpectedAdvance < 0 & TotalAdvance != 0)
        {
            lbl_Errors.Text = "Last Trip Option Select. Driver Is Having Excess Balance. Please Remove Advance Allocation";
            ScriptManager.SetFocus(dg_AdvanceDetails);
            ATS = false;
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }

    private Message Save()
    {

        if (chk_LastTrip.Checked == false || ExpectedAdvance > 0)
        {
            txt_DriverBalance.Text = "0";
            SetBalanceDepositBranch("", "0");
            IsAdjustInSalary = false;
        }


        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@TripExpenseAprovalID", SqlDbType.Int, 0,TripExpenseApprovalID),
            objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0,TripExpenseID),
            objDAL.MakeInParams("@OpeningBalance", SqlDbType.Decimal, 0,OpeningBalance),
            objDAL.MakeInParams("@TotalTripExpense", SqlDbType.Decimal, 0,TotalTripExpense),
            objDAL.MakeInParams("@TotalApprovedAmount", SqlDbType.Decimal, 0,TotalExpenseApproved),
            objDAL.MakeInParams("@ClosingBalance", SqlDbType.Decimal, 0,ClosingBalance),
            objDAL.MakeInParams("@ExpectedAdvance", SqlDbType.Decimal, 0,ExpectedAdvance),
            objDAL.MakeInParams("@AdditionalAdvance", SqlDbType.Decimal, 0,AdditionalAdvance),
            objDAL.MakeInParams("@TotalAdvanceToBeGiven", SqlDbType.Decimal, 0,TotalAdvanceToBeGiven),
            objDAL.MakeInParams("@TotalAdvance", SqlDbType.Decimal, 0,TotalAdvance),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@TripExpenseApprovalXML",SqlDbType.Xml,0,TripExpenseXML),
            objDAL.MakeInParams("@AdvanceDetailsXML", SqlDbType.Xml, 0,AdvanceXML),
            objDAL.MakeInParams("@IsApprove", SqlDbType.VarChar, 2, IsApprove),
            objDAL.MakeInParams("@IsLastTrip", SqlDbType.Bit, 0, IsLastTrip),
            objDAL.MakeInParams("@IsVehicleChange", SqlDbType.Bit, 0, IsVehicleChange),
            objDAL.MakeInParams("@BalanceDepositedAmount", SqlDbType.Decimal, 0,DriverBalanceDeposit),
            objDAL.MakeInParams("@BalanceDepositedBranchID", SqlDbType.Int, 0,  BalanceDepositBranchID) ,
            objDAL.MakeInParams("@IsAdjustInSalary", SqlDbType.Bit, 0, IsAdjustInSalary )

        };

        objDAL.RunProc("dbo.EF_Opr_VehicleTripExpenseApproval_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }
    #endregion
}
