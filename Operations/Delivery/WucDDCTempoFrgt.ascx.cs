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
/// Description   : This Page is For DDCTempoFrgt Operation
/// Page INFO
/// the below statement is used to bind the Supervisor name in user control
/// Raj.EF.CallBackFunction.CallBack.GetSearchEmployee;
/// </summary>

public partial class Operations_Delivery_WucDDCTempoFrgt : System.Web.UI.UserControl, IDDCTempoFrgtView
{
    #region ClassVariables
    DDCTempoFrgtPresenter objDDCTempoFrgtPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "", _strValidate;
    string Mode = "0";
    string _GC_No_XML;
    int ds_index, i;
    PageControls pc = new PageControls();
    TextBox txt, txt_Tempo_Freight_ToBePaid, txt_Bonus, txt_AddTempoFrt;
    decimal Total_Tempo_Freight_ToBePaid, Total_Bonus, Total_AddTempoFrt;
   
    #endregion

    #region ControlsValues

    public string DDCTempoFrgtNo
    {
        set { lbl_DDCTempoFrgt_No.Text = value; }
    }
    public DateTime DDCTempoFrgtDate
    {
        set { dtp_DDCTempoFrgt_Date.SelectedDate = value; }
        get { return dtp_DDCTempoFrgt_Date.SelectedDate; }
    }
    public DateTime FromDate
    {
        set { dtpFromDate.SelectedDate = value; }
        get { return dtpFromDate.SelectedDate; }
    }
    public DateTime ToDate
    {
        set { dtpToDate.SelectedDate = value; }
        get { return dtpToDate.SelectedDate; }
    }
    
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public int Total_No_Of_Records
    {
        set
        {
            hdn_Total_Records.Value = Util.Int2String(value);
            lbl_Total_Records.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_Records.Value); }
    }
    public int Total_No_Of_GC
    {
        set
        {
            hdn_Total_No_Of_GC.Value = Util.Int2String(value);
            lbl_Total_No_Of_GC.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_No_Of_GC.Value); }
    }
    public int Total_DDC_Articles
    {
        set 
        {
            hdn_Total_DDC_Articles.Value = Util.Int2String(value);
            lbl_Total_DDC_Articles.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_DDC_Articles.Value); }
    }
    public decimal Total_GC_Amount
    {
        set 
        { 
            hdn_Total_GC_Amount.Value = Util.Decimal2String(value);
            lbl_Total_GC_Amount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Total_GC_Amount.Value); }
    }

    public decimal Tempo_Freight_ToBePaid
    {
        set
        { 
            hdn_Tempo_Freight_ToBePaid.Value = Util.Decimal2String(value);
            lbl_Tempo_Freight_ToBePaid.Text = Util.Decimal2String(value); 
        }
        get { return Util.String2Decimal(hdn_Tempo_Freight_ToBePaid.Value); }
    }
    public decimal Bonus
    {
        set
        {
            hdn_Bonus.Value = Util.Decimal2String(value);
            lbl_Bonus.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_Bonus.Value); }
    }

    public decimal AddTempoFrt
    {
        set
        {
            hdn_AddTempoFrt.Value = Util.Decimal2String(value);
            lbl_AddTempoFrt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_AddTempoFrt.Value); }
    }

    public string Flag
    {
        get { return _flag; }
    }
 
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }

    public int VendorID
    {
        get { return WucVehicleSearch1.VehicleVendorID; }
        set 
        {
            WucVehicleSearch1.VehicleVendorID = value;
            
        }
    }
    //public string VendorName
    //{
    //    get { return lbltxt_Vendor.Text; }
    //    set { lbltxt_Vendor.Text = value; }
    //}


    public int Ledger_ID
    {
        get { return WucVehicleSearch1.Ledger_ID; }
        set
        {
            WucVehicleSearch1.Ledger_ID = value;
            //hdn_Ledger_ID.Value = Util.Int2String(value);
        }
    }

    public int Credit_Limit
    {
        get { return WucVehicleSearch1.Credit_Limit; }
        set
        {
            WucVehicleSearch1.Credit_Limit = value;
            //hdn_Credit_Limit.Value = Util.Int2String(value);
        }
    }

    public int IsCashCheque
    {
        get { return Util.String2Int(rbtnIsCashCheque.SelectedValue); }
        set
        {
            rbtnIsCashCheque.SelectedValue = value.ToString();
        }
    }
    public decimal CashAmount
    {
        set
        {
            hdnCashAmount.Value = Util.Decimal2String(value);
            txtCashAmount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(txtCashAmount.Text); }
    }
    public decimal ChequeAmount
    {
        set
        {
            hdnChequeAmount.Value = Util.Decimal2String(value);
            txtChequeAmount.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(txtChequeAmount.Text); }
    }
    public int ChequeNo
    {
        set
        {
            hdnChequeNo.Value = Util.Int2String(value);
            txtChequeNo.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(txtChequeNo.Text); }
    }
    public DateTime ChequeDate
    {
        set { dtp_ChequeDate.SelectedDate = value; }
        get { return dtp_ChequeDate.SelectedDate; }
    }

    public int FrgtSettlementTypeID
    {
        set
        {
            hdnFrgtSettlementTypeID.Value = Util.Int2String(value); 
        }
        get { return Util.String2Int(hdnFrgtSettlementTypeID.Value); }
    }

    public string FrgtSettlementTypeName
    {
        set
        {
            hdnFrgtSettlementType.Value = value;
        }
        get { return hdnFrgtSettlementType.Value; }
    }
    public decimal TotalTempoFrgtTBPaid
    {
        set
        {
            hdnTotalTempoFrgtTBPaid.Value = Util.Decimal2String(value);
            txtTotalTempoFrgtTBPaid.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(txtTotalTempoFrgtTBPaid.Text); }
    }

    #endregion

    #region ControlsBind
  
    public void BindDDCTempoFrgtGrid()
    {
        dg_DDCTempoFrgt.DataSource = SessionBindDDCTempoFrgtGrid;
        dg_DDCTempoFrgt.DataBind();
    }

    public DataTable SessionBindDDCTempoFrgtGrid
    {
        get { return StateManager.GetState<DataTable>("BindDDCTempoFrgtGrid"); }
        set 
        { 
            StateManager.SaveState("BindDDCTempoFrgtGrid", value);
            if (StateManager.Exist("BindDDCTempoFrgtGrid"))
            {
                BindDDCTempoFrgtGrid();
            }
        }
    }

    public String DDCTempoFrgtDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindDDCTempoFrgtGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "DDCTempoFrgtGrid_Details";
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


        if (WucVehicleSearch1.VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle No";
        }  
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "Please Select atleast one " + CompanyManager.getCompanyParam().GcCaption;
        }
        //else if ( IsCashCheque  == 1 && ((CashAmount + ChequeAmount) <= 0))
        //{
        //    errorMessage = "Please Enter Cash/Cheque Amount";
        //    _isValid = false;
        //}
        //else if ( IsCashCheque  == 1 && (ChequeAmount >= 0 && txtChequeNo.Text == ""))
        //{
        //    errorMessage = "Please Enter Cheque No";
        //    _isValid = false;
        //}
        //else if ( IsCashCheque == 1 && ((CashAmount + ChequeAmount) != (Tempo_Freight_ToBePaid + Bonus)))
        //{
        //    errorMessage = "Sum of Cash and Cheque is Not Equal to Tempo Freight To Be Paid and Bonus.";
        //    _isValid = false;
        //}
        else if (FrgtSettlementTypeID == 2 && (FromDate != ToDate))
        {
            errorMessage = "Vehicle Contract Type is Daily, From Date and To Date Must be Same";
            _isValid = false;
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
        objDDCTempoFrgtPresenter.CheckVehicle(); 

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
            objDT = SessionBindDDCTempoFrgtGrid;

            for (i = 0; i <= dg_DDCTempoFrgt.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_DDCTempoFrgt.Items[i].FindControl("Chk_Attach");
                //txt_Del_Art = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Delivery_Art");
                //txt_Del_Wt = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Delivery_Wt");
                GC_No = dg_DDCTempoFrgt.Items[i].Cells[1].Text;

                //if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) <= 0)
                //{
                //    errorMessage = "Delivery Articles Must be Greater than zero";
                //    scm_DDCTempoFrgt.SetFocus(txt_Del_Art);
                //    ATS = false;
                //    break;
                //}
                //else if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) > Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()))
                //{
                //    errorMessage = "Delivered Articles Can't be greater than Balance Articles";
                //    scm_DDCTempoFrgt.SetFocus(txt_Del_Art);
                //    ATS = false;
                //    break;
                //}
                //else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) <= 0)
                //{
                //    errorMessage = "Delivered Actual Wt Can't be Zero";
                //    scm_DDCTempoFrgt.SetFocus(txt_Del_Wt);
                //    ATS = false;
                //    break;
                //}
                //else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) > Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString()))
                //{
                //    errorMessage = "Delivered Actual Wt Can't be greater than Balance Actual Wt";
                //    scm_DDCTempoFrgt.SetFocus(txt_Del_Wt);
                //    ATS = false;
                //    break;
                //} 
                //else
                //{
                    ATS = true;
                //}
            }
        }
        return ATS;
    }

    private void calculate_griddetails()
    {
        Total_No_Of_GC = 0;
        CheckBox chk;
        TextBox txt_Tempo_Freight_ToBePaid, txt_Bonus, txt_AddTempoFrt;
        int i;

        if (dg_DDCTempoFrgt.Items.Count > 0)
        {
            objDT = SessionBindDDCTempoFrgtGrid;

            for (i = 0; i <= dg_DDCTempoFrgt.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_DDCTempoFrgt.Items[i].FindControl("Chk_Attach");
                txt_Tempo_Freight_ToBePaid = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Tempo_Freight_ToBePaid");
                txt_Bonus = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Bonus");
                txt_AddTempoFrt = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_AddTempoFrt"); 

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                }
                objDT.Rows[i]["Att"] = chk.Checked;
                objDT.Rows[i]["Tempo_Freight_ToBePaid"] = Util.String2Decimal(txt_Tempo_Freight_ToBePaid.Text);
                objDT.Rows[i]["Bonus"] = Util.String2Decimal(txt_Bonus.Text);
                objDT.Rows[i]["AddTempoFrt"] = Util.String2Decimal(txt_AddTempoFrt.Text);

            }
        }

    }

    private void Next_DDCTempoFrgt_Number()
    {
        DDCTempoFrgtNo = objComm.Get_Next_Number();
    }

    //private void OnGetGCXML(object sender, EventArgs e)
    //{
    //    if (WucSelectedItems1.EnterItem != string.Empty)
    //    {
    //        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
    //        objDDCTempoFrgtPresenter.fillgrid();
    //        WucSelectedItems1.dtdetails = SessionBindDDCTempoFrgtGrid;

    //        //BindDDCTempoFrgtGrid();
    //        Assign_Hidden_Values_For_Reset();
    //        WucSelectedItems1.Get_Not_Selected_Items();
    //    }

    //}

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_Records.Text = hdn_Total_Records.Value;
        lbl_Total_No_Of_GC.Text = hdn_Total_No_Of_GC.Value;
        lbl_Total_DDC_Articles.Text = hdn_Total_DDC_Articles.Value;
        lbl_Total_GC_Amount.Text = hdn_Total_GC_Amount.Value;
        lbl_Tempo_Freight_ToBePaid.Text = hdn_Tempo_Freight_ToBePaid.Value;
        lbl_Bonus.Text = hdn_Bonus.Value;
        lbl_AddTempoFrt.Text = hdn_AddTempoFrt.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_Records.Value = "0";
        hdn_Total_No_Of_GC.Value = "0"; 
        hdn_Total_DDC_Articles.Value = "0";
        hdn_Total_GC_Amount.Value = "0";
        hdn_Tempo_Freight_ToBePaid.Value = "0";
        hdn_Bonus.Value = "0";
        hdn_AddTempoFrt.Value = "0";

        lbl_Total_Records.Text = "0";
        lbl_Total_No_Of_GC.Text = "0"; 
        lbl_Total_DDC_Articles.Text = "0";
        lbl_Total_GC_Amount.Text = "0";
        lbl_Tempo_Freight_ToBePaid.Text = "0";
        lbl_Bonus.Text = "0";
        lbl_AddTempoFrt.Text = "0";
    }
    private void SetStandardCaption()
    {
        //const int GCNoCaption = 1;  //change usermanager to companymanager by Ankit
        //hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
         
        //Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        //dg_DDCTempoFrgt.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    }
    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            Td3.Visible = false;
            dtp_DDCTempoFrgt_Date.Enabled = false;
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;

            WucVehicleSearch1.Can_Add_Vehicle = false; //added by sushant 11-11-2013
            WucVehicleSearch1.Can_View_Vehicle = true; //added by sushant 11-11-2013
            tr_DateRange.Visible = false;
            rbtnIsCashCheque.Enabled = false;
            dtp_ChequeDate.Enabled = false; 
        }
        if (keyID > 0)
        {
            dtp_DDCTempoFrgt_Date.Enabled = false;
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
            WucVehicleSearch1.SetEnabled = false;
            WucVehicleSearch1.Enable_Disable(false);

            WucVehicleSearch1.Can_Add_Vehicle = false; //added by sushant 11-11-2013
            WucVehicleSearch1.Can_View_Vehicle = false; //added by sushant 11-11-2013
            tr_DateRange.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pc.AddAttributes(this.Controls);
        SetPostBackValues();
        SetStandardCaption();
        //dtp_DDCTempoFrgt_Date.AutoPostBackOnSelectionChanged = true;
        dtpFromDate.AutoPostBackOnSelectionChanged = true;
        dtpToDate.AutoPostBackOnSelectionChanged = true; 
       
        //WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save_Exit, btn_Save, btn_Close));

        WucVehicleSearch1.Can_Add_Vehicle = false;
        WucVehicleSearch1.Can_View_Vehicle = true;
        WucVehicleSearch1.TransactionDate = dtp_DDCTempoFrgt_Date.SelectedDate;
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
            hdn_LoginBranch_Id.Value = Util.Int2String(UserManager.getUserParam().MainId);
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            WucVehicleSearch1.TransactionDate = dtp_DDCTempoFrgt_Date.SelectedDate;
             
        }
        objDDCTempoFrgtPresenter = new DDCTempoFrgtPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Next_DDCTempoFrgt_Number();
            }
            else if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e); 
            }
            else
            { 
            }
            CheckIsCashCheque();
        }

        Assign_Hidden_Values_To_TextBox();
    }

    private void CheckFrgtSettlementTypeID()
    {
        TextBox txt_Tempo_Freight_ToBePaid;
        //if (keyID <= 0)
        //{
            if (FrgtSettlementTypeID == 6)
            {
                if (keyID <= 0)
                {
                    rbtnIsCashCheque.SelectedValue = "0";
                    rbtnIsCashCheque.SelectedIndex = 1;

                }
                    rbtnIsCashCheque.Enabled = false;

               if (dg_DDCTempoFrgt.Items.Count > 0)
               {
                   for (i = 0; i <= dg_DDCTempoFrgt.Items.Count - 1; i++)
                   {
                       txt_Tempo_Freight_ToBePaid = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Tempo_Freight_ToBePaid");
                       txt_Bonus = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Bonus");
                       txt_AddTempoFrt = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_AddTempoFrt");

                       txt_Tempo_Freight_ToBePaid.Text = "0";
                       lbl_Tempo_Freight_ToBePaid.Text = "0";
                       hdn_Tempo_Freight_ToBePaid.Value = "0";

                       txt_Bonus.Text = "0";
                       lbl_Bonus.Text = "0";
                       hdn_Bonus.Value = "0";

                       txt_AddTempoFrt.Text = "0";
                       lbl_AddTempoFrt.Text = "0";
                       hdn_AddTempoFrt.Value = "0";

                       txt_Tempo_Freight_ToBePaid.Enabled = false;
                       txt_Bonus.Enabled = false;
                       txt_AddTempoFrt.Enabled = false;
                   } 
               }

               txtTotalTempoFrgtTBPaid.Enabled = true;
               //txtTotalTempoFrgtTBPaid.Visible = true;
               //lblTotalTempoFrgtTBPaid.Visible = true;
            }
            else
            {

                rbtnIsCashCheque.Enabled = true;

                txtTotalTempoFrgtTBPaid.Enabled = false;
                //txtTotalTempoFrgtTBPaid.Visible = false;
                //lblTotalTempoFrgtTBPaid.Visible = false;
                if (keyID <= 0)
                {
                    rbtnIsCashCheque.SelectedValue = "1";
                    rbtnIsCashCheque.SelectedIndex = 0;
                    txtTotalTempoFrgtTBPaid.Text = "0";
                }
            }
            CheckIsCashCheque();
        //}

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
                //VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
                VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
                Ledger_ID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("VendorLedger_Id"));
                Credit_Limit = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Credit_Limit"));
            }
        }
        else
        {
            //VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
            VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
            Ledger_ID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("VendorLedger_Id"));
            Credit_Limit = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Credit_Limit"));
        }
        objDDCTempoFrgtPresenter.fillgrid();

        CheckFrgtSettlementTypeID();
    }


    protected void dg_DDCTempoFrgt_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid = "";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        string calculate_grid3 = "";
        CheckBox chk_Attach;
        TextBox Txt_Delivered_Art, Txt_Delivered_Wt, txt_Tempo_Freight_ToBePaid;
        LinkButton lbtn_DDC_No_For_Print, lbtn_PDS_No_For_Print;
        HiddenField hdn_DDC_ID, hdn_PDS_ID;

        if (e.Item.ItemIndex != -1)
        {
            chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");  
            ds_index = e.Item.ItemIndex;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lbtn_DDC_No_For_Print = (LinkButton)e.Item.FindControl("lbtn_DDC_No_For_Print");
                lbtn_PDS_No_For_Print = (LinkButton)e.Item.FindControl("lbtn_PDS_No_For_Print");
                hdn_DDC_ID = (HiddenField)e.Item.FindControl("hdn_DDC_ID");
                hdn_PDS_ID = (HiddenField)e.Item.FindControl("hdn_PDS_ID");
 
                int MenuItemId = Common.GetMenuItemId();

                int Document_ID = Convert.ToInt32(hdn_DDC_ID.Value);
                int DocumentPDS_ID = Convert.ToInt32(hdn_PDS_ID.Value);
               
                StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                Path.Append("/");
                Path.Append("Operations/Delivery/FrmDDCTempoFrgtDetails.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&Document_Type=DDC");
                lbtn_DDC_No_For_Print.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");

                StringBuilder PathPDS = new StringBuilder(Util.GetBaseURL());
                PathPDS.Append("/");
                PathPDS.Append("Operations/Delivery/FrmDDCTempoFrgtDetails.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(DocumentPDS_ID) + "&Document_Type=PDS");
                lbtn_PDS_No_For_Print.Attributes.Add("onclick", "return Open_Details_WindowPDS('" + PathPDS + "')");
            
            } 
        }
    }

    protected void txt_Total_Tempo_Freight_ToBePaid_TextChanged(object sender, EventArgs e)
    {
        CheckBox Chk_Attach;
        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent; 
 
        Total_Tempo_Freight_ToBePaid = 0; 
         
        objDT = SessionBindDDCTempoFrgtGrid;
        
        if (dg_DDCTempoFrgt.Items.Count > 0)
        {
            for (i = 0; i <= dg_DDCTempoFrgt.Items.Count - 1; i++)
            { 
                Chk_Attach = (CheckBox)dg_DDCTempoFrgt.Items[i].FindControl("Chk_Attach");

                if (Chk_Attach.Checked == true)
                { 
                    txt_Tempo_Freight_ToBePaid = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Tempo_Freight_ToBePaid");

                    Total_Tempo_Freight_ToBePaid = Total_Tempo_Freight_ToBePaid + Convert.ToDecimal(txt_Tempo_Freight_ToBePaid.Text);
                }

            }
            lbl_Tempo_Freight_ToBePaid.Text = Convert.ToString(Total_Tempo_Freight_ToBePaid);
            hdn_Tempo_Freight_ToBePaid.Value = Convert.ToString(Total_Tempo_Freight_ToBePaid);
            //Tempo_Freight_ToBePaid = Total_Tempo_Freight_ToBePaid;
        }
        Total_Tempo_Freight_ToBePaid_Bonus_TextChanged();

    }

    protected void txt_Bonus_TextChanged(object sender, EventArgs e)
    {

        CheckBox Chk_Attach;
        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent;
         
        Total_Bonus = 0;

        objDT = SessionBindDDCTempoFrgtGrid;

        if (dg_DDCTempoFrgt.Items.Count > 0)
        {
            for (i = 0; i <= dg_DDCTempoFrgt.Items.Count - 1; i++)
            {
                txt_Bonus = (TextBox)dg_DDCTempoFrgt.Items[i].FindControl("txt_Bonus");
                Chk_Attach = (CheckBox)dg_DDCTempoFrgt.Items[i].FindControl("Chk_Attach");

                if (Chk_Attach.Checked == true)
                {
                    Total_Bonus = Total_Bonus + Convert.ToDecimal(txt_Bonus.Text);
                }
            }
            lbl_Bonus.Text = Convert.ToString(Total_Bonus);
            hdn_Bonus.Value = Convert.ToString(Total_Bonus); 
        }
        Total_Tempo_Freight_ToBePaid_Bonus_TextChanged();
    }

    protected void Total_Tempo_Freight_ToBePaid_Bonus_TextChanged()
    {
        TotalTempoFrgtTBPaid = Tempo_Freight_ToBePaid + Bonus + AddTempoFrt; 
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
        objDDCTempoFrgtPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objDDCTempoFrgtPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objDDCTempoFrgtPresenter.save();
    }

    protected void btn_Save_Pay_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPay";
        calculate_griddetails();
        objDDCTempoFrgtPresenter.save();
    }


    protected void dtp_DDCTempoFrgt_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();

        //_GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objDDCTempoFrgtPresenter.fillgrid();
        //WucSelectedItems1.dtdetails = SessionBindDDCTempoFrgtGrid;
        //WucSelectedItems1.Get_Not_Selected_Items();

        WucVehicleSearch1.TransactionDate = dtp_DDCTempoFrgt_Date.SelectedDate;

    }
    protected void dtpFromDate_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();
        objDDCTempoFrgtPresenter.fillgrid();
        //WucVehicleSearch1.TransactionDate = dtp_DDCTempoFrgt_Date.SelectedDate; 
    }
    protected void dtpToDate_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset(); 
        objDDCTempoFrgtPresenter.fillgrid();  
        //WucVehicleSearch1.TransactionDate = dtp_DDCTempoFrgt_Date.SelectedDate; 
    }

    public void ClearVariables()
    {
        SessionBindDDCTempoFrgtGrid = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }


    protected void rbtnIsCashCheque_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckIsCashCheque();
    }

    public void CheckIsCashCheque()
    {
        if (rbtnIsCashCheque.SelectedValue == "0")
        {
            CashAmount = 0;
            ChequeAmount = 0;
            ChequeNo = 0;

            txtCashAmount.Enabled = false;
            txtChequeAmount.Enabled = false;
            txtChequeNo.Enabled = false;
            ChequeDate = DateTime.Now.Date;
            dtp_ChequeDate.Enabled = false;
            //tbChequeDate.Disabled = true;
        }
        else
        {
            txtCashAmount.Enabled = true;
            txtChequeAmount.Enabled = true;
            txtChequeNo.Enabled = true;
            dtp_ChequeDate.Enabled = true;
            //tbChequeDate.Disabled = false;
        }
    }
}
