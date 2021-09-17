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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 07/10/2008
/// Description   : This Page is For Master Branch Parameters Details
/// </summary>
/// 

public partial class Master_Branch_WucBranchParameters : System.Web.UI.UserControl,IBranchParametersView
{
    #region ClassVariables
    BranchParametersPresenter objBranchParametersPresenter;
    private ScriptManager scm_BranchParameters;
    #endregion

    #region ControlsValues
       
    public int GCMinStock
    {
        set { Txt_GC.Text = Util.Int2String(value); }
        get { return Util.String2Int(Txt_GC.Text); }
    }
    public int CRMinStock
    {
        set { Txt_CR.Text = Util.Int2String(value); }
        get { return Util.String2Int(Txt_CR.Text); }
    }
    public int MemoMinStock
    {
        set { Txt_MEMO.Text = Util.Int2String(value); }
        get { return Util.String2Int(Txt_MEMO.Text); }
    }
    public int LHPOMinStock
    {
        set { Txt_LHPO.Text = Util.Int2String(value); }
        get { return Util.String2Int(Txt_LHPO.Text); }
    }
    public int DefaultBankLedgerId
    {
        set { DDL_DefaultBankLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(DDL_DefaultBankLedger.SelectedValue); }
    }
    public int DefaultCashLedgerId
    {
        set { DDL_DefaultCashLedger.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(DDL_DefaultCashLedger.SelectedValue); }
    }
    public string BookingStartTime
    {
        set {wuc_BookingStartTime.setTime(value); }
        get { return wuc_BookingStartTime.getTime(); }
    }
    public string BookingEndTime
    {
        set { wuc_BookingEndTime.setTime(value); }
        get { return wuc_BookingEndTime.getTime(); }
    }
        
    #endregion

    #region ControlsBind
   
    public DataTable BindDefaultCashLedger
    {
        set
        {
            DDL_DefaultCashLedger.DataTextField = "Ledger_Name";
            DDL_DefaultCashLedger.DataValueField = "Ledger_Id";
            DDL_DefaultCashLedger.DataSource = value;
            DDL_DefaultCashLedger.DataBind();

            Common.InsertItem(DDL_DefaultCashLedger );
        }
    }

    public DataTable BindDefaultBankLedger
    {
        set
        {
            DDL_DefaultBankLedger.DataTextField = "Ledger_Name";
            DDL_DefaultBankLedger.DataValueField = "Ledger_Id";
            DDL_DefaultBankLedger.DataSource = value;
            DDL_DefaultBankLedger.DataBind();

            Common.InsertItem(DDL_DefaultBankLedger);
        }
    }
  
    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_BranchParameters = value; }
    }

     #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (Util.String2Int(DDL_DefaultCashLedger.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select Default Cash Ledger";
            _isValid = false;
            DDL_DefaultCashLedger.Focus();
        }
        else if (Util.String2Int(DDL_DefaultBankLedger.SelectedValue) == 0)
        {
            lbl_Errors.Text = "Please Select DefaultBank Ledger";
            _isValid = false;
            DDL_DefaultBankLedger.Focus();
        }
        else if (Convert.ToDateTime(BookingStartTime) > Convert.ToDateTime(BookingEndTime))
        {
            lbl_Errors.Text = "Please Select Valid Booking Time Range";
            _isValid = false;
            scm_BranchParameters.SetFocus(wuc_BookingStartTime); 
        }
        else
        {
            _isValid = true;
        }

        return _isValid;

        //bool _isValid = true;
        //return _isValid;
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
            //return 12;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wuc_BookingStartTime.setTime("24");
            wuc_BookingEndTime.setTime("24");

            BookingStartTime = DateTime.Now.ToShortTimeString();
            BookingEndTime = DateTime.Now.ToShortTimeString(); 

            SetStandardCaption();
        }
        objBranchParametersPresenter = new BranchParametersPresenter(this, IsPostBack);
    }

    private void SetStandardCaption()
    {
        label2.Text = CompanyManager.getCompanyParam().GcCaption + " :";
        label5.Text = CompanyManager.getCompanyParam().LHPOCaption + " :";
    }
}
