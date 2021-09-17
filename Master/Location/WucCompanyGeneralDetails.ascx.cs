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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;


public partial class Master_Location_WucCompanyGeneralDetails : System.Web.UI.UserControl,ICompanyGeneralDetailsView
{
    #region ClassVariables
    CompanyGeneralDetailsPresenter objCompanyGeneralDetailsPresenter;
    public bool IsActivateDivision = UserManager.getUserParam().IsDivisionReq;
    private ScriptManager scm_CompanyGeneral;
    #endregion

    #region ControlsValues

    public string CompanyName
    {
        set { txt_CompanyName.Text = value; }
        get { return txt_CompanyName.Text; }
    }
    public string MailingName
    {
        set { txt_MailingName.Text = value; }
        get { return txt_MailingName.Text; }
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    public int CompanyId
    {
        set {hdn_CompanyId.Value =Convert.ToString(value); }
        get { return Util.String2Int(hdn_CompanyId.Value); }
    }

    public int HOLedger 
    {
        get {return Util.String2Int(ddl_HOLedger.SelectedValue);}
    }

    public int PFALedger 
    {
        get {return Util.String2Int(ddl_PFALedger.SelectedValue);}
    }

    public int HOCashLedger
    {
        get { return Util.String2Int(ddl_HOCashLedger.SelectedValue); }
    }

    public int HOBankLedger
    {
        get { return Util.String2Int(ddl_HOBankLedger.SelectedValue); }
    }

    public string ClientCode
    {
        set { txt_ClientCode.Text = value; }
        get { return txt_ClientCode.Text; }
    }
         

#endregion

    #region ControlsBind
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_CompanyName.Text == string.Empty)
        {
            errorMessage = "Please Enter Company Name";// GetLocalResourceObject("Msg_CompanyName").ToString();
            _isValid = false;
        }
        else if (txt_MailingName.Text == string.Empty)
        {
            errorMessage = "Please Enter Mailing Name";// GetLocalResourceObject("Msg_MailingName").ToString();
            _isValid = false;
        }
        else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }
        else if (HOLedger <= 0)
        {
            errorMessage = "Please Select HO Ledger";// GetLocalResourceObject("Msg_HOLedger").ToString();
            _isValid = false;
        }
        else if (PFALedger <= 0)
        {
            errorMessage = "Please Select PFA Ledger";/// GetLocalResourceObject("Msg_PFALedger").ToString();
            _isValid = false;
        }
        else if (HOCashLedger <= 0)
        {
            errorMessage = "Please Select HO Cash Ledger";
            _isValid = false;
        }
        else if (HOBankLedger <= 0)
        {
            errorMessage = "Please Select HO Bank Ledger";
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }



    }
    #region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyGeneral = value; }
    }
    #endregion


    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        // if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucCompanyGeneralDetails.ascx.resx");
        //}
        objCompanyGeneralDetailsPresenter = new CompanyGeneralDetailsPresenter(this, IsPostBack);

        AddressView.VisibleMobileNo = false;


    }

    #endregion


    public void SetHOLedgerId(string text, string value)
    {
        ddl_HOLedger.DataTextField = "Ledger_Name";
        ddl_HOLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_HOLedger);
    }

    public void SetPFALedgerId(string text, string value)
    {
        ddl_PFALedger.DataTextField = "Ledger_Name";
        ddl_PFALedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_PFALedger);
    }

    public void SetHOCashLedger_Id(string text, string value)
    {
        ddl_HOCashLedger.DataTextField = "Ledger_Name";
        ddl_HOCashLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_HOCashLedger);
    }

    public void SetHOBankLedger_Id(string text, string value)
    {
        ddl_HOBankLedger.DataTextField = "Ledger_Name";
        ddl_HOBankLedger.DataValueField = "Ledger_Id";
        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_HOBankLedger);
    }
}
