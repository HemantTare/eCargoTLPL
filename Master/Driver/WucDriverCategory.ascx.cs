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

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EC;


/// <summary>
/// Author        : Vajiha
/// Created On    : 18/04/2008
/// Description   : This Page is For Master Driver Category
/// </summary>

public partial class Master_Driver_WucDriverCategory : System.Web.UI.UserControl, IDriverCategoryView
{

    #region ClassVariables
    DriverCategoryPresenter objDriverCategoryPresenter;
    #endregion


    #region ControlsValues

    public string DriverCategoryName
    {
        set
        {
            txt_Driver_Category_Name.Text = value;
        }
        get
        {
            return txt_Driver_Category_Name.Text;
        }
    }

    #endregion


    #region ControlsBind

    #endregion


    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Driver_Category_Name.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Driver Cateogry";// GetLocalResourceObject("Msg_DriverCategoryName").ToString();
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
            //return 1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods

    #endregion


    #region ControlsEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Driver/App_LocalResources/WucDriverCategory.ascx.resx");
        //}
        objDriverCategoryPresenter = new DriverCategoryPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objDriverCategoryPresenter.Save();
    }
    #endregion


}
