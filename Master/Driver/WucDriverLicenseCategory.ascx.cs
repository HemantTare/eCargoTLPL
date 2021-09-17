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
/// Author: <Nilesh Kumar Jha>
/// Created On: <23/04/2008>
/// Description: This Page is For DriverLicenseCategory  
/// </summary>
public partial class Master_Driver_WucDriverLicenseCategory : System.Web.UI.UserControl, IDriverLicenseCategoryView  
{
    #region "Class Variables"
     DriverLicenseCategoryPresenter objDriverLicenseCategoryPresenter;
    #endregion

    #region "ControlsValues"
   public  string DriverLicenseCategory
    {
         set{txt_DriverLicenseCategory.Text = value;}
        get{return txt_DriverLicenseCategory.Text;}
    }


    #endregion

    #region "IView"
    public bool validateUI()
    {
        bool _IsValid = false;
        if (txt_DriverLicenseCategory.Text == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Driver License Category";// GetLocalResourceObject("Msg_Txt_DriverLicenseCategory").ToString();
            txt_DriverLicenseCategory.Focus();
        }
        else
        {
            _IsValid = true;
        }
        return _IsValid;
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
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Driver/App_LocalResources/WucDriverLicenseCategory.ascx.resx");
        //}
        objDriverLicenseCategoryPresenter = new DriverLicenseCategoryPresenter(this,  IsPostBack);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objDriverLicenseCategoryPresenter.Save();
    }
    #endregion
}
