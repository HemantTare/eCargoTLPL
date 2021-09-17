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

public partial class Master_Location_WucCompanyCaption : System.Web.UI.UserControl,ICompanyCaptionView
{
    #region ClassVariable
    CompanyCaptionPresenter objCompanyCaptionPresenter;
    DataRow DR = null;
    private ScriptManager scm_CompanyCaption;

    #endregion

    #region ControlsValue
    public string GCCaption
    {
        set { txt_GCCaption.Text = value; }
        get { return txt_GCCaption.Text; }
    }
    public string LHPOCaption
    {
        set { txt_LHPOCaption.Text = value; }
        get { return txt_LHPOCaption.Text; }
    }
    public bool IsMemoSeriesRequired
    {
        set { Chk_IsMemoSeriesRequiired.Checked = value; }
        get { return Chk_IsMemoSeriesRequiired.Checked; }
    }
    public bool IsLHPOSeriesRequired
    {
        set { Chk_IsLHPOSeriesRequired.Checked = value; }
        get { return Chk_IsLHPOSeriesRequired.Checked; }
    }

    public bool IsAlsRequired
    {
        set { Chk_IsAlsRequired.Checked = value; }
        get { return Chk_IsAlsRequired.Checked; }
    }
    public bool IsTasRequired
    {
        set { Chk_IsTasRequired.Checked = value; }
        get { return Chk_IsTasRequired.Checked; }
    }
    public int MinDiffTASandAUS
    {
        set { txt_MinDiff.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_MinDiff.Text); }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
       bool _isValid = true;     
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
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }       

    }
    #endregion

    #region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyCaption = value; }
    }
    #endregion

    #region PageLoadEvents

    protected void Page_Load(object sender, EventArgs e)
    {
       objCompanyCaptionPresenter = new CompanyCaptionPresenter(this, IsPostBack);
    }
    #endregion

}
