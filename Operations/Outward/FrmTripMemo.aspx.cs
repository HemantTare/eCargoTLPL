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

public partial class Operations_Outward_FrmTripMemo : ClassLibraryMVP.UI.Page//System.Web.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    string _flag;

    #region properties
    private int VehicleID
    {
        set { DDLVehicleSearch.VehicleID = value; }
        get { return DDLVehicleSearch.VehicleID; }
    }
    private int TripMemoID
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
    private DateTime TripMemoDate
    {
        set { dtpTripMemoDate.SelectedDate = value; }
        get { return dtpTripMemoDate.SelectedDate; }
    }
    private DateTime AttachedLHPODate
    {
        set { hdnAttachedLHPODate.Value = String.Format("{0:MMM/dd/yyyy}", value); }
        get
        {
            if (hdnAttachedLHPODate.Value != "")
            {
                return Convert.ToDateTime(hdnAttachedLHPODate.Value);
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
    private int BranchID
    {
        get { return UserManager.getUserParam().MainId; }
    }
    private DataSet SessionMemoDetails
    {
        get { return StateManager.GetState<DataSet>("MemoDetails"); }
        set { StateManager.SaveState("MemoDetails", value); }
    }
    public string MemoGridXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            int i;
            CheckBox Chk_Attach;
            DataRow _dr;
            _objDs.Tables.Add(SessionMemoDetails.Tables[0].Clone());
            for (i = 0; i < dgMemoDetails.Items.Count; i++)
            {
                Chk_Attach = (CheckBox)dgMemoDetails.Items[i].FindControl("Chk_Attach");
                if (Chk_Attach.Checked == true)
                {
                    _dr = _objDs.Tables[0].NewRow();
                    _dr = SessionMemoDetails.Tables[0].Rows[i];
                    _objDs.Tables[0].ImportRow(_dr);
                }
            }
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
            lnkAddDriver.Enabled = false;
            lnkAddDriver.Enabled = false;
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        DDLVehicleSearch.DDLVehicleIndexChange += new EventHandler(VehicleIndexChange);

        if (!IsPostBack)
        {

            TripMemoID = Util.DecryptToInt(Request.QueryString["Id"]);
            DDLVehicleSearch.SetAutoPostBack = true;
            SetLinks();
            if (TripMemoID <= 0)
            {
                dtpTripMemoDate.SelectedDate = DateTime.Now;
                DDLVehicleSearch.TransactionDate = dtpTripMemoDate.SelectedDate;
            }
            else
            {
                dtpTripMemoDate.Enabled = false;
                td_cal.Visible = false;
                DDLVehicleSearch.SetEnabled = false;
                PopulateTripMemoValues();
                EnableDisableOnTripMemoType();
            }

            FillMemoGrid();

            //Added On 23 Aug 2018
            string Crypt = "", _LoadingDate;
            int _VehicleId, _MenuItemId, hdn_LoginBranch_Id;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["VehicleId"];

            if (Crypt != null)
            {
                _VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                Crypt = System.Web.HttpContext.Current.Request.QueryString["Menu_Item_Id"];
                _MenuItemId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                Crypt = System.Web.HttpContext.Current.Request.QueryString["LoadingDate"];
                _LoadingDate = ClassLibraryMVP.Util.DecryptToString(Crypt);

                if (_VehicleId > 0)
                {
                    HiddenField MenuItemID = (HiddenField)DDLVehicleSearch.FindControl("hdnMenuItemID");
                    HiddenField LoginBranchId = (HiddenField)DDLVehicleSearch.FindControl("hdn_LoginBranch_Id");
                    HiddenField TransactionDate = (HiddenField)DDLVehicleSearch.FindControl("hdnTransactionDate");

                    MenuItemID.Value = Util.Int2String(_MenuItemId);
                    LoginBranchId.Value = Util.Int2String(UserManager.getUserParam().MainId);
                    TransactionDate.Value = _LoadingDate;

                    dtpTripMemoDate.SelectedDate = Convert.ToDateTime(_LoadingDate);

                    VehicleID = _VehicleId;
                    DDLVehicleSearch.VehicleID = VehicleID;
                    VehicleIndexChange(this, e);
                }
            }
            // End 23 Aug 2018
        }

        lstDriver.Style.Add("visibility", "hidden");
    }
    private void VehicleIndexChange(object sender, EventArgs e)
    {
        if (VehicleID > 0)
        {
            hdnDVLPID.Value = DDLVehicleSearch.GetVehicleParameter("DVLPID");
            hdnAUSID.Value = DDLVehicleSearch.GetVehicleParameter("AUSID");
            hdnMainLHPOID.Value = DDLVehicleSearch.GetVehicleParameter("MainLHPOID");
            string DVLPFromBranchID = DDLVehicleSearch.GetVehicleParameter("FromBranchID");

            GetDVLPAttachedBranches();

            objDS = (DataSet)Session["DVLPAB"];
            DataRow[] result = objDS.Tables[0].Select("Branch_Id=" + BranchID.ToString());

            if (BranchID == Util.String2Int(DVLPFromBranchID))
            {
                ddlTripMemoType.SelectedValue = "1";
                lblTripMemoNo.Text = ObjCommon.Get_Next_Number();
                AttachedLHPODate = DateTime.Now;
            }
            else if (result.Length<=0)
            {
                lbl_Errors.Text = "Branch " + UserManager.getUserParam().MainName + " cannot make attached trip with the selected vehicle. Please select different vehicle";
                EnableDisableSaveButtons(false);
                return;
            }
            else
            {
                ddlTripMemoType.SelectedValue = "2";
                lblTripMemoNo.Text = DDLVehicleSearch.GetVehicleParameter("LHPONumber");
                AttachedLHPODate = Convert.ToDateTime(DDLVehicleSearch.GetVehicleParameter("LHPO_Date").ToString());
            }

            EnableDisableSaveButtons(true);

            EnableDisableOnTripMemoType();

            hdnDriverId.Value = DDLVehicleSearch.GetVehicleParameter("Driver_ID");
            txtDriver.Text = DDLVehicleSearch.GetVehicleParameter("Driver_Name");
        }

        FillMemoGrid();
    }
    protected void DateChange(object sender, EventArgs e)
    {
        DDLVehicleSearch.TransactionDate = TripMemoDate;
        DDLVehicleSearch.RetainVehicle = true;
        DDLVehicleSearch.callVehicleSearch();
    }
    #endregion

    #region save
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            _flag = "SaveAndNew";
            Save();
        }
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            _flag = "SaveAndExit";
            Save();
        }
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            _flag = "SaveAndPrint";
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
            lbl_Errors.Text = "Please select Vehicle No";
        }
        else if (ddlTripMemoType.SelectedValue == "2" && hdnMainLHPOID.Value == "0")
        {
            lbl_Errors.Text = "You can't make attached trip without parent trip";
        }
        else if (Util.String2Int(hdnDriverId.Value) <= 0)
        {
            lbl_Errors.Text = "Please select Driver 1";
        }
        else if (Util.String2Int(hdnSelectedMemoCount.Value) <= 0)
        {
            lbl_Errors.Text = "Please select at least one manifest";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    private Message Save()
    {
        Message objMessage = new Message();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
        objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
        objDAL.MakeInParams("@DVLP_ID",SqlDbType.Int,0, Util.String2Int(hdnDVLPID.Value)),
        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,1),
        objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
        objDAL.MakeInParams("@LHPO_Type_ID",SqlDbType.Int,0,Util.String2Int(ddlTripMemoType.SelectedValue)),
        objDAL.MakeInParams("@LHPO_Branch_ID",SqlDbType.Int,0,UserManager.getUserParam().MainId),
        objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Util.String2Int(Common.GetMenuItemId().ToString())),                                            
        objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0, TripMemoID),
        objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,0,UserManager.getUserParam().HierarchyCode),                                            
        objDAL.MakeInParams("@LHPO_Date",SqlDbType.DateTime,0,dtpTripMemoDate.SelectedDate),
        objDAL.MakeInParams("@Main_LHPO_ID",SqlDbType.Int,0,Util.String2Int(hdnMainLHPOID.Value) ),
        objDAL.MakeInParams("@LHPO_No",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@LHPO_No_For_Print",SqlDbType.VarChar,20,lblTripMemoNo.Text),
        objDAL.MakeInParams("@Manual_Ref_No",SqlDbType.VarChar,20,""),
        objDAL.MakeInParams("@Vehicle_Category_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,DDLVehicleSearch.VehicleID),
        objDAL.MakeInParams("@From_Location_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@To_Location_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Broker_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Is_TDS_Certificate_Broker",SqlDbType.Bit,0,false),
        objDAL.MakeInParams("@Total_No_Of_Memo",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Total_Articles",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Total_No_Of_GCs",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Driver1_Id",SqlDbType.Int,0,Util.String2Int(hdnDriverId.Value)),
        objDAL.MakeInParams("@Driver2_Id",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Cleaner_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@LHPO_Freight_Basis_ID",SqlDbType.Int,0,2),
        objDAL.MakeInParams("@Min_Wt_Guarantee",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Rate",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Actual_Kms",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Wt_Kms_Articles_Payable",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Truck_Hire_Charge",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@OtherCharges",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Loading_Charges",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@TDS_Percent",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@TDS_Amount",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Total_Truck_Hire_Payable",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Total_Advance_To_Be_Paid",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Balance_Payble_Amount",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Balance_Payable_Hierarchy_Code",SqlDbType.VarChar,2,""),
        objDAL.MakeInParams("@Balance_Payable_Main_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Crossing_Cost_Payable",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@To_Pay_Collection",SqlDbType.Decimal,0,0),                
        objDAL.MakeInParams("@Delivery_Commission_Payable",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Other_Payable",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Net_Amount",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@Vehicle_Departure_Time",SqlDbType.VarChar,0,""),
        objDAL.MakeInParams("@Transit_Days",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Commited_Delivery_Date",SqlDbType.DateTime,0,DateTime.Now),
        objDAL.MakeInParams("@Loading_Supervisor_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@Remark",SqlDbType.NVarChar,0,""),                            
        objDAL.MakeInParams("@BTH_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@MemoGridXML",SqlDbType.Xml,0,MemoGridXML),                                            
        objDAL.MakeInParams("@AlertBranchesXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@AttachedLHPOBranchesXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@ATHDetailsXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@PenaltyDetailsXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@IncentiveDetailsXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
        objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@OtherChargesDetailsXML",SqlDbType.Xml,0,""),
        objDAL.MakeInParams("@IsTerminatedLHCReceivedCash" ,SqlDbType.Bit,1,false),
        objDAL.MakeInParams("@TerminatedLHCReceivedCash",SqlDbType.Money,0,0),
        objDAL.MakeInParams("@IsTerminatedLHCDebitTo",SqlDbType.Bit,1,false),
        objDAL.MakeInParams("@TermiantedLHCDebitedLedger",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@CharityLedgerId",SqlDbType.Int,0,0),
        objDAL.MakeInParams("@CharityAmount",SqlDbType.Decimal,0,0),
        objDAL.MakeInParams("@TotalAfterTDSDeduction", SqlDbType.Decimal,0,0)
        };


        objDAL.RunProc("EC_Opr_LHPO_Save", objSqlParam);

        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {

            TripMemoID = Convert.ToInt32(objSqlParam[2].Value);

            string _Msg;
            _Msg = "Saved SuccessFully";
            if (_flag == "SaveAndNew")
            {
                int MenuItemId = Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Outward/FrmTripMemo.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }
            else if (_flag == "SaveAndExit")
            {
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            else if (_flag == "SaveAndPrint")
            {
                int MenuItemId = Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
            }
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }
    #endregion

    #region subroutine
    private void FillMemoGrid()
    {

        bool _isAdd = false; ;
        if (TripMemoID <= 0)
            _isAdd = true;

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
                                           objDAL.MakeInParams("@LHPO_Date", SqlDbType.DateTime, 0, TripMemoDate),
                                           objDAL.MakeInParams("@Attached_LHPODate", SqlDbType.DateTime, 0, AttachedLHPODate), 
                                           objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, TripMemoID),                                            
                                           objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, BranchID),                                            
                                           objDAL.MakeInParams("@Is_Add", SqlDbType.Bit, 0, _isAdd),
                                           objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,1)
                                            
                                           };
        objDAL.RunProc("EC_Opr_LHPOHireDetails_FillGrid", objSqlParam, ref objDS);
        Raj.EC.Common.SetTableName(new string[] { "MEMOGRID" }, objDS);

        SessionMemoDetails = objDS;
        dgMemoDetails.DataSource = objDS;
        dgMemoDetails.DataBind();
    }
    private void GetDVLPAttachedBranches()
    {
        SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@DailyVehicleLoadingPlanID", SqlDbType.Int, 0, Util.String2Int(hdnDVLPID.Value)),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
                objDAL.MakeInParams("@LoginBranchID", SqlDbType.Int, 0, BranchID),  
                objDAL.MakeInParams("@TransactionDate", SqlDbType.DateTime, 0, TripMemoDate)
        };

        objDAL.RunProc("EC_Opr_DailyVehicleLoadingPlan_GetAttachedBranches", objSqlParam, ref objDS);
        Session["DVLPAB"] = objDS;
    }
    private void PopulateTripMemoValues()
    {

        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, TripMemoID) };

        objDAL.RunProc("EC_Opr_TripMemo_ReadValues", objSqlParam, ref ds);


        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            TripMemoDate = Convert.ToDateTime(dr["LHPO_Date"].ToString());
            AttachedLHPODate = Convert.ToDateTime(dr["LHPO_Date"].ToString());
            VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
            DDLVehicleSearch.TransactionDate = TripMemoDate;
            ddlTripMemoType.SelectedValue = dr["LHPO_Type_ID"].ToString();
            lblTripMemoNo.Text = dr["LHPO_No_For_Print"].ToString();

            hdnDriverId.Value = dr["Driver1_Id"].ToString();
            txtDriver.Text = dr["Driver1_Name"].ToString();

            hdnDVLPID.Value = dr["DVLP_ID"].ToString();
            hdnMainLHPOID.Value = dr["Main_LHPO_ID"].ToString();

            hdn_TotalArticle.Value = dr["Total_Articles"].ToString();
            txt_TotalArticle.Text = dr["Total_Articles"].ToString();

            hdn_TotalArticleWT.Value = dr["Total_Actual_Weight"].ToString();
            txt_TotalArticleWT.Text = dr["Total_Actual_Weight"].ToString();

            hdn_TotalGC.Value = dr["Total_No_Of_GCs"].ToString();
            txt_TotalGC.Text = dr["Total_No_Of_GCs"].ToString();

            hdnSelectedMemoCount.Value = dr["Total_No_Of_Memo"].ToString();

            GetDVLPAttachedBranches();
        }
    }
    private void SetLinks()
    {
        UserRights uObj;
        uObj = ClassLibraryMVP.StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        //Driver
        fRights = uObj.getForm_Rights(107);
        bool can_add = fRights.canAdd();
        lnkAddDriver.Visible = false;
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdnDriverpath.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(107).AddUrl + "&Call_From=LHPO";

            lnkAddDriver.Visible = true;
        }
        else
        {
            hdnDriverpath.Value = "";
        }
    }
    #endregion

    #region enabledisable
    private void EnableDisableSaveButtons(bool value)
    {
        btn_Save.Enabled = btn_Save_Exit.Enabled=btn_Save_Print.Enabled=value;

    }
    private void EnableDisableOnTripMemoType()
    {
        if (ddlTripMemoType.SelectedValue == "1") // new trip
            txtDriver.Enabled = true;
        else
            txtDriver.Enabled = false;
    }
    #endregion
}
