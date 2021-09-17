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
using Raj.EC.GeneralPresenter;
using Raj.EC.GeneralView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;

public partial class Master_General_WucContainerType : System.Web.UI.UserControl,IContainerTypeView
{
    #region ClassVariables
    ContainerTypePresenter objContainerTypePresenter;
    #endregion

    #region ControlsValue

    public string ContainerTypeName
    {
        set { txt_ContainerType.Text = value; }
        get { return txt_ContainerType.Text; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (txt_ContainerType.Text == string.Empty)
        {
            errorMessage = "Please Enter Container Type Name";
            //_isValid = false;
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
            //return 2;
        }
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
       
        objContainerTypePresenter = new ContainerTypePresenter(this, IsPostBack);

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        objContainerTypePresenter.Save();
    }


    #endregion
}
