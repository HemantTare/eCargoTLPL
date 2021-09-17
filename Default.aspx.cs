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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

//using System;
//using System.Data; 
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Collections;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using System.Text;
//using System.Text.RegularExpressions;
//using ClassLibraryMVP;
//using ClassLibraryMVP.Security;
//using ClassLibraryMVP.DataAccess;
//using Raj.EC.OperationPresenter;
//using Raj.EC.OperationView;

///// <summary>
///// Author        : Shiv kumar mishra
///// Created On    : 10/11/2008
///// Description   : This Page is For DDS Operation
///// </summary>
///// 

//public partial class Operations_Delivery_WucDDC : System.Web.UI.UserControl, IDDCView
//{
//    #region ClassVariables
//    DDCPresenter objDDCPresenter;
//    private IDDCView objIDDCView;
//    Raj.EC.Common objComm = new Raj.EC.Common();
//    DataTable objDT = new DataTable();
//    private DataSet objDS;
//    string _flag = "";
//    string Mode = "0";
//    CheckBox chk;
//    HiddenField hdn_StatusID, hdn_Status, hdn_Actual_Status_Id;
//    TextBox txt_Del_TakenBy, txt_Actual_Status, txt_Del_taken_by;
//    DropDownList ddl_Undelivered_Reason;
//    TimePicker Timepicker1;
//    ComponentArt.Web.UI.Calendar dtp_DD_Date;
//    int ds_index, i;
//    private DAL objDAL = new DAL();
//    private int _branchID = UserManager.getUserParam().MainId;
//    private int _divisionID = UserManager.getUserParam().DivisionId;
//    private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
    
//    #endregion

//    #region ControlsValues

//    public string DDSNo
//    {
//        set { lbl_DDC_No.Text = value; }
//    }
//    public DateTime DDSDate
//    {
//        set { dtp_DDS_Date.SelectedDate = value; } 
//        get { return dtp_DDS_Date.SelectedDate; }
//    }
//    public int DeliveryModeID
//    {
//        get { return Util.String2Int(ddl_DeliveryMode.SelectedValue); }
//        set { ddl_DeliveryMode.SelectedValue = Util.Int2String(value); }
//    }
//    public string DeliveryModeDescription
//    {
//        get { return ddl_DeliveryMode.SelectedItem.Text; }
//        set { ddl_DeliveryMode.SelectedItem.Text = value; }
//    }
//    public string PDSDate
//    {
//        set { lbl_PDSDate.Text = value; }
//        get { return lbl_PDSDate.Text; }
//    }
//    public int PDSId
//    {
//        get { return Util.String2Int(ddl_PDSNo.SelectedValue); }
//        set { ddl_PDSNo.SelectedValue = Util.Int2String(value); }
//    }
//    public string Remarks
//    {
//        set { txt_Remarks.Text = value; }
//        get { return txt_Remarks.Text; }
//    }
//    public int Total_Delivered_Articles
//    {
//        set
//        {
//            hdn_totalDelArt.Value = Util.Int2String(value);
//            lbl_totalDelArt.Text = Util.Int2String(value);
//        }
//    }
//    public decimal Total_Delivered_Weight
//    {
//        set
//        {
//            hdn_totalDelWt.Value = Util.Decimal2String(value);
//            lbl_totalDelWt.Text = Util.Decimal2String(value);
//        }
//    }
//    public int Total_No_Of_GC
//    {
//        set
//        {
//            hdn_Total_GC.Value = Util.Int2String(value);
//            //lbl_Total_GC.Text = Util.Int2String(value);
//        }
//        get { return Util.String2Int(hdn_Total_GC.Value); }
//    }
//    public string Flag
//    {
//        get { return _flag; }
//    }

//    public int VehicleID
//    {
//        get { return WucVehicleSearch1.VehicleID; }
//        set
//        {
//            WucVehicleSearch1.VehicleID = value;
//            hdn_VehicleID.Value = Util.Int2String(value);
//        }
//    }
//    public int VendorID
//    {
//        get { return WucVehicleSearch1.VehicleVendorID; }
//        set
//        {
//            WucVehicleSearch1.VehicleVendorID = value;
//            hdn_Vendor_ID.Value = Util.Int2String(value);
//        }
//    }
//    public string VendorName
//    {
//        get { return lbltxt_Vendor.Text; }
//        set { lbltxt_Vendor.Text = value; }
//    }
//    #endregion

//    #region ControlsBind

//    public DataTable BindDeliveryMode
//    {
//        set
//        {
//            ddl_DeliveryMode.DataTextField = "Delivery_Mode";
//            ddl_DeliveryMode.DataValueField = "Delivery_Mode_ID";
//            ddl_DeliveryMode.DataSource = value;
//            ddl_DeliveryMode.DataBind();
//            ddl_DeliveryMode.Items.Insert(0, new ListItem("-- Select One --", "0"));
//        }
//    }

//    public DataTable BindPreDeliverySheet
//    {
//        set
//        {
//            ddl_PDSNo.DataTextField = "PDS_No";
//            ddl_PDSNo.DataValueField = "PDS_ID";
//            ddl_PDSNo.DataSource = value;
//            ddl_PDSNo.DataBind();
//            if (keyID < 0)
//            {
//                ddl_PDSNo.Items.Insert(0, new ListItem("Select PDS No", "0"));
//            }
//        }
//    }

//    public void BindDDSGrid()
//    {
//        dg_DDS.DataSource = SessionBindDDSGrid;
//        dg_DDS.DataBind();
//    }

//    public DataTable SessionBindDDLUndelReason
//    {
//        get { return StateManager.GetState<DataTable>("BindDDLUndelReason"); }
//        set { StateManager.SaveState("BindDDLUndelReason", value); }
//    }
//    public DataTable SessionBindDDSGrid
//    {
//        get { return StateManager.GetState<DataTable>("BindDDSGrid"); }
//        set
//        {
//            StateManager.SaveState("BindDDSGrid", value);
//            if (StateManager.Exist("BindDDSGrid"))
//            {
//                BindDDSGrid();
//                hdnforselectall.Value = Util.Int2String(SessionBindDDSGrid.Rows.Count);
//            }
//        }
//    }
//    public DataTable SessionBindDDLDeliveryMode
//    {
//        get { return StateManager.GetState<DataTable>("DDLDeliveryMode"); }
//        set { StateManager.SaveState("DDLDeliveryMode", value); }
//    }


//    public String DDSDetailsXML
//    {
//        get
//        {
//            DataSet _objDs = new DataSet();
//            _objDs.Tables.Add(SessionBindDDSGrid.Copy());

//            _objDs.Tables[0].TableName = "DDCGrid_Details";
//            return _objDs.GetXml().ToLower();
//        }
//    }

//    #endregion

//    #region IView
//    public bool validateUI()
//    {
//        bool _isValid = false;

//        if (Datemanager.IsValidProcessDate("OPR_DDC", DDSDate) == false)
//        {
//            errorMessage = "Please Select Valid DDC Date";// GetLocalResourceObject("Msg_dtp_DDSDate").ToString();
//        }
//        else if (PDSId <= 0)
//        {
//            errorMessage = "Please Select PDS No";// GetLocalResourceObject("Msg_PDS_validation").ToString();
//            ddl_PDSNo.Focus();
//        }
//        else if (DeliveryModeID <= 0)
//        {
//            errorMessage = "Please Select Delivery Mode";// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
//            ddl_DeliveryMode.Focus();
//        }
//        else if (SessionBindDDSGrid.Rows.Count <= 0)
//        {
//            errorMessage = "Please Select Atleast One "+ CompanyManager.getCompanyParam().GcCaption;
//        }
//        else if (grid_validation() == false)
//        {
//        }
//        else
//        {
//            _isValid = true;
//        }

//        return _isValid;
//    }

//    public string errorMessage
//    {
//        set { lbl_Errors.Text = value; }
//    }

//    public int keyID
//    {
//        get
//        {
//            return Util.DecryptToInt(Request.QueryString["Id"]);
//        }
//    }
//    #endregion

//    #region OtherMethod
//    private void Next_DDS_Number()
//    {
//        DDSNo = objComm.Get_Next_Number();
//    }

//    private void Assign_Hidden_Values_To_TextBox()
//    {
//        //lbl_Total_GC.Text = hdn_Total_GC.Value;
//        lbl_totalDelArt.Text = hdn_totalDelArt.Value;
//        lbl_totalDelWt.Text = hdn_totalDelWt.Value;
//    }

//    private void Assign_Hidden_Values_For_Reset()
//    {
//        hdn_Total_GC.Value = "0";
//        hdn_totalDelArt.Value = "0";
//        hdn_totalDelWt.Value = "0";

//        //lbl_Total_GC.Text = "0";
//        lbl_totalDelArt.Text = "0";
//        lbl_totalDelWt.Text = "0";
//    }
//    private void SetStandardCaption()
//    {
//        const int GCNoCaption = 1;       //change usermanager to companymanager by Ankit 
//        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
//        dg_DDS.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";
//    }
//    #endregion

//    #region OtherProperty

//    private bool grid_validation()
//    {
//        bool ATS = true;
//        string GC_No;

//        objDT = SessionBindDDSGrid;

//        if (objDT.Rows.Count > 0)
//        {

//            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
//            {
//                txt_Del_TakenBy = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_TakenBy");
//                dtp_DD_Date = (ComponentArt.Web.UI.Calendar)dg_DDS.Items[i].FindControl("dtp_DD_Date");
//                Timepicker1 = (TimePicker)dg_DDS.Items[i].FindControl("TimePicker1");
//                ddl_Undelivered_Reason = (DropDownList)dg_DDS.Items[i].FindControl("ddl_UnDel_Reason");
//                hdn_Actual_Status_Id = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Actual_Status_Id");
//                GC_No = dg_DDS.Items[i].Cells[1].Text;

//                string Del_datetime, unlod_datetime;
//                DateTime dt_Deldatetime, dt_unloddatetime;

//                Del_datetime = dtp_DD_Date.SelectedDate.ToShortDateString() + " " + Timepicker1.getTime();
//                unlod_datetime = objDT.Rows[i]["AUS_Date"].ToString() + " " + objDT.Rows[i]["AUS_Time"].ToString();

//                dt_Deldatetime = Convert.ToDateTime(Del_datetime);
//                dt_unloddatetime = Convert.ToDateTime(unlod_datetime);


//                if (Convert.ToDateTime(dtp_DD_Date.SelectedDate) < Convert.ToDateTime(PDSDate))
//                {
//                    errorMessage = "Door Delivery Date should be greater than PDS Date";
//                    ATS = false;
//                    break;
//                }
//                else if (Convert.ToDateTime(dtp_DD_Date.SelectedDate) > Convert.ToDateTime(DDSDate))
//                {
//                    errorMessage = "Door Delivery Date should be less than DDC Date";
//                    ATS = false;
//                    break;
//                }
//                else if (Util.String2Int(hdn_Actual_Status_Id.Value) == 300 && Util.String2Int(ddl_Undelivered_Reason.SelectedValue) == 0)
//                {
//                    errorMessage = "Please Select Undelivered Reason";
//                    scm_dds.SetFocus(ddl_Undelivered_Reason);
//                    ATS = false;
//                    break;
//                }
//                else if (dt_Deldatetime < dt_unloddatetime)
//                {
//                    errorMessage = "Delivery Date & Time Can't be less than Actual Unloading Date & Time";
//                    ATS = false;
//                    break;
//                }
//                //else if (Util.String2Int(hdn_Actual_Status_Id.Value) != 300 && txt_Del_TakenBy.Text.Trim() == string.Empty)
//                //{
//                //    errorMessage = "Please Enter Delivery Taken By";
//                //    scm_dds.SetFocus(txt_Del_TakenBy);
//                //    ATS = false;
//                //    break;
//                //}
//                else if (Util.String2Bool(objDT.Rows[i]["IsDelDetailsReq"].ToString()) == true && Util.String2Int(objDT.Rows[i]["is_updated"].ToString()) == 0)
//                {
//                    errorMessage = "Please Mention Delivery Details, For " + CompanyManager.getCompanyParam().GcCaption + " :" + GC_No;
//                    ATS = false;
//                    break;
//                }
//                else
//                {
//                    ATS = true;
//                }
//            }
//            if (ATS == true)
//            {
//                calculate_griddetails();
//            }
//        }
//        return ATS;
//    }

//    private void calculate_griddetails()
//    {
//        if (dg_DDS.Items.Count > 0)
//        {
//            objDT = SessionBindDDSGrid;

//            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
//            {
//                chk = (CheckBox)dg_DDS.Items[i].FindControl("Chk_Attach");
//                txt_Del_TakenBy = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_TakenBy");
//                ddl_Undelivered_Reason = (DropDownList)dg_DDS.Items[i].FindControl("ddl_UnDel_Reason");
//                Timepicker1 = (TimePicker)dg_DDS.Items[i].FindControl("TimePicker1");
//                dtp_DD_Date = (ComponentArt.Web.UI.Calendar)dg_DDS.Items[i].FindControl("dtp_DD_Date");
//                hdn_StatusID = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Status_Id");
//                hdn_Status = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Status");

//                if (chk.Checked == true)
//                {
//                    objDT.Rows[i]["Actual_Status_ID"] = Util.String2Int(hdn_StatusID.Value);
//                    objDT.Rows[i]["Actual_Status"] = hdn_Status.Value;
//                }
//                else
//                {
//                    objDT.Rows[i]["Actual_Status_ID"] = 300;
//                    objDT.Rows[i]["Actual_Status"] = "UnDelivered";
//                }
//                objDT.Rows[i]["Att"] = chk.Checked;
//                //objDT.Rows[i]["Delivery_Taken_By"] = txt_Del_TakenBy.Text;
//                objDT.Rows[i]["UnDelivered_Reason_Id"] = Util.String2Int(ddl_Undelivered_Reason.SelectedValue);
//                objDT.Rows[i]["Delivery_Time"] = Timepicker1.getTime();
//                objDT.Rows[i]["Delivery_Date"] = dtp_DD_Date.SelectedDate;
//            }
//        }

//    }



//    #endregion
//    protected void Page_PreRender(object sender, EventArgs e)
//    {
//        if (Mode == "4")
//        {
//            btn_Close.Visible = true;
//            btn_Close.Enabled = true;
//            TD_Calender.Visible = false;
//            dtp_DDS_Date.Enabled = false;
//        }
//    }

//    protected void Page_Load(object sender, EventArgs e)
//    {
//        SetPostBackValues(); 
//        SetStandardCaption();
//        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
//        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
//        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save_Exit, btn_Save, btn_Close));

//        WucVehicleSearch1.Can_Add_Vehicle = false;
//        WucVehicleSearch1.Can_View_Vehicle = true;
//        WucVehicleSearch1.TransactionDate = dtp_DDS_Date.SelectedDate;
//        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

       
//        if (!IsPostBack)
//        {
//            Assign_Hidden_Values_For_Reset();
//        }
//        //if (WucVehicleSearch1.VehicleID > 0)
//        //{ VehicleID = WucVehicleSearch1.VehicleID; }
//        //else
//        //{ VehicleID = 0; }
//        objDDCPresenter = new DDCPresenter(this, IsPostBack, VehicleID);

//        if (!IsPostBack)
//        {
//            //hdf_ResourecString.Value = objComm.GetResourceString("Operations/Delivery/App_LocalResources/WucDDC.ascx.resx");
//            if (keyID <= 0)
//            {
//                Next_DDS_Number();
//            }
//        }
//        Assign_Hidden_Values_To_TextBox();

//    }

//    private void DeliveryModeChange()
//    {
//        if (ddl_DeliveryMode.SelectedValue == "2")
//        {
//            //lbl_DriverName.Text = "Driver Name";
//        }
//        if (ddl_DeliveryMode.SelectedValue == "3")
//        {
//            //lbl_DriverName.Text = "Hand Cart No";
//        }
//        if (ddl_DeliveryMode.SelectedValue == "4")
//        {
//            //lbl_DriverName.Text = "Person Name";
//        }


//    }

//    private void SetPostBackValues()
//    {
//        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
//        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
//    }

//    private void OnDDLVehicleSelection(object sender, EventArgs e)
//    {
//        if (Mode == "2" || Mode == "4")
//        {
//            if (IsPostBack)
//            {
//                VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
//                VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
//            }
//        }
//        else
//        {
//            VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
//            VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
//            VehicleID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vehicle_ID"));
//        }

//        //if (VehicleID >  0 )
//        //{ BindPDSNoFillValues(); }
//        //else
//        //{};
//    }
    
//    public void BindPDSNoFillValues()
//    {
//        DataSet _objDs = new DataSet();
//        _objDs = FillPDSNoValues();
//        //objIDDCView.BindPreDeliverySheet = _objDs.Tables[0];
//    }

//    public DataSet FillPDSNoValues()
//    {
//        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
//            objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, -1) ,
//            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
//            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime, 0, dtp_DDS_Date.SelectedDate) ,
//            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
//            objDAL.MakeInParams("@VehicleID", SqlDbType.VarChar, 5, WucVehicleSearch1.VehicleID)};
//        objDAL.RunProc("dbo.EC_Opr_DDC_FillValues", objSqlParam, ref objDS);
//        return objDS;
//    }

//    #region otherControls
//    protected void btn_Save_Click(object sender, EventArgs e)
//    {
//        errorMessage = "";
//        _flag = "SaveAndNew";
//        objDDCPresenter.save();
//    }
//    protected void btn_Save_Exit_Click(object sender, EventArgs e)
//    {
//        errorMessage = "";
//        _flag = "SaveAndExit";
//        objDDCPresenter.save();
//    }
//    protected void btn_Close_Click(object sender, EventArgs e)
//    {
//        Response.Write("<script language='javascript'>{self.close()}</script>");
//    }
//    protected void btn_Save_Print_Click(object sender, EventArgs e)
//    {
//        errorMessage = "";
//        _flag = "SaveAndPrint";
//        objDDCPresenter.save();
//    }

//    protected void ddl_PDSNo_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        Assign_Hidden_Values_For_Reset();
//        Assign_Hidden_Values_To_TextBox();
//        objDDCPresenter.FillGrid();

//        ddl_PDSNo.Items.Remove(new ListItem("Select PDS No", "0"));
//    }

//    protected void dtp_DDS_Date_SelectionChanged(object sender, EventArgs e)
//    {
//        Assign_Hidden_Values_For_Reset();
//        Assign_Hidden_Values_To_TextBox();
//        objDDCPresenter.FillValues(WucVehicleSearch1.VehicleID);
//        objDDCPresenter.FillGrid();
//    }
//    #endregion

//    protected void dg_DDS_ItemDataBound(object sender, DataGridItemEventArgs e)
//    {
//        LinkButton lbtn_Details;

//        if (e.Item.ItemIndex != -1)
//        {
//            chk = (CheckBox)e.Item.FindControl("Chk_Attach");
//            Timepicker1 = (TimePicker)e.Item.FindControl("TimePicker1");
//            dtp_DD_Date = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_DD_Date");
//            ddl_Undelivered_Reason = (DropDownList)e.Item.FindControl("ddl_UnDel_Reason");
//            hdn_StatusID = (HiddenField)e.Item.FindControl("hdn_Status_Id");
//            lbtn_Details = (LinkButton)e.Item.FindControl("lbtn_details");
//            txt_Del_taken_by = (TextBox)e.Item.FindControl("txt_Delivery_TakenBy");

//            Timepicker1.setFormat("24");

//            ddl_Undelivered_Reason.DataTextField = "Reason";
//            ddl_Undelivered_Reason.DataValueField = "Reason_ID";
//            ddl_Undelivered_Reason.DataSource = SessionBindDDLUndelReason;
//            ddl_Undelivered_Reason.DataBind();
//            ddl_Undelivered_Reason.Items.Insert(0, new ListItem("Select One", "0"));

//            Timepicker1.setTime(SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Delivery_Time"].ToString());
//            dtp_DD_Date.SelectedDate = Convert.ToDateTime(SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Delivery_Date"].ToString());
//            ddl_Undelivered_Reason.SelectedValue = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["UnDelivered_Reason_Id"].ToString();

//            ds_index = e.Item.ItemIndex;
//            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
//            Path.Append("/");
//            Path.Append("Operations/Delivery/FrmGDCGridDetails.aspx?ds_index=" + Util.EncryptInteger(ds_index) + "&_menuItemid=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Mode=" + Mode);
//            lbtn_Details.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");

//            if (chk.Checked == true)
//            {
//                Chk_Attach_CheckedChanged(chk, e);
//            }
//            else
//            {
//                txt_Del_taken_by.Text = "";
//                txt_Del_taken_by.Enabled = false;
//                //lbtn_Details.Enabled = false;
//                e.Item.Cells[12].Enabled = false;
//            }
//        }
//    }

//    protected void Chk_Attach_CheckedChanged(object sender, EventArgs e)
//    {
//        TextBox txt_del_art, txt_del_wt;
//        //LinkButton lbtn_DelDetails;

//        chk = (CheckBox)sender;
//        DataGridItem _item = (DataGridItem)chk.Parent.Parent;

//        txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
//        hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
//        hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
//        hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
//        txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
//        ddl_Undelivered_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
//        txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
//        txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
//        //lbtn_DelDetails = (LinkButton)_item.FindControl("lbtn_details");

//        if (chk.Checked == true)
//        {
//            txt_Actual_Status.Text = hdn_Status.Value;
//            hdn_Actual_Status_Id.Value = hdn_StatusID.Value;
//            ddl_Undelivered_Reason.SelectedValue = "0";
//            ddl_Undelivered_Reason.Enabled = false;
//            txt_Del_taken_by.Enabled = true;
//            //lbtn_DelDetails.Enabled = true;
//            _item.Cells[12].Enabled = true;
//            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) + Util.String2Decimal(txt_del_art.Text));
//            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) + Util.String2Decimal(txt_del_wt.Text));

//        }
//        else
//        {
//            txt_Actual_Status.Text = "UnDelivered";
//            hdn_Actual_Status_Id.Value = "300";
//            ddl_Undelivered_Reason.Enabled = true;
//            txt_Del_taken_by.Enabled = false;
//            txt_Del_taken_by.Text = "";
//            //lbtn_DelDetails.Enabled = false;
//            _item.Cells[12].Enabled = false;
//            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) - Util.String2Decimal(txt_del_art.Text));
//            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) - Util.String2Decimal(txt_del_wt.Text));

//        }
//        Assign_Hidden_Values_To_TextBox();
//    }

//    public void ClearVariables()
//    {
//        SessionBindDDSGrid = null;
//        SessionBindDDLUndelReason = null;
//    }
//    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit : 21-02-09
//    {
//        ClearVariables();
//    }

//    protected void ddl_DeliveryMode_SelectedIndexChanged(object sender, EventArgs e)
//    {
//        WucVehicleSearch1.VehicleID = 0;
//        VendorName = "";
//        VendorID = 0;
//        DeliveryModeChange();
//    }
//}

///----========= Presenter ===
///using System;
//using System.Data;

//using ClassLibraryMVP;
//using ClassLibraryMVP.General;
//using Raj.EC.OperationView;
//using Raj.EC.OperationModel;

//namespace Raj.EC.OperationPresenter
//{
//    public class DDCPresenter : Presenter
//    {
//        private IDDCView objIDDCView;
//        private DDCModel objDDCModel;
//        private DataSet objDS;
//        //public int VehicleID;

//        public DDCPresenter(IDDCView DDCView, bool isPostBack, int VehicleID)
//        {
//            objIDDCView = DDCView;
//            objDDCModel = new DDCModel(objIDDCView);
//            base.Init(objIDDCView, objDDCModel);
//            VehicleID = objIDDCView.VehicleID;
//            if (!isPostBack)
//            {
//                objIDDCView.DDSDate = DateTime.Now.Date;
//                FillDeliveryModeValues();
//                //FillValues();
//                initValues();
//            }
//            FillValues(VehicleID);
//            //initValues();
//        }

//        public void FillDeliveryModeValues()
//        {
//            objDS = objDDCModel.FillDeliveryModeValues();
//            objIDDCView.SessionBindDDLDeliveryMode = objDS.Tables[0];
//        }

//        public void FillValues(int VehicleID)
//        {
//            objDS = objDDCModel.FillValues(VehicleID);
//            objIDDCView.BindPreDeliverySheet = objDS.Tables[0];
//            objIDDCView.SessionBindDDLUndelReason = objDS.Tables[1];
//            objIDDCView.BindDeliveryMode = objDS.Tables[2];
//        }

//        public void FillGrid()
//        {
//            objDS = objDDCModel.ReadValues();
//            objIDDCView.SessionBindDDSGrid = objDS.Tables[0];

//            if (objDS.Tables[0].Rows.Count > 0)
//                objIDDCView.PDSDate = objDS.Tables[0].Rows[0]["PDS_Date"].ToString();
//            else
//                objIDDCView.PDSDate = string.Empty;

//            if (objIDDCView.keyID > 0)
//            {
//                if (objDS.Tables[1].Rows.Count > 0)
//                {
//                    DataRow objDR = objDS.Tables[1].Rows[0];

//                    objIDDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
//                    objIDDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
//                    objIDDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());
//                }
//            }
//        }

//        private void initValues()
//        {
//            objDS = objDDCModel.ReadValues();
//            objIDDCView.SessionBindDDSGrid = objDS.Tables[0];

//            if (objDS.Tables[0].Rows.Count > 0)
//                objIDDCView.PDSDate = objDS.Tables[0].Rows[0]["PDS_Date"].ToString();
//            else
//                objIDDCView.PDSDate = string.Empty;

//            if (objIDDCView.keyID > 0)
//            {
//                if (objDS.Tables[1].Rows.Count > 0)
//                {
//                    DataRow objDR = objDS.Tables[1].Rows[0];

//                    objIDDCView.DDSDate = Convert.ToDateTime(objDR["DDC_Date"].ToString());
//                    objIDDCView.DDSNo = objDR["DDC_No_For_Print"].ToString();
//                    objIDDCView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
//                    objIDDCView.Total_Delivered_Articles = Util.String2Int(objDR["Total_DDC_Articles"].ToString());
//                    objIDDCView.Total_Delivered_Weight = Util.String2Decimal(objDR["Total_DDC_Actual_Wt"].ToString());
//                    objIDDCView.Remarks = objDR["Remarks"].ToString();
//                }
//            }
//        }

//        public void save()
//        {
//            base.DBSave();
//        }
//    }
//}


///// Model

//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
//using Raj.EC.OperationView;

//using ClassLibraryMVP;
//using ClassLibraryMVP.General;
//using ClassLibraryMVP.DataAccess;

//namespace Raj.EC.OperationModel
//{
//    public class DDCModel : IModel
//    {
//        private IDDCView objIDDCView;
//        private DAL objDAL = new DAL();
//        private DataSet objDS;

//        private int _branchID = UserManager.getUserParam().MainId;
//        private int _divisionID = UserManager.getUserParam().DivisionId;
//        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;


//        public DDCModel(IDDCView DDCView)
//        {
//            objIDDCView = DDCView;
//        }
//        public DataSet FillDeliveryModeValues()
//        {
//            objDAL.RunProc("dbo.EC_Opr_GDC_FillValues", ref objDS);
//            return objDS;
//        }

//        public DataSet FillValues(int VehicleID)
//        {

//            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
//            objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, objIDDCView.keyID) ,
//            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
//            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime, 0, objIDDCView.DDSDate) ,
//            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchyCode),
//            objDAL.MakeInParams("@VehicleID", SqlDbType.VarChar, 5, VehicleID)};
//            objDAL.RunProc("dbo.EC_Opr_DDC_FillValues", objSqlParam, ref objDS);
//            return objDS;
//            //objDAL.MakeInParams("@VehicleID", SqlDbType.VarChar, 5, objIDDCView.VehicleID)};
//        }

//        public DataSet ReadValues()
//        {
//            SqlParameter[] objSqlParam ={
//            objDAL.MakeInParams("@DDC_Id", SqlDbType.Int, 0, objIDDCView.keyID) ,
//            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0, objIDDCView.PDSId),
//            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID)};

//            objDAL.RunProc("dbo.EC_Opr_DDC_ReadValues", objSqlParam, ref objDS);
//            return objDS;
//        }

//        public Message Save()
//        {
//            Message objMessage = new Message();
//            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
//            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
//            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
//            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
//            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
//            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
//            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
//            objDAL.MakeInParams("@DDC_Branch_ID", SqlDbType.Int,0,_branchID),
//            objDAL.MakeInParams("@DDC_ID", SqlDbType.Int, 0,objIDDCView.keyID),
//            objDAL.MakeInParams("@PDS_ID", SqlDbType.Int, 0,objIDDCView.PDSId),
//            objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime,0,objIDDCView.DDSDate),
//            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId),
//            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIDDCView.Remarks),
//            objDAL.MakeInParams("@DDCDetailsXML",SqlDbType.Xml,0,objIDDCView.DDSDetailsXML)};

//            objDAL.RunProc("dbo.EC_Opr_DDC_Save", objSqlParam);

//            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
//            objMessage.message = Convert.ToString(objSqlParam[1].Value);

//            if (objMessage.messageID == 0)
//            {

//                objIDDCView.ClearVariables();

//                string _Msg;
//                _Msg = "Saved SuccessFully";
//                if (objIDDCView.Flag == "SaveAndNew")
//                {
//                    int MenuItemId = Common.GetMenuItemId();
//                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
//                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmDDC.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
//                }
//                else if (objIDDCView.Flag == "SaveAndExit")
//                {
//                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
//                }
//                else if (objIDDCView.Flag == "SaveAndPrint")
//                {
//                    int MenuItemId = Common.GetMenuItemId();
//                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
//                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
//                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
//                }
//            }


//            return objMessage;
//        }
//    }
//}