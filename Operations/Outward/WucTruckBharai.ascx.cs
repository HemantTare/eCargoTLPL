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
/// Description   : This Page is For TruckBharai Operation
/// Page INFO
/// the below statement is used to bind the Supervisor name in user control
/// Raj.EF.CallBackFunction.CallBack.GetSearchEmployee;
/// </summary>

public partial class Operations_Outward_WucTruckBharai : System.Web.UI.UserControl, ITruckBharaiView
{
    #region ClassVariables
    Hashtable htSelectedLRs = new Hashtable();
    Hashtable htSelectedDeliveryMemos = new Hashtable();
    string DeliveryMemoIDs = "";
    int deliveryBranchId;

    TruckBharaiPresenter objTruckBharaiPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "", _strValidate;
    string Mode = "0";
    string _GC_No_XML, _strMemo_Ids;
    PageControls pc = new PageControls();
    TextBox  txt,txt_Hamali_Paid;//txt_Hamali_Charges,
    HiddenField hdn_Hamali_Charges, hdn_Hamali_Paid; 
    #endregion

    #region ControlsValues
 
    public string TransactionNo
    {
        set { txtlblTransactionNo.Text = value; }
    }
    public DateTime TransactionDate
    {
        set { dtpTransactionDate.SelectedDate = value; }
        get { return dtpTransactionDate.SelectedDate; }
    }  
     
    public int SupervisorID
    {
        get { return Util.String2Int(ddl_LoadedBy.SelectedValue); }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
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
    public int Total_SelectedMemo
    {
        set
        {
            hdn_Total_SelectedMemo.Value = Util.Int2String(value);
            lbl_Total_SelectedMemo.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_SelectedMemo.Value); }
    }
    public decimal Total_Hamali_Charges
    {
        set
        {
            hdn_Total_Hamali_Charges.Value = Util.Decimal2String(value);
            lbl_Total_Hamali_Charges.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Hamali_Charges.Value); }
    }
    public decimal Total_Hamali_Paid
    {
        set
        {
            hdn_Total_Hamali_Paid.Value = Util.Decimal2String(value);
            lbl_Total_Hamali_Paid.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_Hamali_Paid.Value); }
    }

    public string Flag
    {
        get { return _flag; }
    }

    public void SetSuperviserId(string text, string value)
    {
        ddl_LoadedBy.DataTextField = "Emp_Name";
        ddl_LoadedBy.DataValueField = "Emp_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_LoadedBy);
    }
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    } 
    

    #endregion

    #region ControlsBind

     
    public void BindTruckBharaiGrid()
    {
        dg_TruckBharai.DataSource = SessionBindTruckBharaiGrid;
        dg_TruckBharai.DataBind();
    }

    public DataTable SessionBindTruckBharaiGrid
    {
        get { return StateManager.GetState<DataTable>("BindTruckBharaiGrid"); }
        set 
        { 
            StateManager.SaveState("BindTruckBharaiGrid", value);
            if (StateManager.Exist("BindTruckBharaiGrid"))
            {
                BindTruckBharaiGrid();
            }
        }
    }
    public void BindSelectedMemosGrid()
    {
        dg_SelectedMemoDtls.DataSource = SessionBindSelectedMemoGrid;
        dg_SelectedMemoDtls.DataBind();
    }
    public DataTable SessionBindSelectedMemoGrid
    {
        get { return StateManager.GetState<DataTable>("BindSelectedMemoGrid"); }
        set
        {
            StateManager.SaveState("BindSelectedMemoGrid", value);
            if (StateManager.Exist("BindSelectedMemoGrid"))
            {
                BindSelectedMemosGrid();
            }
        }
    }

    public String TruckBharaiDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindSelectedMemoGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "TruckBharaiGrid_Details";
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

    public string strMemo_Ids
    {
        set { _strMemo_Ids = value; }
        get { return _strMemo_Ids.ToString(); }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        TextBox Txt_LoadedBy;

        Txt_LoadedBy = (TextBox)ddl_LoadedBy.FindControl("txtBoxddl_LoadedBy");

        if (Datemanager.IsValidProcessDate("OPR_TruckBharai", TransactionDate) == false)
        {
            errorMessage = "Please Select Valid TruckBharai Date";// GetLocalResourceObject("Msg_dtp_TruckBharaiDate").ToString();
        }
        else if (WucVehicleSearch1.VehicleID <= 0)
        {
            errorMessage = "Please Select Vehicle No";
        }
        else if (Total_SelectedMemo <= 0)
        {
            errorMessage = "Please Select All " + CompanyManager.getCompanyParam().GcCaption;
        }
        else if (Total_Hamali_Paid <= 0)
        {
            errorMessage = "Total Hamali Paid Should be Greater Than Zero";
        }  
        else if (SupervisorID <= 0)
        {
            errorMessage = "Please Select Loaded By";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            Txt_LoadedBy.Focus();
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
        objTruckBharaiPresenter.CheckVehicle(); 

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
        TextBox txt_Hamali_Paid;//txt_Hamali_Charges, 
        CheckBox chk;
        int i;
        bool ATS = true;
        string GC_No;

        if (Total_No_Of_GC > 0)
        {
            objDT = SessionBindSelectedMemoGrid;

            for (i = 0; i <= dg_SelectedMemoDtls.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_SelectedMemoDtls.Items[i].FindControl("Chk_Attach");
                //txt_Hamali_Charges = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Charges");
                txt_Hamali_Paid = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Paid");
                GC_No = dg_SelectedMemoDtls.Items[i].Cells[2].Text;

             
                if (chk.Checked == true && Util.String2Decimal(txt_Hamali_Paid.Text) < 0)
                {
                    errorMessage = "Hamali Paid Can't be Less Than Zero for " + GC_No;
                    scm_TruckBharai.SetFocus(txt_Hamali_Paid);
                    ATS = false;
                    break;
                }  
                else
                {
                    ATS = true;
                }
                if (ATS == true)
                {
                    calculate_griddetails();
                }
            }
        }
        return ATS;
    }

    private void calculate_griddetails()
    {
        Total_No_Of_GC = 0;
        CheckBox chk;
        TextBox txt_Hamali_Paid; //txt_Hamali_Charges, 
        int i;

        if (dg_SelectedMemoDtls.Items.Count > 0)
        {
            objDT = SessionBindSelectedMemoGrid;

            for (i = 0; i <= dg_SelectedMemoDtls.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_SelectedMemoDtls.Items[i].FindControl("Chk_Attach");
                //txt_Hamali_Charges = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Charges");
                txt_Hamali_Paid = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Paid");

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                   
                    objDT.Rows[i]["Hamali_Paid"] = Util.String2Decimal(txt_Hamali_Paid.Text);
                }
                objDT.Rows[i]["Att"] = chk.Checked;

            }
        }

    }

    private void Next_TruckBharai_Number()
    {
        TransactionNo = objComm.Get_Next_Number();
    } 

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        //lbl_BalArt.Text = hdn_BalArt.Value; 
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0"; 
        //hdn_BalArt.Value = "0";
        

        lbl_Total_GC.Text = "0"; 
        //lbl_BalArt.Text = "0"; 
    }

    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtpTransactionDate.Enabled = false;

            WucVehicleSearch1.Can_Add_Vehicle = false; //added by sushant 11-11-2013
            WucVehicleSearch1.Can_View_Vehicle = true; //added by sushant 11-11-2013
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        SetPostBackValues();

        ddl_LoadedBy.DataTextField = "Emp_Name";
        ddl_LoadedBy.DataValueField = "Emp_ID";
        
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        //btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save_Exit,  btn_Close));

        WucVehicleSearch1.Can_Add_Vehicle = false;
        WucVehicleSearch1.Can_View_Vehicle = true;
        WucVehicleSearch1.TransactionDate = dtpTransactionDate.SelectedDate;
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        hdn_Mode.Value = Mode;
        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
            hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId);
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            WucVehicleSearch1.TransactionDate = dtpTransactionDate.SelectedDate;
        }
        objTruckBharaiPresenter = new TruckBharaiPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Next_TruckBharai_Number();
            }
            else if (keyID > 0)
            {
                UpdatePanel1.Visible = false;
                UpdatePanel2.Visible = false;
                
                tr_dg_TruckBharai1.Visible = false;
                tr_dg_TruckBharai2.Visible = false;

                OnDDLVehicleSelection(sender, e);
                WucVehicleSearch1.Enable_Disable(false);
                dtpTransactionDate.Enabled = false;
                btnGo.Enabled = false; 
            }
            else
            {
            }
            
        }
        
         
        Assign_Hidden_Values_To_TextBox();
    }

    private void BindMemo()
    {
        Common objCommon = new Common();

        ddlMemo.DataTextField = "Memo_No_For_Print";
        ddlMemo.DataValueField = "Memo_Id";
        ddlMemo.DataSource = SessionBindTruckBharaiGrid;
        ddlMemo.DataBind();
        ddlMemo.Visible = false;
        Common.InsertItem(ddlMemo);

        for (int i = 0; i < ddlMemo.Items.Count; i++)
        {
            htSelectedLRs.Add(ddlMemo.Items[i].Value, "");
            htSelectedDeliveryMemos.Add(ddlMemo.Items[i].Value, "");
        }

        ViewState["htSelectedLRs"] = htSelectedLRs;
        ViewState["htSelectedDeliveryMemos"] = htSelectedDeliveryMemos;
    }

    private void SetPostBackValues()
    {
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    { 
            if (keyID <= 0)
            {
                objTruckBharaiPresenter.fillgrid();
                if (dg_TruckBharai.Items.Count <= 0)
                {
                    btnGo_Click(sender, e);
                }
            }  
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        BindMemo();
        DeliveryMemoIDs = "";
        int TruckBharaiGridCount = dg_TruckBharai.Items.Count;
        CheckBox Chk_Attach;
        HiddenField hdnMemo_Id;
        int serviceLocationGridCount = dg_TruckBharai.Items.Count;
        for (int i = 0; i < serviceLocationGridCount; i++)
        {
            Chk_Attach = (CheckBox)dg_TruckBharai.Items[i].FindControl("Chk_Attach");
            hdnMemo_Id = (HiddenField)dg_TruckBharai.Items[i].FindControl("hdnMemo_Id");

            if (Chk_Attach.Checked)
            {
                if (DeliveryMemoIDs == "")
                    DeliveryMemoIDs = hdnMemo_Id.Value;
                else
                    DeliveryMemoIDs = DeliveryMemoIDs + "," + hdnMemo_Id.Value;
            }
        } 
 
        htSelectedDeliveryMemos = (Hashtable)ViewState["htSelectedDeliveryMemos"];
        htSelectedDeliveryMemos[DeliveryMemoIDs] = DeliveryMemoIDs;
        ViewState["htSelectedDeliveryMemos"] = htSelectedDeliveryMemos;
        strMemo_Ids = DeliveryMemoIDs; 
        
        objTruckBharaiPresenter.fillSelectedMemoGrid();
        
        if (dg_SelectedMemoDtls.Items.Count > 0 )
        {    
            for (int i =0; i < dg_SelectedMemoDtls.Items.Count;i ++ )
            {
                txt_Hamali_Paid = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Paid");
                txt_Hamali_Paid.Enabled = false;  
            }
        }
    }
    //protected void txt_Hamali_Paid_TextChanged(object sender, EventArgs e)
    //{
        //TextBox txt_Hamali_Paid_Tmp;
        //decimal Old_txt_Hamali_Paid_Tmp;
        //txt = (TextBox)sender;
        //DataGridItem _item = (DataGridItem)txt.Parent.Parent;
        //txt_Hamali_Paid_Tmp = (TextBox)_item.FindControl("txt_Hamali_Paid");
        //hdn_Hamali_Paid = (HiddenField)_item.FindControl("hdn_Hamali_Paid");
        //Total_Hamali_Paid = 0;
        //Old_txt_Hamali_Paid_Tmp = Convert.ToDecimal(hdn_Hamali_Paid.Value);

        //if (dg_SelectedMemoDtls.Items.Count > 0)
        //{
        //    for (int i = 0; i <= dg_SelectedMemoDtls.Items.Count - 1; i++)
        //    {
        //        Total_Hamali_Paid = 0;
        //        txt_Hamali_Paid = (TextBox)dg_SelectedMemoDtls.Items[i].FindControl("txt_Hamali_Paid");
        //        Total_Hamali_Paid = Total_Hamali_Paid + Convert.ToDecimal(txt_Hamali_Paid.Text);
            
                 
        //    }
        //}
    //}
    protected void dg_TruckBharai_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        CheckBox chk_Attach, chkAllItems;
        TextBox Txt_Delivered_Art, Txt_Delivered_Wt;

        if (e.Item.ItemIndex != -1)
        {
            chkAllItems = (CheckBox)e.Item.FindControl("chkAllItems");
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
            //txt_Hamali_Charges = (TextBox)e.Item.FindControl("txt_Hamali_Charges");
            hdn_Hamali_Charges = (HiddenField)e.Item.FindControl("hdn_Hamali_Charges");
            txt_Hamali_Paid = (TextBox)e.Item.FindControl("txt_Hamali_Paid");
            hdn_Hamali_Paid = (HiddenField)e.Item.FindControl("hdn_Hamali_Paid");

            //txt_Hamali_Charges.Text = SessionBindSelectedMemoGrid.Rows[e.Item.ItemIndex]["Hamali_Charges"].ToString();
            //hdn_Hamali_Charges.Value = SessionBindSelectedMemoGrid.Rows[e.Item.ItemIndex]["Hamali_Charges"].ToString();

            //txt_Hamali_Paid.Text = SessionBindSelectedMemoGrid.Rows[e.Item.ItemIndex]["Hamali_Paid"].ToString();
            //hdn_Hamali_Paid.Value = SessionBindSelectedMemoGrid.Rows[e.Item.ItemIndex]["Hamali_Paid"].ToString();
          
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
        objTruckBharaiPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objTruckBharaiPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objTruckBharaiPresenter.save();
    }
    

    public void ClearVariables()
    {
        SessionBindTruckBharaiGrid = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }



    protected void dtpTransactionDate_SelectionChanged(object sender, EventArgs e)
    {

        Assign_Hidden_Values_For_Reset();
        WucVehicleSearch1.TransactionDate = dtpTransactionDate.SelectedDate;
        WucVehicleSearch1.RetainVehicle = true;
        objTruckBharaiPresenter.fillgrid();

        if (dg_TruckBharai.Items.Count <= 0)
        {
            btnGo_Click(sender, e);
        }
    }

    protected void dg_SelectedMemoDtls_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        CheckBox chk_Attach, chkAllItems;
        if (e.Item.ItemIndex != -1)
        {
            chkAllItems = (CheckBox)e.Item.FindControl("chkAllItems");
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

            TextBox txt_Hamali_Paid = (TextBox)(e.Item.FindControl("txt_Hamali_Paid"));

            HiddenField hdn_Hamali_Charges = (HiddenField)(e.Item.FindControl("hdn_Hamali_Charges"));

            txt_Hamali_Paid.Attributes.Add("onblur", "Onblur_Hamali_Paid('" + txt_Hamali_Paid.ClientID + "','" + hdn_Hamali_Charges.ClientID + "');");

        }
    }
}
