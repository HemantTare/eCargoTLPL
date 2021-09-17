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

public partial class FA_Common_Accounting_Vouchers_WucFBTHelper : System.Web.UI.UserControl,IFBTHelperView
{
    #region ClassVariable
    FBTHelperPresenter objFBTHelperPresenter;
    #endregion

    #region ControlsValue

    

    public int FBTLedger
    {

        set {ddl_FBTLedger.SelectedValue=Util.Int2String(value);}
        get { return Util.String2Int(ddl_FBTLedger.SelectedValue); }
    }

    public string TypeOfPayment
    {
        get {return ddl_TypeOfPayment.SelectedValue;}
        set { ddl_TypeOfPayment.SelectedValue = value; }
    }

    public int CashBankAc
    {
        set { ddl_CashBankAc.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_CashBankAc.SelectedValue); }
    }

    public DateTime ToDateValue
    {
        set { ToDate.SelectedDate = value; }
        get { return ToDate.SelectedDate; }
    }

    public DateTime FromDateValue
    {
        set { FromDate.SelectedDate = value; }
        get { return FromDate.SelectedDate; }
    }
    #endregion

    #region ControlsBind
    public DataSet BindFBTLedger
    {
        set
        {
            ddl_FBTLedger.DataSource = value;
            ddl_FBTLedger.DataValueField = "Ledger_Id";
            ddl_FBTLedger.DataTextField = "Ledger_Name";
            ddl_FBTLedger.DataBind();
            ddl_FBTLedger.Items.Insert(0, new ListItem("Select One", "0"));


           
        }
    }
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

        if (ToDateValue < FromDateValue)
        {
            lbl_Errors.Text = "To Date Cannot be Less Than From Date";
            _isValid = false;

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
        if (!IsPostBack)
        {
            ToDateValue = System.DateTime.Now;
            FromDateValue = System.DateTime.Now;
        }
        objFBTHelperPresenter = new FBTHelperPresenter(this, IsPostBack);
    }
    protected void btn_Ok_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {

            DataSet objDS = null;
            objDS = objFBTHelperPresenter.FillValues();

            if (objDS.Tables.Count == 0)
            {
                lbl_Errors.Text = "FBT Payable Amount For Selected Date Range Not Found";
            }
            else
            {
                Session["FBTPaymentType"] = ddl_TypeOfPayment.SelectedValue;
                Session["FromFBTHelper"] = true;
                Session["TDS_FBT_Vouchers"] = objDS;
                Session["Upto_Date"] = ToDateValue;


                Response.Redirect("~/RedirectPage.aspx?RefreshParentPage=1");
            }
        }

    }
    #endregion

   
}
