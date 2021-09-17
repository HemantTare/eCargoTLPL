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
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

public partial class Operations_Delivery_frmDlyCreditApproval : System.Web.UI.Page, IDlyCreditApprovalView
{
    DlyCreditApprovalPresenter objDlyCreditApprovalPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    bool _status = false;
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public bool Status
    {
        set { _status = value; }
        get { return _status; }
    }
    public int GCID
    {
        set { hdngcid.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdngcid.Value); }
    }
    public int PDSID
    {
        set { hdnpdsid.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnpdsid.Value); }
    }
    public int GCNo
    {
        get { return Util.String2Int(txt_LRNo.Text.Trim()); }
    }
    public string Consignor
    {
        set { lblConsignor.Text = value; }
    }
    public string Consignee
    {
        set { lblConsignee.Text = value; }
    }
    public string PaymentMode
    {
        set { lblPaymentMode.Text = value; }
    }
    public string TotalArticles
    {
        set { lblTotalArticles.Text = value; }
    }
    public string TotalGCAmount
    {
        set { lblTotalAmount.Text = value; }
    }
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public int IsApproved
    {
        get { return chkApproved.Checked ? 1 : 0; }
    }
    public int BillingPartyId
    {
        get { return Util.String2Int(hdn_BillingPartyId.Value); }
    }
    public string MTransactionID
    {
        get { return txtMobilePayment.Text.Trim(); }
    }
    public string ReasonUnApproved
    {
        get { return txtReason.Text.Trim(); }
    }
    public bool PaymentReceivedbyCredit
    {
        get { return Rbl_Receivedby.Items[0].Selected; }
    }
    public bool validateUI()
    {
        bool _isValid = false;

        if (PDSID <= 0 || GCID <= 0)
        {
            errorMessage = "Please Search GC No.";
        }
        else if (IsApproved == 0 && ReasonUnApproved == string.Empty)
        {
            errorMessage = "Please Enter Reason For Non Approval.";
            txtReason.Focus();
        }
        else if ((IsApproved == 1 && PaymentReceivedbyCredit) && BillingPartyId <= 0)
        {
            errorMessage = "Please Select Billing Party.";
            txt_BillingParty.Focus();
        }
        else if ((IsApproved == 1 && !PaymentReceivedbyCredit) && MTransactionID == string.Empty)
        {
            errorMessage = "Please Enter MPayment Transaction ID.";
            txtMobilePayment.Focus();
        }       
        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EC.OperationModel.NewGCSearch));
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Close));
        objDlyCreditApprovalPresenter = new DlyCreditApprovalPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        objDlyCreditApprovalPresenter.save();
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        if (txt_LRNo.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter LR No.";
        }
        else
        {
            errorMessage = "";
            objDlyCreditApprovalPresenter.FillValues();
        }
        
        tr_Consignor.Visible = Status;
        tr_PaymentMode.Visible = Status;
        tr_TotalAmount.Visible = Status;
        tr_Approved.Visible = Status;
        tr_Reason.Visible = Status;
        tr_btnsave.Visible = Status; 
    }
}
