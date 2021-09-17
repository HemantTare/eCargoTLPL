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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author        : Aashish Lad
/// Created On    : 25th October 2008
/// Description   : This is the Page For LHPO Forms First Tab takes LHPO Branches Alert
/// </summary>

public partial class Operations_Outward_WucLHPOAlertsBranches : System.Web.UI.UserControl, ILHPOAlertsBranchesView
{
    #region ClassVariables
    LHPOAlertsBranchesPresenter objLHPOAlertsBranchesPresenter;
    ClassLibrary.UIControl.DDLSearch ddl_Branch;
    LinkButton lbtn_Delete;
    TextBox txt_Description;
    DataSet objDS;
    DataSet objATHDS;
    Label lbl_BranchID;
    TextBox txt_AdvanceAmount, txt_RefNo;
    Controls_WucHierarchyWithID WucHierarchyWithID1;
    TimePicker wuc_SchArrTime;
    ComponentArt.Web.UI.Calendar wuc_SchArrDate;
    private ScriptManager scm_LHPOAlertsBranches;
    bool isValid = false;
    public EventHandler GetLHPODate;
    string Mode = "0";
    Common objComm = new Common();
    #endregion

    #region ControlsValue

    public int BranchID
    {
        get { return Util.String2Int(ddl_Branch.SelectedValue); }
    }
    public decimal TotalAdvance
    {
        set
        {
            txt_TotalAdvance.Text = Util.Decimal2String(value);
            hdn_TotalAdvance.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalAdvance.Value); }
    }
    public decimal TotalAdvanceGrid
    {
        set { hdn_ToalAdvanceGrid.Value = Util.Decimal2String(value); }
        get { return Util.String2Decimal(hdn_ToalAdvanceGrid.Value); }
    }
    public DateTime LHPODate
    {
        set { hdn_LHPODate.Value = String.Format("{0:MMMM dd, yyyy}", value); }
        get { return Convert.ToDateTime(hdn_LHPODate.Value); }
    }
    public int MenuItemId
    {
        get { return Raj.EC.Common.GetMenuItemId(); }
    }
    #endregion

    #region LHPOParametersValues
    private int FromLocationParameterId
    {
        set { hdn_FromLocation_Parameter.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_FromLocation_Parameter.Value); }
    }

    private string AdvancePayAtParameter
    {
        set { hdn_Advance_Pay_At_Parameter.Value = value; }
        get { return hdn_Advance_Pay_At_Parameter.Value; }
    }

    private int ATHGridMaxRowsParameter
    {
        set { hdn_ATH_Grid_Max_Rows.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ATH_Grid_Max_Rows.Value); }
    }

    private bool IsPostBackRequiredOnAdvanceAmt
    {
        set { chk_Is_PostBack_On_Advance_Amt.Checked = value; }
        get { return chk_Is_PostBack_On_Advance_Amt.Checked; }
    }

    private int FromBranchId
    {
        set { hdn_From_BranchId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_From_BranchId.Value); }
    }
    #endregion

    #region ControlsBind

    public DataSet Bind_dg_AlertBranches
    {
        set
        {
            dg_AlertBranches.DataSource = value;
            dg_AlertBranches.DataBind();
        }
    }
    public DataSet Bind_dg_ATHDetails
    {
        set
        {
            dg_ATHDetails.DataSource = value;
            dg_ATHDetails.DataBind();

            if (FromLocationParameterId == 1)
            {
                if (value.Tables[0].Rows.Count == ATHGridMaxRowsParameter)
                {
                    dg_ATHDetails.ShowFooter = false;
                }
                else
                {
                    dg_ATHDetails.ShowFooter = true;
                }
            }
        }
    }
    public DataSet SessionATHDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("ATHDetails"); }
        set { StateManager.SaveState("ATHDetails", value); }
    }
    public DataSet SessionAlertBranchesGrid
    {
        get { return StateManager.GetState<DataSet>("AlertBranches"); }
        set { StateManager.SaveState("AlertBranches", value); }
    }
    public string ATHDetailsXML
    {
        get { return SessionATHDetailsGrid.GetXml().ToLower(); }
    }
    public string AlertBranchesXML
    {
        get { return SessionAlertBranchesGrid.GetXml(); }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = true;

        string TotalAdvance = "";
        isValid = true;
        if (SessionATHDetailsGrid.Tables[0].Rows.Count > 0)
        {
            TotalAdvance = SessionATHDetailsGrid.Tables[0].Compute("SUM(Advance_Amount)", string.Empty).ToString();
            // hdn_ToalAdvanceGrid.Value = TotalAdvance;
        }
        if (TotalAdvance != "")
        {
            if (Convert.ToDouble(TotalAdvance) > Convert.ToDouble(hdn_TotalAdvance.Value))
            {
                errorMessage = "Advance Amount must not be greater than Total Advance";
                isValid = false;
            }
        }
        return isValid;
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { scm_LHPOAlertsBranches = value; }
    }
    private void DisableControlForRectification()
    {
        dg_AlertBranches.Enabled = false;
    }
    #endregion

    #region OtherMethods
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataRow DR = null;
        ddl_Branch = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_Branch");
        txt_Description = (TextBox)e.Item.FindControl("txt_Description");
        lbl_BranchID = (Label)e.Item.FindControl("lbl_BranchID");

        objDS = SessionAlertBranchesGrid;

        if (e.CommandName == "ADD")
        {
            DR = objDS.Tables[0].NewRow();
        }
        if (e.CommandName == "Update")
        {
            DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["BranchID"] = ddl_Branch.SelectedValue;
            DR["BranchName"] = ddl_Branch.SelectedText;
            DR["Description"] = txt_Description.Text;
            if (e.CommandName == "ADD")
            {
                objDS.Tables[0].Rows.Add(DR);
            }
            SessionAlertBranchesGrid = objDS;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Branch";
        }
        else
            isValid = true;

        return isValid;
    }
    public void SetBranchID(string Branch_Name, string BranchID)
    {
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_Id";
        Raj.EC.Common.SetValueToDDLSearch(Branch_Name, BranchID, ddl_Branch);
    }
    private void Insert_Update_Dataset_For_ATHDetails(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataRow DR = null;
        int Flag = 0;
        WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
        wuc_SchArrTime = (TimePicker)e.Item.FindControl("wuc_SchArrTime");
        txt_AdvanceAmount = (TextBox)e.Item.FindControl("txt_AdvanceAmount");
        txt_RefNo = (TextBox)e.Item.FindControl("txt_RefNo");
        wuc_SchArrDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("wuc_SchArrDate");

        objATHDS = SessionATHDetailsGrid;

        if (objATHDS.Tables[0].Rows.Count > 0)
        {
            hdn_LHPOATHPayDetails.Value = objATHDS.Tables[0].Rows[0]["LHPO_ATH_Payable_Details_Id"].ToString();
        }
        else
        {
            hdn_LHPOATHPayDetails.Value = "0";
        }
        GetLHPODate(source, e);
        if (Allow_To_Add_Update_For_ATHDetails() == true)
        {
            if (CheckTotalAdvance(source, e) == false)
            {
                return;
            }
            if (e.CommandName == "ADD")
            {
                DR = objATHDS.Tables[0].NewRow();
            }
            if (e.CommandName == "Update")
            {
                DR = objATHDS.Tables[0].Rows[e.Item.ItemIndex];
            }
            DR["ATH_Payable_Hierarchy_Code"] = WucHierarchyWithID1.HierarchyCode;
            DR["Hierarchy_Name"] = WucHierarchyWithID1.GetHierarchyText;

            if (WucHierarchyWithID1.HierarchyCode == "HO")
            {
                DR["ATH_Payable_Main_ID"] = 0;
                DR["Main_Name"] = "";
            }
            else
            {
                DR["ATH_Payable_Main_ID"] = WucHierarchyWithID1.MainId;
                DR["Main_Name"] = WucHierarchyWithID1.GetLocationText;
            }
            DR["Advance_Amount"] = txt_AdvanceAmount.Text;
            DR["Schedule_Arr_Date"] = wuc_SchArrDate.SelectedDate;
            DR["Schedule_Arr_Time"] = wuc_SchArrTime.getTime();
            DR["Ref_No"] = txt_RefNo.Text;
            if (MenuItemId == 198 && e.CommandName == "ADD")
            {
                hdn_LHPOATHPayDetails.Value = "0";
                DR["LHPO_ATH_Payable_Details_Id"] = hdn_LHPOATHPayDetails.Value;
                DR["ATH_ID"] = 0;
            }

            if (e.CommandName == "ADD")
            {
                objATHDS.Tables[0].Rows.Add(DR);
            }
            SessionATHDetailsGrid = objATHDS;
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    private bool Allow_To_Add_Update_For_ATHDetails()
    {
        if (WucHierarchyWithID1.validateHierarchyWithIdUI(lbl_Errors) == false)
        {
            if (lbl_Errors.Text == "Please Select Hierarchy")
            {
                errorMessage = "Please Select Advance Payable At";
            }
            else if (lbl_Errors.Text == "Please Select Location")
            {
                errorMessage = "Please Select Advance Location";
            }
            isValid = false;
            WucHierarchyWithID1.Focus();
        }
        else if (txt_AdvanceAmount.Text == string.Empty)
        {
            errorMessage = "Please Enter Advance Amount";
            txt_AdvanceAmount.Focus();
        }
        else if (Util.String2Decimal(txt_AdvanceAmount.Text) <= 0)
        {
            errorMessage = "Please Enter Advance Amount Greater Than  Zero";
            txt_AdvanceAmount.Focus();
        }
        else if (wuc_SchArrDate.SelectedDate < LHPODate)
        {
            errorMessage = "Schedule Arrival Date should be greater than or equal to LHPO date";
            wuc_SchArrDate.Focus();
        }
        //else if (Datemanager.IsValidProcessDate("OPR_LHPO", wuc_SchArrDate.SelectedDate) == false)
        //{
        //    errorMessage = "Please Select Valid Sch. Arr. Date"; //GetLocalResourceObject("Msg_SchArrDate").ToString();
        //}
        else
            isValid = true;

        return isValid;
    }
    private bool CheckTotalAdvance(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        string TotalAdvance = "";
        double SumTotalAdvance = 0, UpdateTotalAdvance;
        isValid = true;
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (objATHDS.Tables[0].Rows.Count > 0)
        {
            TotalAdvance = objATHDS.Tables[0].Compute("SUM(Advance_Amount)", string.Empty).ToString();
        }
        if (TotalAdvance != "")
        {
            UpdateTotalAdvance = Convert.ToDouble(TotalAdvance);
            if (e.CommandName == "Update")
            {
                UpdateTotalAdvance = Convert.ToDouble(TotalAdvance) - Convert.ToDouble(objATHDS.Tables[0].Rows[e.Item.ItemIndex]["Advance_Amount"]);
            }
            SumTotalAdvance = Convert.ToDouble(UpdateTotalAdvance) + Convert.ToDouble(txt_AdvanceAmount.Text);
        }
        else
        {
            SumTotalAdvance = Convert.ToDouble(txt_AdvanceAmount.Text);
        }
        hdn_ToalAdvanceGrid.Value = SumTotalAdvance.ToString();
        Upd_Pnl_hdnTotalAdvanceGrid.Update();

        if (SumTotalAdvance > Convert.ToDouble(hdn_TotalAdvance.Value))
        {
            errorMessage = "Advance Amount must not be greater than Total Advance"; //GetLocalResourceObject("Msg_AdvanceAmount").ToString();            
            isValid = false;
        }
        //else if (SumTotalAdvance == Convert.ToDouble(hdn_TotalAdvance.Value))
        //{
        //    errorMessage = "You Cannot add More Than Total Advance";            
        //}
        return isValid;
    }
    public void BindGridAlertBranches()
    {
        dg_AlertBranches.DataSource = SessionAlertBranchesGrid;
        dg_AlertBranches.DataBind();
    }
    public void BindGridATHDetails()
    {
        dg_ATHDetails.DataSource = SessionATHDetailsGrid;
        dg_ATHDetails.DataBind();

        if (FromLocationParameterId == 1)
        {
            if (SessionATHDetailsGrid.Tables[0].Rows.Count == ATHGridMaxRowsParameter)
            {
                dg_ATHDetails.ShowFooter = false;
            }
            else
            {
                dg_ATHDetails.ShowFooter = true;
            }
        }
    }
    private void CheckLHPOTypeID()
    {
        int LHPOTypeID;
        dg_AlertBranches.Enabled = true;
        dg_ATHDetails.Enabled = true;
        if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null)
        {
            LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
            if (LHPOTypeID == 2)
            {
                dg_AlertBranches.Enabled = false;
                dg_ATHDetails.Enabled = false;
            }
            else
            {
                dg_AlertBranches.Enabled = true;
                dg_ATHDetails.Enabled = true;
            }
        }
    }
    private void AssignTotalAdvanceGrid()
    {
        string TotalAdvance = "";
        if (SessionATHDetailsGrid.Tables[0].Rows.Count > 0)
        {
            TotalAdvance = SessionATHDetailsGrid.Tables[0].Compute("SUM(Advance_Amount)", string.Empty).ToString();
            hdn_ToalAdvanceGrid.Value = TotalAdvance;
        }
        else
        {
            hdn_ToalAdvanceGrid.Value = "0";
        }
        Upd_Pnl_hdnTotalAdvanceGrid.Update();
    }
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        Fill_LHPO_Parameters();

        objLHPOAlertsBranchesPresenter = new LHPOAlertsBranchesPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                CheckLHPOTypeID();
                AssignTotalAdvanceGrid();
            }
        }

        if (MenuItemId == 198)
        {
            DisableControlForRectification();
        }

        txt_TotalAdvance.Text = hdn_TotalAdvance.Value;
        Upd_Pnl_ATHDetails.Update();
        Upd_Pnl_dg_AlertBranches.Update();
        Upd_Pnl_hdnTotalAdvanceGrid.Update();
    }

    private void Fill_LHPO_Parameters()
    {
        DataSet DsLHPOParameters = new DataSet();
        DsLHPOParameters = objComm.Get_Values_Where("EC_Master_Company_LHPO_Parameters", "*", "", "Balance_Payable_At", false);

        FromLocationParameterId = Util.String2Int(DsLHPOParameters.Tables[0].Rows[0]["From_Loc_To_Be_Filled_ID"].ToString());
        AdvancePayAtParameter = DsLHPOParameters.Tables[0].Rows[0]["Advance_Payable_At"].ToString();
        ATHGridMaxRowsParameter = Util.String2Int(DsLHPOParameters.Tables[0].Rows[0]["ATH_Grid_Max_Rows"].ToString());
        IsPostBackRequiredOnAdvanceAmt = Convert.ToBoolean(DsLHPOParameters.Tables[0].Rows[0]["Is_PostBack_Required_On_Advance_Amount"].ToString());
    }
    public void FillGrid(object o, EventArgs e)
    {
        objLHPOAlertsBranchesPresenter.FillGridOnAttachedLHPONoChanged();
        Upd_Pnl_ATHDetails.Update();
        Upd_Pnl_dg_AlertBranches.Update();
        Upd_Pnl_hdnTotalAdvanceGrid.Update();
        CheckLHPOTypeID();
    }
    public void BindATHGridOnFromLocationSelection(object o, EventArgs e)
    {
        //Fill_LHPO_Parameters();

        FromBranchId = Util.String2Int(o.ToString());
        Bind_dg_ATHDetails = SessionATHDetailsGrid;
    }
    public void AssignTotalAdvance(object sender, EventArgs e)
    {
        txt_TotalAdvance.Text = sender.ToString();
        hdn_TotalAdvance.Value = sender.ToString();
        hdn_ToalAdvanceGrid.Value = sender.ToString();
        Upd_Pnl_txt_TotalAdvance.Update();
        Upd_Pnl_hdnTotalAdvanceGrid.Update();
    }
    protected void dg_AlertBranches_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_AlertBranches.EditItemIndex = -1;
        dg_AlertBranches.ShowFooter = true;
        BindGridAlertBranches();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_AlertBranches_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionAlertBranchesGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionAlertBranchesGrid = objDS;
            dg_AlertBranches.EditItemIndex = -1;
            dg_AlertBranches.ShowFooter = true;
            BindGridAlertBranches();
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_AlertBranches_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_AlertBranches.EditItemIndex = e.Item.ItemIndex;
        dg_AlertBranches.ShowFooter = false;
        BindGridAlertBranches();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_AlertBranches_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_AlertBranches.EditItemIndex = -1;
                dg_AlertBranches.ShowFooter = true;
                BindGridAlertBranches();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Branch";
            return;
        }
    }
    protected void dg_AlertBranches_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Branch = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_Branch");
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = SessionAlertBranchesGrid;
                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];

                SetBranchID(DR["BranchName"].ToString(), DR["BranchID"].ToString());
            }
        }
    }
    protected void dg_AlertBranches_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            try
            {
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindGridAlertBranches();
                    dg_AlertBranches.EditItemIndex = -1;
                    dg_AlertBranches.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Branch";
                return;
            }
        }
    }
    protected void dg_ATHDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                WucHierarchyWithID1 = (Controls_WucHierarchyWithID)e.Item.FindControl("WucHierarchyWithID1");
                wuc_SchArrTime = (TimePicker)e.Item.FindControl("wuc_SchArrTime");
                txt_AdvanceAmount = (TextBox)e.Item.FindControl("txt_AdvanceAmount");
                txt_RefNo = (TextBox)e.Item.FindControl("txt_RefNo");
                wuc_SchArrDate = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("wuc_SchArrDate");

                WucHierarchyWithID1.Allow_All_Hierarchy = true;
                WucHierarchyWithID1.Set_Default_Values(sender, e);

                if (FromLocationParameterId == 1)
                {
                    if (FromBranchId > 0 && SessionATHDetailsGrid.Tables[0].Rows.Count == 0)
                    {
                        WucHierarchyWithID1.HierarchyCode = AdvancePayAtParameter;
                        WucHierarchyWithID1.MainId = FromBranchId;
                    }
                    else if (FromBranchId > 0 && SessionATHDetailsGrid.Tables[0].Rows.Count == 1)
                    { }
                    else
                    {
                        WucHierarchyWithID1.Set_Default_Values(sender, e);
                    }

                    WucHierarchyWithID1.SetEnabled = false;
                }

                if (IsPostBackRequiredOnAdvanceAmt == true)
                {
                    if (TotalAdvance > 0 && SessionATHDetailsGrid.Tables[0].Rows.Count == 0)
                        txt_AdvanceAmount.Text = TotalAdvance.ToString();
                    else
                        txt_AdvanceAmount.Text = "0";

                    txt_AdvanceAmount.Enabled = false;
                }
                else
                {
                    txt_AdvanceAmount.Enabled = true;
                }

                WucHierarchyWithID1.Set_TD_Caption_Visible = false;
                wuc_SchArrDate.SelectedDate = DateTime.Now.Date;
                wuc_SchArrTime.setFormat("24");
                wuc_SchArrTime.setTime(DateTime.Now.ToShortTimeString());
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objATHDS = SessionATHDetailsGrid;

                DataRow DR = objATHDS.Tables[0].Rows[e.Item.ItemIndex];
                Upd_Pnl_ATHDetails.Update();
                string Hierarchy_Code = "0";
                Hierarchy_Code = DR["ATH_Payable_Hierarchy_Code"].ToString();

                WucHierarchyWithID1.HierarchyCode = Hierarchy_Code.ToUpper();
                WucHierarchyWithID1.MainId = Util.String2Int(DR["ATH_Payable_Main_ID"].ToString());
                txt_AdvanceAmount.Text = DR["Advance_Amount"].ToString();
                txt_RefNo.Text = DR["Ref_No"].ToString();
                wuc_SchArrTime.setTime(DR["Schedule_Arr_Time"].ToString());
                wuc_SchArrDate.SelectedDate = Convert.ToDateTime(DR["Schedule_Arr_Date"]);
                hdn_LHPOATHPayDetails.Value = DR["LHPO_ATH_Payable_Details_ID"].ToString();
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (MenuItemId == 198)
                {
                    if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "ATH_ID").ToString()) > 0)
                    {
                        e.Item.Enabled = false;
                    }
                }
            }
            Upd_Pnl_ATHDetails.Update();
        }
    }
    protected void dg_ATHDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_ATHDetails.EditItemIndex = -1;
        dg_ATHDetails.ShowFooter = true;
        BindGridATHDetails();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_ATHDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objATHDS = SessionATHDetailsGrid;
            objATHDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objATHDS.Tables[0].AcceptChanges();
            SessionATHDetailsGrid = objATHDS;
            dg_ATHDetails.EditItemIndex = -1;
            dg_ATHDetails.ShowFooter = true;
            BindGridATHDetails();
            AssignTotalAdvanceGrid();
        }
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_ATHDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_ATHDetails.EditItemIndex = e.Item.ItemIndex;
        dg_ATHDetails.ShowFooter = false;
        BindGridATHDetails();
        string script = "<script language='javascript'> " + "EnabledDisabledControlOnFreightType(0);CalculateTruckHireCharge(0);" + "</script>";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "CallJS", script, false);
    }
    protected void dg_ATHDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            Insert_Update_Dataset_For_ATHDetails(source, e);
            if (isValid == true)
            {
                dg_ATHDetails.EditItemIndex = -1;
                dg_ATHDetails.ShowFooter = true;
                BindGridATHDetails();
            }
        }
        catch (ConstraintException)
        {
            lbl_Errors.Visible = true;
            errorMessage = "Duplicate Advance Location";
            return;
        }
    }
    protected void dg_ATHDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.CommandName == "ADD")
        {
            try
            {
                Insert_Update_Dataset_For_ATHDetails(source, e);
                if (isValid == true)
                {
                    dg_ATHDetails.EditItemIndex = -1;
                    dg_ATHDetails.ShowFooter = true;
                    BindGridATHDetails();
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Advance Location";
                return;
            }
        }
    }
    #endregion
}
