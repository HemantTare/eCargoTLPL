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
using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 30/04/2008
/// Description   : This Page is For Service Category
/// </summary>
/// 

public partial class Master_Work_Order_WucServiceCategory : System.Web.UI.UserControl,IServiceCategoryView 
{
    #region ClassVariables
    ServiceCategoryPresenter objIServiceCategoryPresenter;
    #endregion

    #region ControlsValues
    public string ServiceCategory
    {
        set { txt_ServiceCategory.Text = value; }
        get { return txt_ServiceCategory.Text; }
    }
    public string ServiceDescription
    {
        set { txt_Description .Text = value; }
        get { return txt_Description.Text; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_ServiceCategory.Text.Trim() == string.Empty)
        {
            errorMessage= GetLocalResourceObject("Msg_txt_ServiceCategory").ToString(); 
            txt_ServiceCategory.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
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
            //return 2;
        }
    }

    #endregion

    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objIServiceCategoryPresenter = new ServiceCategoryPresenter(this, IsPostBack);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objIServiceCategoryPresenter.Save();
    }
    #endregion
}
