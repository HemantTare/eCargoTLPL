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




public partial class Master_General_WucDepartment : System.Web.UI.UserControl,IDepartmentView
{
    #region ClassVariables
    DepartmentPresenter objDepartmentPresenter;
    #endregion

    #region ControlsValue

    public string DepartmentName
    {
        set { txt_DepartmentName.Text = value; }
        get { return txt_DepartmentName.Text; }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        
        if (txt_DepartmentName.Text == string.Empty)
        {
            errorMessage = "Please Enter Department Name";// GetLocalResourceObject("Msg_DepartmentName").ToString();
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
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/General/App_LocalResources/WucDepartment.ascx.resx");
        //}
       objDepartmentPresenter = new DepartmentPresenter(this, IsPostBack);

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {

        objDepartmentPresenter.Save();
    }


    #endregion

}
