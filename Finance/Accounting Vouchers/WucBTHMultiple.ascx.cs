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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using Raj.EC;
using Raj.EC.ControlsView;

public partial class Finance_Accounting_Vouchers_WucBTHMultiple : System.Web.UI.UserControl,IBTHMultipleView
{
    BTHMultiplePresenter objBTHMultiplePresenter;
    MRCashChequePresenter objCashChequePresenter;
    Common ObjCommon = new Common();
    DataTable objDT = null;
    DataRow DR = null;
    string Mode = "0";
    CheckBox chk;
    TextBox txt_BalanceToBePaid,txt_Other_Amount,txt_ActualBalance;
    DropDownList ddl_AddLess;
    Label lbl_LHPONo,lbl_TDS_On_Other_Charge;
    ClassLibrary.UIControl.DDLSearch ddl_Ledger;
    string _flag = "";
    string ClientCode = CompanyManager.getCompanyParam().ClientCode;

    #region Controls Value

    public string Flag
    {
        get { return _flag; }
    }

    public int Msg
    {
        get
        {
            if (Session["Msg"] == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(Session["Msg"]);
            }
        }
        set
        {
            Session["Msg"] = value;
        }
    }


    public string BTHVoucherNo
    {
        get { return txt_BTHVoucherNo.Text.Trim(); }
        set { txt_BTHVoucherNo.Text = value; }
    }
    public DateTime BTHVoucherDate
    {
        get { return Wuc_BTHVoucherDate.SelectedDate; }
        set { Wuc_BTHVoucherDate.SelectedDate = value; }
    }
    public DateTime LHPOFromDate
    {
        get { return dtp_LHPO_From.SelectedDate; }
        set { dtp_LHPO_From.SelectedDate = value; }
    }
    public DateTime LHPOToDate
    {
        get { return dtp_LHPO_To.SelectedDate; }
        set { dtp_LHPO_To.SelectedDate = value; }
    }
    public string ReferenceNo
    {
        get { return txt_ReferenceNo.Text.Trim(); }
        set { txt_ReferenceNo.Text = value; }
    }
    public decimal TotalPayableAmount
    {
        get { return Util.String2Decimal(hdn_Total_Payable_Amount.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Total_Payable_Amount.Value); }
        set
        {
            txt_TotalPayableAmount.Text = value.ToString();
            hdn_Total_Payable_Amount.Value = value.ToString();
        }
    }
    public decimal TotalOtherAmount
    {
        get { return Util.String2Decimal(hdn_Total_Advance_Amount.Value); }
        set
        {
            txt_Total_Advance_Amount.Text = value.ToString();
            hdn_Total_Advance_Amount.Value = value.ToString();
        }
    }

    public decimal TotalTDSAmount
    {
        get { return Util.String2Decimal(hdn_Total_TDS_Amount.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Total_TDS_Amount.Value); }
        set
        {
            txt_Total_TDS_Amount.Text = value.ToString();
            hdn_Total_TDS_Amount.Value = value.ToString();
        }
    }

    public int Total_No_Of_LHPO
    {
        get { return Util.String2Int(hdn_TotalLHPO.Value); }
        set { hdn_TotalLHPO.Value = value.ToString(); }
    }

    public decimal TotalBalanceToBePaid
    {
        get { return Util.String2Decimal(hdn_Total_Balance_To_Be_Paid.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Total_Balance_To_Be_Paid.Value); }
        set
        {
            txt_Total_Balance_To_Be_Paid.Text = value.ToString();
            hdn_Total_Balance_To_Be_Paid.Value = value.ToString();
        }
    }

    public decimal Other_Amount
    {
        get { return Util.String2Decimal(txt_Other_Amount.Text) <= 0 ? 0 : Util.String2Decimal(txt_Other_Amount.Text); }
    }
    private int Ledger_ID
    {
        get { return Util.String2Int(ddl_Ledger.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_Ledger.SelectedValue); }
    }
    private int Is_Add_Less
    {
        get { return Util.String2Int(ddl_AddLess.SelectedValue) <= 0 ? 0 : Util.String2Int(ddl_AddLess.SelectedValue); }
    }
    public string Remark
    {
        get { return txt_Remarks.Text.Trim(); }
        set { txt_Remarks.Text = value; }
    }
    public int BrokerOwnerID
    {
        get { return Util.String2Int(ddl_OwnerBroker.SelectedValue); }
    }

    public decimal Tax_Rate
    {
        get { return Util.String2Decimal(hdn_Tax_Rate.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Tax_Rate.Value); }
        set { hdn_Tax_Rate.Value = value.ToString(); }
    }

    public decimal Surcharge_Rate
    {
        get { return Util.String2Decimal(hdn_Surcharge_Rate.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Surcharge_Rate.Value); }
        set { hdn_Surcharge_Rate.Value = value.ToString(); }
    }

    public decimal Add_Surcharge_Rate
    {
        get { return Util.String2Decimal(hdn_Add_Surcharge_Rate.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Add_Surcharge_Rate.Value); }
        set { hdn_Add_Surcharge_Rate.Value = value.ToString(); }
    }

    public decimal Add_Edu_Cess_Rate
    {
        get { return Util.String2Decimal(hdn_Add_Edu_Cess_Rate.Value) <= 0 ? 0 : Util.String2Decimal(hdn_Add_Edu_Cess_Rate.Value); }
        set { hdn_Add_Edu_Cess_Rate.Value = value.ToString(); }
    }

    public IMRCashChequeDetailsView MRCashChequeDetailsView
    {
        get { return (IMRCashChequeDetailsView)WucMRCashChequeDetails1; }
    }
   
    public void BindLHPODetailsGrid()
    {
       dg_BTH_LHPO.DataSource = SessionLHPODetailsGrid;
       dg_BTH_LHPO.DataBind();
    }
    public DataTable SessionLHPODetailsGrid
    {
        get { return StateManager.GetState<DataTable>("LHPODetailsGrid"); }
        set
        {
            StateManager.SaveState("LHPODetailsGrid", value);
            if (StateManager.Exist("LHPODetailsGrid"))
            {
                BindLHPODetailsGrid();
            }
        }
    }
    public String LHPODetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = ObjCommon.Get_View_Table(SessionLHPODetailsGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "LHPOGridDetails";
            return _objDs.GetXml().ToLower();
        }
    }

    public void SetLedgerId(string text, string value)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    }
    public void SetOwnerID(string Name, string ID)
    {
        ddl_OwnerBroker.DataTextField = "Vendor_Name";
        ddl_OwnerBroker.DataValueField = "Vendor_ID";
        Raj.EC.Common.SetValueToDDLSearch(Name, ID, ddl_OwnerBroker);
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }
    public string errorMessage
    {
        set{lbl_Errors.Text = value; }
    }

    public bool validateUI()
    {
        bool _isvalid = false;

        //if (Datemanager.IsValidProcessDate("BTH", BTHVoucherDate) == false)
        //{
        //    errorMessage = "Please Enter Valid BTH Date";
        //}
      
        if (BTHVoucherDate > DateTime.Now)
        {
            errorMessage = "Please Select Valid BTH Voucher Date";
        }
        //else if (keyID <= 0 && LHPOFromDate < UserManager.getUserParam().StartDate)
        //{
        //    errorMessage = "LHPO From Date must be in current financial year";
       // }
        else if (keyID <= 0 && LHPOToDate > BTHVoucherDate)
        {
            errorMessage = "LHPO To Date Can't be greater than BTH Voucher Date";
        }
        else if (BrokerOwnerID <= 0)
        {
            errorMessage = "Please Select Owner/Broker";
        }        
        else if (Total_No_Of_LHPO <= 0)
        {
            errorMessage = "Please Select Atleast One " + Hdn_LHPOCaption.Value + " No.";
        }
        else if (TotalPayableAmount <= 0)
        {
            errorMessage = "Total Payable Amount Should be Greater Than Zero";
        }
        else if (TotalPayableAmount > 0 && WucMRCashChequeDetails1.validateWUCChequeDetails(lbl_Errors) == false)
        {
            _isvalid = false;
        }
        else if ((WucMRCashChequeDetails1.CashAmount + WucMRCashChequeDetails1.ChequeAmount) != TotalPayableAmount)
        {
           errorMessage = "Cash Amt and Cheque Amt should be equal to Total Payable Amount";
           _isvalid = false;
        }
        else if (WucMRCashChequeDetails1.ChequeAmount > 0 && WucMRCashChequeDetails1.ChequeAmount != MRCashChequeDetailsView.Total_ChequeAmount)
        {
            errorMessage = "Please Enter Valid Cheque Amount";
            _isvalid = false;
        }
        else if (Validate_Grid() == false)
        {
            _isvalid = false;
        }
        else
        {
            _isvalid = true;
        }

        //if (keyID > 0)
        //{
        //    if (BTHVoucherDate < LHPO_Date)
        //    {
        //        errorMessage = "Please Enter BTHVoucherDate Less Than LHPO date";
        //        _isvalid = false;
        //    }
        //}
        return _isvalid;
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
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        if (!IsPostBack)
        {
            GetCompanyParameters();
        }
        SetStandardCaption();

        objCashChequePresenter = new MRCashChequePresenter(WucMRCashChequeDetails1, IsPostBack);
        objBTHMultiplePresenter = new BTHMultiplePresenter(this, IsPostBack);

        WucMRCashChequeDetails1.Scmcheque = SCMBTHMultiple;

        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
            
        if (!IsPostBack)
        {
            if (Msg == 1)
            {
                Response.Write("<script language='Javascript'>alert('Saved Successfully')</script>");
            }
            Msg = 0;

            if (keyID <= 0)
            {
                LHPOFromDate = DateTime.Now;
                LHPOToDate = DateTime.Now;

                SetHiddenValues_To_Zero();
                BTHVoucherNo = ObjCommon.Get_Next_Number();
                BindLHPODetailsGrid();
            }
            else
            {
                ddl_OwnerBroker.Enabled = false;
                tr_LhPODate.Visible = false;
                btn_Go.Enabled = false;
                Wuc_BTHVoucherDate.AutoPostBackOnSelectionChanged = false;
            }

            if (keyID > 0)
            {
                btn_SaveNew.Style.Add("display", "none");
            }
        }

        SetHiddenValues_To_textBox();

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }

    private void SetStandardCaption()
    {      
        dg_BTH_LHPO.Columns[1].HeaderText = Hdn_LHPOCaption.Value + "  No";
        dg_BTH_LHPO.Columns[2].HeaderText = Hdn_AUSCaption.Value + "  No";
        dg_BTH_LHPO.Columns[3].HeaderText = Hdn_LHPOCaption.Value + "  Date";
        dg_BTH_LHPO.Columns[7].HeaderText = Hdn_LHPOCaption.Value + "  Branch";
        if (Hdn_BTH_Setteled_By_Vehicle.Value.ToLower() == "false")
            lbl_OwnerBroker.Text= "Owner/Broker :";
        else
            lbl_OwnerBroker.Text = "Vehicle No :";
    }

    private void GetCompanyParameters()
    {
        DataSet ds = new DataSet();
        ds = ObjCommon.Get_Values_Where("EC_Master_Company_Parameters", "LHPO_Caption,AUS_Caption,Is_BTH_Setteled_By_Vehicle", "", "Company_Parameters_ID", false);

        Hdn_LHPOCaption.Value = ds.Tables[0].Rows[0]["LHPO_Caption"].ToString();
        Hdn_AUSCaption.Value = ds.Tables[0].Rows[0]["AUS_Caption"].ToString();
        Hdn_BTH_Setteled_By_Vehicle.Value = ds.Tables[0].Rows[0]["Is_BTH_Setteled_By_Vehicle"].ToString();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_lhpogriddetails();
        objBTHMultiplePresenter.Save();
    }

    protected void btn_SaveNew_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_lhpogriddetails();
        objBTHMultiplePresenter.Save();
    }

    protected void btn_SavePrint_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndPrint";
        calculate_lhpogriddetails();
        objBTHMultiplePresenter.Save();        
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }   

    public void SetHiddenValues_To_textBox()
    {
        txt_Total_Advance_Amount.Text = hdn_Total_Advance_Amount.Value;
        txt_Total_Balance_To_Be_Paid.Text = hdn_Total_Balance_To_Be_Paid.Value;
        txt_TotalPayableAmount.Text = hdn_Total_Payable_Amount.Value;
        txt_Total_TDS_Amount.Text = hdn_Total_TDS_Amount.Value;
    }

    public void SetHiddenValues_To_Zero()
    {
        hdn_TotalLHPO.Value = "0";
        hdn_Total_Balance_To_Be_Paid.Value = "0";
        hdn_Total_Advance_Amount.Value = "0";
        hdn_Total_TDS_Amount.Value = "0";
        hdn_Total_Payable_Amount.Value = "0";
        txt_Total_Balance_To_Be_Paid.Text = "0";
        txt_Total_Advance_Amount.Text = "0";
        txt_TotalPayableAmount.Text = "0";
        txt_Total_TDS_Amount.Text = "0";
    }   
    protected void Wuc_BTHVoucherDate1_SelectionChanged(object sender, EventArgs e)
    {
        if (keyID <= 0)
        {
            objBTHMultiplePresenter.Fill_LHPONoDetails();
            BindLHPODetailsGrid();
            Clear_Fields();
        }
    }
    public void ClearVariables()
    {
        MRCashChequeDetailsView.Session_ChequeDetailsGrid = null;
        MRCashChequeDetailsView.Session_ddl_DepositIn = null;
        SessionLHPODetailsGrid = null;
    }
    public void Clear_Fields()
    {
        MRCashChequeDetailsView.CashAmount = 0;
        MRCashChequeDetailsView.ChequeAmount = 0;
        MRCashChequeDetailsView.Total_ChequeAmount = 0;
        SetHiddenValues_To_Zero();

        String Script = "<script type='text/javascript'>Cheque_Amount(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }
    protected void ddl_OwnerBroker_TxtChange(object sender, EventArgs e)
    {
        objBTHMultiplePresenter.Fill_LHPONoDetails();
        BindLHPODetailsGrid();
        Clear_Fields();
    }
    protected void btn_Go_OnClick(object sender, EventArgs e)
    {
        objBTHMultiplePresenter.Fill_LHPONoDetails();
        BindLHPODetailsGrid();
        Clear_Fields();
    }
    private void calculate_lhpogriddetails()
    {
        Total_No_Of_LHPO = 0;
        TotalPayableAmount = 0;
        TotalOtherAmount = 0;
        TotalBalanceToBePaid = 0;
        TotalTDSAmount = 0;
        int i;

        if (dg_BTH_LHPO.Items.Count > 0)
        {
            for (i = 0; i <= dg_BTH_LHPO.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_BTH_LHPO.Items[i].FindControl("Chk_Attach");
                txt_Other_Amount = (TextBox)dg_BTH_LHPO.Items[i].FindControl("txt_Amount");
                txt_BalanceToBePaid = (TextBox)dg_BTH_LHPO.Items[i].FindControl("txt_BalanceToBePaid");
                ddl_AddLess = (DropDownList)dg_BTH_LHPO.Items[i].FindControl("ddl_AddLess");
                ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)dg_BTH_LHPO.Items[i].FindControl("ddl_LedgerName");
                lbl_TDS_On_Other_Charge = (Label)dg_BTH_LHPO.Items[i].FindControl("lbl_TDSonOtherCharge");
                decimal tdsamount = 0;

                tdsamount = Calculate_TDS_Details(chk);
                
                if (chk.Checked == true)
                {
                    Total_No_Of_LHPO = Total_No_Of_LHPO + 1;
                    if (Is_Add_Less == 0)
                    {
                        TotalOtherAmount = TotalOtherAmount + 0;
                    }
                    else if(Is_Add_Less == 1)
                    {
                        TotalOtherAmount = TotalOtherAmount + Other_Amount;
                    }
                    else if (Is_Add_Less == 2)
                    {
                        TotalOtherAmount = TotalOtherAmount - Other_Amount;
                    }
                    TotalBalanceToBePaid = TotalBalanceToBePaid + Util.String2Decimal(txt_BalanceToBePaid.Text);
                    TotalPayableAmount = TotalBalanceToBePaid + TotalOtherAmount;
                }

                SessionLHPODetailsGrid.Rows[i]["Att"] = chk.Checked;
                SessionLHPODetailsGrid.Rows[i]["IsAddLess"] = Is_Add_Less;
                SessionLHPODetailsGrid.Rows[i]["Ledger_Id"] = Ledger_ID;
                SessionLHPODetailsGrid.Rows[i]["Other_Charge_Amount"] = Other_Amount;
                SessionLHPODetailsGrid.Rows[i]["TDS_On_OtherCharge"] = Math.Ceiling(tdsamount);
                SessionLHPODetailsGrid.Rows[i]["Balance_Paid_Amount"] = Util.String2Decimal(txt_BalanceToBePaid.Text) <= 0 ? 0 : Util.String2Decimal(txt_BalanceToBePaid.Text);
                lbl_TDS_On_Other_Charge.Text = Convert.ToString(Math.Ceiling(tdsamount));
            }

            TotalTDSAmount = Math.Ceiling(Convert.ToDecimal(SessionLHPODetailsGrid.Compute("Sum(TDS_On_OtherCharge)", "")));
            TotalPayableAmount = TotalPayableAmount - TotalTDSAmount;
        }
    }

    private decimal Calculate_TDS_Details(CheckBox chk1)
    {
        decimal TDS_amount,Tax_amount,Surcharge_amount,Add_Surcharge_amount,Add_Edu_Cess_amount;
        Tax_amount  = Surcharge_amount  = Add_Surcharge_amount = Add_Edu_Cess_amount = 0;

        TDS_amount = (Other_Amount * Tax_Rate) / 100;
        Surcharge_amount = (TDS_amount * Surcharge_Rate) / 100;
        Add_Surcharge_amount = ((TDS_amount + Surcharge_amount) * Add_Surcharge_Rate) / 100;
        Add_Edu_Cess_amount = ((TDS_amount + Surcharge_amount) * Add_Edu_Cess_Rate) / 100;

        if (Is_Add_Less == 1 && chk1.Checked == true)
        {
            TDS_amount = TDS_amount + Surcharge_amount + Add_Surcharge_amount + Add_Edu_Cess_amount;
        }
        else
        {
            TDS_amount = 0;
        }

        return TDS_amount;
    }

    private bool Validate_Grid()
    {
        bool ATS = true;
        errorMessage = "";
        int i;

        if (dg_BTH_LHPO.Items.Count > 0)
        {
            for (i = 0; i <= dg_BTH_LHPO.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_BTH_LHPO.Items[i].FindControl("Chk_Attach");
                txt_Other_Amount = (TextBox)dg_BTH_LHPO.Items[i].FindControl("txt_Amount");
                ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)dg_BTH_LHPO.Items[i].FindControl("ddl_LedgerName");
                lbl_LHPONo = (Label)dg_BTH_LHPO.Items[i].FindControl("lbl_LHPONo");
                txt_ActualBalance = (TextBox)dg_BTH_LHPO.Items[i].FindControl("txt_ActualBalance");
                txt_BalanceToBePaid = (TextBox)dg_BTH_LHPO.Items[i].FindControl("txt_BalanceToBePaid");

                if (Convert.ToDecimal(txt_ActualBalance.Text) < (Util.String2Decimal(txt_BalanceToBePaid.Text) <= 0 ? 0 : Util.String2Decimal(txt_BalanceToBePaid.Text)))
                {
                    errorMessage = "Please Enter Balance To Be Paid Less than Actual Balance for " + CompanyManager.getCompanyParam().LHPOCaption + " No." + lbl_LHPONo.Text;
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Ledger_ID > 0 && Other_Amount <= 0)
                {
                    errorMessage = "Please Enter Other Amount for " + CompanyManager.getCompanyParam().LHPOCaption + " No." + lbl_LHPONo.Text;
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Other_Amount > 0 && Ledger_ID <= 0)
                {
                    errorMessage = "Please Select Ledger for " + CompanyManager.getCompanyParam().LHPOCaption + " No." + lbl_LHPONo.Text;
                    ATS = false;
                    break;
                }
               
            }
        }

        return ATS;
    }

    protected void dg_BTH_LHPO_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculategrid = "";
        string calculate_grid = "";
        string calculate_grid1 = "";
        string calculate_grid2 = "";
        LinkButton lbtn_AUS_No;
        HiddenField hdn_AUS_Id;
        CheckBox chk_Attach;
        TextBox Txt_Other_Amount;
        DropDownList ddl_AddLess;
        TextBox txt_BalanceToBePaid;

        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lbtn_AUS_No = (LinkButton)(e.Item.FindControl("lbtn_AUSNo"));
                hdn_AUS_Id = (HiddenField)(e.Item.FindControl("hdn_AUS_Id"));
                chk_Attach = (CheckBox)e.Item.FindControl("Chk_Attach");
                Txt_Other_Amount = (TextBox)e.Item.FindControl("txt_Amount");
                ddl_AddLess = (DropDownList)e.Item.FindControl("ddl_AddLess");
                ddl_Ledger = (ClassLibrary.UIControl.DDLSearch)e.Item.FindControl("ddl_LedgerName");
                lbl_TDS_On_Other_Charge = (Label)e.Item.FindControl("lbl_TDSonOtherCharge");
                txt_BalanceToBePaid = (TextBox)e.Item.FindControl("txt_BalanceToBePaid");
                txt_ActualBalance = (TextBox)e.Item.FindControl("txt_ActualBalance");


                calculategrid = "Check_Single(" + chk_Attach.ClientID + ",'j','5')";
                calculate_grid = "Check_Single(" + chk_Attach.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk_Attach.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk_Attach.ClientID + ",'j','4')";

                txt_BalanceToBePaid.Attributes.Add("onchange", calculategrid);
               
                Txt_Other_Amount.Attributes.Add("onblur", calculate_grid);
                Txt_Other_Amount.Attributes.Add("onfocus", calculate_grid1);
                ddl_AddLess.Attributes.Add("onchange", calculate_grid2);

                lbtn_AUS_No.Attributes.Add("onclick", "return viewwindow('AUS','" + hdn_AUS_Id.Value + "')");

                if (ClientCode == "reach")
                {
                    txt_ActualBalance.Enabled = false;  //  For Reach Added on 2/12/2009 by Vajiha
                }
                  

                if (keyID > 0)
                {
                    objDT = SessionLHPODetailsGrid;
                    DR = objDT.Rows[e.Item.ItemIndex];

                    ddl_AddLess.SelectedValue = DR["IsAddLess"].ToString();
                    SetLedgerId(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());
                }
            }
        }
    }
}
