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
using Raj.CRM.MasterView;
using Raj.CRM.MasterPresenter;

public partial class CRM_Masters_WucReasonForFault : System.Web.UI.UserControl,IReasonForFaultView
{
    #region page variables
    ReasonForFaultPresenter objReasonForFaultPresenter;
    #endregion

    #region Properties
    public string ReasonForFault
    {
        set { txt_ReasonForFault.Text = value; }
        get { return txt_ReasonForFault.Text; }
    }

    public string Description
    {
        set { txt_Description.Text = value; }
        get { return txt_Description.Text; }
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    #endregion

    #region Validation
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_ReasonForFault.Text.Trim()== string.Empty)
        {
            errorMessage = "Please Enter Reason For Fault";
            txt_ReasonForFault.Focus();
        }
        else
            _isValid = true;

        return _isValid;
    }
    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        objReasonForFaultPresenter = new ReasonForFaultPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            txt_ReasonForFault.Focus();
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objReasonForFaultPresenter.save();
    }
    #endregion
}