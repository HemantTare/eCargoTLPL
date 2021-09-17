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

public partial class Operations_VehicleTripExpense_FrmVehicleTripExpense : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    DropDownList ddl_Branch;
    string Mode = "0";
    PageControls pc = new PageControls();
    TextBox txt_GCNo, txt_Amount, txtExpenseHead, txtRemark;
    DataRow dr;
    LinkButton lbtn_Add_GridToPayVasuli, lbtn_Add_GridPODDetails;
    bool Allow_To_Save;


    DataTable objDT;
    string _flag;

    #region properties
    public string TripNo
    {
        set { lbl_TripNo.Text = value; }
        get { return lbl_TripNo.Text; }
    }
    public string PriviousRoute
    {
        set { lbl_PreviousRoute.Text = value; }
        get { return lbl_PreviousRoute.Text; }
    }
    public string CurrentRoute
    {
        set { txt_CurrentRoute.Text = value; }
        get { return txt_CurrentRoute.Text; }
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
    private DateTime PreviousTripWeightDate
    {
        set { dtpPreviousTripWeightDate.SelectedDate = value; }
        get { return dtpPreviousTripWeightDate.SelectedDate; }
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
        set { dtpTripExpenseDate.SelectedDate = value; }
        get { return dtpTripExpenseDate.SelectedDate; }
    }

    private DateTime FromDate
    {
        set { dtpFromDate.SelectedDate = value; }
        get { return dtpFromDate.SelectedDate; }
    }

    private DateTime ToDate
    {
        set { dtpToDate.SelectedDate = value; }
        get { return dtpToDate.SelectedDate; }
    }

    private int PreviousTripWeight
    {
        set{txt_PreviousTripWeight.Text = Util.Int2String(value);}
        get { return Util.String2Int(txt_PreviousTripWeight.Text); }
    }
    private decimal TotalTripExpense
    {
        set {
            lblTotalTripExpense.Text = Util.Decimal2String(value);
            hdnTotalTripExpense.Value = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdnTotalTripExpense.Value); }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public string ReturnRoute
    {
        set { txt_ReturnRoute.Text = value; }
        get { return txt_ReturnRoute.Text; }
    }
    public bool IsEmptyReturn
    {
        set { chk_EmptyReturn.Checked = value; }
        get { return chk_EmptyReturn.Checked; }
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

    public bool IsEmptyTrip
    {
        set { chk_EmptyTrip.Checked = value; }
        get { return chk_EmptyTrip.Checked; }
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
    public DataTable Session_BranchDdl
    {
        get { return StateManager.GetState<DataTable>("BranchDdl"); }
        set { StateManager.SaveState("BranchDdl", value); }
    }

    public DataTable BindBranch
    {
        set { Set_Common_DDL(ddl_Branch, "Branch_Name", "Branch_ID", value, true); }
    }

    public DataTable Session_TripExpense
    {
        get { return StateManager.GetState<DataTable>("TripExpenseGrid"); }
        set { StateManager.SaveState("TripExpenseGrid", value); }
    }

    public DataTable Session_ToPayVasuli
    {
        get { return StateManager.GetState<DataTable>("VasuliGrid"); }
        set { StateManager.SaveState("VasuliGrid", value); }
    }

    public DataTable Session_PODDetails
    {
        get { return StateManager.GetState<DataTable>("PODGrid"); }
        set { StateManager.SaveState("PODGrid", value); }
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
    public String VasuliXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_ToPayVasuli.Copy());
            _objDs.Tables[0].TableName = "VasuliDetails";
            return _objDs.GetXml().ToLower();
        }
    }
    public String PODDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(Session_PODDetails.Copy());
            _objDs.Tables[0].TableName = "PODDetails";
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
            DDLVehicleSearch.Can_Add_Vehicle = false;
            DDLVehicleSearch.Can_View_Vehicle = true;
            dtpTripExpenseDate.disableForView = false;
            dtpFromDate.Enabled = false;
            TDFromDate.Visible = false;
            dtpToDate.Enabled = false;
            TDToDate.Visible = false;
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

            fillBranchList();

            if (TripExpenseID <= 0)
            {
                dtpTripExpenseDate.SelectedDate = DateTime.Now;
                FromDate = DateTime.Now;
                ToDate = DateTime.Now;
                DDLVehicleSearch.TransactionDate = dtpTripExpenseDate.SelectedDate;
                FillTripExpenseGrid();
            }
            else
            {
                DDLVehicleSearch.SetEnabled = false;
            }
            ReadValues();
        }
        DDLVehicleSearch.DDLVehicleIndexChange += new EventHandler(GetDetails);

        if (IsEmptyTrip == false)
        {
            txt_CurrentRoute.ReadOnly = true;
        }
        else
        {
            txt_CurrentRoute.ReadOnly = false;
        }
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
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate)};

        objDAL.RunProc("EF_Opr_VehicleTripExpense_Vehicle_Details", objSqlParam, ref objDS);
        if (VehicleID > 0)
        {
            TripNo = objSqlParam[0].Value.ToString();
            PriviousRoute = objSqlParam[1].Value.ToString();
            CurrentRoute = objSqlParam[2].Value.ToString();
            CurrentDailyVehicleLoadingPlanID = Util.String2Int(objSqlParam[3].Value.ToString());
            FromDate = Convert.ToDateTime(objSqlParam[4].Value.ToString());
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

            Session_TripExpense = objDS.Tables[0];
            Bind_dg_TripExpense();

            CalculateTotalTripExpense();
        }
    }

    public void GetRouteOnFromToDateChange(bool isfromdate,DateTime date,DateTime FromDate,DateTime ToDate )
    {
        SqlParameter[] objSqlParam ={
        objDAL.MakeOutParams("@VehicleRoute", SqlDbType.VarChar , 50 ),
        objDAL.MakeOutParams("@CurrentDailyVehicleLoadingPlanID", SqlDbType.Int , 0 ),
        objDAL.MakeInParams("@VehicleId", SqlDbType.Int, 0, VehicleID),
        objDAL.MakeInParams("@Date", SqlDbType.DateTime, 0, date),
        objDAL.MakeInParams("@isfromdate", SqlDbType.Bit, 0, isfromdate),
        objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
        objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
        objDAL.MakeInParams("@CleanerID", SqlDbType.Int, 0, CleanerID)};

        objDAL.RunProc("EF_Opr_VehicleTripExpense_Vehicle_Route_Details", objSqlParam, ref objDS);
        if (VehicleID > 0)
        {
            if (isfromdate)
            {
                PriviousRoute = objSqlParam[0].Value.ToString();
            }
            else
            {
                CurrentRoute = objSqlParam[0].Value.ToString();
                CurrentDailyVehicleLoadingPlanID = Util.String2Int(objSqlParam[1].Value.ToString());
            }

            Session_TripExpense = objDS.Tables[0];
            Bind_dg_TripExpense();

            CalculateTotalTripExpense();
        }        
    }

    protected void DDLCleaner_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GetRouteOnFromToDateChange(false, TripExpenseDate, FromDate, ToDate);
        ScriptManager.SetFocus(txt_ReturnRoute);
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

    protected void dtpFromDate_SelectionChanged(object sender, EventArgs e)
    {
        GetRouteOnFromToDateChange(true, TripExpenseDate, FromDate, ToDate);
        ScriptManager.SetFocus(DDLVehicleSearch.txtabc);
    }

    protected void dtpToDate_SelectionChanged(object sender, EventArgs e)
    {
        GetRouteOnFromToDateChange(false, TripExpenseDate, FromDate, ToDate);
        TextBox txtdriver = (TextBox)DDLDriver.FindControl("txtBoxDDLDriver");
        ScriptManager.SetFocus(txtdriver);        
    }

    protected void dg_GridTripExpense_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int TripExpenseHeadID;
            TextBox txtExpenseHead, txtAmount;
            Label lbllblExpenseHead;
            txtExpenseHead = (TextBox)e.Item.FindControl("txtExpenseHead");
            lbllblExpenseHead = (Label)e.Item.FindControl("lblExpenseHead");
            txtAmount = (TextBox)e.Item.FindControl("txtAmount");

            TripExpenseHeadID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "TripExpenseHeadID").ToString());

            if (TripExpenseHeadID == 9998 || TripExpenseHeadID == 9999 || TripExpenseHeadID == 10000 || TripExpenseHeadID == 10001 || TripExpenseHeadID == 10002)
            {
                txtExpenseHead.ReadOnly = false;
                lbllblExpenseHead.Visible = false;
                txtExpenseHead.Visible = true;
            }
            else
            {
                txtExpenseHead.ReadOnly = true;
                //txtExpenseHead.Enabled = false;
                lbllblExpenseHead.Visible = true;
                txtExpenseHead.Visible = false;
            }

            if (TripExpenseHeadID == 2 || TripExpenseHeadID == 3)
            {
                txtAmount.ReadOnly = true;
            }
        }
    }

    private void CalculateTotalTripExpense()
    {
        decimal TotalExpense;

        TotalExpense = 0;
        decimal amount = 0;
        int i;

        TextBox txtAmount;

        if (dg_GridTripExpense.Items.Count > 0 && StateManager.IsValidSession("TripExpenseGrid"))
        {
            for (i = 0; i <= dg_GridTripExpense.Items.Count - 1; i++)
            {

                txtAmount = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtAmount");
                txtExpenseHead = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtExpenseHead");
                Label lbl_tripexpId = (Label)dg_GridTripExpense.Items[i].FindControl("lblTripExpenseHeadID");
                txtRemark = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtRemark");

                if (string.IsNullOrEmpty(txtAmount.Text))
                    amount = 0;
                else
                    amount = Util.String2Decimal(txtAmount.Text);

                TotalExpense = TotalExpense + amount; //Convert.ToInt32(txtAmount.Text);
                Session_TripExpense.Rows[i]["Amount"] = Util.String2Decimal(txtAmount.Text);
                Session_TripExpense.Rows[i]["Remark"] = txtRemark.Text;

                if (Util.String2Int(lbl_tripexpId.Text) == 9998 || Util.String2Int(lbl_tripexpId.Text) == 9999 || Util.String2Int(lbl_tripexpId.Text) == 10000 || Util.String2Int(lbl_tripexpId.Text) == 10001 || Util.String2Int(lbl_tripexpId.Text) == 10002)
                    Session_TripExpense.Rows[i]["TripExpenseHead"] = txtExpenseHead.Text;
            }

            TotalTripExpense = TotalExpense;
        }
        else
        {
            TotalTripExpense = 0;
        }
    }

    private bool grid_validation()
    {
        int i;
        bool ATS = true;
        CalculateTotalTripExpense();
        if (dg_GridTripExpense.Items.Count > 0)
        {
            for (i = 0; i <= dg_GridTripExpense.Items.Count - 1; i++)
            {
                txtExpenseHead = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtExpenseHead");
                txt_Amount = (TextBox)dg_GridTripExpense.Items[i].FindControl("txtAmount");
                Label lbl_tripexpId = (Label)dg_GridTripExpense.Items[i].FindControl("lblTripExpenseHeadID");

                int TripExpenseHeadID = Util.String2Int(lbl_tripexpId.Text);
                decimal amount = (txt_Amount.Text.Trim() == string.Empty ? 0 : Util.String2Decimal(txt_Amount.Text));

                if (TripExpenseHeadID == 9998 || TripExpenseHeadID == 9999 || TripExpenseHeadID == 10000 || TripExpenseHeadID == 10001 || TripExpenseHeadID == 10002)
                    Session_TripExpense.Rows[i]["TripExpenseHead"] = txtExpenseHead.Text;

                if ((TripExpenseHeadID == 9998 || TripExpenseHeadID == 9999 || TripExpenseHeadID == 10000 || TripExpenseHeadID == 10001 || TripExpenseHeadID == 10002) && txtExpenseHead.Text.Trim() != string.Empty && amount <= 0)
                {
                    lbl_Errors.Text = "Please Enter Amount For ExpenseHead " + txtExpenseHead.Text;
                    ScriptManager.SetFocus(txt_Amount);
                    ATS = false;
                    break;
                }
                else if ((TripExpenseHeadID == 9998 || TripExpenseHeadID == 9999 || TripExpenseHeadID == 10000 || TripExpenseHeadID == 10001 || TripExpenseHeadID == 10002) && amount != 0 && txtExpenseHead.Text.Trim() == string.Empty)
                {
                    lbl_Errors.Text = "Please Enter ExpenseHead For Amount " + amount;
                    ScriptManager.SetFocus(txtExpenseHead);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }

    //protected void txtAmount_TextChanged(object sender, EventArgs e)
    //{
    //    CalculateTotalTripExpense();
    //}

    private void fillBranchList()
    {
        objDAL.RunProc("EC_Fill_Branch_List", ref objDS);
        Session_BranchDdl = objDS.Tables[0];
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

        objDAL.RunProc("EF_Opr_Trip_Expense_Sheet_Details", objSqlParam, ref objDS);

        Session_ToPayVasuli = objDS.Tables[2];
        Session_PODDetails = objDS.Tables[3];

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            TripExpenseDate = Convert.ToDateTime(objDR["TripExpenseDate"].ToString());
            FromDate = Convert.ToDateTime(objDR["FromDate"].ToString());

            hdnTripSettledUpTo.Value = String.Format("{0:dd MMM yyyy}", objDR["FromDate"].ToString()); 
            
            ToDate = Convert.ToDateTime(objDR["ToDate"].ToString());
            TripNo = objDR["TripNo"].ToString();
            PriviousRoute = objDR["PreviousRoute"].ToString();
            CurrentRoute = objDR["CurrentRoute"].ToString();
            CurrentDailyVehicleLoadingPlanID = Util.String2Int(objDR["CurrentDailyVehicleLoadingPlanID"].ToString());
            ReturnRoute = objDR["ReturnRoute"].ToString();
            PreviousTripWeight = Util.String2Int(objDR["PreviousTripWeight"].ToString());
            PreviousTripWeightDate = Convert.ToDateTime(objDR["PreviousTripWeightDate2"].ToString());
            TotalTripExpense = Util.String2Decimal(objDR["TotalTripExpense"].ToString());
            Remarks = objDR["Remark"].ToString();
            SetDriver(objDR["Driver_Name"].ToString(), objDR["Driver_ID"].ToString());
            if (Util.String2Int(objDR["Cleaner_ID"].ToString()) == 0)
                SetCleaner("NO CLEANER", objDR["Cleaner_ID"].ToString());
            else
                SetCleaner(objDR["Cleaner_Name"].ToString(), objDR["Cleaner_ID"].ToString());
            VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
            IsEmptyReturn = objDR["IsEmptyReturn"].ToString() != string.Empty ? Util.String2Bool(objDR["IsEmptyReturn"].ToString()): false;
            IsLastTrip = objDR["IsLastTrip"].ToString() != string.Empty ? Util.String2Bool(objDR["IsLastTrip"].ToString()) : false;
            IsVehicleChange = objDR["IsVehicleChange"].ToString() != string.Empty ? Util.String2Bool(objDR["IsVehicleChange"].ToString()) : false;

            IsEmptyTrip = objDR["IsEmptyTrip"].ToString() != string.Empty ? Util.String2Bool(objDR["IsEmptyTrip"].ToString()) : false;

            if (IsEmptyTrip == false )
            {
                txt_CurrentRoute.ReadOnly = true;
            }
            else
            {
                txt_CurrentRoute.ReadOnly = false;
            }
        }

        Bind_dg_GridToPayVasuli();
        Bind_dg_GridPODDetails();

        if (TripExpenseID > 0)
        {
            Session_TripExpense = objDS.Tables[1];
            Bind_dg_TripExpense();
            //dtpTripExpenseDate.Disable = true;
        }
    }

    private void Bind_dg_GridToPayVasuli()
    {
        dg_GridToPayVasuli.DataSource = Session_ToPayVasuli;
        dg_GridToPayVasuli.DataBind();
    }

    private void Bind_dg_GridPODDetails()
    {
        dg_GridPODDetails.DataSource = Session_PODDetails;
        dg_GridPODDetails.DataBind();
    }

    private void Bind_dg_TripExpense()
    {
        dg_GridTripExpense.DataSource = Session_TripExpense;
        dg_GridTripExpense.DataBind();
        UpdatePanel12.Update();
    }

    protected void dg_GridToPayVasuli_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_VasuliBranch"));

                txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));

                lbtn_Add_GridToPayVasuli = (LinkButton)(e.Item.FindControl("lbtn_Add_GridToPayVasuli"));

                BindBranch = Session_BranchDdl;

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataRow DR = null;
                DataTable dt = Session_ToPayVasuli;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                ddl_Branch.SelectedValue = DR["Branch_ID"].ToString();
                txt_GCNo.Text = DR["GC_No"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();
            }
        }
    }

    protected void dg_GridPODDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_PODBranch"));

                txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNoPOD"));

                lbtn_Add_GridPODDetails = (LinkButton)(e.Item.FindControl("lbtn_Add_GridPODDetails"));

                BindBranch = Session_BranchDdl;

            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataRow DR = null;
                DataTable dt = Session_PODDetails;//for grid topic
                DR = dt.Rows[e.Item.ItemIndex];

                ddl_Branch.SelectedValue = DR["Branch_ID"].ToString();
                txt_GCNo.Text = DR["GC_No"].ToString();
            }
        }
    }

    protected void ddl_VasuliBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Branch = (DropDownList)sender;
        DataGridItem dg_GridToPayVasuli = (DataGridItem)ddl_Branch.Parent.Parent;

        ddl_Branch = (DropDownList)(dg_GridToPayVasuli.FindControl("ddl_VasuliBranch"));

        ScriptManager.SetFocus(ddl_Branch);
    }

    protected void ddl_PODBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Branch = (DropDownList)sender;
        DataGridItem dg_GridPODDetails = (DataGridItem)ddl_Branch.Parent.Parent;

        ddl_Branch = (DropDownList)(dg_GridPODDetails.FindControl("ddl_PODBranch"));

        ScriptManager.SetFocus(ddl_Branch);
    }

    protected void dg_GridToPayVasuli_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_GridToPayVasuli.EditItemIndex = e.Item.ItemIndex;
        dg_GridToPayVasuli.ShowFooter = false;
        Bind_dg_GridToPayVasuli();
        ErrorMsg = "";

    }

    protected void dg_GridPODDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_GridPODDetails.EditItemIndex = e.Item.ItemIndex;
        dg_GridPODDetails.ShowFooter = false;
        Bind_dg_GridPODDetails();
        ErrorMsg = "";
    }

    protected void dg_GridToPayVasuli_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_GridToPayVasuli.EditItemIndex = -1;
        dg_GridToPayVasuli.ShowFooter = true;

        Bind_dg_GridToPayVasuli();
        ErrorMsg = "";
    }

    protected void dg_GridPODDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_GridPODDetails.EditItemIndex = -1;
        dg_GridPODDetails.ShowFooter = true;

        Bind_dg_GridPODDetails();
        ErrorMsg = "";
    }

    protected void dg_GridToPayVasuli_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_ToPayVasuli.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_ToPayVasuli.AcceptChanges();
            dg_GridToPayVasuli.EditItemIndex = -1;
            dg_GridToPayVasuli.ShowFooter = true;
            Bind_dg_GridToPayVasuli();
        }
    }

    protected void dg_GridPODDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            dr = Session_PODDetails.Rows[e.Item.ItemIndex];
            dr.Delete();
            Session_PODDetails.AcceptChanges();
            dg_GridPODDetails.EditItemIndex = -1;
            dg_GridPODDetails.ShowFooter = true;
            Bind_dg_GridPODDetails();
        }
    }

    private void Insert_Update_dg_GridToPayVasuli_Dataset(object source, DataGridCommandEventArgs e)
    {
        ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_VasuliBranch"));

        txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNo"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));

        if (Allow_To_Add_Update_ToPayVasuli())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_ToPayVasuli.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_ToPayVasuli.Rows[e.Item.ItemIndex];
            }

            dr["Branch_ID"] = ddl_Branch.SelectedValue;
            dr["Branch_Name"] = Util.String2Int(ddl_Branch.SelectedValue) == 0 ? "" : ddl_Branch.SelectedItem.Text;
            dr["GC_No"] = txt_GCNo.Text.Trim() == string.Empty ? "0" : txt_GCNo.Text.Trim();
            dr["Amount"] = txt_Amount.Text.Trim() == string.Empty ? "0" : txt_Amount.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_ToPayVasuli.Rows.Add(dr);
            }
        }
    }

    private void Insert_Update_dg_GridPODDetails_Dataset(object source, DataGridCommandEventArgs e)
    {
        ddl_Branch = (DropDownList)(e.Item.FindControl("ddl_PODBranch"));

        txt_GCNo = (TextBox)(e.Item.FindControl("txt_GCNoPOD"));

        if (Allow_To_Add_Update_PODDetails())
        {
            if (e.CommandName == "Add")
            {
                dr = Session_PODDetails.NewRow();
            }
            else if (e.CommandName == "Update")
            {
                dr = Session_PODDetails.Rows[e.Item.ItemIndex];
            }

            dr["Branch_ID"] = ddl_Branch.SelectedValue;
            dr["Branch_Name"] = Util.String2Int(ddl_Branch.SelectedValue) == 0 ? "" : ddl_Branch.SelectedItem.Text;
            dr["GC_No"] = txt_GCNo.Text.Trim() == string.Empty ? "0" : txt_GCNo.Text.Trim();

            if (e.CommandName == "Add")
            {
                Session_PODDetails.Rows.Add(dr);
            }
        }
    }

    protected void dg_GridToPayVasuli_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_dg_GridToPayVasuli_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_GridToPayVasuli.EditItemIndex = -1;
                dg_GridToPayVasuli.ShowFooter = true;

                Bind_dg_GridToPayVasuli();
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate LR NO ";
        }
    }

    protected void dg_GridPODDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Insert_Update_dg_GridPODDetails_Dataset(source, e);
            if (Allow_To_Save == true)
            {
                dg_GridPODDetails.EditItemIndex = -1;
                dg_GridPODDetails.ShowFooter = true;

                Bind_dg_GridPODDetails ();
            }
        }
        catch (ConstraintException)
        {
            ErrorMsg = "Duplicate LR NO ";
        }
    }

    public bool Allow_To_Add_Update_ToPayVasuli()
    {
        Allow_To_Save = false;
        ErrorMsg = "";
   
        if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Branch";
            ScriptManager.SetFocus(ddl_Branch);
        }
        else if (txt_GCNo.Text.Trim() == "")
        {
            ErrorMsg = "Please Enter LR No.";
            ScriptManager.SetFocus(txt_GCNo);
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

    public bool Allow_To_Add_Update_PODDetails()
    {
        Allow_To_Save = false;
        ErrorMsg = "";
        
        if (txt_GCNo.Text.Trim() == "")
        {
            ErrorMsg = "Please Enter LR No.";
            ScriptManager.SetFocus(txt_GCNo);
        }
        else if (Util.String2Int(ddl_Branch.SelectedValue) <= 0)
        {
            ErrorMsg = "Please Select Branch";
            ScriptManager.SetFocus(ddl_Branch);
        }
        else
        {
            Allow_To_Save = true;
        }

        return Allow_To_Save;
    }

    protected void dg_GridToPayVasuli_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_ToPayVasuli;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["GC_No"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_dg_GridToPayVasuli_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_GridToPayVasuli();
                    dg_GridToPayVasuli.EditItemIndex = -1;
                    dg_GridToPayVasuli.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate LR No.";
            }
        }
    }

    protected void dg_GridPODDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                objDT = Session_PODDetails;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["GC_No"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_dg_GridPODDetails_Dataset(source, e);
                if (Allow_To_Save == true)
                {
                    Bind_dg_GridPODDetails();
                    dg_GridPODDetails.EditItemIndex = -1;
                    dg_GridPODDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate LR No.";
            }
        }
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
            ScriptManager.SetFocus(dtpTripExpenseDate);
        }

        else if (FromDate > ToDate)
        {
            lbl_Errors.Text = "FromDate Can Not Be Greater Than ToDate";
            ScriptManager.SetFocus(dtpFromDate);
        }

        else if (FromDate < Convert.ToDateTime(hdnTripSettledUpTo.Value))
        {
            lbl_Errors.Text = "From Date Can Not Be Less Than Previous Trip Setttled Date";
            ScriptManager.SetFocus(dtpFromDate);
        }

        else if (ToDate > DateTime.Now.Date)
        {
            lbl_Errors.Text = "ToDate Can Not Be Greater Than Today";
            ScriptManager.SetFocus(dtpFromDate);
        }
        else if (PreviousTripWeightDate  > ToDate )
        {
            lbl_Errors.Text = "Previous Trip Weighing Date Can Not Be Greater Than ToDate";
            ScriptManager.SetFocus(dtpPreviousTripWeightDate);
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
        else if (PreviousTripWeight <= 0)
        {
            lbl_Errors.Text = "Please Enter Previous Trip Gross Weight ";
            ScriptManager.SetFocus(txt_PreviousTripWeight);
        }

        else if (grid_validation() == false)
        {
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
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@TripExpenseID", SqlDbType.Int, 0,TripExpenseID),
            objDAL.MakeInParams("@TripNo", SqlDbType.VarChar, 5, TripNo),
            objDAL.MakeInParams("@TripExpenseDate", SqlDbType.DateTime, 0,TripExpenseDate),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime, 0,FromDate),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime, 0,ToDate),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,VehicleID),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,DriverID),
            objDAL.MakeInParams("@Cleaner_ID", SqlDbType.Int, 0,CleanerID),
            objDAL.MakeInParams("@PreviousRoute", SqlDbType.VarChar, 100, PriviousRoute),
            objDAL.MakeInParams("@ReturnRoute", SqlDbType.VarChar, 100, ReturnRoute),
            objDAL.MakeInParams("@CurrentRoute", SqlDbType.VarChar, 100, CurrentRoute),
            objDAL.MakeInParams("@PreviousTripWeight", SqlDbType.Int, 0, PreviousTripWeight),
            objDAL.MakeInParams("@PreviousTripWeightDate", SqlDbType.DateTime, 0, PreviousTripWeightDate),
            objDAL.MakeInParams("@CurrentDailyVehicleLoadingPlanID", SqlDbType.Int, 0,CurrentDailyVehicleLoadingPlanID ),
            objDAL.MakeInParams("@TotalTripExpense", SqlDbType.Decimal, 0,TotalTripExpense),
            objDAL.MakeInParams("@IsEmptyReturn", SqlDbType.Bit, 0,IsEmptyReturn),
            objDAL.MakeInParams("@IsLastTrip", SqlDbType.Bit, 0,IsLastTrip),
            objDAL.MakeInParams("@IsVehicleChange", SqlDbType.Bit, 0,IsVehicleChange),
            objDAL.MakeInParams("@IsEmptyTrip", SqlDbType.Bit, 0,IsEmptyTrip),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId), 
            objDAL.MakeInParams("@TripExpenseXML",SqlDbType.Xml,0,TripExpenseXML),
            objDAL.MakeInParams("@VasuliXML", SqlDbType.Xml, 0,VasuliXML),
            objDAL.MakeInParams("@PODXML", SqlDbType.Xml, 0,PODDetailsXML)};

        objDAL.RunProc("dbo.EF_Opr_VehicleTripExpense_Save", objSqlParam);

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
