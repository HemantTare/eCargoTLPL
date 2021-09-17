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
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;


public partial class Master_Preventive_Maintainance_WucTemplate : System.Web.UI.UserControl,ITemplateView
{
    #region page variables
    TemplatePresenter objTemplatePresenter;   
    #endregion

    #region Properties
    public string TemplateName
    {
        set { txt_TemplateName.Text = value; }
        get { return txt_TemplateName.Text; }
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
        if (txt_TemplateName.Text == string.Empty)
        {
            errorMessage= "Please Enter Template Name";
            _isValid = false;
        }
        else if (txt_Description.Text == string.Empty)
        {
            errorMessage= "Please Enter Description";
            _isValid = false;
        }
        else
            _isValid = true;

        return _isValid;
    }
    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
      
        objTemplatePresenter = new TemplatePresenter(this, IsPostBack); 
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objTemplatePresenter.Save();
        
    }
    #endregion
}