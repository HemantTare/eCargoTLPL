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
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinancePresenter;
using ClassLibraryMVP;

public partial class FA_Common_Accounting_Vouchers_WucTDSHelper : System.Web.UI.UserControl,ITDSHelperView
{
    #region ClassVariable
    TDSHelperPresenter objTDSHelperPresenter;
    #endregion

    #region ControlsValue

    public int CashBankAc
    {
        set {ddl_CashBankAc.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_CashBankAc.SelectedValue); }
    }

    public int TDSLedgerAc
    {

        set { ddl_TdsledgerAc.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_TdsledgerAc.SelectedValue); }
    }

    public string DeducteeStatus
    {
        get { return ddl_DeducteeStatus.SelectedValue; }
        set { ddl_DeducteeStatus.SelectedValue = value; }
    }

    public DateTime ToDateValue
    {
        set {ToDate.SelectedDate= value; }
        get { return ToDate.SelectedDate; }
    }


    #endregion

    #region ControlsBind

    public DataSet BindCashBankAc
    {
        set
        {
            ddl_CashBankAc.DataSource = value;
            ddl_CashBankAc.DataValueField = "Ledger_Id";
            ddl_CashBankAc.DataTextField = "Ledger_Name";
            ddl_CashBankAc.DataBind();
            ddl_CashBankAc.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }

    public DataSet BindTDSLedgerAc
    {
        set
        {
            ddl_TdsledgerAc.DataSource = value;
            ddl_TdsledgerAc.DataValueField = "Ledger_Id";
            ddl_TdsledgerAc.DataTextField = "Ledger_Name";
            ddl_TdsledgerAc.DataBind();
            ddl_TdsledgerAc.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.String2Int(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public bool validateUI()
    {
        bool _isValid = false;
        if (ddl_CashBankAc.SelectedValue == "0")
        {
            lbl_Errors.Text = "Please Select Cash Bank Account";
            ddl_CashBankAc.Focus();

        }
        else if (ddl_TdsledgerAc.SelectedValue == "0")
        {
            lbl_Errors.Text = "Please Select TDS Ledger Account";
            ddl_TdsledgerAc.Focus();
        }
        else
        {
            _isValid = true;
        }
        return true;
    }

    #endregion

    #region PageEvent

    protected void Page_Load(object sender, EventArgs e)
    {
          
        objTDSHelperPresenter = new TDSHelperPresenter(this, IsPostBack);
    }

    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        DataSet objDS = null;
        objDS = objTDSHelperPresenter.FillValues();

        if (objDS.Tables.Count  == 0)
        {

            lbl_Errors.Text = "No Pending Bills Found For Selected TDS Ledger Account";
        }
        else
        {
            Session["FromTDSHelper"] = true;
            Session["TDS_FBT_Vouchers"] = objDS;
            Session["Upto_Date"] =ToDateValue;
            Response.Redirect("~/RedirectPage.aspx?RefreshParentPage=1");
        }
        
    }
    #endregion

   
}
