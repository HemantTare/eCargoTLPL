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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Finance_Accounting_Vouchers_FrmTripMemoFinance : ClassLibraryMVP.UI.Page
{
    string Mode = "0";
    ClassLibrary.UIControl.DDLSearch DDLAttachBranch;
    Raj.EC.Common objComm = new Raj.EC.Common();
    bool ATS = false;
    private DataSet objDS;
    DAL objDAL = new DAL();
    DataTable objDT = new DataTable();
    int ds_index, i;
    CheckBox chk;

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    public string TripMemoNo
    {
        set { lbl_TripMemoNo.Text = value; }
    }

    public DateTime TripMemo_Date
    {
        set 
        { 
            dtp_TripMemo_Date.SelectedDate = value;
            hdn_TripMemo_Date.Value = dtp_TripMemo_Date.SelectedDate.ToString();
        }
        get { return dtp_TripMemo_Date.SelectedDate; }
    }
    public DateTime FromDate
    {
        set
        {
            dtpFromDate.SelectedDate = value;
            hdn_From_Date.Value = dtpFromDate.SelectedDate.ToString();
        }
        get { return dtpFromDate.SelectedDate; }
    }
    public DateTime ToDate
    {
        set
        {
            dtpToDate.SelectedDate = value;
            hdn_To_Date.Value = dtpToDate.SelectedDate.ToString();
        }
        get { return dtpToDate.SelectedDate; }
    }

    public int VehicleID
    {
        get { return DDLVehicle.VehicleID; }
        set
        {
            DDLVehicle.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public string VehicleCategoryIds
    {
        get { return DDLVehicle.VehicleCategoryIds; }
        set
        {
            DDLVehicle.VehicleCategoryIds = value;
            //hdn_VehicleCategoryIds.Value = value;
        }
    }

    public int VendorID
    {
        get { return DDLVehicle.VehicleVendorID; }
        set
        {
            DDLVehicle.VehicleVendorID = value;
             
        }
    }
    public int BrokerID
    {
        set { DDLBroker.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(DDLBroker.SelectedValue); }
    }

    public int Session_LHPOID
    {
       set { hdnlhpoID.Value = Util.Int2String(value); }
       get { return hdnlhpoID.Value == string.Empty ? 0 : Util.String2Int(hdnlhpoID.Value); }
    }

    public DataTable SessionBindGrid
    {
        get { return StateManager.GetState<DataTable>("BindGrid"); }
        set { StateManager.SaveState("BindGrid", value); }
    }

    public String GridDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "Grid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            DDLVehicle.Can_Add_Vehicle = false;
            DDLVehicle.Can_View_Vehicle = false;
            //lnkAddBroker.Enabled = false;
            //lnkAddDriver.Enabled = false;
            //lnkAddBroker.Visible = false;
            //lnkAddDriver.Visible = false;
        }


    }

    #region DateControls
    protected void dtpFromDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_From_Date.Value = dtpFromDate.SelectedDate.ToString();
    }

    protected void dtpToDate_SelectionChanged(object sender, EventArgs e)
    {
        hdn_To_Date.Value = dtpToDate.SelectedDate.ToString();
    }

    protected void dtp_TripMemo_Date_SelectionChanged(object sender, EventArgs e)
    {
        hdn_TripMemo_Date.Value = dtp_TripMemo_Date.SelectedDate.ToString();
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {        
        //VehicleCategoryIds = "";        
        SetPostBackValues(); 
     
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack)); 

        DDLVehicle.Can_Add_Vehicle = false;
        DDLVehicle.Can_View_Vehicle = false;        
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        lblTDSAmount.Width = txtHireAmount.Width;
        lblTruckHirePayable.Width = txtHireAmount.Width;
        tr_DateRange.Visible = false;
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

            WucHierarchyWithIDATH.Set_Hierarchy_Caption = "ATH Payable At";
            WucHierarchyWithIDATH.Set_Location_Caption = "ATH Location";
            WucHierarchyWithIDATH.Set_TD_Caption_Width = "25%";
            WucHierarchyWithIDATH.Allow_All_Hierarchy = true;
            WucHierarchyWithIDATH.HierarchyCode = "BO";

            WucHierarchyWithIDBTH.Set_Hierarchy_Caption = "BTH Payable At";
            WucHierarchyWithIDBTH.Set_Location_Caption = "BTH Location";
            WucHierarchyWithIDBTH.Set_TD_Caption_Width = "25%";
            WucHierarchyWithIDBTH.Set_TD_Data_Width = "29%";
            WucHierarchyWithIDBTH.Allow_All_Hierarchy = true;
            WucHierarchyWithIDBTH.HierarchyCode = "BO";

           

            TripMemo_Date = DateTime.Now.Date;
            FromDate = DateTime.Now.Date;
            ToDate = DateTime.Now.Date;

            if (keyID <= 0)
            {
                Next_TripMemoNo();
            }
            else
            {  
                tr_NoDate.Attributes.Add("disabled", "true");   
                tr_VehicleDriver.Attributes.Add("disabled", "true"); 
                tr_StartEndBranch.Attributes.Add("disabled", "true"); 
            }

            //SetLinks();
            FillGrid();
            DDLVehicle.SetAutoPostBack = true;
            //DDLVehicle.VehicleCategoryIds = "";
            
        }
            ddlTDSCertificateTo.SelectedValue = "1";
            //trTDSCertificateTo.Visible = false;
            //trIsRCRecieved.Visible = false;
            //trIsPanCardRecieved.Visible = false;
            //trTDSPercent.Visible = false;
            //trSurChargePercent.Visible = false;
            //trAdditionalSurcharge.Visible = false;
            //trAdditionalEducation.Visible = false;
            //trTotalTDSAmount.Visible = false;
            //trTruckHirePayable.Visible = false;
    }

    private void Next_TripMemoNo()
    {

        TripMemoNo = objComm.Get_Next_Number();
        
    }
  
    #region Vehicle

    private void SetPostBackValues()
    {
        DDLVehicle.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
    }
    
    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCategoryIds = "";
        VehicleCategoryIds = DDLVehicle.GetVehicleParameter("Vehicle_Category_ID");
        hdn_VehicleCategoryIds.Value = VehicleCategoryIds;
         
        SetVehicleInfoOnVehicleChanged();
        Chang_dlTDSCertificateTo();
       
        SessionBindGrid.Clear();
        dg_Memo.DataSource = SessionBindGrid;
        dg_Memo.DataBind();
        BindGrid();
    }

    public void SetVehicleInfoOnVehicleChanged()
    {
        if (VehicleID > 0)
        {
            objDS = GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0]; 
                DDLBroker.DataTextField = "BrokerName";
                DDLBroker.DataValueField = "BrokerID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["BrokerName"].ToString(), objDR["BrokerID"].ToString(), DDLBroker);

            }
        }
    }

    public DataSet GetVehicleInformationOnVehicleChanged()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
                                         objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0, VehicleCategoryIds) 
                                           };
        objDAL.RunProc("EC_Opr_LHPOHireDetails_GetValuesOnVahicleChanged", objSqlParam, ref objDS);
        return objDS;

    }
    #endregion

    #region grid operation
    
    private void FillGrid()
    {

        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@KeyID",SqlDbType.Int,0,keyID),
                objDAL.MakeInParams("@DivisionID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,dtpFromDate.SelectedDate),
                objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,dtpToDate.SelectedDate),
                objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,VehicleID)};


        objDAL.RunProc("dbo.EC_FA_AV_GetTripMemo_Finance_Details_DateWise", objSqlParam, ref objDS);

        SessionBindGrid = objDS.Tables[0];
        //if (keyID > 0)
        //{
            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];
                //TripMemoNo = objDR["Trip_Memo_For_Print"].ToString();
                //TripMemo_Date = Convert.ToDateTime(objDR["Trip_Memo_Date"].ToString()); 

                TripMemoNo = objDR["LHPO_No_For_Print"].ToString();
                TripMemo_Date = Convert.ToDateTime(objDR["LHPO_Date"].ToString()); 


                DDLVehicle.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                DDLVehicle.VehicleCategoryIds = objDR["Vehicle_Category_ID"].ToString();
                hdn_VehicleCategoryIds.Value = objDR["Vehicle_Category_ID"].ToString();

                DDLBroker.DataTextField = "BrokerName";
                DDLBroker.DataValueField = "BrokerID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["BrokerName"].ToString(), objDR["Broker_ID"].ToString(), DDLBroker);

                DDLFromBranch.DataTextField = "FromBranch";
                DDLFromBranch.DataValueField = "FromBranchID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["FromBranch"].ToString(), objDR["From_Branch_ID"].ToString(), DDLFromBranch);

                DDLToBranch.DataTextField = "ToBranch";
                DDLToBranch.DataValueField = "ToBranchID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["ToBranch"].ToString(), objDR["To_Branch_ID"].ToString(), DDLToBranch);

                DDLDriver.DataTextField = "DriverName";
                DDLDriver.DataValueField = "Driver_ID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["Driver_Name"].ToString(), objDR["Driver1_ID"].ToString(), DDLDriver);

                ddlTDSCertificateTo.SelectedValue = objDR["Is_TDS_Certificate_Broker"].ToString();

                bool Is_TDS_Certificate_Broker;
                Is_TDS_Certificate_Broker = Convert.ToBoolean(objDR["Is_TDS_Certificate_Broker"].ToString());

                if (Is_TDS_Certificate_Broker == true)
                {
                  ddlTDSCertificateTo.SelectedValue = "1";
                }
                else if(Is_TDS_Certificate_Broker == false)
                {
                  ddlTDSCertificateTo.SelectedValue = "2";
                }

                chkIsRCRecieved.Checked = Util.String2Bool(objDR["Is_RC_Received"].ToString());
                chkIsPanCardRecieved.Checked = Util.String2Bool(objDR["Is_PAN_Card_Received"].ToString());
                txtHireAmount.Text = objDR["Truck_Hire_Charge"].ToString();

                //lblTDSPercent.Text = "TDS " + objDR["TDS_Percent"].ToString() + " %:";
                lblTDSPercent.Text = "TDS " + "0 " + " %:";
                hdnTDSPercent.Value = "0";//objDR["TDS_Percent"].ToString();
                lblTDSAmount.Text = "0";//objDR["TDS_Amount"].ToString();
                hdnTDSAmount.Value = "0";//objDR["TDS_Amount"].ToString();

                //lblSurChargePercent.Text = "Surcharge " + objDR["SurchargePercent"].ToString() + " %;";
                lblSurChargePercent.Text = "Surcharge " + "0 " + " %;";
                hdnSurchargePercent.Value = "0";//objDR["SurchargePercent"].ToString();
                lblSurChargeAmount.Text = "0";//objDR["SurchargeAmount"].ToString();
                hdnSurChargeAmount.Value = "0";//objDR["SurchargeAmount"].ToString();

                //lblAdditionalSurchargeCessPercent.Text = "Additional Surcharge Cess " + objDR["AdditionalSurchargeCessPercent"].ToString() + " %;";
                lblAdditionalSurchargeCessPercent.Text = "Additional Surcharge Cess " + "0 " + " %;";
                lblAdditionalSurchargeCessPercent.Text = "0";// objDR["AdditionalSurchargeCessPercent"].ToString();
                lblAdditionalSurchargeCessAmount.Text = "0";// objDR["AdditionalSurchargeCessAmount"].ToString();
                hdnAdditionalSurchargeCessAmount.Value = "0";// objDR["AdditionalSurchargeCessAmount"].ToString();

                //lblAddistionalEducationCessPercent.Text = "Additional Education Cess " + objDR["AdditionalEducationCessPercent"].ToString() + " %;";
                lblAddistionalEducationCessPercent.Text = "Additional Education Cess " + "0 " + " %;";
                hdnAddistionalEducationCessPercent.Value = "0";//objDR["AdditionalEducationCessPercent"].ToString();
                lblAddistionalEducationCessAmount.Text = "0";//objDR["AdditionalEducationCessAmount"].ToString();
                hdnAddistionalEducationCessAmount.Value = "0";//objDR["AdditionalEducationCessAmount"].ToString();

                lblTotalTDSAmount.Text = "0";//objDR["Total_TDS_Amount"].ToString();
                hdnTotalTDSAmount.Value = "0";//objDR["Total_TDS_Amount"].ToString();

                lblTruckHirePayable.Text = objDR["Truck_Hire_Charge"].ToString();//objDR["Total_Truck_Hire_Payable"].ToString();
                hdnTruckHirePayable.Value = objDR["Truck_Hire_Charge"].ToString();// objDR["Total_Truck_Hire_Payable"].ToString();
                txtRemarks.Text = objDR["Remarks"].ToString();

                lbl_Total_GC.Text = objDR["Total_No_Of_GCs"].ToString();
                lbl_Total_Memo.Text = objDR["Total_No_Of_Memo"].ToString();
                lbl_totalFreight.Text = objDR["Total_Freight"].ToString();
                lbl_totalpackage.Text = objDR["Total_Articles"].ToString();

                if (DDLVehicle.VehicleCategoryIds != "5")
                {
                    //trBrokerName.Visible = false;
                }
                if (DDLVehicle.VehicleCategoryIds == "1")
                {
                    ddlTDSCertificateTo.Enabled = false;
                    chkIsRCRecieved.Enabled = false;
                    chkIsPanCardRecieved.Enabled = false;
                }
                //DDLVehicle.Enable_Disable(false);

                txtAdvance.Text = objDR["AdvanceAmount"].ToString();
                lblBalance.Text = objDR["BalanceAmount"].ToString();
                hdnBalance.Value = objDR["BalanceAmount"].ToString();
                WucHierarchyWithIDATH.HierarchyCode = objDR["ATHPayableHierarchyCode"].ToString();
                WucHierarchyWithIDATH.MainId = Util.String2Int(objDR["ATHPayableLocationID"].ToString());
                WucHierarchyWithIDBTH.HierarchyCode = objDR["BTHPayableHierarchyCode"].ToString();
                WucHierarchyWithIDBTH.MainId = Util.String2Int(objDR["BTHPayableLocationID"].ToString()); 

            }
        
        //}

        BindGrid();

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lblHeaderErrors.Text = "";
        }
        else
        {
            if (IsPostBack)
            { lblHeaderErrors.Text = "No Records Found"; }
        }
    }
    
    public void BindGrid()
    {
        dg_Memo.DataSource = SessionBindGrid;
        dg_Memo.DataBind(); 
    }

    #endregion

    protected void btn_Show_Click(object sender, EventArgs e)
    {
        //if (ValidDate())
        //{
            FillGrid();
        //}
    } 

    private void Chang_dlTDSCertificateTo()
    { 
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CallMyFunction", "TDSCertificateToChange();", true);
    }

    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@DailyVehicleLoadingPlanID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        objDAL.RunProc("dbo.EC_Opr_DailyVehicleLoadingPlan_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            dtp_TripMemo_Date.SelectedDate = Convert.ToDateTime(objDR["LoadingDate"].ToString());
            dtpFromDate.SelectedDate = Convert.ToDateTime(objDR["LoadingDate"].ToString());
            dtpToDate.SelectedDate = Convert.ToDateTime(objDR["LoadingDate"].ToString());
            DDLVehicle.VehicleID = Util.String2Int(objDR["VehicleID"].ToString());
            DDLVehicle.VehicleCategoryIds =  objDR["Vehicle_Category_ID"].ToString();
            hdn_VehicleCategoryIds.Value = objDR["Vehicle_Category_ID"].ToString();


            DDLBroker.DataTextField = "BrokerName";
            DDLBroker.DataValueField = "BrokerID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["BrokerName"].ToString(), objDR["BrokerID"].ToString(), DDLBroker);

            DDLFromBranch.DataTextField = "FromBranch";
            DDLFromBranch.DataValueField = "FromBranchID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["FromBranch"].ToString(), objDR["FromBranchID"].ToString(), DDLFromBranch);

            DDLToBranch.DataTextField = "ToBranch";
            DDLToBranch.DataValueField = "ToBranchID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["ToBranch"].ToString(), objDR["ToBranchID"].ToString(), DDLToBranch);

            DDLDriver.DataTextField = "DriverName";
            DDLDriver.DataValueField = "DriverID";
            Raj.EC.Common.SetValueToDDLSearch(objDR["DriverName"].ToString(), objDR["DriverID"].ToString(), DDLDriver);

            ddlTDSCertificateTo.SelectedValue = objDR["TDSCertificateTo"].ToString();
            chkIsRCRecieved.Checked = Util.String2Bool(objDR["IsRCRecieved"].ToString());
            chkIsPanCardRecieved.Checked = Util.String2Bool(objDR["IsPanCardRecieved"].ToString());
            txtHireAmount.Text = objDR["HireAmount"].ToString();

            lblTDSPercent.Text = "TDS " + objDR["TDSPercent"].ToString() + " %;";
            hdnTDSPercent.Value = objDR["TDSPercent"].ToString();
            lblTDSAmount.Text = objDR["TDSAmount"].ToString();
            hdnTDSAmount.Value = objDR["TDSAmount"].ToString();

            lblSurChargePercent.Text = "Surcharge " + objDR["SurchargePercent"].ToString() + " %;";
            hdnSurchargePercent.Value = objDR["SurchargePercent"].ToString();
            lblSurChargeAmount.Text = objDR["SurchargeAmount"].ToString();
            hdnSurChargeAmount.Value = objDR["SurchargeAmount"].ToString();

            lblAdditionalSurchargeCessPercent.Text = "Additional Surcharge Cess " + objDR["AdditionalSurchargeCessPercent"].ToString() + " %;";
            lblAdditionalSurchargeCessPercent.Text = objDR["AdditionalSurchargeCessPercent"].ToString();
            lblAdditionalSurchargeCessAmount.Text = objDR["AdditionalSurchargeCessAmount"].ToString();
            hdnAdditionalSurchargeCessAmount.Value = objDR["AdditionalSurchargeCessAmount"].ToString();

            lblAddistionalEducationCessPercent.Text = "Additional Education Cess " + objDR["AdditionalEducationCessPercent"].ToString() + " %;";
            hdnAddistionalEducationCessPercent.Value = objDR["AdditionalEducationCessPercent"].ToString();
            lblAddistionalEducationCessAmount.Text = objDR["AdditionalEducationCessAmount"].ToString();
            hdnAddistionalEducationCessAmount.Value = objDR["AdditionalEducationCessAmount"].ToString();

            lblTotalTDSAmount.Text = objDR["TotalTDSAmount"].ToString();
            hdnTotalTDSAmount.Value = objDR["TotalTDSAmount"].ToString();

            lblTruckHirePayable.Text = objDR["TruckHirePayable"].ToString();
            hdnTruckHirePayable.Value = objDR["TruckHirePayable"].ToString();
            txtAdvance.Text = objDR["AdvanceAmount"].ToString();
            lblBalance.Text = objDR["BalanceAmount"].ToString();
            hdnBalance.Value = objDR["BalanceAmount"].ToString();
            WucHierarchyWithIDATH.HierarchyCode = objDR["ATHPayableHierarchyCode"].ToString();
            WucHierarchyWithIDATH.MainId = Util.String2Int(objDR["ATHPayableLocationID"].ToString());
            WucHierarchyWithIDBTH.HierarchyCode = objDR["BTHPayableHierarchyCode"].ToString();
            WucHierarchyWithIDBTH.MainId = Util.String2Int(objDR["BTHPayableLocationID"].ToString()); 
            txtRemarks.Text = objDR["Remarks"].ToString();
  
            if (DDLVehicle.VehicleCategoryIds != "5")
            {
                //trBrokerName.Visible = false;
            }
            if (DDLVehicle.VehicleCategoryIds == "1")
            {
                ddlTDSCertificateTo.Enabled = false;
                chkIsRCRecieved.Enabled = false;
                chkIsPanCardRecieved.Enabled = false; 
            }
            DDLVehicle.Enable_Disable(false);

            Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
            if (Mode == "2")
            {
                if (Util.String2Int(objDR["LHPOID"].ToString()) > 0)
                {
                    Session_LHPOID = Util.String2Int(objDR["LHPOID"].ToString());

                    dtp_TripMemo_Date.Enabled = false;
                    dtpFromDate.Enabled = false;
                    dtpToDate.Enabled = false;
                    DDLBroker.Enabled = false;
                    DDLFromBranch.Enabled = false;

                    DDLToBranch.Enabled = false;
                    DDLDriver.Enabled = false;

                    ddlTDSCertificateTo.Enabled = false;
                    chkIsRCRecieved.Enabled = false;
                    chkIsPanCardRecieved.Enabled = false;
                    txtHireAmount.Enabled = false;

                    lblTDSPercent.Enabled = false;
                    lblTDSAmount.Enabled = false;
                    lblSurChargePercent.Enabled = false;
                    lblSurChargeAmount.Enabled = false;

                    lblAdditionalSurchargeCessPercent.Enabled = false;
                    lblAdditionalSurchargeCessAmount.Enabled = false;

                    lblAddistionalEducationCessPercent.Enabled = false;
                    lblAddistionalEducationCessAmount.Enabled = false;
                    lblTotalTDSAmount.Enabled = false;
                    lblTruckHirePayable.Enabled = false;

                    txtAdvance.Enabled = false;
                    lblBalance.Enabled = false;

                    WucHierarchyWithIDATH.SetEnabled = false;
                    WucHierarchyWithIDBTH.SetEnabled = false;

                    //trBrokerName.Disabled = true;
                    ddlTDSCertificateTo.Enabled = false;
                    chkIsRCRecieved.Enabled = false;
                    chkIsPanCardRecieved.Enabled = false;

                    DDLVehicle.Enable_Disable(false);


                }
                else
                { 
                    Session_LHPOID = 0;
                }

            }
        }

        Session["DVLPAB"] = ds.Tables[1];
    }
    
 
    private void SetDDLBranch(string text, string value, ClassLibrary.UIControl.DDLSearch ddlSearch)
    {
        ddlSearch.DataTextField = "Branch_Name";
        ddlSearch.DataValueField = "Branch_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddlSearch);
    }

    protected void DDLFromBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Common obj = new Common();
        DataSet ds = obj.EC_Common_Pass_Query("select area_id from dbo.ec_master_branch where branch_id = " + DDLFromBranch.SelectedValue.ToString());

        WucHierarchyWithIDATH.AreaID = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
        WucHierarchyWithIDBTH.AreaID = WucHierarchyWithIDATH.AreaID;

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CallMyFunction", "TDSCertificateToChange();", true);
    }

    private void SetLinks()
    {
        UserRights uObj;
        uObj = ClassLibraryMVP.StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        //Driver
        fRights = uObj.getForm_Rights(107);
        bool can_add = fRights.canAdd();
        //lnkAddDriver.Visible = false;
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdnDriverpath.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(107).AddUrl + "&Call_From=LHPO";

            //lnkAddDriver.Visible = true;
        }
        else
        {
            hdnDriverpath.Value = "";
        }

        //Broker

        fRights = uObj.getForm_Rights(69);
        can_add = fRights.canAdd();
        //lnkAddBroker.Visible = false;
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdnBrokerPath.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(69).AddUrl + "&Call_From=DVLP";

            //lnkAddBroker.Visible = false;
        }
        else
        {
            hdnBrokerPath.Value = "";
        }
    }

    #region Validation

    private bool ValidateUI()
    {
        bool ATS = false;
        string msg = "";
        DateCommon objDateCommon = new DateCommon();
        //DataTable DT = (DataTable)(Session["DVLPAB"]);

        //DataRow[] result = DT.Select("Branch_Id=" + Util.String2Int(DDLToBranch.SelectedValue).ToString());

        if (DDLVehicle.VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle No";
        }
        //else if (Util.String2Int(hdnKeyID.Value) <=0 && !IsVehicleValid())
        //{
        //    lblErrors.Text = "Daily vehicle loading plan created with this vehicle has not been used in any Trip Memo. Please create Trip Memo first";
        //}
        //else if (Util.String2Int(DDLBroker.SelectedValue) <= 0)
        //{
        //    lblErrors.Text = "Please Select Broker";
        //}
        else if ((objDateCommon.Vaildate_Date(UserManager.getUserParam().StartDate, dtp_TripMemo_Date.SelectedDate, ref msg)) == false)
        {
            lblErrors.Text = msg.Replace("To Date", "Trip Memo Date"); 
        }
        else if (DDLVehicle.VehicleCategoryIds == "5" && Util.String2Int(DDLBroker.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Broker";
        }
        else if (Util.String2Int(DDLFromBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select From branch";
        }
        else if (Util.String2Int(DDLToBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select To branch";
        }
        //else if (result.Length > 0)
        //{
        //    lblErrors.Text = "To Branch and Attached Branch cannot be same";
        //}
        else if ((Util.String2Int(DDLFromBranch.SelectedValue) > 0) && (Util.String2Int(DDLToBranch.SelectedValue) > 0)
                && (Util.String2Int(DDLFromBranch.SelectedValue) == Util.String2Int(DDLToBranch.SelectedValue)))
        {
            lblErrors.Text = "From branch and To branch cannot be same";
        }
        //else if (Util.String2Int(DDLDriver.SelectedValue) <= 0)
        //{
        //    lblErrors.Text = "Please Select Driver";
        //}
        //else if (Util.String2Int(ddlTDSCertificateTo.SelectedValue) <= 0)
        //{
        //    lblErrors.Text = "Please Select TDS Certificate to";
        //}
        else if (Util.String2Decimal(txtAdvance.Text) > 0 && (WucHierarchyWithIDATH.HierarchyCode == "0"))
        {
            lblErrors.Text = "Please Select ATH payable at";
        }
        else if (Util.String2Decimal(txtAdvance.Text) > 0 && WucHierarchyWithIDATH.HierarchyCode != "HO" && WucHierarchyWithIDATH.MainId <= 0)
        {
            lblErrors.Text = "Please Select ATH payable at location";
        }
        else if (Util.String2Decimal(hdnBalance.Value) > 0 && WucHierarchyWithIDBTH.HierarchyCode == "0")
        {
            lblErrors.Text = "Please Select BTH payable at";
        }
        else if (Util.String2Decimal(hdnBalance.Value) > 0 && WucHierarchyWithIDBTH.HierarchyCode != "HO" && WucHierarchyWithIDBTH.MainId <= 0)
        {
            lblErrors.Text = "Please Select BTH payable at location";
        } 
        else
        {
            ATS = true;
        }

        return ATS;
    }

    private Boolean ValidDate()
    {
        Boolean _isValid;
        _isValid = true;

        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(UserManager.getUserParam().StartDate, dtp_TripMemo_Date.SelectedDate, ref msg)) == false)
        {
            lblHeaderErrors.Text = msg.Replace("To Date", "Trip Memo Date");
            _isValid = false;
        }
        //else if ((objDateCommon.Vaildate_Date(dtpFromDate.SelectedDate, dtpToDate.SelectedDate, ref msg)) == false)
        //{
        //    lblHeaderErrors.Text = msg;
        //    _isValid = false;
        //}
        else
        {
            lblHeaderErrors.Text = "";
        }
        return _isValid;
    }

    #endregion


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            Save();
        }
    }

    public Message Save()
    {
        griddetails();
        DAL objDAL = new DAL();  
  
        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeOutParams("@Trip_Memo_ID", SqlDbType.Int, 0), 
                objDAL.MakeInParams("@KeyID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)),
                objDAL.MakeInParams("@DivisionID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId), 
                objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode), 
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,2,UserManager.getUserParam().HierarchyCode), 
                objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0,UserManager.getUserParam().MainId), 
                objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
                objDAL.MakeInParams("@TripMemo_Date",SqlDbType.DateTime,0,dtp_TripMemo_Date.SelectedDate),
                objDAL.MakeInParams("@Vehicle_Category_ID",SqlDbType.Int,0,Util.String2Int(DDLVehicle.VehicleCategoryIds)),
                objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,DDLVehicle.VehicleID),
                objDAL.MakeInParams("@Driver1_Id",SqlDbType.Int,0,Util.String2Int(DDLDriver.SelectedValue)),
                objDAL.MakeInParams("@From_Branch_ID",SqlDbType.Int,0,Util.String2Int(DDLFromBranch.SelectedValue)),
                objDAL.MakeInParams("@To_Branch_ID",SqlDbType.Int,0,Util.String2Int(DDLToBranch.SelectedValue)),
                objDAL.MakeInParams("@Broker_ID",SqlDbType.Int,0,Util.String2Int(DDLBroker.SelectedValue)), 
                objDAL.MakeInParams("@Is_TDS_Certificate_Broker",SqlDbType.TinyInt,0,Util.String2Int(ddlTDSCertificateTo.SelectedValue)),
                objDAL.MakeInParams("@Is_RC_Received",SqlDbType.Bit,0,chkIsRCRecieved.Checked),
                objDAL.MakeInParams("@Is_PAN_Card_Received",SqlDbType.Bit,0,chkIsPanCardRecieved.Checked),
                objDAL.MakeInParams("@Truck_Hire_Charge",SqlDbType.Decimal,0,Util.String2Decimal(txtHireAmount.Text)),
                objDAL.MakeInParams("@AdvanceAmount",SqlDbType.Decimal,0,Util.String2Decimal(txtAdvance.Text)),
                objDAL.MakeInParams("@BalanceAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnBalance.Value)),
                objDAL.MakeInParams("@ATHPayableHierarchyCode",SqlDbType.VarChar,2,WucHierarchyWithIDATH.HierarchyCode),
                objDAL.MakeInParams("@ATHPayableLocationID",SqlDbType.Int,0,WucHierarchyWithIDATH.MainId),
                objDAL.MakeInParams("@BTHPayableHierarchyCode",SqlDbType.VarChar,2,WucHierarchyWithIDBTH.HierarchyCode),
                objDAL.MakeInParams("@BTHPayableLocationID",SqlDbType.Int,0,WucHierarchyWithIDBTH.MainId),
                objDAL.MakeInParams("@TDS_Percent",SqlDbType.Decimal,0,Util.String2Decimal(hdnTDSPercent.Value)),
                objDAL.MakeInParams("@TDS_Amount",SqlDbType.Decimal,0,Util.String2Decimal(hdnTDSAmount.Value)),
                objDAL.MakeInParams("@SurchargePercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnSurchargePercent.Value)),
                objDAL.MakeInParams("@SurchargeAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnSurChargeAmount.Value)),
                objDAL.MakeInParams("@AdditionalSurchargeCessPercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnAdditionalSurchargeCessPercent.Value)),
                objDAL.MakeInParams("@AdditionalSurchargeCessAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnAdditionalSurchargeCessAmount.Value)),
                objDAL.MakeInParams("@AdditionalEducationCessPercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnAddistionalEducationCessPercent.Value)),
                objDAL.MakeInParams("@AdditionalEducationCessAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnAddistionalEducationCessAmount.Value)),
                objDAL.MakeInParams("@Total_Truck_Hire_Payable",SqlDbType.Decimal,0,Util.String2Decimal(hdnTruckHirePayable.Value)), 
                objDAL.MakeInParams("@Total_TDS_Amount",SqlDbType.Decimal,0,Util.String2Decimal(hdnTotalTDSAmount.Value)),
                objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,100,txtRemarks.Text),
                objDAL.MakeInParams("@Updated_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
                objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,GridDetailsXML)};
        objDAL.RunProc("dbo.EC_FA_AV_TripMemo_Finance_Save", objSqlParam);


        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";  
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    private void griddetails()
    {  
        if (dg_Memo.Items.Count > 0)
        {
            objDT = SessionBindGrid;

            for (i = 0; i <= dg_Memo.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_Memo.Items[i].FindControl("Chk_Attach");
                objDT.Rows[i]["Att"] = chk.Checked; 
            }
        }

    } 

}
