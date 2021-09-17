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
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

public partial class Finance_IBT_WucVoucherForApproval : System.Web.UI.UserControl,IVoucherForApprovalView
{
    private VoucherForApprovalPresenter objVoucherForApprovalPresenter;
    DataSet ds = new DataSet();

    #region control Values

    public int Voucher_ID 
    {
        get { return Util.DecryptToInt(Request.QueryString["Voucher_Id"]);}
    }

    public int Branch_Id
    {
        get { return Util.DecryptToInt(Request.QueryString["Branch_Id"]);}
    }

    public string Branch_Name
    {
        get { return Util.DecryptToString(Request.QueryString["Branch_Name"]);}
    }

    public decimal Total_Amount
    {
        get { return Util.DecryptToDecimal(Request.QueryString["Total_Amount"]);}
    }

    public DataTable Bind_VoucherGrid 
    {
        set 
        {
            dg_Voucher.DataSource = value;
            dg_Voucher.DataBind();
        }
    }

    public DataSet Set_LabelTextBox
    {
        set 
        {
            DataRow DR = value.Tables[1].Rows[0];
            lbl_heading_name.Text = "VOUCHER ENTRY DONE BY : " + Branch_Name;
            lbl_Voucher_No.Text = DR["Voucher_No"].ToString();
            lbl_Voucher_Date.Text = DR["Voucher_Date"].ToString();
            lbl_Ref_No.Text = DR["Ref_No"].ToString();
            lbl_Voucher_type.Text = DR["Voucher_Type_Name"].ToString();
            txt_Narration.Text = DR["Narration"].ToString();
            lbl_Debit_Total.Text = value.Tables[0].Compute("Sum(Debit)","").ToString();
            lbl_Credit_Total.Text = value.Tables[0].Compute("Sum(Credit)", "").ToString();
                       
        }
    }


    #endregion


    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        _isValid = true;
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            ;
        }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       objVoucherForApprovalPresenter = new VoucherForApprovalPresenter(this, IsPostBack);
    }

   
    public string Parse_CrDr(double amt)
    {
        if (amt != 0)
        {
            return "Cr";
        }
        else
        {
            return "Dr";
        }
    }


    public string Parse_Amt(double amt)
    {
        if (amt != 0)
        {
            return Convert.ToString(amt);
        }
        else
        {
            return "";
        }
    }
    protected void btn_Accept_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Finance/Accounting Vouchers/FrmVoucher.aspx?Id=" + Util.EncryptInteger(Voucher_ID) + "&Menu_Item_Id=OQA0AA==&Mode=MgA=");
    }
    protected void btn_Unacceptable_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Finance/IBT/FrmVoucherForApprovalRemark.aspx?Voucher_Id=" + Util.EncryptInteger(Voucher_ID));
    }
    protected void dg_Voucher_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Voucher.CurrentPageIndex = e.NewPageIndex;
        Bind_VoucherGrid = ds.Tables[0];
    }
}
