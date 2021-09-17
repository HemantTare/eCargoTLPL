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
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_WucOnaccountAdjustment : System.Web.UI.UserControl,OnaccountAdjustmentView
{

    decimal BillAmount = 0, PendingAmount = 0;
    Common objCommon = new Common();

    #region ONACCOUNTADJUSTMENT.VIEW
    public string errorMessage
    {
        set
        {
            lbl_Error.Text = value;
            //up_lblError.Update();
        }
    }

    public int keyID
    {
        get { return 0; }
    }

    public bool validateUI()
    {
        return validateUnadjustedAmount();
    }

    public DataTable BindOnAccountUnAdjustedGrid
    {
        set
        {
            dg_OnAccountAdjustment.DataSource = value;
            dg_OnAccountAdjustment.DataBind();
        }
    }

    public DataTable BindOnAccountAdjustedGrid
    {
        set
        {
            dg_Adjusted.DataSource = value;
            dg_Adjusted.DataBind();
        }

    }

    public DataTable SessionOnAccount
    {
        get { return StateManager.GetState<DataTable>("dtOnaccount"); }
        set { StateManager.SaveState("dtOnaccount", value); }
    }

    public DataTable SessionOnAccountAdjusted
    {
        get { return StateManager.GetState<DataTable>("dtonaccountadjusted"); }
        set { StateManager.SaveState("dtonaccountadjusted", value); }
    }

    public decimal UnAdjustedAmount
    {
        get { return Convert.ToDecimal(ViewState["unadjustamount"]); }
        set { ViewState["unadjustamount"] = value;
              hdn_unAdjAmount.Value = value.ToString();
            }
    }

    public int UnAdjustedVoucherId
    {
        get { return Convert.ToInt32(ViewState["unadjustVoucherID"]); }
        set { ViewState["unadjustVoucherID"] = value; }
    }

    public int UnAdjustedSrNo
    {
        get { return Convert.ToInt32(ViewState["UnadjustSrno"]); }
        set { ViewState["UnadjustSrno"] = value; }
    }

    public DateTime UnAdjustVoucherDate
    {
        get { return Convert.ToDateTime(ViewState["UnadjustVchDAte"]); }
        set { ViewState["UnadjustVchDAte"] = value; }
    }

    //public int LedgerId
    //{
    //    get { return _LedgerId; }
    //    set { _LedgerId = value; }
    //}

    public DataTable BindLedgerGroup
    {
        set
        {
            ddl_LedgerGroup.DataSource = value;
            ddl_LedgerGroup.DataTextField = "Ledger_Group_Name";
            ddl_LedgerGroup.DataValueField = "Ledger_Group_Id";
            ddl_LedgerGroup.DataBind();

            SessionLedgerGroupId = Util.String2Int(ddl_LedgerGroup.SelectedValue);
        }

    }

    public DataTable BindLedger
    {
        set
        {
            //ddl_Ledger.DataSource = value;
            ddl_Ledger.DataTextField = "Ledger_Name";
            ddl_Ledger.DataValueField = "Ledger_Id";
            //ddl_Ledger.DataBind();


        }
    }


    public int SessionLedgerGroupId
    {

        set { StateManager.SaveState("onaccountledgergroupId", value); }
    }

    public bool hideApprove
    {
        set
        {
            btn_Approve.Visible = value;
        }
    }

    public int LedgerId
    {
        get { return Util.String2Int(ddl_Ledger.SelectedValue); }
    }

    public int LedgerGroupId

    { get { return Util.String2Int(ddl_LedgerGroup.SelectedValue); } }

    public bool hideAutoAdjust
    {
        set { chk_AutoAdjust.Visible = value; }
    }

    public decimal Balance_Amount
    {
        get { return Util.String2Decimal(lbl_BalanceAmount.Text); }
        set { lbl_BalanceAmount.Text = value.ToString("0.00");}
    }
    public void ClearVariables()
    {
        SessionOnAccount = null;
        SessionOnAccountAdjusted = null;
    }

    public string convertToDrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return convertToAbs(value) + " Cr"; }
        else { return convertToAbs(value) + " Dr"; }
    }

    public string DrCr(object value)
    {
        if (convertToDecimal(value) > 0)
        { return "Dr"; }
        else { return "Cr"; }
    }

    public string convertToAbs(object value)
    {
        decimal val = Math.Abs(Convert.ToDecimal(value));

        return val.ToString("0.00");
    }

    public decimal convertToDecimal(object value)
    {
        if (Convert.IsDBNull(value) || value.ToString().Trim() == string.Empty)
        { return 0; }
        else { return Convert.ToDecimal(value); }
    }

    #endregion

    private OnaccountAdjustmentPresenter _OnAcctAdjPresenter;


    protected void Page_Load(object sender, EventArgs e)
    {

        btn_Approve.Attributes.Add("onclick", objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Approve,btn_null_sessions));

        doInit();
        if (!IsPostBack)
        {
            btn_null_sessions.Style.Add("display", "none");
        }
        
        _OnAcctAdjPresenter = new OnaccountAdjustmentPresenter(this, IsPostBack);

        ddl_Ledger.OtherColumns = ddl_LedgerGroup.SelectedValue;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
    }


    private void doInit()
    {
        errorMessage = "";
    }

    protected void rb_Unadjusted_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        DataGridItem dgItem = (DataGridItem)rb.Parent.Parent;

        Label lblLedgerId = (Label)dgItem.FindControl("lbl_LedgerId");
        Label lblUnadjustedAmount = (Label)dgItem.FindControl("lblUnadjustedAmount");
        Label lblUnadjustedVoucherId = (Label)dgItem.FindControl("lbl_Voucher_ID");
        Label lblUnadjustedSrNo = (Label)dgItem.FindControl("lbl_SrNo");
        Label lblUnadjustVoucherDate = (Label)dgItem.FindControl("lbl_VoucherDate");

        int ledgerId = Convert.ToInt32(lblLedgerId.Text);

        //_LedgerId = ledgerId;
        UnAdjustedAmount = Convert.ToDecimal(lblUnadjustedAmount.Text);
        UnAdjustedVoucherId = Convert.ToInt32(lblUnadjustedVoucherId.Text);
        UnAdjustedSrNo = Convert.ToInt32(lblUnadjustedSrNo.Text);
        UnAdjustVoucherDate = Convert.ToDateTime(lblUnadjustVoucherDate.Text);

        unCheckRadioButton();
        rb.Checked = true;

        chk_AutoAdjust.Checked = false;

        lbl_BalanceAmount.Text = convertToDrCr(UnAdjustedAmount.ToString("0.00")); 
        
        _OnAcctAdjPresenter.bindAdjustedGrid();

    }
    protected void dg_OnAccountAdjustment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblBillAmount = (Label)e.Item.FindControl("lblbillAmount");
            Label lblUnadjustedAmount = (Label)e.Item.FindControl("lblUnadjustedAmount");

            BillAmount = BillAmount + Convert.ToDecimal(lblBillAmount.Text);
            PendingAmount = PendingAmount + Convert.ToDecimal(lblUnadjustedAmount.Text);
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalBillAmount = (Label)e.Item.FindControl("lblTotalBillAmount");
            Label lblTotalUnadjustedAmount = (Label)e.Item.FindControl("lblTotalUnadjustedAmount");

            Label lblTotalBillAmountDrCR = (Label)e.Item.FindControl("lblTotalBillAmountDrCR");
            Label lblTotalUnadjustedAmountDrCR = (Label)e.Item.FindControl("lblTotalUnadjustedAmountDrCR");

            lblTotalBillAmount.Text = BillAmount.ToString("00.000");
            lblTotalUnadjustedAmount.Text = PendingAmount.ToString("00.000");

            lblTotalBillAmountDrCR.Text = convertToDrCr(BillAmount);
            lblTotalUnadjustedAmountDrCR.Text = convertToDrCr(PendingAmount);
        }
    }
    protected void dg_Adjusted_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            BillAmount = 0;
            PendingAmount = 0;
        }

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblBillAmount = (Label)e.Item.FindControl("lblbillAmount");
            Label lblUnadjustedAmount = (Label)e.Item.FindControl("lblPendingAmount");
            Label lbl_DrCr = (Label)e.Item.FindControl("lbl_DrCR");

            BillAmount = BillAmount + Convert.ToDecimal(lblBillAmount.Text);
            PendingAmount = PendingAmount + Convert.ToDecimal(lblUnadjustedAmount.Text);

            lbl_DrCr.Text = DrCr(Convert.ToDecimal(lblUnadjustedAmount.Text));
        }

        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lblTotalBillAmount = (Label)e.Item.FindControl("lblTotalBillAmount");
            Label lblTotalUnadjustedAmount = (Label)e.Item.FindControl("lblTotalPendingAmount");

            Label lblTotalBillAmountDrCr = (Label)e.Item.FindControl("lblTotalBillAmountDrCr");
            Label lblTotalPendingAmountDrCr = (Label)e.Item.FindControl("lblTotalPendingAmountDrCr");

            lblTotalBillAmount.Text = BillAmount.ToString("00.000");
            lblTotalUnadjustedAmount.Text = PendingAmount.ToString("00.000");

            lblTotalBillAmountDrCr.Text = convertToDrCr(BillAmount);
            lblTotalPendingAmountDrCr.Text = convertToDrCr(PendingAmount);
        }
    }
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        string CloseScript1 = "<script language='javascript'>OnTxtChange();</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(up_balancamt, typeof(Page), "CloseScript1", CloseScript1, false);


        if (validateUI())
        {
            _OnAcctAdjPresenter.save();
                     

            string CloseScript = "<script language='javascript'> " + "alert('Saved SuccessFully');self.close();" + "</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(up_balancamt, typeof(Page), "CloseScript", CloseScript, false);
        }
    }
    private bool validateUnadjustedAmount()
    {
        ///This proc checks that adjusted amount
        ///does not exceeds unadjusted amount.

        decimal pendingAmount = 0;
        decimal AdjustAmount = 0;
        decimal totalAdjustAmount = 0;
        decimal chkAmount = 0;

        DataTable dtadjust = SessionOnAccountAdjusted;

        for (int i = 0; i <= dg_Adjusted.Items.Count - 1; i++)
        {
            Label lblpendingAmount = (Label)dg_Adjusted.Items[i].FindControl("lblPendingAmount"); // adjustable pending amount
            TextBox txtAdjustedAmount = (TextBox)dg_Adjusted.Items[i].FindControl("txtAdjustedAmount"); //adjust amount
            Label lbl_Ref_No = (Label)dg_Adjusted.Items[i].FindControl("lbl_Ref_No");
            Label lbl_DrCr = (Label)dg_Adjusted.Items[i].FindControl("lbl_DrCr");

            if (txtAdjustedAmount.Text.Trim() != "")
            {
                pendingAmount = Convert.ToDecimal(lblpendingAmount.Text);

                if (lbl_DrCr.Text.Trim() == "Dr")
                {
                    AdjustAmount = -1 * Convert.ToDecimal(txtAdjustedAmount.Text);
                }
                else
                {
                    AdjustAmount = Convert.ToDecimal(txtAdjustedAmount.Text);
                }

                
                //AdjustAmount = Convert.ToDecimal(txtAdjustedAmount.Text);

                if (pendingAmount > UnAdjustedAmount)
                    chkAmount = UnAdjustedAmount;
                else
                    chkAmount = pendingAmount;

                ///validate

                //if (AdjustAmount > chkAmount)
                //{
                //    errorMessage = "Adjusted Amount should be less than or equal to unadjusted amount or pending amount whichever is less.";
                //    return false;
                //}

                if (Math.Abs(chkAmount) < Math.Abs(AdjustAmount))
                {
                    errorMessage = "Adjusted amount of Ref No (" + lbl_Ref_No.Text + ") is greater than Bill Amount";
                    return false;
                }

                //totalAdjustAmount = totalAdjustAmount + AdjustAmount;

                //if (totalAdjustAmount > UnAdjustedAmount)
                //{
                //    errorMessage = "Adjusted Amount should be less than unadjusted amount.";
                //    return false;
                //}


                dtadjust.Rows[i]["AdjustedAmount"] = AdjustAmount;
                dtadjust.AcceptChanges();
            }
        }

        if (Math.Abs(Convert.ToDecimal(dtadjust.Compute("Sum(AdjustedAmount)", ""))) > Convert.ToDecimal(UnAdjustedAmount))
        {
            errorMessage = "Total Adjusted Amount Cannot Be Greater Than UnAdjusted Amount";
            return false;
        }

        return true;
    }

    protected void ddl_LedgerGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        SessionLedgerGroupId = Util.String2Int(ddl_LedgerGroup.SelectedValue);
        //_OnAcctAdjPresenter.bindBlankLedger();

        //TextBox txtbox = (TextBox)ddl_Ledger.Controls[0];

        //txtbox.Text = "";

        SetLedgerId("", "");

        _OnAcctAdjPresenter.bindBlankUnadjustedGrid();
        _OnAcctAdjPresenter.bindBlankAdjustedGrid();
        
    }
    protected void ddl_Ledger_SelectedIndexChanged(object sender, EventArgs e)
    {
        _OnAcctAdjPresenter.bindUnAdjustedGrid();
        _OnAcctAdjPresenter.bindBlankAdjustedGrid();
       
    }
    protected void chk_AutoAdjust_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_AutoAdjust.Checked == true)
            AutoAdjust();
    }

    private void AutoAdjust()
    {
        decimal totalAmount = UnAdjustedAmount;
        decimal pendingAmount = 0;
        decimal temp = totalAmount;


        for (int i = 0; i <= dg_Adjusted.Items.Count - 1; i++)
        {
            Label lblpendingAmount = (Label)dg_Adjusted.Items[i].FindControl("lblPendingAmount");
            TextBox txtAdjustedAmount = (TextBox)dg_Adjusted.Items[i].FindControl("txtAdjustedAmount"); //adjust amount

            pendingAmount = Math.Abs(Convert.ToDecimal(lblpendingAmount.Text));

            if (temp <= 0)
                txtAdjustedAmount.Text = "0";

            if (temp == pendingAmount)
            {
                txtAdjustedAmount.Text = convertToAbs(temp);
                temp = 0;
            }
            else if (temp < pendingAmount)
            {
                txtAdjustedAmount.Text = convertToAbs(temp);
                temp = 0;
            }
            else if (temp > pendingAmount)
            {
                temp = temp - pendingAmount;
                txtAdjustedAmount.Text = convertToAbs(pendingAmount);
            }

        }

        lbl_BalanceAmount.Text = convertToDrCr(temp.ToString("0.00"));
    }
    private void unCheckRadioButton()
    {
        for (int i = 0; i <= dg_OnAccountAdjustment.Items.Count - 1; i++)
        {
            RadioButton rb = (RadioButton)dg_OnAccountAdjustment.Items[i].FindControl("rb_Unadjusted");

            rb.Checked = false;
        }
    }

    //protected void txtAdjustedAmount_TextChanged(object sender, EventArgs e)
    //{
    //    decimal UnAdjustAmount = UnAdjustedAmount;
    //    decimal AdjustedAmount = totalAdjustedAmount();

    //    decimal balAmount = UnAdjustedAmount - AdjustedAmount;

    //    lbl_BalanceAmount.Text = convertToDrCr(balAmount.ToString("0.00"));
    //}
    //OnTextChanged="txtAdjustedAmount_TextChanged" AutoPostBack = "true"

    private decimal totalAdjustedAmount()
    {
        decimal temp = 0;

        for (int i = 0; i <= dg_Adjusted.Items.Count - 1; i++)
        {
            TextBox txtAdjustedAmount = (TextBox)dg_Adjusted.Items[i].FindControl("txtAdjustedAmount"); //adjust amount

            if (txtAdjustedAmount.Text.Trim() != "")
                temp = temp + Convert.ToDecimal(txtAdjustedAmount.Text);
            
        }

        return temp;
    }

    private void SetLedgerId(string value, string text)
    {
        ddl_Ledger.DataTextField = "Ledger_Name";
        ddl_Ledger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Ledger);
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

}
