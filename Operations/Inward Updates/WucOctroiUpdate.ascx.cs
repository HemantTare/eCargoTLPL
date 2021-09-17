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
using System.Text;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Operations_Octroi_Update_WucOctroiUpdate : System.Web.UI.UserControl, IOctroiUpdateView
{
    #region ClassVariables
    OtherChargeLedgerPresenter objOtherChargeLedgerPresenter;
    OctroiUpdatePresenter objOctroiUpdatePresenter;
    PageControls pc = new PageControls();
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    DropDownList ddl_OctroiFormType, ddl_OctroiPaidBy;
    Label lbl_GCNo, lbl_BookingDate, lbl_BookingBranch, lbl_DeliveryBranch;
    TextBox txt_OctroiReceiptNo, txt_OctroiAmount, txt_Remark;
    string _GC_No_XML;
    int Mode;
    DAL objDAL;
    #endregion

    #region ControlsValues
    public string TransactionNo
    {
        set { lbl_Tranaction_No.Text = value; }
    }
    public DateTime TransactionDate
    {
        set { dtp_Transaction_Date.SelectedDate = value; }
        get { return dtp_Transaction_Date.SelectedDate; }
    }
    public int LedgerID
    {
        get { return Util.String2Int(ddl_LedgerName.SelectedValue); }
        set { ddl_LedgerName.SelectedValue = Util.Int2String(value); }
    }
    public string BillNo
    {
        get { return txt_BillNo.Text; }
        set { txt_BillNo.Text = value; }
    }
    public DateTime BillDate
    {
        set { dtp_Bill_Date.SelectedDate = value; }
        get { return dtp_Bill_Date.SelectedDate; }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public string ChequeNo
    {
        set { txt_ChequeNo.Text = value; }
        get { return txt_ChequeNo.Text; }
    }
    public DateTime ChequeDate
    {
        set { dtp_ChequeDate.SelectedDate = value; }
        get { return dtp_ChequeDate.SelectedDate; }
    }
    public string NameOfBank
    {
        set { txt_BankName.Text = value; }
        get { return txt_BankName.Text; }
    }
    public string GCAlreadyUpdated
    {
        set { txt_GetGCUpdated.Text = value; }
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
    public decimal Total_Amount
    {
        set
        {
            hdn_TotalAmount.Value = Util.Decimal2String(value);
            lbl_TotalAmt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalAmount.Value); }
    }

    public decimal Grand_Total
    {
        set 
        {
            txt_Total_Amount_Value.Text = value.ToString("0.00");
        }
        get
        {
            return Convert.ToDecimal(txt_Total_Amount_Value.Text);
        }
    }

    public decimal OtherChargeAmount
    {
        get
        {
            return OtherChargeLedgerView.TotalAmount;
        }
        set
        {
            OtherChargeLedgerView.TotalAmount = value;
        }
    }

    public DataTable SessionBindOctroiUpdateGrid
    {
        get { return StateManager.GetState<DataTable>("OctroiUpdateGrid"); }
        set
        {
                Mode = Raj.EC.Common.GetMode();

                if (Mode == 4 || Mode == 2)
                {
                    StateManager.SaveState("OctroiUpdateGrid", value);
                }
                else
                {
                    if (value != null)
                    {
                        DataTable dt;
                        dt = objComm.Get_View_Table(value, "is_octroi_updated=0").ToTable();
                        StateManager.SaveState("OctroiUpdateGrid", dt);
                        dt = null;
                    }
                }
        }
    }

    public String OctroiUpdateDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            _objDs.Tables.Add(SessionBindOctroiUpdateGrid.Copy());

            _objDs.Tables[0].TableName = "OctroiUpdateDetails";
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
    public int LedgerGroupId
    {
        set { hdn_LedgerGroupId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_LedgerGroupId.Value); }
    }

    public void SetLedgerId(string text, string value)
    {
        ddl_LedgerName.DataTextField = "Ledger_Name";
        ddl_LedgerName.DataValueField = "Ledger_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_LedgerName);
    }

    public IOtherChargeLedgerView OtherChargeLedgerView
    {
        get { return (IOtherChargeLedgerView)WucOtherChargeLedger1; }
    }

    #endregion

    #region ControlsBind

    public DataTable BindOctroiUpdateGrid
    {
        set
        {
            dg_OctroiUpdate.DataSource = SessionBindOctroiUpdateGrid;
            dg_OctroiUpdate.DataBind();
        }
    }

    public DataTable BindOctroiFormType
    {
        set
        {
            ddl_OctroiFormType.DataSource = SessionOctroiFormType;
            ddl_OctroiFormType.DataTextField = "Octroi_Form_Type";
            ddl_OctroiFormType.DataValueField = "Octroi_Form_Type_ID";
            ddl_OctroiFormType.DataBind();
            ddl_OctroiFormType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable SessionOctroiFormType
    {
        get { return StateManager.GetState<DataTable>("OctroiFormTypeDropDown"); }
        set { StateManager.SaveState("OctroiFormTypeDropDown", value); }
    }

    public DataTable BindOctroiPaidBy
    {
        set
        {
            ddl_OctroiPaidBy.DataSource = SessionOctroiPaidBy;
            ddl_OctroiPaidBy.DataTextField = "Octroi_Paid_By";
            ddl_OctroiPaidBy.DataValueField = "Octroi_Paid_By_ID";
            ddl_OctroiPaidBy.DataBind();
            ddl_OctroiPaidBy.Items.Insert(0, new ListItem("Select one", "0"));
        }
    }

    public DataTable SessionOctroiPaidBy
    {
        get { return StateManager.GetState<DataTable>("OctroiPaidByDropDown"); }
        set { StateManager.SaveState("OctroiPaidByDropDown", value);}
    }
    

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (Datemanager.IsValidProcessDate("OCT_UPD", TransactionDate) == false)
        {
            errorMessage = "Please Select Valid Transaction Date";
        }
        else if (Convert.ToDateTime(TransactionDate) < Convert.ToDateTime(BillDate))
        {
            errorMessage = "Bill Date can't be greater than Transaction Date";
        }
        else if (LedgerID <= 0)
        {
            errorMessage = "Please Select Ledger Name";
            ddl_LedgerName.Focus();
        }
        else if (txt_BillNo.Text == string.Empty && pc.Control_Is_Mandatory(txt_BillNo))
        {
            errorMessage = "Please Enter Bill No";
            txt_BillNo.Focus();
        }
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "No " + CompanyManager.getCompanyParam().GcCaption + " details Found";
        }
        else if (GridValidation() == false)
        {
        }
        else if (LedgerGroupId == 19 && txt_ChequeNo.Text == string.Empty)
        {
            errorMessage = "Please Enter Cheque No";
            txt_ChequeNo.Focus();
        }
        else if (LedgerGroupId == 19 && txt_BankName.Text == string.Empty)
        {
            errorMessage = "Please Enter Bank Name";
            txt_BankName.Focus();
        }
        else if (Grand_Total < 0)
        {
            errorMessage = "Grand Total cannot be less than 0";
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

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    #endregion

    #region OtherMethod
    private void MakeDT()
    {
        DataTable objDT;

        int i = 0;

        if (dg_OctroiUpdate.Items.Count > 0)
        {
            for (i = 0; i <= dg_OctroiUpdate.Items.Count - 1; i++)
            {
                Label GC_Id = (Label)dg_OctroiUpdate.Items[i].FindControl("lbl_GC_ID");
                ddl_OctroiFormType = (DropDownList)dg_OctroiUpdate.Items[i].FindControl("ddl_OctroiFormType");
                ddl_OctroiPaidBy = (DropDownList)dg_OctroiUpdate.Items[i].FindControl("ddl_OctroiPaidBy");
                txt_OctroiReceiptNo = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_OctroiReceiptNo");
                txt_OctroiAmount = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_OctroiAmount");
                txt_Remark = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_Remark");

                SessionBindOctroiUpdateGrid.Rows[i]["GC_ID"] = GC_Id.Text;
                SessionBindOctroiUpdateGrid.Rows[i]["Octroi_Form_Type"] = ddl_OctroiFormType.SelectedItem.Text;
                SessionBindOctroiUpdateGrid.Rows[i]["Octroi_Form_Type_Id"] = Util.String2Int(ddl_OctroiFormType.SelectedValue);
                SessionBindOctroiUpdateGrid.Rows[i]["Octroi_Paid_By_Id"] = Util.String2Int(ddl_OctroiPaidBy.SelectedValue);
                SessionBindOctroiUpdateGrid.Rows[i]["Oct_Receipt_No"] = txt_OctroiReceiptNo.Text;
                SessionBindOctroiUpdateGrid.Rows[i]["Oct_Amount"] = Util.String2Decimal(txt_OctroiAmount.Text);
                SessionBindOctroiUpdateGrid.Rows[i]["Oct_Remark"] = txt_Remark.Text;
            }
            objDT = SessionBindOctroiUpdateGrid;
        }
    }

    private void CalculateTotalAmount()
    {
        int i = 0;
        decimal Amount = 0;
        Total_Amount = 0;

        if (dg_OctroiUpdate.Items.Count > 0)
        {
            for (i = 0; i <= dg_OctroiUpdate.Items.Count - 1; i++)
            {
                ddl_OctroiPaidBy = (DropDownList)dg_OctroiUpdate.Items[i].FindControl("ddl_OctroiPaidBy");
                txt_OctroiAmount = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_OctroiAmount");

                if ( Util.String2Int(ddl_OctroiPaidBy.SelectedValue) == 3)
                {
                    if (txt_OctroiAmount.Text == "" || txt_OctroiAmount.Text == string.Empty)
                    {
                        txt_OctroiAmount.Text = "0";
                    }

                    Amount = Amount + Util.String2Decimal(txt_OctroiAmount.Text);
                }
            }
        }
        Total_Amount = Amount;

        Grand_Total = Total_Amount + OtherChargeAmount;
    }

    private bool GridValidation()
    {
        bool ATS = true;
        int i = 0;

        if (dg_OctroiUpdate.Items.Count > 0)
        {
            for (i = 0; i <= dg_OctroiUpdate.Items.Count - 1; i++)
            {
                Label GC_No = (Label)dg_OctroiUpdate.Items[i].FindControl("lbl_GCNo");
                ddl_OctroiFormType = (DropDownList)dg_OctroiUpdate.Items[i].FindControl("ddl_OctroiFormType");
                ddl_OctroiPaidBy = (DropDownList)dg_OctroiUpdate.Items[i].FindControl("ddl_OctroiPaidBy");
                txt_OctroiReceiptNo = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_OctroiReceiptNo");
                txt_OctroiAmount = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_OctroiAmount");
                txt_Remark = (TextBox)dg_OctroiUpdate.Items[i].FindControl("txt_Remark");
                lbl_BookingDate = (Label)(dg_OctroiUpdate.Items[i].FindControl("lbl_BookingDate"));


                if (Convert.ToDateTime(TransactionDate) < Convert.ToDateTime(lbl_BookingDate.Text))
                {
                    errorMessage = "Transaction Date can't be less than Booking Date of " + CompanyManager.getCompanyParam().GcCaption + " No : " + GC_No.Text;
                    ATS = false;
                    break;
                }
                else if (Convert.ToDateTime(BillDate) < Convert.ToDateTime(lbl_BookingDate.Text))
                {
                    errorMessage = "Octroi Bill Date can't be less than Booking Date of " + CompanyManager.getCompanyParam().GcCaption + " No : " + GC_No.Text;
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_OctroiFormType.SelectedValue) == 0)
                {
                    errorMessage = "Please Select Octroi Form Type";
                    scm_OctroiUpdate.SetFocus(ddl_OctroiFormType);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_OctroiPaidBy.SelectedValue) == 0)
                {
                    errorMessage = "Please Select Octroi Paid By";
                    scm_OctroiUpdate.SetFocus(ddl_OctroiPaidBy);
                    ATS = false;
                    break;
                }
                else if (txt_OctroiReceiptNo.Text == string.Empty)
                {
                    errorMessage = "Please Enter Octroi Receipt No";
                    scm_OctroiUpdate.SetFocus(txt_OctroiReceiptNo);
                    ATS = false;
                    break;
                }
                else if (Util.String2Decimal(txt_OctroiAmount.Text) <= 0)
                {
                    errorMessage = "Please Enter Octroi Amount";
                    scm_OctroiUpdate.SetFocus(txt_OctroiAmount);
                    ATS = false;
                    break;
                }
                else if (txt_Remark.Text == string.Empty)
                {
                    errorMessage = "Please Enter Remarks";
                    scm_OctroiUpdate.SetFocus(txt_Remark);
                    ATS = false;
                    break;
                }
            }
        }

        return ATS;
    }
    private void NexTransactionNumber()
    {
        TransactionNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {
            if (WucSelectedItems1.EnterItem != string.Empty)
            {
                _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
                objOctroiUpdatePresenter.fillgrid();
                WucSelectedItems1.dtdetails = SessionBindOctroiUpdateGrid;
                calculate_griddetails();
                CalculateTotalAmount();
                WucSelectedItems1.Get_Not_Selected_Items();
            }
        }
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 3;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + " Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + " Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = CompanyManager.getCompanyParam().GcCaption; //added Ankit
        lbl_TotalGC.Text = "Total  " + hdn_GCCaption.Value + " :";
        lbl_GetGCUpdated.Text = hdn_GCCaption.Value + " Already Updated :";
        dg_OctroiUpdate.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + " No";
    }

    private void calculate_griddetails()
    {
        int TotalGC = 0;
        int i;

        if (dg_OctroiUpdate.Items.Count > 0)
        {
            objDT = SessionBindOctroiUpdateGrid;

            for (i = 0; i <= dg_OctroiUpdate.Items.Count - 1; i++)
            {
                TotalGC = TotalGC + 1;
            }
        }
        Total_No_Of_GC = TotalGC;
    }

    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_LedgerName.DataTextField = "Ledger_Name";
        ddl_LedgerName.DataValueField = "Ledger_Id";

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        SetStandardCaption();

        
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Close));

        objOctroiUpdatePresenter = new OctroiUpdatePresenter(this, IsPostBack);
        WucOtherChargeLedger1.OtherDetailsGrid += new EventHandler(Calculate_Total_On_OtherCharge);


        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);

            if (keyID <= 0)
            {
                NexTransactionNumber();
                
            }
            else
            {
                td_gccontrol.Style.Add("display", "none");
                td_gcalreadyupdated.Style.Add("display", "none");
            }
            if (LedgerGroupId == 19)
            {
                tr_ChequeDetails.Visible = true;
                tr_BankName.Visible = true;
            }
            else
            {
                tr_ChequeDetails.Visible = false;
                tr_BankName.Visible = false;
            }
                 
           
        }
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        lbl_TotalAmt.Text = hdn_TotalAmount.Value;
        hdn_Ledger.Value = ddl_LedgerName.SelectedValue;
        //objComm.Disable_save_button_on_click(Page, btn_Save, "ValidateUI()");

    }
    protected void ddl_LedgerName_TxtChange(object sender, EventArgs e)
    {
        DataSet objDS = objOctroiUpdatePresenter.GetLedgerGroupId();
        LedgerGroupId = Util.String2Int(objDS.Tables[0].Rows[0]["Ledger_Group_Id"].ToString());
        if (LedgerGroupId == 19)
        {
            tr_ChequeDetails.Visible = true;
            tr_BankName.Visible = true;
        }
        else
        {
            tr_ChequeDetails.Visible = false;
            tr_BankName.Visible = false;
            ChequeNo = "0";
            NameOfBank = "";
            //ChequeDate = 0;
        }  
    }

    protected void btnAmount_Click(object sender, EventArgs e)
    {
        CalculateTotalAmount();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        MakeDT();
        calculate_griddetails();
        objOctroiUpdatePresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    #endregion

    #region GridEvents
    protected void dg_OctroiUpdate_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            lbl_GCNo = (Label)(e.Item.FindControl("lbl_GCNo"));
            lbl_BookingDate = (Label)(e.Item.FindControl("lbl_BookingDate"));
            lbl_BookingBranch = (Label)(e.Item.FindControl("lbl_BookingBranch"));
            lbl_DeliveryBranch = (Label)(e.Item.FindControl("lbl_DeliveryBranch"));
            ddl_OctroiFormType = (DropDownList)(e.Item.FindControl("ddl_OctroiFormType"));
            ddl_OctroiPaidBy = (DropDownList)(e.Item.FindControl("ddl_OctroiPaidBy"));
            txt_OctroiReceiptNo = (TextBox)(e.Item.FindControl("txt_OctroiReceiptNo"));
            txt_OctroiAmount = (TextBox)(e.Item.FindControl("txt_OctroiAmount"));
            txt_Remark = (TextBox)(e.Item.FindControl("txt_Remark"));
            Label lbl_CanEdit = (Label)(e.Item.FindControl("lbl_CanEdit"));

            BindOctroiFormType = SessionOctroiFormType;
            BindOctroiPaidBy = SessionOctroiPaidBy;

            if (keyID > 0)
            {
                ddl_OctroiFormType.SelectedValue = SessionBindOctroiUpdateGrid.Rows[e.Item.ItemIndex]["Octroi_Form_Type_Id"].ToString();
                ddl_OctroiPaidBy.SelectedValue = SessionBindOctroiUpdateGrid.Rows[e.Item.ItemIndex]["Octroi_Paid_By_id"].ToString();

                if (ddl_OctroiPaidBy.SelectedValue == "1" && lbl_CanEdit.Text == Convert.ToBoolean(0).ToString())
                {
                    ddl_OctroiFormType.Enabled = false;
                    ddl_OctroiPaidBy.Enabled = false;
                    txt_OctroiReceiptNo.Enabled = false;
                    txt_Remark.Enabled = false;
                    txt_OctroiAmount.Enabled = false;
                   // e.Item.Enabled = false;
                }

                if (Convert.ToBoolean(DataBinder.Eval(e.Item.DataItem, "Is_Delivery_MR_Or_Credit_Memo_Prepared")) == false )
                    e.Item.Enabled=false;
            }
        }
    }
    #endregion

    public void ClearVariables() // added Ankit
    {
        SessionBindOctroiUpdateGrid = null;
        SessionOctroiFormType = null;
        SessionOctroiPaidBy = null;
    }

    public void Calculate_Total_On_OtherCharge(object sender, EventArgs e)
    {
        Grand_Total = (Total_Amount < 0 ? 0 : Total_Amount) + OtherChargeAmount;
    }
   
}