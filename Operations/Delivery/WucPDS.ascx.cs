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
using System.Drawing;
using System.Text;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 05/11/2008
/// Modified by   : Sushant K on 14-Nov-13
/// Description   : This Page is For PDS Operation
/// Page INFO
/// the below statement is used to bind the Supervisor name in user control
/// Raj.EF.CallBackFunction.CallBack.GetSearchEmployee;
/// </summary>

public partial class Operations_Delivery_WucPDS : System.Web.UI.UserControl, IPDSView
{
    #region ClassVariables
    PDSPresenter objPDSPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "", _strValidate;
    string Mode = "0";
    string _GC_No_XML;
    PageControls pc = new PageControls();
    #endregion

    #region ControlsValues

    public string PDSNo
    {
        set { lbl_PDS_No.Text = value; }
    }
    public DateTime PDSDate
    {
        set { dtp_PDS_Date.SelectedDate = value; }
        get { return dtp_PDS_Date.SelectedDate; }
    }
    public int DeliveryModeID
    {
        get { return Util.String2Int(ddl_DeliveryMode.SelectedValue); }
        set { ddl_DeliveryMode.SelectedValue = Util.Int2String(value); }
    }
    public string DeliveryModeDescription
    {
        get { return ddl_DeliveryMode.SelectedItem.Text; }
        set { ddl_DeliveryMode.SelectedItem.Text = value; }
    } 
    public int VAID
    {
        get { return Util.String2Int(ddl_Associate_Name.SelectedValue); }
        set { ddl_Associate_Name.SelectedValue = Util.Int2String(value); }
    }
    //public string DeliveryModeDescription
    //{
    //    get { return txt_DeliveryModeDesc.Text; }
    //    set { txt_DeliveryModeDesc.Text = value; }
    //}
    public string DiverName
    {
        get { return txt_DriverName.Text; }
        set { txt_DriverName.Text = value; }
    }
    public int SupervisorID
    {
        get { return Util.String2Int(ddl_GodownSupervisor.SelectedValue); }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public int Total_Bal_Articles
    {
        set 
        { 
            hdn_BalArt.Value = Util.Int2String(value);
            lbl_BalArt.Text = Util.Int2String(value); 
        }
    }
    public decimal Total_Bal_Weight
    {
        set 
        { 
            hdn_BalActWt.Value = Util.Decimal2String(value);
            lbl_BalActWt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_BalActWt.Value); }
    }
    public int Total_Delivered_Articles
    {
        set
        { 
            hdn_totalDelArt.Value = Util.Int2String(value);
            lbl_totalDelArt.Text = Util.Int2String(value);
        }
    }
    public decimal Total_GC_Amount
    {
        set
        {
            hdn_totalGCAmt.Value = Util.Decimal2String(value);
            lbl_totalGCAmt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_totalGCAmt.Value); }
    }
    public decimal Total_Delivered_Weight
    {
        set
        { 
            hdn_totalDelWt.Value = Util.Decimal2String(value);
            lbl_totalDelWt.Text = Util.Decimal2String(value); 
        }
        get { return Util.String2Decimal(hdn_totalDelWt.Value); }
    }
    public int Total_No_Of_GC
    {
        set
        { 
            hdn_Total_GC.Value = Util.Int2String(value);
            lbl_Total_GC.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_GC.Value); }
    }

    public string Flag
    {
        get { return _flag; }
    }

    public void SetSuperviserId(string text, string value)
    {
        ddl_GodownSupervisor.DataTextField = "Emp_Name";
        ddl_GodownSupervisor.DataValueField = "Emp_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_GodownSupervisor);
    }
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value;
        hdn_VehicleID.Value = Util.Int2String(value);
    }
    }

    public int VendorID
    {
        get { return WucVehicleSearch1.VehicleVendorID; }
        set 
        {
            WucVehicleSearch1.VehicleVendorID = value;
            hdn_Vendor_ID.Value = Util.Int2String(value);
        }
    }
    public string VendorName
    {
        get { return lbltxt_Vendor.Text; }
        set { lbltxt_Vendor.Text = value; }
    }


    public int Ledger_ID
    {
        get { return WucVehicleSearch1.Ledger_ID; }
        set
        {
            WucVehicleSearch1.Ledger_ID = value;
            hdn_Ledger_ID.Value = Util.Int2String(value);
        }
    }

    public int Credit_Limit
    {
        get { return WucVehicleSearch1.Credit_Limit; }
        set
        {
            WucVehicleSearch1.Credit_Limit = value;
            hdn_Credit_Limit.Value = Util.Int2String(value);
        }
    }
    public string MobileNumber
    {
        get { return txt_MobileNo.Text; }
        set { txt_MobileNo.Text = value; }
    }

    #endregion

    #region ControlsBind

    public DataTable BindDeliveryMode
    {
        set
        {
            ddl_DeliveryMode.DataTextField = "Delivery_Mode";
            ddl_DeliveryMode.DataValueField = "Delivery_Mode_ID";
            ddl_DeliveryMode.DataSource = value;
            ddl_DeliveryMode.DataBind();
            //ddl_DeliveryMode.Items.Insert(0, new ListItem("-- Select One --", "0"));
            
            ListItem removeItem = ddl_DeliveryMode.Items.FindByValue("3");
            ddl_DeliveryMode.Items.Remove(removeItem);

            ListItem removeItem2 = ddl_DeliveryMode.Items.FindByValue("4");
            ddl_DeliveryMode.Items.Remove(removeItem2);
        }
    }
    public DataTable BindAssociates
    {
        set
        {
            ddl_Associate_Name.DataTextField = "VA_Name";
            ddl_Associate_Name.DataValueField = "VA_ID";
            ddl_Associate_Name.DataSource = value;
            ddl_Associate_Name.DataBind();
        }
    }
    public void BindPDSGrid()
    {
        dg_PDS.DataSource = SessionBindPDSGrid;
        dg_PDS.DataBind();
    }

    public DataTable SessionBindPDSGrid
    {
        get { return StateManager.GetState<DataTable>("BindPDSGrid"); }
        set 
        { 
            StateManager.SaveState("BindPDSGrid", value);
            if (StateManager.Exist("BindPDSGrid"))
            {
                BindPDSGrid();
            }
        }
    }

    public String PDSDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindPDSGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "PDSGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        TextBox Txt_GodownSupervisor;

        Txt_GodownSupervisor = (TextBox)ddl_GodownSupervisor.FindControl("txtBoxddl_GodownSupervisor");

        ////if (WucVehicleSearch1.VehicleID <= 0)
        ////{
        ////    lblErrors.Text = "Please Select Vehicle No";
        ////}

        if (Datemanager.IsValidProcessDate("OPR_PDS", PDSDate) == false)
        {
            errorMessage = "Please Select Valid PDS Date";// GetLocalResourceObject("Msg_dtp_PDSDate").ToString();
        }
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "Please Select atleast one " + CompanyManager.getCompanyParam().GcCaption;
        }
        //else if (Total_No_Of_GC == 1 && Util.String2Int(SessionBindPDSGrid.Rows[0]["Octroi_Updated"].ToString()) == 0)
        //{
        //    errorMessage = "Please Update Octroi ";
        //}
        else if (DeliveryModeID <= 0)
        {
            errorMessage = "Please Select Delivery Mode";// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
            ddl_DeliveryMode.Focus();
        }
        //else if ((DeliveryModeID == 2) && VehicleID <= 0)
        //{
        //    errorMessage = "Please Select Vehicle No.";
        //    ddl_DeliveryMode.Focus();
        //}
        else if ((DeliveryModeID == 2) && ValidateVehicle() == false)
        {
            errorMessage = strValidate_Vehicle.ToString();// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
            ddl_DeliveryMode.Focus();
        }
        else if (SupervisorID <= 0)
        {
            errorMessage = "Please Select Godown Supervisor";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            Txt_GodownSupervisor.Focus();
        }
        else if (grid_validation() == false)
        {
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public string strValidate_Vehicle
    {

        get { return Convert.ToString(ViewState["_strValidate"]); }
        set { ViewState["_strValidate"] = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region OtherMethod

    private bool ValidateVehicle()
    { 
        bool ATS = true;
        objPDSPresenter.CheckVehicle(); 

        if (strValidate_Vehicle.Length > 0)
        {
            errorMessage = strValidate_Vehicle.ToString(); 
            ATS = false; 
        } 
        else
        {
          ATS = true;
        } 
        return ATS;

    }

    private bool grid_validation()
    {
        TextBox txt_Del_Art, txt_Del_Wt;
        CheckBox chk;
        int i;
        bool ATS = true;
        string GC_No;

        if (Total_No_Of_GC > 0)
        {
            objDT = SessionBindPDSGrid;

            for (i = 0; i <= dg_PDS.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_PDS.Items[i].FindControl("Chk_Attach");
                txt_Del_Art = (TextBox)dg_PDS.Items[i].FindControl("txt_Delivery_Art");
                txt_Del_Wt = (TextBox)dg_PDS.Items[i].FindControl("txt_Delivery_Wt");
                GC_No = dg_PDS.Items[i].Cells[1].Text;

                if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) <= 0)
                {
                    errorMessage = "Delivery Articles Must be Greater than zero";
                    scm_pds.SetFocus(txt_Del_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) > Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()))
                {
                    errorMessage = "Delivered Articles Can't be greater than Balance Articles";
                    scm_pds.SetFocus(txt_Del_Art);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) <= 0)
                {
                    errorMessage = "Delivered Actual Wt Can't be Zero";
                    scm_pds.SetFocus(txt_Del_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) > Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString()))
                {
                    errorMessage = "Delivered Actual Wt Can't be greater than Balance Actual Wt";
                    scm_pds.SetFocus(txt_Del_Wt);
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(SessionBindPDSGrid.Rows[0]["Octroi_Updated"].ToString()) == 0)
                {
                    errorMessage = "Octroi Not Updated. , Please Update Octroi For " + CompanyManager.getCompanyParam().GcCaption + " :" + GC_No;
                    ATS = false;
                    break;
                }
                else if (objPDSPresenter.ValidateConsigneeClient() == false)
                {
                    errorMessage = "Select LR of Same Consignee, if the Delivery Mode is Person";
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

    private void calculate_griddetails()
    {
        Total_No_Of_GC = 0;
        CheckBox chk;
        TextBox txt_Del_Art, txt_Del_Wt;
        int i;

        if (dg_PDS.Items.Count > 0)
        {
            objDT = SessionBindPDSGrid;

            for (i = 0; i <= dg_PDS.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_PDS.Items[i].FindControl("Chk_Attach");
                txt_Del_Art = (TextBox)dg_PDS.Items[i].FindControl("txt_Delivery_Art");
                txt_Del_Wt = (TextBox)dg_PDS.Items[i].FindControl("txt_Delivery_Wt");

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                }
                objDT.Rows[i]["Att"] = chk.Checked;
                objDT.Rows[i]["Delivery_Articles"] = Util.String2Int(txt_Del_Art.Text);
                objDT.Rows[i]["Delivery_Actual_Wt"] = Util.String2Decimal(txt_Del_Wt.Text);

            }
        }

    }

    private void Next_PDS_Number()
    {
        PDSNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (WucSelectedItems1.EnterItem != string.Empty)
        {
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objPDSPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindPDSGrid;

            //BindPDSGrid();
            Assign_Hidden_Values_For_Reset();
            WucSelectedItems1.Get_Not_Selected_Items();
        }

    }

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        lbl_totalGCAmt.Text = hdn_totalGCAmt.Value;
        lbl_BalArt.Text = hdn_BalArt.Value;
        lbl_BalActWt.Text = hdn_BalActWt.Value;
        lbl_totalDelArt.Text = hdn_totalDelArt.Value;
        lbl_totalDelWt.Text = hdn_totalDelWt.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_totalGCAmt.Value = "0"; 
        hdn_BalArt.Value = "0";
        hdn_BalActWt.Value = "0";
        hdn_totalDelArt.Value = "0";
        hdn_totalDelWt.Value = "0";

        lbl_Total_GC.Text = "0";
        lbl_totalGCAmt.Text = "0"; 
        lbl_BalArt.Text = "0";
        lbl_BalActWt.Text = "0";
        lbl_totalDelArt.Text = "0";
        lbl_totalDelWt.Text = "0";
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;  //change usermanager to companymanager by Ankit
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + "   Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = hdn_GCCaption.Value;

        Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        dg_PDS.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    }
    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtp_PDS_Date.Enabled = false;

            WucVehicleSearch1.Can_Add_Vehicle = false; //added by sushant 11-11-2013
            WucVehicleSearch1.Can_View_Vehicle = true; //added by sushant 11-11-2013
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        SetPostBackValues();
        SetStandardCaption();
        dtp_PDS_Date.AutoPostBackOnSelectionChanged = false;
        ddl_GodownSupervisor.DataTextField = "Emp_Name";
        ddl_GodownSupervisor.DataValueField = "Emp_ID";
       
        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save_Exit, btn_Save, btn_Close));

        WucVehicleSearch1.Can_Add_Vehicle = false;
        WucVehicleSearch1.Can_View_Vehicle = true;
        WucVehicleSearch1.TransactionDate = dtp_PDS_Date.SelectedDate;
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
            hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId);
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            WucVehicleSearch1.TransactionDate = dtp_PDS_Date.SelectedDate;
             
        }
        objPDSPresenter = new PDSPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Next_PDS_Number();
            }
            else if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e);
                td_gccontrol.Style.Add("display", "none");
            }
            else
            {
                td_gccontrol.Style.Add("visibility", "hidden");
            }
        }
        DeliveryModeChange();
         
        Assign_Hidden_Values_To_TextBox();
    }

    private void DeliveryModeChange()
    {
        if (ddl_DeliveryMode.SelectedValue == "2")
        {
            lbl_DriverName.Text = "Driver Name";
            WucVehicleSearch1.Enable_Disable(true);
        }
        if (ddl_DeliveryMode.SelectedValue == "3")
        {
            lbl_DriverName.Text = "Hand Cart No";
            WucVehicleSearch1.Enable_Disable(false);
        } 
        if (ddl_DeliveryMode.SelectedValue == "4")
        {
            lbl_DriverName.Text = "Person Name";
            WucVehicleSearch1.Enable_Disable(false);
        }
         
    
    }

    private void SetPostBackValues()
    {
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        if (Mode =="2" || Mode == "4")
        {
             if (IsPostBack)
            {
                VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
                VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
                Ledger_ID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("VendorLedger_Id"));
                Credit_Limit = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Credit_Limit"));
            }
        }
        else
        {
            VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
            VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
            Ledger_ID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("VendorLedger_Id"));
            Credit_Limit = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Credit_Limit"));
        }
    }


    protected void dg_PDS_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid = "";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        string calculate_grid3 = "";
        CheckBox chk_Attach;
        TextBox Txt_Delivered_Art, Txt_Delivered_Wt;

        if (e.Item.ItemIndex != -1)
        {
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
            Txt_Delivered_Art = (TextBox)e.Item.FindControl("txt_Delivery_Art");
            Txt_Delivered_Wt = (TextBox)e.Item.FindControl("txt_Delivery_Wt");

            if (CompanyManager.getCompanyParam().IsPartLoadingRequired == false)
            {
                disable_Textbox(Txt_Delivered_Art, Txt_Delivered_Wt);
            }
            else
            {
                calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";
                calculate_grid3 = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";

                Txt_Delivered_Art.Attributes.Add("onblur", calculate_grid);
                Txt_Delivered_Wt.Attributes.Add("onblur", calculate_grid1);

                Txt_Delivered_Art.Attributes.Add("onfocus", calculate_grid2);
                Txt_Delivered_Wt.Attributes.Add("onfocus", calculate_grid3);
            }

            if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Octroi_Updated").ToString()) == 0) // 'Reserved GCs
            {
                e.Item.CssClass = "NOTUPDATEDLBL";
            }

            if (DataBinder.Eval(e.Item.DataItem, "Is_Show_eWayBillInPDS").ToString() == "True")
            {
                lbl_eWayBills.Text = "UPDATE E-WayBill";
                lbl_eWayBills.Visible = true;
            }
            else 
            {
                lbl_eWayBills.Visible = false;

            }

        }
    }
    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objPDSPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objPDSPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objPDSPresenter.save();
    }
    protected void dtp_PDS_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();

        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objPDSPresenter.fillgrid();
        WucSelectedItems1.dtdetails = SessionBindPDSGrid;
        WucSelectedItems1.Get_Not_Selected_Items();

        WucVehicleSearch1.TransactionDate = dtp_PDS_Date.SelectedDate;

    }

    public void ClearVariables()
    {
        SessionBindPDSGrid = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }
    protected void ddl_DeliveryMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleID = 0;
        VendorName = "";
        VendorID = 0;
        DeliveryModeChange();
    }
     
}
