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
using Raj.EC.AdminPresenter;
using Raj.EC.AdminView;
using Raj.EC;

public partial class Administration_Rights_WucDateSettings : System.Web.UI.UserControl,IDateSettingsView
{
    #region ClassVariables
    DateSettingsPresenter objDateSettingsPresenter;
    #endregion

    #region ControlsValues

    public string ProcessName
    {
        get { return txt_ProcessName.Text; }
        set { txt_ProcessName.Text = value; }
    }
    public string Code
    {
        get { return Txt_code.Text; }
        set { Txt_code.Text = value; }
    }
    public int MinHrs
    {
        get { return Util.String2Int(txt_MinHrs.Text); }
        set { txt_MinHrs.Text = value.ToString(); }
    }
    public int MaxHrs
    {
        get { return Util.String2Int(txt_MaxHrs.Text); }
        set { txt_MaxHrs.Text = value.ToString(); }
    }
    #endregion


    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_ProcessName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Process Name";
            _isValid = false;
        }        
      
        else if (Txt_code.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Code";
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    #endregion
    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objDateSettingsPresenter = new DateSettingsPresenter(this, IsPostBack);
        
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        objDateSettingsPresenter.Save();
    }
    #endregion



}
