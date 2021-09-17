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
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 06/12/2008
/// Description   : This Page is For ATH Finance
/// </summary> 
/// 
public partial class Finance_Accounting_Vouchers_WucATH : System.Web.UI.UserControl,IATHView
{
    #region ClassVariables
    ATHPresenter objATHPresenter;

    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DataSet objDS = new DataSet();
    PageControls pc = new PageControls();
    ClassLibrary.UIControl.DDLSearch ddl_PetrolPump;
    TextBox txt_Amount, txt_SlipNo, txt_GridRemarks;
    DataRow DR = null;
    bool isValid = false;
    string Mode = "0";
    string petrolpanel;
    #endregion

    #region ControlsValues
    public IMRCashChequeDetailsView MRCashChequeDetailsView
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }

    public bool SetCreditToEnabled
    {
        set 
        {
            TextBox txt_ledger = (TextBox)ddl_Ledger.Controls[0];
            txt_CreditAmount.Enabled = value;
            txt_ledger.Enabled = value;

            if (VehicleCategory == "Own")
            {
                txt_CreditAmount.Text = "0";
                SetCreditToLedgerID("", "0");
            }
        }
    }

    public int LHPOID
    {
        get { return Util.String2Int(ddl_LHPONo.SelectedValue); }
        set { ddl_LHPONo.SelectedValue = Util.Int2String(value); }
    }
    public int CreditToLedgerId
    {
        get { return Util.String2Int(ddl_Ledger.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_Ledger.SelectedValue); }
    }
    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set { WucVehicleSearch1.VehicleID = value; }
    }    
    public DateTime ATHDate
    {
        set { ATHV_Date.SelectedDate = value; }
        get { return ATHV_Date.SelectedDate; }
    }    
    public string ReferenceNo
    {
        set { txt_ReferenceNo.Text = value; }
        get { return txt_ReferenceNo.Text; }
    }
    public decimal AdvancePayableAmount
    {
        set { lbl_AdvancePayable.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(lbl_AdvancePayable.Text); }
    }
    public decimal TotalPaidAmount
    {
        set { txt_TotalPaidAmt.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_TotalPaidAmt.Text); }
    }
    public decimal CreditAmountTo
    {
        set { txt_CreditAmount.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_CreditAmount.Text) <= 0 ? 0 : Util.String2Decimal(txt_CreditAmount.Text); }
    }
    
    public decimal TotalPetrolAmount
    {
        set { lbl_TotalPetrolAmt.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(lbl_TotalPetrolAmt.Text); }
    } 
    public string ATHNo
    {
        set { lbl_ATHVoucherNo.Text = value; }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    private string VehicleCategory
    {
        set { lbl_VehicleCotegory.Text = value; }
        get { return lbl_VehicleCotegory.Text.Trim() ; }
    }
    private string OwnerName
    {
        set { lbl_Owner.Text = value; }
    }
    private string BrokerName
    {
        set { lbl_Broker.Text = value; }
    }
    private string ManualReferenceNo
    {
        set { lbl_ManualRefNo.Text = value; }
    }
    private string LHPODate
    {
        set { lbl_LHPODate.Text = value; }
    }
    private string Driver
    {
        set { lbl_Driver1.Text = value; }
    }   
    public void SetPetrolPump(string Text, string Value)
    {
        ddl_PetrolPump.DataTextField = "Petrol_Pump_Name";
        ddl_PetrolPump.DataValueField = "Petrol_Pump_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, ddl_PetrolPump);
    }
    public void SetCreditToLedgerID(string text, string value)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);

    }
    public IMRCashChequeDetailsView CashChequeDetailsView
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }
    #endregion

    #region ControlsBind

    public DataTable BindLHPONo
    {
        set
        {
            ddl_LHPONo.DataTextField = "LHPO_No";
            ddl_LHPONo.DataValueField = "LHPO_Id";
            ddl_LHPONo.DataSource = value;
            ddl_LHPONo.DataBind();
            if (keyID < 0)
            {
                ddl_LHPONo.Items.Insert(0, new ListItem("Select " + CompanyManager.getCompanyParam().LHPOCaption + " No.", "0"));
            }
        }
    }

    public void BindPetrolPumpGrid()
    {
        dg_Petrol.DataSource = SessionPetrolGrid;
        dg_Petrol.DataBind();
        if (SessionPetrolGrid.Rows.Count > 0)
            TotalPetrolAmount = Util.String2Decimal(SessionPetrolGrid.Compute("SUM(Petrol_Amount)", "").ToString());
        else
            TotalPetrolAmount = 0;
    }

    public DataTable SessionPetrolGrid
    {
        get { return StateManager.GetState<DataTable>("PetrolGrid"); }
        set
        {
            StateManager.SaveState("PetrolGrid", value);
            if (StateManager.Exist("PetrolGrid"))
                BindPetrolPumpGrid();
        }
    }

    public String petrolDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionPetrolGrid.Copy());

            _objDs.Tables[0].TableName = "petrolpumpdetails";
            return _objDs.GetXml().ToLower();
        }
    }
   
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        TextBox Txt_VehicleNo;
        errorMessage = "";
        Txt_VehicleNo = (TextBox)WucVehicleSearch1.FindControl("txt_Vehicle_Last_4_Digits");

        if (VehicleID <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ddl_vehicle").ToString();
            scm_ATH.SetFocus(Txt_VehicleNo);
        }
        else if (Datemanager.IsValidProcessDate("ATH_VD", ATHDate) == false)
        {
            errorMessage = "Invalid ATH Voucher Date";
        }
        else if (LHPOID <= 0)
        {
            errorMessage = "Please Select " + CompanyManager.getCompanyParam().LHPOCaption + " No";
            scm_ATH.SetFocus(ddl_LHPONo);
        }       
        else if (TotalPaidAmount > AdvancePayableAmount)
        {
            errorMessage = "Total Paid Amount can't be greater than Advance Payable Amount";
            scm_ATH.SetFocus(txt_TotalPaidAmt);
        }
        else if (TotalPaidAmount <= 0)
        {
            errorMessage = "Total Paid Amount should be greater than Zero";
            scm_ATH.SetFocus(txt_TotalPaidAmt);
        }
        //else if (WucMRCashChequeDetails1.CashAmount >= 20000)
        //{
        //    errorMessage = "Cash Amount Should Be Less Than 20000.";            
        //}
        else if (Convert.ToDateTime(ATHDate) < Convert.ToDateTime(lbl_LHPODate.Text))
        {
            errorMessage = "ATH Voucher date can't be less than " + CompanyManager.getCompanyParam().LHPOCaption + " Date";
        }
        else if (TotalPaidAmount > 0 && WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors) == false)
        {
            _isValid = false;
        }
        else if ((WucMRCashChequeDetails1.CashAmount + WucMRCashChequeDetails1.ChequeAmount + TotalPetrolAmount + CreditAmountTo )!= TotalPaidAmount)
        {
            if (Allocation_Id > 0)
                errorMessage = "Cash Amt,Cheque Amt,Credit Amount and Petrol Amt should be equal to Total Paid Amount";
            else
                errorMessage = "Cash Amt , Cheque Amt and Credit Amount should be equal to Total Paid Amount";

            _isValid = false;
        }
        else if (CreditToLedgerId > 0 && CreditAmountTo <= 0)
        {
            errorMessage = "Credit Amount Should Be Greater Than Zero";
        }
        else if (CreditAmountTo > 0 && CreditToLedgerId <= 0)
        {
            errorMessage = "Please Select Credit To Ledger";
        }
        else if (WucMRCashChequeDetails1.ChequeAmount > 0 && WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Chq_Amt").ToString();
            _isValid = false;
        }
        //else if (TotalPaidAmount > AdvancePayableAmount)
        //{
        //    if (Allocation_Id > 0)
        //        errorMessage = GetLocalResourceObject("Msg_txt_Petrol_Alloc").ToString();
        //    else
        //        errorMessage = GetLocalResourceObject("Msg_txt_Petrol").ToString();

        //    scm_ATH.SetFocus(txt_TotalPaidAmt);
        //}
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

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion      

    #region OtherMethod
    public int NextNo
    {
        set { hdn_NextNo.Value = Convert.ToInt32(value).ToString(); }
        get { return Util.String2Int(hdn_NextNo.Value); }
    }
    public int EndNo
    {
        set { hdn_EndNo.Value = Convert.ToInt32(value).ToString(); }
        get { return Util.String2Int(hdn_EndNo.Value); }
    }
    public int StartNo
    {
        set { hdn_StartNo.Value = Convert.ToInt32(value).ToString(); }
        get { return Util.String2Int(hdn_StartNo.Value); }
    }
    public int Allocation_Id
    {
        set { hdn_Allocation_Id.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_Allocation_Id.Value); }
    }
    public void Get_Next_No()
    {
        Label lbl_Slipno = new Label();

        int local_AllocationId;
        int local_Start_No;
        int local_End_No;
        int local_Next_No;


        local_AllocationId = 0;
        local_Start_No = 0;
        local_Next_No = 0;
        local_End_No = 0;


        objComm.Get_Document_Allocation_Details(ref local_AllocationId, ref local_Start_No,
                                                     ref local_End_No, ref local_Next_No, 0,
                                                     UserManager.getUserParam().MainId, 6);


        Allocation_Id = local_AllocationId;
        StartNo = local_Start_No;
        EndNo = local_End_No;
        NextNo = local_Next_No;

        if (Allocation_Id <= 0)
        {
            tr_petrol_grid.Visible = false;
            tr_petrol_amt.Visible = false;
        }
        else
        {
            petrolpanel = GetLocalResourceObject("Msg_Panel_GroupingText").ToString();
            lbl_Slipno.Text = "<b> (Please Allocate Slip No Between (" + StartNo + " To " + EndNo + "))</b";
            
            lbl_Slipno.Visible = true;
            tr_petrol_grid.Visible = true;
            tr_petrol_amt.Visible = true;
            pnp_Petrol.GroupingText = petrolpanel + lbl_Slipno.Text;            
        }
    }
       
    private void Next_ATH_Number()
    {
        ATHNo = objComm.Get_Next_Number();
    }
    public void ClearVariables()
    {
        SessionPetrolGrid = null;
    }
    #endregion

    #region AddVehicle

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        string VehicleCategoryId = WucVehicleSearch1.GetVehicleParameter("Vehicle_Category_ID");
        objDS = objATHPresenter.FillVehicleDetails();
        
        if (objDS.Tables[0].Rows.Count > 0)
        {
            VehicleCategory = objDS.Tables[0].Rows[0]["Vehicle_Category"].ToString();

            if (VehicleCategoryId == "1")
            {
                lbl_broker_text.Style.Add("visibility", "hidden");
                lbl_Broker.Style.Add("visibility", "hidden");
                OwnerName = objDS.Tables[0].Rows[0]["Vendor_Name"].ToString();

                SetCreditToEnabled = false;
            }
            else if (VehicleCategoryId == "5")
            {
                lbl_broker_text.Style.Add("visibility", "visible");
                lbl_Broker.Style.Add("visibility", "visible");
                OwnerName = objDS.Tables[0].Rows[0]["Owner_Name"].ToString();

                SetCreditToEnabled = true;
            }
            else
            {
                lbl_broker_text.Style.Add("display", "none");
                lbl_Broker.Style.Add("display", "none");
                OwnerName = objDS.Tables[0].Rows[0]["Vendor_Name"].ToString();

                SetCreditToEnabled = true;
            }
        }
        else
        {
            VehicleCategory = "0";
            OwnerName = "";
        }

        objATHPresenter.fillValues();
        ddl_LHPONo_SelectedIndexChanged(sender, e);

    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        pc.AddAttributes(this.Controls);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
      
        objATHPresenter = new ATHPresenter(this, IsPostBack);
        WucMRCashChequeDetails1.Scmcheque = scm_ATH;
        WucMRCashChequeDetails1.Is_AutoCalculate  = false ;

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            WucVehicleSearch1.SetAutoPostBack = true;
            Get_Next_No();

            if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e);
                ATHV_Date.AutoPostBackOnSelectionChanged = false;
                ddl_LHPONo.Enabled = false;
                WucVehicleSearch1.SetEnabled = false;
            }
            else
            {
               ATHV_Date.AutoPostBackOnSelectionChanged = true;
               Next_ATH_Number();
            }
            if(LHPOID<= 0)
               txt_TotalPaidAmt.Enabled = false;
            else
               txt_TotalPaidAmt.Enabled = true;

            SetStandardCaption();
        }
           
    }

    private void SetStandardCaption()
    {
        lbl_LHPO_no.Text = CompanyManager.getCompanyParam().LHPOCaption + "  No. :";
        lbl_LHPO_date.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Date :";
        lbl_broker_text.Text = CompanyManager.getCompanyParam().LHPOCaption + "  Broker :";
    }
    protected void dg_Petrol_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_PetrolPump = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_PetrolPump"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                txt_SlipNo = (TextBox)(e.Item.FindControl("txt_SlipNo"));
                txt_GridRemarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

                ddl_PetrolPump.DataTextField = "Petrol_Pump_Name";
                ddl_PetrolPump.DataValueField = "Petrol_Pump_ID";
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionPetrolGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                SetPetrolPump(DR["Petrol_Pump_Name"].ToString(), DR["Petrol_Pump_ID"].ToString());
                txt_Amount.Text = DR["Petrol_Amount"].ToString();
                txt_SlipNo.Text = DR["Petrol_Slip_No"].ToString();
                txt_GridRemarks.Text = DR["Remarks"].ToString();
            }
        }
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        ddl_PetrolPump = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_PetrolPump"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        txt_SlipNo = (TextBox)(e.Item.FindControl("txt_SlipNo"));
        txt_GridRemarks = (TextBox)(e.Item.FindControl("txt_Remarks"));

        objDT = SessionPetrolGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Petrol_Pump_ID"] = ddl_PetrolPump.SelectedValue;
            DR["Petrol_Pump_Name"] = ddl_PetrolPump.SelectedItem;
            DR["Petrol_Amount"] = txt_Amount.Text;
            DR["Petrol_Slip_No"] = txt_SlipNo.Text;
            DR["Remarks"] = txt_GridRemarks.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionPetrolGrid = objDT;
        }
    }
    private bool Allow_To_Add_Update()
    {

        lbl_Errors.Text = "";

        TextBox txt = (TextBox)ddl_PetrolPump.FindControl("txtBoxddl_PetrolPump");
        if (Util.String2Int(ddl_PetrolPump.SelectedValue) <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_ddl_PetrolPump").ToString();
            scm_ATH.SetFocus(txt);
        }
        else if (txt_Amount.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Amount").ToString();
            scm_ATH.SetFocus(txt_Amount);
        }
        else if (txt_SlipNo.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_SlipNo").ToString();
            scm_ATH.SetFocus(txt_SlipNo);
        }
        else if ((Util.String2Int(txt_SlipNo.Text) < StartNo) || (Util.String2Int(txt_SlipNo.Text) > EndNo))
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Petrol_Slip_Validation").ToString() + " (" + StartNo + " to " + EndNo + ")";
            scm_ATH.SetFocus(txt_SlipNo);
        }    
        //else if (txt_GridRemarks.Text.Trim() == string.Empty)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_txt_GridRemarks").ToString();
        //    scm_ATH.SetFocus(txt_GridRemarks);
        //}       
        else
        {
            isValid = true;
        }

        return isValid;
    }
    protected void dg_Petrol_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionPetrolGrid;

                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDT.Columns["Petrol_Slip_No"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindPetrolPumpGrid();
                    dg_Petrol.EditItemIndex = -1;
                    dg_Petrol.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = GetLocalResourceObject("Msg_Duplicate_Petrolslip").ToString();
                scm_ATH.SetFocus(txt_SlipNo);
            }
        }
    }
    protected void dg_Petrol_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Petrol.EditItemIndex = e.Item.ItemIndex;
        dg_Petrol.ShowFooter = false;
        BindPetrolPumpGrid();
    }
    protected void dg_Petrol_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionPetrolGrid;
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionPetrolGrid = objDT;
            dg_Petrol.EditItemIndex = -1;
            dg_Petrol.ShowFooter = true;
            BindPetrolPumpGrid();
        }
    }
    protected void dg_Petrol_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_Petrol.EditItemIndex = -1;
        dg_Petrol.ShowFooter = true;
        BindPetrolPumpGrid();
    }
    protected void dg_Petrol_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionPetrolGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDT.Columns["Petrol_Slip_No"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_Petrol.EditItemIndex = -1;
                dg_Petrol.ShowFooter = true;

                BindPetrolPumpGrid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = GetLocalResourceObject("Msg_Duplicate_Petrolslip").ToString();
            scm_ATH.SetFocus(txt_SlipNo);
        }
    }
    protected void ddl_LHPONo_SelectedIndexChanged(object sender, EventArgs e)
    {
        objDS = objATHPresenter.FillLHPODetails();
        if (objDS.Tables[0].Rows.Count > 0)
        {
            ManualReferenceNo = objDS.Tables[0].Rows[0]["Manual_Ref_No"].ToString();
            Driver = objDS.Tables[0].Rows[0]["Driver_Name"].ToString();
            AdvancePayableAmount =Util.String2Decimal(objDS.Tables[0].Rows[0]["AdvancePayable"].ToString());
            LHPODate = objDS.Tables[0].Rows[0]["LHPO_Date"].ToString();
            BrokerName = objDS.Tables[0].Rows[0]["Vendor_Name"].ToString();
            //TotalPaidAmount = Util.String2Decimal(objDS.Tables[0].Rows[0]["AdvancePayable"].ToString());
        }
        else
        {
            ManualReferenceNo = "0";
            Driver = "";
            AdvancePayableAmount = 0;
            LHPODate="";
            BrokerName = "";
            //TotalPaidAmount = 0;
        }
        if (LHPOID <= 0)
            txt_TotalPaidAmt.Enabled = false;
        else
            txt_TotalPaidAmt.Enabled = true;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objATHPresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void ATHV_Date_SelectionChanged(object sender, EventArgs e)
    {
        if (ATHV_Date.AutoPostBackOnSelectionChanged == true)
        {
            objATHPresenter.fillValues();
            ddl_LHPONo_SelectedIndexChanged(sender, e);
        }
    }
}
