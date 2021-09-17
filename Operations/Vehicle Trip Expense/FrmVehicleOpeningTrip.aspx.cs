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
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

public partial class Operations_VehicleTripExpense_FrmVehicleOpeningTrip : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList ddl_Branch;
    string Mode = "0";
    PageControls pc = new PageControls();
    DataRow dr;

    TextBox txt_Amount;
    Label lbl_ClosingCash;

    bool Allow_To_Save;


    DataTable objDT;
    string _flag;

    #region properties
    public string TripNo
    {
        set { lbl_TripNo.Text = value; }
        get { return lbl_TripNo.Text; }
    }

    public string CurrentRoute
    {
        set { lbl_CurrentRoute.Text = value; }
        get { return lbl_CurrentRoute.Text; }
    }

    private int CurrentDailyVehicleLoadingPlanID
    {
        set
        {
            hdn_CurrentDailyVehicleLoadingPlanID.Value = Util.Int2String (value);
        }
        get { return Util.String2Int(hdn_CurrentDailyVehicleLoadingPlanID.Value); }
    }

    private int VehicleID
    {
        set { DDLVehicleSearch.VehicleID = value; }
        get { return DDLVehicleSearch.VehicleID; }
    }
    
    private int TripExpenseID
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

    private DateTime TripExpenseDate
    {
        set { dtpFromDate.SelectedDate = value; }
        get { return dtpFromDate.SelectedDate; }
    }

    
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public int CleanerID
    {
        get { return Util.String2Int(DDLCleaner.SelectedValue); }
    }

    public int DriverID
    {
        get { return Util.String2Int(DDLDriver.SelectedValue); }
    }
    
    public void SetDriver(string text, string value)
    {
        DDLDriver.DataTextField = "Driver_Name";
        DDLDriver.DataValueField = "Driver_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDLDriver);
    }
    
    public void SetCleaner(string text, string value)
    {
        DDLCleaner.DataTextField = "Cleaner_Name";
        DDLCleaner.DataValueField = "Cleaner_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, DDLCleaner);
    }

    private string ErrorMsg
    {
        set { lbl_Errors.Text = value; }
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

    #endregion

    #region events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Util.DecryptToString(Request.QueryString["Mode"].ToString()) == "4")
        {
            DDLVehicleSearch.Can_Add_Vehicle = false;
            DDLVehicleSearch.Can_View_Vehicle = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        btn_Save_Exit.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));

        
        if (!IsPostBack)
        {

            TripExpenseID = Util.DecryptToInt(Request.QueryString["Id"]);
            DDLVehicleSearch.SetAutoPostBack = true;


            if (TripExpenseID <= 0)
            {
                TripExpenseDate = DateTime.Now;

                DDLVehicleSearch.TransactionDate = dtpFromDate.SelectedDate;
            }
            else
            {
                DDLVehicleSearch.SetEnabled = false;
            }

            clearsessions();
            fillBranchList();
            ReadValues();
        }
        DDLVehicleSearch.DDLVehicleIndexChange += new EventHandler(GetDetails);
    }

    protected void dtpFromDate_SelectionChanged(object sender, EventArgs e)
    {
        GetRouteOnFromToDateChange(false, TripExpenseDate, TripExpenseDate , TripExpenseDate );
    }


    private void fillBranchList()
    {
        objDAL.RunProc("EC_Fill_Branch_List", ref objDS);
        Session_BranchDdl = objDS.Tables[0];
    }


    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0, TripExpenseID) };

        objDAL.RunProc("EF_Opr_Opening_Trip_Expense_Sheet_Details", objSqlParam, ref objDS);

        Session_AdvanceDetails  = objDS.Tables[1];
        
        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            TripExpenseDate = Convert.ToDateTime(objDR["TripExpenseDate"].ToString());

            hdnTripSettledUpTo.Value = String.Format("{0:dd MMM yyyy}", objDR["FromDate"].ToString());

            TripNo = objDR["TripNo"].ToString();
            CurrentRoute = objDR["CurrentRoute"].ToString();
            CurrentDailyVehicleLoadingPlanID = Util.String2Int(objDR["CurrentDailyVehicleLoadingPlanID"].ToString());

            Remarks = objDR["Remark"].ToString();
            SetDriver(objDR["Driver_Name"].ToString(), objDR["Driver_ID"].ToString());
            if (Util.String2Int(objDR["Cleaner_ID"].ToString()) == 0)
                SetCleaner("NO CLEANER", objDR["Cleaner_ID"].ToString());
            else
                SetCleaner(objDR["Cleaner_Name"].ToString(), objDR["Cleaner_ID"].ToString());
            VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());

        }

        Bind_dg_GridAdvanceDetails();

        if (TripExpenseID > 0)
        {
            Session_AdvanceDetails = objDS.Tables[1];
            Bind_dg_GridAdvanceDetails();
        }

        if (objDS.Tables[2].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[2].Rows[0];

            TotalAdvance = Util.String2Decimal(objDR["TotalAdvance"].ToString());
        }
    }

    private void clearsessions()
    {
        Session_AdvanceDetails = null;
    }

    public void GetDetails(object sender, EventArgs e)
    {
        SqlParameter[] objSqlParam ={
            objDAL.MakeOutParams("@TripNo", SqlDbType.VarChar , 20 ),
            objDAL.MakeOutParams("@VehiclePreRoute", SqlDbType.VarChar , 50 ),                                                          
            objDAL.MakeOutParams("@VehicleCurrentRoute", SqlDbType.VarChar , 50 ),
            objDAL.MakeOutParams("@CurrentDailyVehicleLoadingPlanID", SqlDbType.Int , 0 ),
            objDAL.MakeOutParams("@FromDate", SqlDbType.DateTime,0),

            objDAL.MakeOutParams("@DriverID", SqlDbType.Int , 0 ),
            objDAL.MakeOutParams("@DriverName", SqlDbType.VarChar , 50 ),
            objDAL.MakeOutParams("@CleanerID", SqlDbType.Int , 0 ),
            objDAL.MakeOutParams("@CleanerName", SqlDbType.VarChar , 50 ),

            objDAL.MakeInParams("@VehicleId", SqlDbType.Int, 0, VehicleID),
            objDAL.MakeInParams("@TripExpenseDate", SqlDbType.DateTime, 0, TripExpenseDate),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, TripExpenseDate)};

        objDAL.RunProc("EF_Opr_VehicleTripExpense_Vehicle_Details", objSqlParam, ref objDS);

        if (VehicleID > 0)
        {
            TripNo = objSqlParam[0].Value.ToString();
            CurrentRoute = objSqlParam[2].Value.ToString();
            CurrentDailyVehicleLoadingPlanID = Util.String2Int(objSqlParam[3].Value.ToString());
            hdnTripSettledUpTo.Value = String.Format("{0:dd MMM yyyy}", objSqlParam[4].Value.ToString()); 

            if (objSqlParam[5].Value.ToString() != "0")
            {
                DDLDriver.DataValueField = objSqlParam[5].Value.ToString();
                DDLDriver.DataTextField = objSqlParam[6].Value.ToString();
                Raj.EC.Common.SetValueToDDLSearch(objSqlParam[6].Value.ToString(), objSqlParam[5].Value.ToString(), DDLDriver);
            }

            if (objSqlParam[7].Value.ToString() != "0")
            {
                DDLCleaner.DataValueField = objSqlParam[7].Value.ToString();
                DDLCleaner.DataTextField = objSqlParam[8].Value.ToString();
                Raj.EC.Common.SetValueToDDLSearch(objSqlParam[8].Value.ToString(), objSqlParam[7].Value.ToString(), DDLCleaner);
            }
            else
            {
                DDLCleaner.DataValueField = objSqlParam[7].Value.ToString();
                DDLCleaner.DataTextField = objSqlParam[8].Value.ToString();
                Raj.EC.Common.SetValueToDDLSearch(objSqlParam[8].Value.ToString(), objSqlParam[7].Value.ToString(), DDLCleaner);
            }
        }
    }

    public void GetRouteOnFromToDateChange(bool isfromdate,DateTime date,DateTime FromDate,DateTime ToDate )
    {
        SqlParameter[] objSqlParam ={
        objDAL.MakeOutParams("@VehicleRoute", SqlDbType.VarChar , 50 ),
        objDAL.MakeOutParams("@CurrentDailyVehicleLoadingPlanID", SqlDbType.Int , 0 ),
        objDAL.MakeInParams("@VehicleId", SqlDbType.Int, 0, VehicleID),
        objDAL.MakeInParams("@Date", SqlDbType.DateTime, 0, date),
        objDAL.MakeInParams("@isfromdate", SqlDbType.Bit, 0, false),
        objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
        objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
        objDAL.MakeInParams("@CleanerID", SqlDbType.Int, 0, CleanerID)};

        objDAL.RunProc("EF_Opr_VehicleTripExpense_Vehicle_Route_Details", objSqlParam, ref objDS);
        if (VehicleID > 0)
        {
            if (isfromdate == false)
            {
                CurrentRoute = objSqlParam[0].Value.ToString();
                CurrentDailyVehicleLoadingPlanID = Util.String2Int(objSqlParam[1].Value.ToString());
            }

        }        
    }

    //protected void DDLCleaner_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetRouteOnFromToDateChange(false, TripExpenseDate, TripExpenseDate, TripExpenseDate);
    //}

    private void Set_Common_DDL(DropDownList DDl, string TextField, string ValueField, DataTable DT, bool Is_ZeroInex)
    {
        DDl.DataSource = DT;
        DDl.DataTextField = TextField;
        DDl.DataValueField = ValueField;
        DDl.DataBind();
        if (Is_ZeroInex)
            DDl.Items.Insert(0, new ListItem("Select One", "0"));
    }

    #endregion

    #region save
  
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {

            _flag = "SaveAndExit";

            Save();
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close();}</script>");
    }
    private bool AllowToSave()
    {
        bool ATS = false;
        lbl_Errors.Text = "Fields with * mark are mandatory";
        if (VehicleID <= 0)
        {
            lbl_Errors.Text = "Please Select Vehicle No";
            ScriptManager.SetFocus(DDLVehicleSearch);
        }
        else if (TripExpenseDate  > DateTime.Now.Date)
        {
            lbl_Errors.Text = "Trip Expense Date Can Not Be Greater Than Today";
            ScriptManager.SetFocus(dtpFromDate);
        }

        else if (dtpFromDate.SelectedDate < Convert.ToDateTime(hdnTripSettledUpTo.Value))
        {
            lbl_Errors.Text = "From Date Can Not Be Less Than Previous Trip Setttled Date";
            ScriptManager.SetFocus(dtpFromDate);
        }

        else if (DriverID <= 0)
        {
            lbl_Errors.Text = "Please Select Driver ";
            ScriptManager.SetFocus(DDLDriver);
        }
        else if (CleanerID < 0)
        {
            lbl_Errors.Text = "Please Select Clearner ";
            ScriptManager.SetFocus(DDLCleaner);
        }
        else if (TotalAdvance <= 0)
        {
            lbl_Errors.Text = "Please Enter Advance Details ";
        
        }
        else
        {
            ATS = true;
        }

        return ATS;
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

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0,TripExpenseID),
            objDAL.MakeInParams("@TripNo", SqlDbType.VarChar, 5, TripNo),
            objDAL.MakeInParams("@TripExpenseDate", SqlDbType.DateTime, 0,TripExpenseDate ),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0,TripExpenseDate),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0,TripExpenseDate),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,VehicleID),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,DriverID),
            objDAL.MakeInParams("@Cleaner_ID", SqlDbType.Int, 0,CleanerID),
            objDAL.MakeInParams("@PreviousRoute", SqlDbType.VarChar, 100,""),
            objDAL.MakeInParams("@ReturnRoute", SqlDbType.VarChar, 50, ""),
            objDAL.MakeInParams("@CurrentRoute", SqlDbType.VarChar, 100, CurrentRoute),
            objDAL.MakeInParams("@PreviousTripWeight", SqlDbType.Int, 0, 0),
            objDAL.MakeInParams("@PreviousTripWeightDate", SqlDbType.DateTime, 0, TripExpenseDate),
            objDAL.MakeInParams("@CurrentDailyVehicleLoadingPlanID", SqlDbType.Int, 0,CurrentDailyVehicleLoadingPlanID ),
            objDAL.MakeInParams("@TotalTripExpense", SqlDbType.Decimal, 0,0),
            objDAL.MakeInParams("@TotalAdvance", SqlDbType.Decimal, 0,TotalAdvance),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@AdvanceDetailsXML", SqlDbType.Xml, 0,AdvanceXML)};

        objDAL.RunProc("dbo.EF_Opr_VehicleOpeningTrip_Save", objSqlParam);

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
