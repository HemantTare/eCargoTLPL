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
using System.Data.SqlClient;

public partial class CRM_Masters_WucComplaintNature : System.Web.UI.UserControl,IComplaintNatureView
{
    #region page variables
    ComplaintNaturePresenter objComplaintNaturePresenter;
    #endregion

    #region Properties
    public string ComplaintNatureName
    {
        set { txt_ComplaintNatureName.Text = value; }
        get { return txt_ComplaintNatureName.Text; }
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
        if (txt_ComplaintNatureName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Complaint Nature Name";
            txt_ComplaintNatureName.Focus();
        }
        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        objComplaintNaturePresenter = new ComplaintNaturePresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            txt_ComplaintNatureName.Focus();
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objComplaintNaturePresenter.save();
    }
    #endregion
}