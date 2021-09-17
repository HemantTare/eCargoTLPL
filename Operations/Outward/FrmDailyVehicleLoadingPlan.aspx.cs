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

public partial class Operations_Outward_FrmDailyVehicleLoadingPlan : ClassLibraryMVP.UI.Page
{
    string Mode = "0";
    bool ATS = false;
    private DataSet objDS;
    TextBox txt_AttachBranch;
    ListBox lst_AttachBranch;
    HiddenField hdn_AttachBranchId;
    public int VehicleID
    {
        get { return DDLVehicle.VehicleID; }
        set
        {
            DDLVehicle.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public int FromBranchID
    {
        get { return Util.String2Int(hdnFrombranch.Value); }
        set
        {
            hdnFrombranch.Value = Util.Int2String(value);
        }
    }
    public int ToBranchID
    {
        get { return Util.String2Int(hdn_Tobranch.Value); }
        set
        {
            hdn_Tobranch.Value = Util.Int2String(value);
        }
    }
    public int DriverID
    {
        get { return Util.String2Int(hdnDriver.Value); }
        set
        {
            hdnDriver.Value = Util.Int2String(value);
        }
    }
    public int CleanerID
    {
        get { return Util.String2Int(hdnCleaner.Value); }
        set
        {
            hdnCleaner.Value = Util.Int2String(value);
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
        set { hdnBroker.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnBroker.Value); }
    }
    public int Session_LHPOID
    {
        set { hdnlhpoID.Value = Util.Int2String(value); }
        get { return hdnlhpoID.Value == string.Empty ? 0 : Util.String2Int(hdnlhpoID.Value); }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            DDLVehicle.Can_Add_Vehicle = false;
            DDLVehicle.Can_View_Vehicle = true;
            lnkAddBroker.Enabled = false;
            lnkAddDriver.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Start added on 11-12-13
        VehicleCategoryIds = "";
        SetPostBackValues();
        //End added on 11-12-13
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

            WucHierarchyWithIDATH.Set_Hierarchy_Caption = "ATH Payable At";
            WucHierarchyWithIDATH.Set_Location_Caption = "ATH Location";
            WucHierarchyWithIDATH.Set_TD_Caption_Width = "25%";
            WucHierarchyWithIDATH.Allow_All_Hierarchy = true;

            WucHierarchyWithIDBTH.Set_Hierarchy_Caption = "BTH Payable At";
            WucHierarchyWithIDBTH.Set_Location_Caption = "BTH Location";
            WucHierarchyWithIDBTH.Set_TD_Caption_Width = "25%";
            WucHierarchyWithIDBTH.Set_TD_Data_Width = "29%";
            WucHierarchyWithIDBTH.Allow_All_Hierarchy = true;

            SetLinks();

            ReadValues();
            BindAttachedBranchGrid();

            //Session.Remove("LHPOID");
        }

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        lblTDSAmount.Width = txtHireAmount.Width;
        lblTruckHirePayable.Width = txtHireAmount.Width;
        lblBalance.Width = txtHireAmount.Width;

        if (!IsPostBack)
        {
            DDLVehicle.SetAutoPostBack = true;
            DDLVehicle.VehicleCategoryIds = "";
        }
        lst_ToBranch.Style.Add("visibility", "hidden");
        lst_FromBranch.Style.Add("visibility", "hidden");
        lstDriver.Style.Add("visibility", "hidden");
        lstCleaner.Style.Add("visibility", "hidden");
        lstBroker.Style.Add("visibility", "hidden");
    }


    private void SetPostBackValues()
    {
        DDLVehicle.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        //if (Mode == "2" || Mode == "4")
        //{
        //    if (ddl_DeliveryMode.SelectedValue == "2")
        //    {
        //        //VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
        //        VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
        //    }
        //}
        //else
        //{

        VehicleCategoryIds = DDLVehicle.GetVehicleParameter("Vehicle_Category_ID");
        hdn_VehicleCategoryIds.Value = VehicleCategoryIds;

        SetVehicleInfoOnVehicleChanged();
        Chang_dlTDSCertificateTo();
        if (hdn_VehicleCategoryIds.Value == "5")
            trBrokerName.Visible = true;
        else
            trBrokerName.Visible = false;


        //}
    }

    public void SetVehicleInfoOnVehicleChanged()
    {
        if (VehicleID > 0)
        {
            objDS = GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                //objILHPOHireDetailsView.SetBrokerName(DR["Vendor_Name"].ToString(), DR["Vendor_Id"].ToString());
                txtBroker.Text = objDR["BrokerName"].ToString();
                BrokerID = Util.String2Int(objDR["BrokerID"].ToString());

                txtDriver.Text = objDR["Driver_Name"].ToString();
                DriverID = Util.String2Int(objDR["Driver_ID"].ToString());

                txt_MobileNo1.Text = objDR["Mobile_No_1"].ToString();
                txt_MobileNo2.Text = objDR["Mobile_No_2"].ToString();

                txtCleaner.Text = objDR["Cleaner_Name"].ToString();
                CleanerID = Util.String2Int(objDR["Cleaner_ID"].ToString());

                txt_MobileNo1CL.Text = objDR["Mobile_No_1CL"].ToString();
                txt_MobileNo2CL.Text = objDR["Mobile_No_2CL"].ToString();
            }
            else
            {
                txt_MobileNo1.Text = "";
                txt_MobileNo2.Text = "";
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

    //protected void btn_driverhidden_Click(object sender, EventArgs e)
    //{
    //    if (DriverID > 0)
    //    {
    //        Common obj = new Common();
    //        DataSet ds = obj.EC_Common_Pass_Query("select IsNull(Mobile_No,'') Mobile_No_1, IsNull(Phone_No,'') Mobile_No_2 from dbo.EF_Master_Driver where Driver_ID = " + DriverID.ToString());

    //        txt_MobileNo1.Text = ds.Tables[0].Rows[0][0].ToString();
    //        txt_MobileNo2.Text = ds.Tables[0].Rows[0][1].ToString();
    //    }
    //    else
    //    {
    //        txt_MobileNo1.Text = "";
    //        txt_MobileNo2.Text = "";

    //        txt_MobileNo1CL.Text = "";
    //        txt_MobileNo2CL.Text = "";
    //    }
    //}

    //protected void btn_cleanerhidden_Click(object sender, EventArgs e)
    //{
    //    if (CleanerID > 0)
    //    {
    //        Common obj = new Common();
    //        DataSet ds = obj.EC_Common_Pass_Query("select IsNull(Mobile_No,'') Mobile_No_1, IsNull(Phone_No,'') Mobile_No_2 from dbo.EF_Master_Driver where Driver_ID = " + CleanerID.ToString());

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txt_MobileNo1CL.Text = ds.Tables[0].Rows[0][0].ToString();
    //            txt_MobileNo2CL.Text = ds.Tables[0].Rows[0][1].ToString();
    //        }
    //        else
    //        {
    //            txt_MobileNo1CL.Text = "";
    //            txt_MobileNo2CL.Text = "";
    //        }
    //    }
    //    else
    //    {
    //        txt_MobileNo1CL.Text = "";
    //        txt_MobileNo2CL.Text = "";
    //    }
    //}

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

            dtpLoadingDate.SelectedDate = Convert.ToDateTime(objDR["LoadingDate"].ToString());
            DDLVehicle.VehicleID = Util.String2Int(objDR["VehicleID"].ToString());
            DDLVehicle.VehicleCategoryIds = objDR["Vehicle_Category_ID"].ToString();
            hdn_VehicleCategoryIds.Value = objDR["Vehicle_Category_ID"].ToString();

            txtBroker.Text = objDR["BrokerName"].ToString();
            BrokerID = Util.String2Int(objDR["BrokerID"].ToString());

            txt_FromBranch.Text = objDR["FromBranch"].ToString();
            FromBranchID = Util.String2Int(objDR["FromBranchID"].ToString());

            txt_ToBranch.Text = objDR["ToBranch"].ToString();
            ToBranchID = Util.String2Int(objDR["ToBranchID"].ToString());

            txtDriver.Text = objDR["DriverName"].ToString();
            DriverID = Util.String2Int(objDR["DriverID"].ToString());

            ddlTDSCertificateTo.SelectedValue = objDR["TDSCertificateTo"].ToString();
            chkIsRCRecieved.Checked = Util.String2Bool(objDR["IsRCRecieved"].ToString());
            chkIsPanCardRecieved.Checked = Util.String2Bool(objDR["IsPanCardRecieved"].ToString());
            txtHireAmount.Text = objDR["HireAmount"].ToString();

            lblTDSPercent.Text = "TDS " + objDR["TDSPercent"].ToString() + " %:";
            hdnTDSPercent.Value = objDR["TDSPercent"].ToString();
            lblTDSAmount.Text = objDR["TDSAmount"].ToString();
            hdnTDSAmount.Value = objDR["TDSAmount"].ToString();

            lblSurChargePercent.Text = "Surcharge " + objDR["SurchargePercent"].ToString() + " %:";
            hdnSurchargePercent.Value = objDR["SurchargePercent"].ToString();
            lblSurChargeAmount.Text = objDR["SurchargeAmount"].ToString();
            hdnSurChargeAmount.Value = objDR["SurchargeAmount"].ToString();

            lblAdditionalSurchargeCessPercent.Text = "Additional Surcharge Cess " + objDR["AdditionalSurchargeCessPercent"].ToString() + " %:";
            //lblAdditionalSurchargeCessPercent.Text = objDR["AdditionalSurchargeCessPercent"].ToString();
            lblAdditionalSurchargeCessAmount.Text = objDR["AdditionalSurchargeCessAmount"].ToString();
            hdnAdditionalSurchargeCessAmount.Value = objDR["AdditionalSurchargeCessAmount"].ToString();

            lblAddistionalEducationCessPercent.Text = "Additional Education Cess " + objDR["AdditionalEducationCessPercent"].ToString() + " %:";
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

            txt_MobileNo1.Text = objDR["Mobile_No_1"].ToString();
            txt_MobileNo2.Text = objDR["Mobile_No_2"].ToString();

            txtCleaner.Text = objDR["CleanerName"].ToString();
            CleanerID = Util.String2Int(objDR["CleanerID"].ToString());

            txt_MobileNo1CL.Text = objDR["Mobile_No_1CL"].ToString();
            txt_MobileNo2CL.Text = objDR["Mobile_No_2CL"].ToString();

            if (DDLVehicle.VehicleCategoryIds != "5")
            {
                trBrokerName.Visible = false;
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

                    dtpLoadingDate.Disable = true;
                    txtBroker.Enabled = false;
                    txt_FromBranch.Enabled = false;

                    txt_ToBranch.Enabled = false;
                    txtDriver.Enabled = false;

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
                    trBrokerName.Disabled = true;
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


    #region grid operation

    protected void dgGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgGrid.EditItemIndex = -1;
        dgGrid.ShowFooter = true;
        BindAttachedBranchGrid();
    }
    protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DVLPAB"]);
        DataRow DR = DT.Rows[e.Item.ItemIndex];
        DT.Rows.Remove(DR);
        Session["DVLPAB"] = DT;
        BindAttachedBranchGrid();
    }
    protected void dgGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgGrid.EditItemIndex = e.Item.ItemIndex;
        dgGrid.ShowFooter = false;
        BindAttachedBranchGrid();
    }
    protected void dgGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (ATS)
            {
                BindAttachedBranchGrid();
                dgGrid.EditItemIndex = -1;
                dgGrid.ShowFooter = true;
                if (IsPostBack)
                    txt_AttachBranch.Focus();
            }
        }
    }
    protected void dgGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (ATS == true)
        {
            dgGrid.EditItemIndex = -1;
            dgGrid.ShowFooter = true;

            BindAttachedBranchGrid();
        }
    }
    protected void dgGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                txt_AttachBranch = (TextBox)(e.Item.FindControl("txt_AttachBranch"));
                hdn_AttachBranchId = (HiddenField)(e.Item.FindControl("hdn_AttachBranchId"));
                lst_AttachBranch = (ListBox)(e.Item.FindControl("lst_AttachBranch"));
                if (IsPostBack)
                    txt_AttachBranch.Focus();
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                txt_AttachBranch = (TextBox)(e.Item.FindControl("txt_AttachBranch"));
                hdn_AttachBranchId = (HiddenField)(e.Item.FindControl("hdn_AttachBranchId"));
                lst_AttachBranch = (ListBox)(e.Item.FindControl("lst_AttachBranch"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataTable DT = (DataTable)(Session["DVLPAB"]);
                DataRow DR = DT.Rows[e.Item.ItemIndex];

                txt_AttachBranch.Text = DR["Branch_Name"].ToString();
                hdn_AttachBranchId.Value = DR["Branch_Id"].ToString();
            }

            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_AttachBranch.Attributes.Add("onblur", "On_LostFocus('" + txt_AttachBranch.ClientID + "','" + lst_AttachBranch.ClientID + "','" + hdn_AttachBranchId.ClientID + "')");
                txt_AttachBranch.Attributes.Add("onkeyup", "Search_Branch(event,this,'" + lst_AttachBranch.ClientID + "','AttachedBranch',2)");
                txt_AttachBranch.Attributes.Add("onfocus", "On_Focus('" + txt_AttachBranch.ClientID + "','" + lst_AttachBranch.ClientID + "')");
                txt_AttachBranch.Attributes.Add("onkeydown", "return on_keydown(event,this,'" + lst_AttachBranch.ClientID + "')");

                lst_AttachBranch.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_AttachBranch.ClientID + "','" + txt_AttachBranch.ClientID + "')");
                lst_AttachBranch.Attributes.Add("onfocus", "listboxonfocus('" + txt_AttachBranch.ClientID + "')");

                lst_AttachBranch.Style.Add("visibility", "hidden");
            }
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
            if (Mode == "2")
            {
                if (Session_LHPOID > 0)
                {
                    e.Item.Cells[1].Enabled = false;
                    e.Item.Cells[2].Enabled = false;
                }
            }
        }
    }


    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DVLPAB"]);
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            txt_AttachBranch = (TextBox)(e.Item.FindControl("txt_AttachBranch"));
            hdn_AttachBranchId = (HiddenField)(e.Item.FindControl("hdn_AttachBranchId"));
            lst_AttachBranch = (ListBox)(e.Item.FindControl("lst_AttachBranch"));

            DR = DT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            txt_AttachBranch = (TextBox)(e.Item.FindControl("txt_AttachBranch"));
            hdn_AttachBranchId = (HiddenField)(e.Item.FindControl("hdn_AttachBranchId"));
            lst_AttachBranch = (ListBox)(e.Item.FindControl("lst_AttachBranch"));

            DR = DT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Branch_Id"] = hdn_AttachBranchId.Value;
            DR["Branch_Name"] = txt_AttachBranch.Text;

            if (e.CommandName == "Add") { DT.Rows.Add(DR); }
            Session["DVLPAB"] = DT;
        }

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Footer)
            {
                txt_AttachBranch.Attributes.Add("onblur", "On_LostFocus('" + txt_AttachBranch.ClientID + "','" + lst_AttachBranch.ClientID + "','" + hdn_AttachBranchId.ClientID + "')");
                txt_AttachBranch.Attributes.Add("onkeyup", "Search_Branch(event,this,'" + lst_AttachBranch.ClientID + "','AttachedBranch',2)");
                txt_AttachBranch.Attributes.Add("onfocus", "On_Focus('" + txt_AttachBranch.ClientID + "','" + lst_AttachBranch.ClientID + "')");
                txt_AttachBranch.Attributes.Add("onkeydown", "return on_keydown(event,this,'" + lst_AttachBranch.ClientID + "')");

                lst_AttachBranch.Attributes.Add("onkeydown", "listboxonfocus(event,'" + lst_AttachBranch.ClientID + "','" + txt_AttachBranch.ClientID + "')");
                lst_AttachBranch.Attributes.Add("onfocus", "listboxonfocus('" + txt_AttachBranch.ClientID + "')");

                //lst_AttachBranch.Style.Add("visibility", "hidden");
            }
        }

        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CallMyFunction", "TDSCertificateToChange();", true);
    }

    private bool Allow_To_Add_Update()
    {
        DataTable DT = (DataTable)(Session["DVLPAB"]);

        DataRow[] result = DT.Select("Branch_Id=" + hdn_AttachBranchId.Value);

        if (Util.String2Int(hdn_AttachBranchId.Value) <= 0)
        {
            lblErrors.Text = "Please select branch";
            txt_AttachBranch.Focus();
        }
        else if (result.Length > 0)
        {
            lblErrors.Text = "Branch already selected";
        }
        else if (hdn_AttachBranchId.Value == hdn_Tobranch.Value)
        {
            lblErrors.Text = "Attached Branch and To Branch cannot be same";
        }
        else
            ATS = true;

        return ATS;
    }
    #endregion

    private void BindAttachedBranchGrid()
    {
        dgGrid.DataSource = (DataTable)(Session["DVLPAB"]);
        dgGrid.DataBind();
    }

    protected void btn_hidden_Click(object sender, EventArgs e)
    {
        if (FromBranchID > 0)
        {
            Common obj = new Common();
            DataSet ds = obj.EC_Common_Pass_Query("select area_id from dbo.ec_master_branch where branch_id = " + FromBranchID.ToString());

            WucHierarchyWithIDATH.AreaID = Util.String2Int(ds.Tables[0].Rows[0][0].ToString());
            WucHierarchyWithIDBTH.AreaID = WucHierarchyWithIDATH.AreaID;
            FromBranchID = Util.String2Int(hdnFrombranch.Value);
            txt_FromBranch.Focus();
            if (hdn_VehicleCategoryIds.Value == "5")
                trBrokerName.Visible = true;
            else
                trBrokerName.Visible = false;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CallMyFunction", "TDSCertificateToChange();", true);
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
            //hdnDriverpath.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(107).AddUrl + "&Call_From=LHPO";
            hdnDriverpath.Value = "../../Master/Driver/FrmDriverShortForm.aspx?Menu_Item_Id=MQAwADcA&Mode=MQA=&Call_From=LHPO";
            lnkAddDriver.Visible = true;
        }
        else
        {
            hdnDriverpath.Value = "";
        }

        //Broker

        fRights = uObj.getForm_Rights(69);
        can_add = fRights.canAdd();
        lnkAddBroker.Visible = false;
        if (can_add == true)
        {
            StateManager.SaveState("QueryString", "2");
            hdnBrokerPath.Value = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(69).AddUrl + "&Call_From=DVLP";

            lnkAddBroker.Visible = true;
        }
        else
        {
            hdnBrokerPath.Value = "";
        }
    }

    private bool ValidateUI()
    {
        bool ATS = false;

        DataTable DT = (DataTable)(Session["DVLPAB"]);

        DataRow[] result = DT.Select("Branch_Id=" + Util.String2Int(hdn_Tobranch.Value).ToString());

        if (DDLVehicle.VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle No";
        }
        else if (Datemanager.IsValidProcessDate("OPR_MEMO", dtpLoadingDate.SelectedDate) == false)
        {
            lblErrors.Text = "Please select valid Loading Date";
        }
        else if (Util.String2Int(hdnKeyID.Value) <= 0 && !IsVehicleValid())
        {
            lblErrors.Text = "Daily vehicle loading plan created with this vehicle has not been used in any Trip Memo. Please create Trip Memo first";
        }
        //else if (BrokerID <= 0)
        //{
        //    lblErrors.Text = "Please Select Broker";
        //    txtBroker.Focus();
        //}
        else if (DDLVehicle.VehicleCategoryIds == "5" && BrokerID <= 0)
        {
            lblErrors.Text = "Please Select Broker";
            txtBroker.Focus();
        }
        else if (FromBranchID <= 0)
        {
            lblErrors.Text = "Please Select From branch";
            txt_FromBranch.Focus();
        }
        else if (ToBranchID <= 0)
        {
            lblErrors.Text = "Please Select To branch";
            txt_ToBranch.Focus();
        }
        else if (result.Length > 0)
        {
            lblErrors.Text = "To Branch and Attached Branch cannot be same";
        }
        else if ((FromBranchID > 0) && (ToBranchID > 0)
           && (FromBranchID == ToBranchID))
        {
            lblErrors.Text = "From branch and To branch cannot be same";
            txt_ToBranch.Focus();
        }
        else if (DriverID <= 0)
        {
            lblErrors.Text = "Please Select Driver";
            txtDriver.Focus();
        }
        else if (txt_MobileNo1.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Driver Mobile No. 1";
            txt_MobileNo1.Focus();
        }
        else if (Util.String2Int(ddlTDSCertificateTo.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select TDS Certificate to";
        }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            Save();
        }
    }

    public Message Save()
    {
        DataTable DT = (DataTable)(Session["DVLPAB"]);
        DT.TableName = "AttachedBranches";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string attachedBrachesXML = ds.GetXml().ToLower();

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeOutParams("@DailyVehicleLoadingPlanID", SqlDbType.Int, 0), 
                objDAL.MakeInParams("@KeyID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)),
                objDAL.MakeInParams("@LoadingDate",SqlDbType.DateTime,0,dtpLoadingDate.SelectedDate),
                objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,DDLVehicle.VehicleID),
                objDAL.MakeInParams("@BrokerID",SqlDbType.Int,0,BrokerID),
                objDAL.MakeInParams("@FromBranchID",SqlDbType.Int,0,FromBranchID),
                objDAL.MakeInParams("@ToBranchID",SqlDbType.Int,0,ToBranchID),
                objDAL.MakeInParams("@DriverID",SqlDbType.Int,0,DriverID),
                objDAL.MakeInParams("@TDSCertificateTo",SqlDbType.TinyInt,0,Util.String2Int(ddlTDSCertificateTo.SelectedValue)),
                objDAL.MakeInParams("@IsRCRecieved",SqlDbType.Bit,0,chkIsRCRecieved.Checked),
                objDAL.MakeInParams("@IsPanCardRecieved",SqlDbType.Bit,0,chkIsPanCardRecieved.Checked),
                objDAL.MakeInParams("@HireAmount",SqlDbType.Decimal,0,Util.String2Decimal(txtHireAmount.Text)),
                objDAL.MakeInParams("@TDSPercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnTDSPercent.Value)),
                objDAL.MakeInParams("@TDSAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnTDSAmount.Value)),
                objDAL.MakeInParams("@SurchargePercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnSurchargePercent.Value)),
                objDAL.MakeInParams("@SurchargeAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnSurChargeAmount.Value)),
                objDAL.MakeInParams("@AdditionalSurchargeCessPercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnAdditionalSurchargeCessPercent.Value)),
                objDAL.MakeInParams("@AdditionalSurchargeCessAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnAdditionalSurchargeCessAmount.Value)),
                objDAL.MakeInParams("@AdditionalEducationCessPercent",SqlDbType.Decimal,0,Util.String2Decimal(hdnAddistionalEducationCessPercent.Value)),
                objDAL.MakeInParams("@AdditionalEducationCessAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnAddistionalEducationCessAmount.Value)),
                objDAL.MakeInParams("@TotalTDSAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnTotalTDSAmount.Value)),
                objDAL.MakeInParams("@TruckHirePayable",SqlDbType.Decimal,0,Util.String2Decimal(hdnTruckHirePayable.Value)),
                objDAL.MakeInParams("@AdvanceAmount",SqlDbType.Decimal,0,Util.String2Decimal(txtAdvance.Text)),
                objDAL.MakeInParams("@BalanceAmount",SqlDbType.Decimal,0,Util.String2Decimal(hdnBalance.Value)),
                objDAL.MakeInParams("@ATHPayableHierarchyCode",SqlDbType.VarChar,2,WucHierarchyWithIDATH.HierarchyCode),
                objDAL.MakeInParams("@ATHPayableLocationID",SqlDbType.Int,0,WucHierarchyWithIDATH.MainId),
                objDAL.MakeInParams("@BTHPayableHierarchyCode",SqlDbType.VarChar,2,WucHierarchyWithIDBTH.HierarchyCode),
                objDAL.MakeInParams("@BTHPayableLocationID",SqlDbType.Int,0,WucHierarchyWithIDBTH.MainId),
                objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,100,txtRemarks.Text),
                objDAL.MakeInParams("@DivisionID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                objDAL.MakeInParams("@AttachedBrachesXML",SqlDbType.Xml,0,attachedBrachesXML),
                objDAL.MakeInParams("@Mobile_No_1",SqlDbType.VarChar  ,15,txt_MobileNo1.Text),
                objDAL.MakeInParams("@Mobile_No_2",SqlDbType.VarChar  ,15,txt_MobileNo2.Text),
                objDAL.MakeInParams("@Cleaner_ID",SqlDbType.Int,0,CleanerID),
                objDAL.MakeInParams("@Mobile_No_1CL",SqlDbType.VarChar  ,15,txt_MobileNo1CL.Text),
                objDAL.MakeInParams("@Mobile_No_2CL",SqlDbType.VarChar  ,15,txt_MobileNo2CL.Text),
                objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};


        objDAL.RunProc("dbo.EC_Opr_DailyVehicleLoadingPlan_Save", objSqlParam);


        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
            string _Msg;
            _Msg = "Saved SuccessFully";
            lblErrors.Text = _Msg;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    private bool IsVehicleValid()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@IsVehicleValid", SqlDbType.Bit, 0), 
                objDAL.MakeInParams("@Vehicleid",SqlDbType.Int,0,DDLVehicle.VehicleID),
        objDAL.MakeInParams("@MenuitemID",SqlDbType.Int,0,Common.GetMenuItemId())};

        objDAL.RunProc("dbo.CheckVehicleValidity", objSqlParam);

        bool isVehicleValid = Convert.ToBoolean(objSqlParam[0].Value);

        return isVehicleValid;
    }
}
